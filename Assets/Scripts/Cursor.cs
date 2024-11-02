using System;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public ContainerView containerView;
    public static Cursor instance;
    public ItemPreview itemPreview;


    private ItemSlotView overItemView = null;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (overItemView != null)
        {
            var check = containerView.container.checkShape(itemPreview.item.shape, overItemView.itemSlot.pos);
            if (check.badParts.Length == 0)
            {
                foreach (var partPos in check.okParts) itemPreview.parts[partPos].ok = true;
            }
            else
            {
                foreach (var partPos in check.okParts) itemPreview.parts[partPos].ok = true;
                foreach (var partPos in check.badParts) itemPreview.parts[partPos].ok = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (check.badParts.Length == 0)
                {
                    containerView.container.applyItem(overItemView.itemSlot, itemPreview.item);
                }
            }
        }


        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        if (overItemView != null)
        {
            itemPreview.transform.position = overItemView.transform.position;
            overItemView = null;
        }
        else
            itemPreview.transform.position = pos;

        transform.position = pos;
    }


    public void isOverItemSlot(ItemSlotView itemSlotView)
    {
        overItemView = itemSlotView;
    }
}