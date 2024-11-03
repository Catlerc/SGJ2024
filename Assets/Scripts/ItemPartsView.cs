using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemPartsView : MonoBehaviour
{
    public Item item;

    public GameObject partPrefab;

    public float spacing = 0.5f;

    public Dictionary<Vector2Int, ItemPart> parts = new Dictionary<Vector2Int, ItemPart>();


    public void generateParts()
    {
        clearParts();
        foreach (var point in item.shape.points)
        {
            var newPart = Instantiate(partPrefab, transform).GetComponent<ItemPart>();
            newPart.transform.localPosition = new Vector3(
                point.x * spacing,
                point.y * spacing,
                -1
            );
            parts[point] = newPart;
        }
    }

    public void clearParts()
    {
        foreach (var (key, part) in parts)
        {
            Destroy(part.gameObject);
        }

        parts = new Dictionary<Vector2Int, ItemPart>();
    }
}