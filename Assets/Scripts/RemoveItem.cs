using System;
using UnityEngine;

public class RemoveItem: MonoBehaviour
{
    private void Awake()
    {
        Cursor.instance.removeItemFromHand();
    }
}