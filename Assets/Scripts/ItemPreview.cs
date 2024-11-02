using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemPreview : MonoBehaviour
{
    public Item item;

    public ItemType testType;

    public GameObject partPrefab;

    public float spacing = 0.5f;

    public SpriteRenderer itemSprite;

    public List<GameObject> parts = new List<GameObject>();
    public GameObject itemRotationPoint;

    private void Start()
    {
        item = new Item(this.testType);
        generateParts(item.shape);
        itemSprite.sprite = item.type.image;
        itemSprite.transform.localPosition = new Vector3(
            item.shape.minX * spacing - spacing / 2,
            item.shape.minY * spacing - spacing / 2,
            0
        );
        var targetSize = new Vector2(
            item.shape.maxX - item.shape.minX + 1,
            item.shape.maxY - item.shape.minY + 1
        );
        targetSize *= spacing;
        print(targetSize);
        Vector2 spriteSize = itemSprite.sprite.bounds.size;
        Vector2 scale = new Vector2(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y);
        itemSprite.transform.localScale = new Vector3(scale.x, scale.y, 1);
    }

    public void generateParts(Shape shape)
    {
        clearParts();
        foreach (var point in shape.places)
        {
            var newPart = Instantiate(partPrefab, transform);
            newPart.transform.localPosition = new Vector3(
                point.x * spacing,
                point.y * spacing,
                0
            );
            parts.Add(newPart);
        }
    }

    public void clearParts()
    {
        foreach (var part in parts)
        {
            Destroy(part);
        }

        parts = new List<GameObject>();
    }


    private void Update()
    {
        itemRotationPoint.transform.rotation = Quaternion.Euler(0, 0, 90 * item.rotation);
        //TEST
        if (Input.GetMouseButtonDown(1))
        {
            print("down");
            this.clearParts();
            item.rotate();
            this.generateParts(item.shape);
        }
        //TEST
    }
}