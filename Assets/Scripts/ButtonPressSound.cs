using System;
using UnityEngine;

public class ButtonPressSound : MonoBehaviour
{
    public AudioSource audioSource;
    
    public static ButtonPressSound instance;

    private void Start()
    {
        instance = this;
    }
    
    
}