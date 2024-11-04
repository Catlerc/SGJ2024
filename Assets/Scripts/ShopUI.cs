using System;
using System.Collections.Generic;
using TMPro;
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

    public GameObject[] lines = Array.Empty<GameObject>();
    public GameObject linePrefab;
    public Transform lineStartMarker;

    public TextMeshPro bountyText;

    public GameObject winScreen;
    public AudioSource openDoorSound;
    public AudioSource paySound;
    private void Start()
    {
        instance = this;
    }

    public void regenerateLines()
    {
        foreach (var obj in lines)
        {
            Destroy(obj);
        }

        var i = 0;
        var l = new List<GameObject>();
        foreach (var itemType in actualShop.items)
        {
            var line = Instantiate(linePrefab, lineStartMarker).GetComponent<ButItemLine>();
            line.init(itemType);
            line.transform.localPosition = new Vector3(0, -i, -3);
            l.Add(line.gameObject);
            i += 1;
        }

        lines = l.ToArray();
    }

    public void openShopUI()
    {
        bountyText.text = "Bounty: " + actualShop.bounty;
        inventoryTransform.position = markOpenInv.position;
        dropButtonTransform.position = markOpenDropButton.position;
        shopInterface.SetActive(true);
    }

    public void closeShopUI()
    {
        shopInterface.SetActive(false);
        inventoryTransform.position = markNormInv.position;
        dropButtonTransform.position = markNormDropButton.position;
        if (actualShop.isLast)
        {
            winScreen.SetActive(true);
        }

        foreach (var obj in lines)
        {
            Destroy(obj);
        }
    }

    public void payBounty()
    {
        var count = Economics.instance.countMoney();
        if (count >= actualShop.bounty && Cursor.instance.itemInHand == null)
        {
            Economics.instance.removeMoney(actualShop.bounty);
            Player.instance.walking = true;
            // Cursor.instance.grabItem(item);
            closeShopUI();
            openDoorSound.Play();
            paySound.Play();
        }
    }
}