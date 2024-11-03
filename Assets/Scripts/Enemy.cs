using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health health;
    public EnemyType type;
    public SpriteRenderer spriteRenderer;
    public GameObject lootPrefab;

    // state
    private bool inAnimation = false;
    public bool myTurn = false;

    private void Start()
    {
        spriteRenderer.sprite = type.sprite;
        health.maxHealth = type.maxHP;
        health.health = type.maxHP;
    }

    public void attack(float dmg)
    {
        health.health -= dmg;
        if (health.health <= 0) dead();
    }

    private void Update()
    {
        if (!inAnimation)
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
            spriteRenderer.transform.localScale = new Vector3(1 + c, 1 - c, 1);
            spriteRenderer.transform.localPosition = new Vector3(-c, 0, 0);

            yield return new WaitForEndOfFrame();
        }

        Player.instance.attack(type.attack);
        inAnimation = false;
        myTurn = false;
        Player.instance.myTurn = true;
    }

    public void dead()
    {
        Player.instance.walking = true;
        var loot = Instantiate(lootPrefab, Player.instance.mapObj.transform).GetComponent<Loot>();
        loot.transform.position = transform.position + new Vector3(0, 0, -0.2f);
        loot.init(type.randomItemFromLoootTable());
        Destroy(gameObject);
    }
}