using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemType : ScriptableObject
{
    public string name;
    public string cost;
    public string description;
    public Sprite image;

    public string[] rawPlaces;
    public Vector2Int center;

    public Shape getShape()
    {
        return Shape.fromStrings(this.rawPlaces, center);
    }
}