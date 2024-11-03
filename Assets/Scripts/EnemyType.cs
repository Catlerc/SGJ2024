using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyType : ScriptableObject
{
    public Sprite sprite;
    public ItemType[] lootTable;
    public float maxHP;
    public float attack;
    
    
    public Item randomItemFromLoootTable()
    {
        var itemType = lootTable[Random.Range(0, lootTable.Length)];
        var item = new Item(itemType);
        return item;
    }

}