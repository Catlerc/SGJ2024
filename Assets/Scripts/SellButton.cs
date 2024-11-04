using System;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        ButtonPressSound.instance.audioSource.Play();
        Economics.instance.trySell();
    }
}