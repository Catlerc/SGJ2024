﻿using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

public class Container
{
    public Dictionary<Vector2Int, ItemSlot> slots;

    public Container(int width, int height)
    {
        slots = new Dictionary<Vector2Int, ItemSlot>();
        for (int x = -width; x < width; x++)
        for (int y = -height; y < height; y++)
        {
            var pos = new Vector2Int(x, y);
            slots[pos] = new ItemSlot(pos);
        }
    }

    public ShapeCheckResult checkShape(Shape shape, Vector2Int applyPos)
    {
        var okSlots = new List<Vector2Int>();
        var okPart = new List<Vector2Int>();
        var badPart = new List<Vector2Int>();
        foreach (var itemPoint in shape.points)
        {
            var containerPoint = itemPoint + applyPos;
            var slotAtPoint = getSlot(containerPoint);
            if (slotAtPoint == null || slotAtPoint.item != null)
            {
                badPart.Add(itemPoint);
            }
            else
            {
                okPart.Add(itemPoint);
                okSlots.Add(containerPoint);
            }
        }

        return new ShapeCheckResult
        {
            okSlots = okSlots.ToArray(),
            okParts = okPart.ToArray(),
            badParts = badPart.ToArray(),
        };
    }

    public void applyItem(ItemSlot slot, Item item)
    {
        foreach (var point in item.shape.points)
        {
            var slotAtPoint = getSlot(point + slot.pos);
            slotAtPoint.item = item;
            slotAtPoint.posInItemShape = point;
        }
    }

    public void removeItem(Item item)
    {
        foreach (var (pos, slot) in this.slots)
        {
            if (slot.item != null && slot.item.id == item.id)
                slot.item = null;
        }
    }

    public ItemSlot getSlot(Vector2Int point)
    {
        if (slots.ContainsKey(point))
            return slots[point];
        return null;
    }
}

public class ShapeCheckResult
{
    public Vector2Int[] okSlots;
    public Vector2Int[] okParts;
    public Vector2Int[] badParts;
}