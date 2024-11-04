using System;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public ContainerView containerView;
    public static Cursor instance;
    // public ItemType testType;

    public GameObject itemObjPrefab;
    public Player player;

    public GameObject lootPrefab;

    public Transform dropPoint;


    public GameObject tooltip;
    public TextMeshPro nameText;
    public TextMeshPro descText;
    public TextMeshPro costText;

    public TextMeshPro categoryText;

    // item in hand
    public Item itemInHand;
    private GameObject itemInHandObj;
    private ItemPartsView itemInHandPartsView;
    private ItemView itemInHandView;

    //state
    private ItemSlotView overItemView = null;
    public bool clickOnPlayer = false;
    [CanBeNull] public Loot overLoot;
    public bool overShopItem = false;

    private void Start()
    {
        instance = this;
        // grabItem(new Item(this.testType));
    }

    public void grabItem(Item item)
    {
        removeItemFromHand();
        itemInHand = item;
        itemInHandObj = Instantiate(itemObjPrefab, transform);
        itemInHandPartsView = itemInHandObj.GetComponentInChildren<ItemPartsView>();
        itemInHandView = itemInHandObj.GetComponentInChildren<ItemView>();
        itemInHandPartsView.item = item;
        itemInHandView.item = item;
        itemInHandPartsView.generateParts();
        itemInHandView.updateSpriteSize();
    }

    public void updateToolTip(Item item)
    {
        tooltip.SetActive(true);
        nameText.text = item.type.name;
        descText.text = item.type.description;
        categoryText.text = item.type.category;
        costText.text = item.type.cost.ToString();
    }

    public void disableToolTip()
    {
        tooltip.SetActive(false);
    }

    private void placeItemInContainer()
    {
        var check = containerView.container.checkShape(itemInHand.shape, overItemView.itemSlot.pos);

        if (check.badParts.Length == 0)
        {
            containerView.applyItem(overItemView, itemInHand);
            removeItemFromHand();
        }
    }

    public void removeItemFromHand()
    {
        if (itemInHand != null)
        {
            itemInHand = null;
            Destroy(itemInHandObj);
        }
    }


    private void rotateItemHand()
    {
        itemInHand.rotate();
        itemInHandView.updateSpriteSize();
        itemInHandPartsView.generateParts();
    }

    private void updateItemHandPosition()
    {
        var offset = new Vector3(0, 0, -20);
        if (overItemView != null)
        {
            itemInHandView.transform.position = overItemView.transform.position + offset;
            itemInHandPartsView.transform.position = overItemView.transform.position + offset;
        }
        else
        {
            itemInHandView.transform.position = transform.position + offset;
            itemInHandPartsView.transform.position = transform.position + offset;
        }
    }

    private void disableItemPartsRender()
    {
        foreach (var (pos, part) in itemInHandPartsView.parts)
        {
            part.gameObject.SetActive(false);
        }
    }

    private void updateItemContainerIntersection()

    {
        foreach (var (pos, part) in itemInHandPartsView.parts)
        {
            part.gameObject.SetActive(true);
            part.isKey = false;
        }

        var check = containerView.container.checkShape(itemInHand.shape, overItemView.itemSlot.pos);
        if (check.badParts.Length == 0)
        {
            foreach (var partPos in check.okParts) itemInHandPartsView.parts[partPos].ok = true;
        }
        else
        {
            if (itemInHand.type is KeyItemType && overItemView.itemSlot.item != null &&
                overItemView.itemSlot.item.type is ChestItemType)
            {
                foreach (var partPos in check.badParts) itemInHandPartsView.parts[partPos].isKey = true;
            }
            else
            {
                foreach (var partPos in check.okParts)
                {
                    itemInHandPartsView.parts[partPos].ok = true;
                }

                foreach (var partPos in check.badParts)
                {
                    itemInHandPartsView.parts[partPos].ok = false;
                }
            }
        }
    }

    private void grabItemFromContainer()
    {
        grabItem(overItemView.itemSlot.item.withCustomOrigin(overItemView.itemSlot.posInItemShape.Value));
        containerView.removeItem(overItemView.itemSlot.item);
    }

    private void updateCursorPosition()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    private void dropItemFromHand()
    {
        var loot = Instantiate(lootPrefab, player.mapObj.transform).GetComponent<Loot>();
        loot.transform.position = dropPoint.position + new Vector3(0, 0, -0.2f);
        loot.init(itemInHand);
        removeItemFromHand();
    }

    private void applyPotionToPlayer()
    {
        if (itemInHand.type is HealPotionItemType)
        {
            print("health");
            player.health.health += ((HealPotionItemType)itemInHand.type).amount;
            if (player.health.health > player.health.maxHealth) player.health.health = player.health.maxHealth;
        }

        if (itemInHand.type is InvisibilityPotionItemType)
        {
            print("inviz");
            player.invisTime += ((InvisibilityPotionItemType)itemInHand.type).duration;
        }

        removeItemFromHand();
    }

    private void setItemToPlayer()
    {
        var item = itemInHand;
        removeItemFromHand();
        player.setItem(item);
    }

    private void Update()
    {
        // print(itemInHand);
        if (itemInHand != null && Input.GetMouseButtonDown(1))
        {
            rotateItemHand();
            goto skipOtherActions;
        }

        if (overLoot != null && itemInHand == null)
        {
            grabItem(overLoot.item);
            Destroy(overLoot.gameObject);
            goto skipOtherActions;
        }
        // if (itemInHand != null && Input.GetMouseButtonDown(0) && overItemView == null && !clickOnPlayer)
        // {
        //     dropItemFromHand();
        //     goto skipOtherActions;
        // }


        // print((itemInHand != null) + " " + clickOnPlayer + " " + (overItemView == null));

        if (itemInHand != null) updateToolTip(itemInHand);
        else if (overItemView != null && overItemView.itemSlot.item != null) updateToolTip(overItemView.itemSlot.item);
        else if (!overShopItem)disableToolTip();
        
        if (itemInHand != null && clickOnPlayer && overItemView == null)
        {
            if (itemInHand.type is HealPotionItemType || itemInHand.type is InvisibilityPotionItemType)
                applyPotionToPlayer();
            else
                setItemToPlayer();

            goto skipOtherActions;
        }

        if (itemInHand != null && Input.GetMouseButtonDown(0) && overItemView != null &&
            overItemView.itemSlot.item != null && overItemView.itemSlot.item.type is ChestItemType &&
            itemInHand.type is KeyItemType)
        {
            (overItemView.itemSlot.item.type as ChestItemType).openChest(containerView, overItemView.itemSlot.item);
            removeItemFromHand();

            goto skipOtherActions;
        }

        if (itemInHand != null && Input.GetMouseButtonDown(0) && overItemView != null)
        {
            placeItemInContainer();
            goto skipOtherActions;
        }

        if (itemInHand == null && Input.GetMouseButtonDown(0) && overItemView != null &&
            overItemView.itemSlot.item != null)
        {
            grabItemFromContainer();
            goto skipOtherActions;
        }

        skipOtherActions:

        updateCursorPosition();
        if (itemInHand != null)
            if (overItemView != null) updateItemContainerIntersection();
            else disableItemPartsRender();

        if (itemInHand != null) updateItemHandPosition();

        overItemView = null;
        clickOnPlayer = false;
        overLoot = null;
        overShopItem = false;
    }

    public void dropItem()
    {
        if (itemInHand != null)
            dropItemFromHand();
    }

    public void isOverItemSlot(ItemSlotView itemSlotView)
    {
        overItemView = itemSlotView;
    }
}