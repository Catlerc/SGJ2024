using System;
using UnityEngine;

public class Economics : MonoBehaviour
{
    public static Economics instance;
    public ContainerView containerView;
    public ItemType[] coinTypes;

    private void Start()
    {
        instance = this;
    }


    public int countMoney()
    {
        var sum = 0;
        foreach (var (key, value) in containerView.slots)
        {
            if (value.itemSlot.item != null && value.itemSlot.item.type is CoinItemType)
                sum += value.itemSlot.item.type.cost;
        }

        return sum;
    }
    
    public 
    
}