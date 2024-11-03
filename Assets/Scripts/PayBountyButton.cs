using System;
using UnityEngine;

public class PayBountyButton: MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        ShopUI.instance.payBounty();
    }
}