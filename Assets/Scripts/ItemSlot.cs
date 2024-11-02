using JetBrains.Annotations;
using UnityEngine;

public class ItemSlot
{
    [CanBeNull] public Item Item;
    public Vector2Int pos;

    public ItemSlot([CanBeNull] Item item, Vector2Int pos)
    {
        Item = item;
        this.pos = pos;
    }
}

