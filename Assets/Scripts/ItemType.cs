using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemType : ScriptableObject
{
    public string name;
    public string cost;
    public string description;
    public string category;
    public Sprite image;

    public string[] rawPlaces;

    public Shape getShape()
    {
        return Shape.fromStrings(this.rawPlaces);
    }
}