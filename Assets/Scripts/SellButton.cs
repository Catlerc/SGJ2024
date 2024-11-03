using System;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Economics.instance.trySell();
    }
}