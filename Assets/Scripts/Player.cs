using System;
using System.Collections;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    //links
    public GameObject mapObj;
    public GameObject playerObj;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer playerSpriteRenderer2;
    public SpriteRenderer itemRenderer;
    public Cursor cursor;
    public GameObject gameOverScreen;


    public Sprite GGWalk1;
    public Sprite GGWalk2;
    public Sprite VGWalk1;
    public Sprite VGWalk2;

    //settings
    public float moveSpeed = 1f;

    public ItemType startingItem;

    //state
    public bool walking = true;
    public Health health;
    [CanBeNull] private Enemy enemy;
    private bool inAnimation = false;
    public bool myTurn = false;
    private Item itemInHand;
    public float invisTime = 0;

    private float time;
    public GameObject lootPrefab;
    public AudioSource damageSound;

    public static Player instance;
    private bool walkType = false;

    private void Start()
    {
        instance = this;
        setItem(new Item(startingItem));
    }

    private void Update()
    {
        if (walking)
        {
            mapObj.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            time += Time.deltaTime;
            if (time > 0.3f)
            {
                time = 0;
                walkType = !walkType;

                if (Random.Range(0f, 100f) < 2) dropCurrentItem();
            }

            playerSpriteRenderer.sprite = walkType ? GGWalk1 : GGWalk2;
            playerSpriteRenderer2.sprite = walkType ? VGWalk1 : VGWalk2;
        }

        if (health.health <= 0) GameOver();
        if (!inAnimation && enemy != null)
        {
            if (invisTime > 0)
            {
                enemy = null;
                walking = true;
            }

            if (myTurn)
            {
                StartCoroutine(startFightAnimation());
            }
        }

        if (invisTime > 0)
        {
            invisTime -= Time.deltaTime;
            itemRenderer.color = Color.grey;
            playerSpriteRenderer.color = Color.grey;
            playerSpriteRenderer2.color = Color.grey;
        }
        else
        {
            itemRenderer.color = Color.white;
            playerSpriteRenderer.color = Color.white;
            playerSpriteRenderer2.color = Color.white;
        }
    }

    IEnumerator startFightAnimation()
    {
        inAnimation = true;
        var time = 0f;
        var maxTime = 0.5f;

        while (time < maxTime)
        {
            time += Time.deltaTime;
            var t = time / maxTime;

            var c = Mathf.Sin(t * Mathf.PI) / 7;
            playerObj.transform.localScale = new Vector3(1 + c, 1 - c, 1);
            playerObj.transform.localPosition = new Vector3(c, 0, 0);

            yield return new WaitForEndOfFrame();
        }

        if (itemInHand != null)
        {
            enemy.attack(itemInHand.type.damage);
            damageSound.Play();
        }
        inAnimation = false;
        myTurn = false;
        enemy.myTurn = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy") && invisTime <= 0)
        {
            walking = false;
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.myTurn = false;
            myTurn = true;
        }

        if (other.CompareTag("shop"))
        {
            walking = false;
            var shop = other.gameObject.GetComponent<Shop>();
            shop.openShopUI();
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void attack(float dmg)
    {
        damageSound.Play();
        health.health -= dmg;
    }

    public void dropCurrentItem()
    {
        if (itemInHand != null)
        {
            var loot = Instantiate(lootPrefab, Player.instance.mapObj.transform).GetComponent<Loot>();
            loot.transform.position = playerSpriteRenderer.transform.position + new Vector3(0, 0, -0.2f);
            loot.init(itemInHand);
            itemInHand = null;
            itemRenderer.gameObject.SetActive(false);
        }   
    }

    public void setItem([CanBeNull] Item item)
    {
        if (itemInHand != null)
        {
            dropCurrentItem();
        }

        if (item != null)
        {
            itemInHand = item;
            itemRenderer.sprite = item.type.image;
            itemRenderer.gameObject.SetActive(true);
        }
        else
        {
            itemInHand = null;
            itemRenderer.gameObject.SetActive(false);
        }
    }


    private void OnMouseUpAsButton()
    {
        print("click");
        cursor.clickOnPlayer = true;
    }
}