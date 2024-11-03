using UnityEngine;

[CreateAssetMenu(menuName = "Chest")]
public class ChestItemType : ItemType
{
    public ItemType[] lootTable;


    public Item randomItemFromLoootTable()
    {
        var itemType = lootTable[Random.Range(0, lootTable.Length)];
        var item = new Item(itemType);
        return item;
    }

    public void openChest(ContainerView containerView, Item chest)
    {
        var lootItem = randomItemFromLoootTable();
        lootItem = lootItem.withCustomOrigin(new Vector2Int(lootItem.shape.minX, lootItem.shape.minY));
        for (int i = 0; i < Random.Range(0, 3); i++)
        {
            lootItem.rotate();
        }

        lootItem = lootItem.withCustomOrigin(new Vector2Int(lootItem.shape.minX, lootItem.shape.minY));

        var bottomLeftSlot = containerView.getBottomLeftOfItemInContainer(chest);
        containerView.removeItem(chest);

        containerView.applyItem(bottomLeftSlot, lootItem);
    }
}