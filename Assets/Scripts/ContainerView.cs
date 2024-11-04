using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ContainerView : MonoBehaviour
{
    public GameObject slotPrefab;
    public Container container;
    public float spacing;
    private Dictionary<int, ItemView> itemViews = new Dictionary<int, ItemView>();
    public GameObject itemViewPrefab;
    public Transform itemsBag;
    public Transform slotsBag;

    public Transform startingPoint;

    public Dictionary<Vector2Int, ItemSlotView> slots = new Dictionary<Vector2Int, ItemSlotView>();

    private void Start()
    {
        transform.position = startingPoint.position;
        //test
        this.container = new Container(4, 2);

        generateSlots();
        //test
    }

    public void generateSlots()
    {
        slots = new Dictionary<Vector2Int, ItemSlotView>();
        foreach (var (key, slot) in container.slots)
        {
            var newSlot = Instantiate(slotPrefab, slotsBag).GetComponent<ItemSlotView>();
            newSlot.transform.localPosition = new Vector3(key.x * spacing, key.y * spacing, 0);
            newSlot.itemSlot = slot;
            slots[key] = newSlot;
        }
    }

    public void applyItem(ItemSlotView slot, Item item)
    {
        container.applyItem(slot.itemSlot, item);
        var itemView = Instantiate(itemViewPrefab, itemsBag).GetComponent<ItemView>();
        itemView.transform.position = slot.transform.position;
        itemView.item = item;
        itemView.updateSpriteSize();
        itemViews[item.id] = itemView;
    }

    public void removeItem(Item item)
    {
        container.removeItem(item);
        var itemView = itemViews[item.id];
        itemViews[item.id] = null;
        Destroy(itemView.gameObject);
    }

    [CanBeNull]
    public ItemSlotView getBottomLeftOfItemInContainer(Item item)
    {
        var oldPos = new Vector2Int(9999, 9999);
        ItemSlotView slotView = null;
        foreach (var (pos, slot) in slots)
        {
            if (slot.itemSlot.item != null && slot.itemSlot.item.id == item.id)
            {
                if (oldPos.x > pos.x || oldPos.y > pos.y)
                {
                    oldPos = pos;
                    slotView = slot;
                }
            }
        }

        return slotView;
    }
}