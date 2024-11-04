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
            spriteRenderer.color = Color.yellow;
        else if (ok)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }
}