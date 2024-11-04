using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;
    public Shop actualShop;

    // public Transform inventoryTransform;
    // public Transform dropButtonTransform;
    //
    // public Transform markNormDropButton;
    // public Transform markNormInv;
    // public Transform markOpenDropButton;
    // public Transform markOpenInv;
    public WinNote winNoteType;
    public GameObject lootPrefab;
    public GameObject shopInterface;

    public GameObject[] lines = Array.Empty<GameObject>();
    public GameObject linePrefab;
    public Transform lineStartMarker;

    public TextMeshPro bountyText;

    public GameObject winScreen;
    public AudioSource openDoorSound;
    public AudioSource paySound;
    public TextMeshPro goText;
    public bool ripAnim;
    public GameObject ripScreen;
    public Transform ripCheliki;
    public Transform playerMarker;
    public GameObject musicObj;

    public Transform playerObj;
    public Transform playerObjMarker;
    public Transform winDropMarker;

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
            line.transform.localPosition = new Vector3(0, -i * 0.78f, -3);
            l.Add(line.gameObject);
            i += 1;
        }

        lines = l.ToArray();
    }

    public void openShopUI()
    {
        bountyText.text = "Нужно денег чтобы пройти: " + actualShop.bounty;

        shopInterface.SetActive(true);
    }

    public void closeShopUI()
    {
        shopInterface.SetActive(false);
        // inventoryTransform.position = markNormInv.position;
        // dropButtonTransform.position = markNormDropButton.position;
        if (actualShop.isLast)
        {
            Player.instance.walking = false;
            var loot = Instantiate(lootPrefab, Player.instance.mapObj.transform).GetComponent<Loot>();
            loot.transform.position = winDropMarker.position + new Vector3(0, 0, -0.2f);
            loot.init(new Item(winNoteType));
            StartCoroutine(startRunAnim());
            // winScreen.SetActive(true);
        }

        foreach (var obj in lines)
        {
            Destroy(obj);
        }
    }

    IEnumerator startRunAnim()
    {
        var time = 0f;
        var maxTime = 1f;
        while (time < maxTime)
        {
            time += Time.deltaTime;
            var t = time / maxTime;
            playerObj.position = Vector3.Lerp(playerObj.position, playerObjMarker.position, 0.007f);
            yield return new WaitForEndOfFrame();
            
        }
    }

    public void payBounty()
    {
        var count = Economics.instance.countMoney();
        if (Cursor.instance.itemInHand != null) return;
        if (count >= actualShop.bounty)
        {
            Economics.instance.removeMoney(actualShop.bounty);
            Player.instance.walking = true;
            // Cursor.instance.grabItem(item);
            closeShopUI();
            openDoorSound.Play();
            paySound.Play();
        }
        else if (!ripAnim)
        {
            ripAnim = true;
            StartCoroutine(startRipAnim());
        }
    }

    IEnumerator startRipAnim()
    {
        var time = 0f;
        var maxTime = 1f;
        while (time < maxTime)
        {
            time += Time.deltaTime;
            var t = time / maxTime;
            ripCheliki.position = Vector3.Lerp(ripCheliki.position, playerMarker.position, 0.05f);
            yield return new WaitForEndOfFrame();
        }

        musicObj.SetActive(false);
        ripScreen.SetActive(true);
    }

    private void Update()
    {
        var money = Economics.instance.countMoney();
        if (actualShop == null) return;
        if (actualShop.bounty > money)
        {
            goText.text = "Сдаться";
        }
        else
        {
            goText.text = "Заплатить за проход";
        }
    }
}