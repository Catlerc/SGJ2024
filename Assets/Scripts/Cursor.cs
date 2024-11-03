using System;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
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

    // item in hand
    public Item itemInHand;
    private GameObject itemInHandObj;
    private ItemPartsView itemInHandPartsView;
    private ItemView itemInHandView;

    //state
    private ItemSlotView overItemView = null;
    public bool clickOnPlayer = false;
    [CanBeNull] public Loot overLoot;

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
        if (overItemView != null)
        {
            itemInHandView.transform.position = overItemView.transform.position;
            itemInHandPartsView.transform.position = overItemView.transform.position;
        }
        else
        {
            itemInHandView.transform.position = transform.position;
            itemInHandPartsView.transform.position = transform.position;
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
        }

        var check = containerView.container.checkShape(itemInHand.shape, overItemView.itemSlot.pos);
        if (check.badParts.Length == 0)
        {
            foreach (var partPos in check.okParts) itemInHandPartsView.parts[partPos].ok = true;
        }
        else
        {
            foreach (var partPos in check.okParts) itemInHandPartsView.parts[partPos].ok = true;
            foreach (var partPos in check.badParts) itemInHandPartsView.parts[partPos].ok = false;
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
            player.health.health += ((HealPotionItemType)itemInHand.type).amount;
            if (player.health.health > player.health.maxHealth) player.health.health = player.health.maxHealth;
        }

        if (itemInHand.type is InvisibilityPotionItemType)
        {
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