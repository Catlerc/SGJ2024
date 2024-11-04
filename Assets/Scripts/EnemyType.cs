using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyType : ScriptableObject
{
    public Sprite sprite;
    public Sprite sprite2;
    public ItemType[] lootTable;
    public float maxHP;
    public float attack;
    public float size;
    
    
    public Item randomItemFromLoootTable()
    {
        var itemType = lootTable[Random.Range(0, lootTable.Length)];
        var item = new Item(itemType);
        return item;
    }

}