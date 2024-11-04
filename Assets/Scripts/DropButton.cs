using System;
using UnityEngine;

public class DropButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        ButtonPressSound.instance.audioSource.Play();
        Cursor.instance.dropItem();
    }
}