using System;
using UnityEngine;

public class ItemSlotView : MonoBehaviour
{
    public ItemSlot itemSlot;
    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (itemSlot.Item is null)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }
    }

    private void OnMouseOver()
    {
        Cursor.instance?.isOverItemSlot(this);
    }
}