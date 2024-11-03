using System;
using UnityEngine;

public class DropButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Cursor.instance.dropItem();
    }
}