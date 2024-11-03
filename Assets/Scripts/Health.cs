using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public SpriteRenderer healthBar;

    private void Update()
    {
        healthBar.transform.localScale = new Vector3(health / maxHealth, 1f, 1f);
    }
}