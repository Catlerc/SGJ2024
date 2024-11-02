using JetBrains.Annotations;
using UnityEngine;

public class ItemSlot
{
    [NotNull] public Vector2Int pos;
    [CanBeNull] public Item item = null;
    [CanBeNull] public Vector2Int? posInItemShape = null;

    public ItemSlot(Vector2Int pos)
    {
        this.pos = pos;
    }
}