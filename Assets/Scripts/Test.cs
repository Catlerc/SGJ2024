using UnityEngine;

public class Test : MonoBehaviour
{
    public Cursor cursor;
    public KeyItemType keyItemType;
    public ChestItemType chestItemType;
    public InvisibilityPotionItemType invizType;
    public HealPotionItemType healType;
    public ItemType itemType;

    public void onGetKey()
    {
        cursor.grabItem(new Item(keyItemType));
    }

    public void onGetItem()
    {
        cursor.grabItem(new Item(itemType));
    }

    public void onGetChest()
    {
        cursor.grabItem(new Item(chestItemType));
    }
    
    public void onGetHeal()
    {
        cursor.grabItem(new Item(healType));
    }
    
    public void onGetInviz()
    {
        cursor.grabItem(new Item(invizType));
    }
}