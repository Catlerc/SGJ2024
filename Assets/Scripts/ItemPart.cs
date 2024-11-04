using System;
using UnityEngine;

public class ItemPart : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool ok;
    public bool isKey;

    private void Update()
    {
        if (isKey)
        {
            var c = Color.yellow;
            c.a = 0.5f;
            spriteRenderer.color = c;
        }
        else if (ok)
        {
            var c = Color.green;
            c.a = 0.5f;
            spriteRenderer.color = c;
        }
        else
        {
            var c = Color.red;
            c.a = 0.5f;
            spriteRenderer.color = c;
        }
    }
}