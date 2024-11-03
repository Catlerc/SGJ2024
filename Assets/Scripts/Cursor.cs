using System;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public ContainerView containerView;
    public static Cursor instance;
    public ItemType testType;

    public GameObject itemObjPrefab;

    // item in hand
    public Item itemInHand;
    private GameObject itemInHandObj;
    private ItemPartsView itemInHandPartsView;
    private ItemView itemInHandView;

    //state
    private ItemSlotView overItemView = null;

    private void Start()
    {
        instance = this;
        grabItem(new Item(this.testType));
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

    private void updateItemContainerIntersection()
    {
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

    private void Update()
    {
        if (itemInHand != null && Input.GetMouseButtonDown(1))
        {
            rotateItemHand();
            goto skipOtherActions;
        }

        if (itemInHand != null && Input.GetMouseButtonDown(0) && overItemView != null &&
            overItemView.itemSlot.item != null && overItemView.itemSlot.item.type is ChestItemType &&
            itemInHand.type is KeyItemType)
        {
            // grabItemFromContainer();
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
        if (itemInHand != null && overItemView != null) updateItemContainerIntersection();
        if (itemInHand != null) updateItemHandPosition();

        overItemView = null;
    }


    public void isOverItemSlot(ItemSlotView itemSlotView)
    {
        overItemView = itemSlotView;
    }
}