using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        ButtonPressSound.instance.audioSource.Play();
        SceneManager.LoadScene(0);
    }
}