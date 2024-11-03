using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //links
    public GameObject mapObj;

    //settings
    public float moveSpeed = 1f;

    //state
    public bool walking = true;
    public float hp = 100f;

    private void Update()
    {
        if (walking)
        {
            mapObj.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            walking = false;
        }
    }
}