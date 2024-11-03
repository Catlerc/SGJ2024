using System;
using UnityEngine;

public class ItemSlotView : MonoBehaviour
{
    public ItemSlot itemSlot;
    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (itemSlot.item is null)
        {
            spriteRenderer.color = Color.grey;
            spriteRenderer.gameObject.SetActive(true);
        }
        else
        {
            spriteRenderer.gameObject.SetActive(false);
            // spriteRenderer.color = Color.green;
        }
    }

    private void OnMouseOver()
    {
        Cursor.instance?.isOverItemSlot(this);
    }
}