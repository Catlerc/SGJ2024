using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    public Item item;


    public float spacing = 0.5f;

    public SpriteRenderer itemSprite;
    public GameObject itemRotationPoint;

    public void updateSpriteSize()
    {
        itemSprite.sprite = item.type.image;
        itemRotationPoint.transform.localPosition = new Vector3(
            (item.shape.maxX + item.shape.minX) / 2f * spacing,
            (item.shape.maxY + item.shape.minY) / 2f * spacing,
            0
        );
        var targetSize = new Vector2(
            item.shape.maxX - item.shape.minX + 1,
            item.shape.maxY - item.shape.minY + 1
        );
        
        if (math.abs(item.rotation % 2) == 1)
        {
            targetSize = new Vector2(targetSize.y, targetSize.x);
        }

        targetSize *= spacing;
        Vector2 spriteSize = itemSprite.sprite.bounds.size;


        Vector2 scale = new Vector2(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y);
        

        itemSprite.transform.localScale = new Vector3(scale.x, scale.y, 1);
        itemRotationPoint.transform.rotation = Quaternion.Euler(0, 0, 90 * item.rotation);
    }
}