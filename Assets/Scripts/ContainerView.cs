using System.Collections.Generic;
using UnityEngine;

public class ContainerView : MonoBehaviour
{
    public GameObject slotPrefab;
    public Container container;
    public float spacing;

    public Dictionary<Vector2Int, ItemSlotView> slots;

    private void Start()
    {
        //test
        this.container = new Container(30, 10);
        //test
    }

    public void generateSlots()
    {
        foreach (var (key, slot) in container.slots)
        {
            var newSlot = Instantiate(slotPrefab, transform).GetComponent<ItemSlotView>();
            newSlot.transform.localPosition = new Vector3(key.x * spacing, key.y * spacing, 0);
            slots[key] = newSlot;
        }
    }
}