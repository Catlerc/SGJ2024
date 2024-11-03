using System;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent evets;

    private void OnMouseUpAsButton()
    {
        evets.Invoke();
    }
}