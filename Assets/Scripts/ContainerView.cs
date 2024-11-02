using System.Collections.Generic;
using UnityEngine;

public class ContainerView : MonoBehaviour
{
    public GameObject slotPrefab;
    public Container container;
    public float spacing;

    public Dictionary<Vector2Int, ItemSlotView> slots = new Dictionary<Vector2Int, ItemSlotView>();

    private void Start()
    {
        //test
        this.container = new Container(30, 10);

        generateSlots();
        //test
    }

    public void generateSlots()
    {
        slots = new Dictionary<Vector2Int, ItemSlotView>();
        foreach (var (key, slot) in container.slots)
        {
            var newSlot = Instantiate(slotPrefab, transform).GetComponent<ItemSlotView>();
            newSlot.transform.localPosition = new Vector3(key.x * spacing, key.y * spacing, 0);
            newSlot.itemSlot = slot;
            slots[key] = newSlot;
        }
    }
}