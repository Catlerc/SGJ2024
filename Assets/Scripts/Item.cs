using UnityEngine;
using UnityEngine.UIElements;

public class Item
{
    public int id;
    public ItemType type;
    public int rotation = 0;
    public Shape shape;

    public Item(ItemType type)
    {
        this.id = Random.Range(int.MinValue, int.MaxValue);
        this.type = type;
        this.shape = Shape.fromStrings(type.rawPlaces);
    }

    public void rotate()
    {
        // только поворот по часовой покачто.
        this.rotation = (this.rotation - 1) % 4; // =  0,1,2,3
        this.shape = shape.rotate();
    }

    public Item withCustomOrigin(Vector2Int origin)
    {
        var newItem = new Item(type);
        newItem.id = this.id;
        newItem.rotation = this.rotation;
        newItem.shape = this.shape.withCustomOrigin(origin);
        return newItem;
    }
}