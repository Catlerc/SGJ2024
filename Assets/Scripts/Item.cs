using UnityEngine.UIElements;

public class Item
{
    public ItemType type;
    public int rotation = 0;
    public Shape shape;

    public Item(ItemType type)
    {
        this.type = type;
        this.shape = Shape.fromStrings(type.rawPlaces, type.center);
    }

    public void rotate()
    {// только поворот по часовой покачто.
        this.rotation = (this.rotation - 1) % 4; // =  0,1,2,3
        this.shape = shape.rotate();
    }
}