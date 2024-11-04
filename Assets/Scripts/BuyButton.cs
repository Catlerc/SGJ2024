using System;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public ItemType item;
    public AudioSource paySound;

    private void OnMouseUpAsButton()
    {
        var count = Economics.instance.countMoney();
        if (count >= item.cost && Cursor.instance.itemInHand == null)
        {
            paySound.Play();
            Economics.instance.removeMoney(item.cost);
            Cursor.instance.grabItem(new Item(item));
        }
    }
}