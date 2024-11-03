using UnityEngine;

public class Shop : MonoBehaviour
{
    public int bounty;
    public int[] costs;
    public ItemType[] items;


    public void openShopUI()
    {
        ShopUI.instance.actualShop = this;
        ShopUI.instance.openShopUI();
    }
}