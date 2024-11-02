using System;
using UnityEngine;

public class ItemPart : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool ok;

    private void Update()
    {
        if (ok)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }
}