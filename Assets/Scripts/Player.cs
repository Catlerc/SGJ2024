using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    //links
    public GameObject mapObj;
    public GameObject playerObj;

    public SpriteRenderer itemRenderer;
    public Cursor cursor;

    //settings
    public float moveSpeed = 1f;

    //state
    public bool walking = true;
    public Health health;
    [CanBeNull] private Enemy enemy;
    private bool inAnimation = false;
    public bool myTurn = false;
    private Item itemInHand;


    private void Update()
    {
        if (walking)
        {
            mapObj.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }

        if (health.health <= 0) GameOver();
        if (!inAnimation && enemy != null)
        {
            if (myTurn)
            {
                StartCoroutine(startFightAnimation());
            }
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

        if (itemInHand != null) enemy.attack(itemInHand.type.damage);
        inAnimation = false;
        myTurn = false;
        enemy.myTurn = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            walking = false;
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.player = this;
            enemy.myTurn = false;
            myTurn = true;
        }
    }

    public void GameOver()
    {
    }

    public void attack(float dmg)
    {
        health.health -= dmg;
    }

    public void setItem([CanBeNull] Item item)
    {
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
        cursor.clickOnPlayer = true;
        
    }
}