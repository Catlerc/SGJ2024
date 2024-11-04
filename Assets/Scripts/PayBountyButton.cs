using System;
using UnityEngine;

public class PayBountyButton: MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        ButtonPressSound.instance.audioSource.Play();
        ShopUI.instance.payBounty();
    }
}