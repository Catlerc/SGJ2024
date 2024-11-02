using System;
using System.Collections.Generic;
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
        // var shape = item.type
        //     .getShape(); // нам нужена изначальная форма предмета, а не та, что получилась после поворота.
        itemRotationPoint.transform.localPosition = new Vector3(
            (item.shape.maxX + item.shape.minX) / 2f * spacing, // - spacing / 2,
            (item.shape.maxY + item.shape.minY) / 2f * spacing, // - spacing / 2,
            0
        );
        var targetSize = new Vector2(
            item.shape.maxX - item.shape.minX + 1,
            item.shape.maxY - item.shape.minY + 1
        );

        targetSize *= spacing;
        Vector2 spriteSize = itemSprite.sprite.bounds.size;
        if (item.rotation % 2 == -1)
        {
            spriteSize = new Vector2(spriteSize.y, spriteSize.x);
        }
        Vector2 scale = new Vector2(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y);


        itemSprite.transform.localScale = new Vector3(scale.x, scale.y, 1);
        itemRotationPoint.transform.rotation = Quaternion.Euler(0, 0, 90 * item.rotation);
    }


    private void Update()
    {
        // itemRotationPoint.transform.rotation = Quaternion.Euler(0, 0, 90 * item.rotation);
    }
}