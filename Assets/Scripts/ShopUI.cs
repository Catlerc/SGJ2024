using System;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;
    public Shop actualShop;

    public Transform inventoryTransform;
    public Transform dropButtonTransform;

    public Transform markNormDropButton;
    public Transform markNormInv;
    public Transform markOpenDropButton;
    public Transform markOpenInv;

    public GameObject shopInterface;

    private void Start()
    {
        instance = this;
    }


    public void openShopUI()
    {
        inventoryTransform.position = markOpenInv.position;
        dropButtonTransform.position = markOpenDropButton.position;
        shopInterface.SetActive(true);
    }

    public void closeShopUI()
    {
        shopInterface.SetActive(false);
        inventoryTransform.position = markNormInv.position;
        dropButtonTransform.position = markNormDropButton.position;
    }
}