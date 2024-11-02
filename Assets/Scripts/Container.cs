using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Container
{
    public int width;
    public int height;
    public Dictionary<Vector2Int, ItemSlot> slots;

    public Container(int width, int height)
    {
        this.width = width;
        this.height = height;
        slots = new Dictionary<Vector2Int, ItemSlot>();
        for (int x = 0; x < width; x++)
        for (int y = 0; y < width; y++)
        {
            slots[new Vector2Int(x, y)] = new ItemSlot();
        }
    }
}

