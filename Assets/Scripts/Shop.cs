using UnityEngine;

public class Shop : MonoBehaviour
{
    public int bounty;
    public ItemType[] items;

    public bool isLast;


    public void openShopUI()
    {
        ShopUI.instance.actualShop = this;
        ShopUI.instance.regenerateLines();
        ShopUI.instance.openShopUI();
    }
}