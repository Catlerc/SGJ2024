using System;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public Item item;
    public AudioSource paySound;

    private void OnMouseUpAsButton()
    {
        var count = Economics.instance.countMoney();
        if (count >= item.type.cost && Cursor.instance.itemInHand == null)
        {
            paySound.Play();
            Economics.instance.removeMoney(item.type.cost);
            Cursor.instance.grabItem(item);
        }
    }
}