using System;
using System.Collections.Generic;
using System.Linq;
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

    public void removeMoney(int amount)
    {
        var coinList = new List<ItemSlotView>();
        foreach (var (key, value) in containerView.slots)
        {
            if (value.itemSlot.item != null && value.itemSlot.item.type is CoinItemType)
            {
                coinList.Add(value);
            }
        }

        var itemSlotViews = coinList.OrderBy(slot => slot.itemSlot.item.type.cost).ToList();
        foreach (var slot in itemSlotViews)
        {
            var slotConst = slot.itemSlot.item.type.cost;
            var min = Mathf.Min(amount, slotConst);
            var newSlotCoinTypeIndex = slotConst - min - 1;
            if (newSlotCoinTypeIndex == -1)
            {
                containerView.removeItem(slot.itemSlot.item);
            }
            else
            {
                containerView.removeItem(slot.itemSlot.item);
                containerView.applyItem(slot, new Item(coinTypes[newSlotCoinTypeIndex]));
            }

            amount -= min;
            if (amount == 0) break;
        }
    }

    public int addMoneyToAvailableSlotRec(int amount)
    {
        for (int i = 0; i < 100; i++)
        {
            var newAmount = addMoneyToAvailableSlot(amount);
            if (newAmount == amount || newAmount == 0) return newAmount;
            amount = newAmount;
        }

        return amount;
    }

    public int addMoneyToAvailableSlot(int amount)
    {
        ItemSlotView nonFullSlot = null;
        ItemSlotView emptySlot = null;
        foreach (var (key, value) in containerView.slots)
        {
            if (value.itemSlot.item == null)
            {
                emptySlot = value;
            }
            else if (value.itemSlot.item.type is CoinItemType && value.itemSlot.item.type.cost != 10)
            {
                nonFullSlot = value;
            }
        }

        if (nonFullSlot != null)
        {
            var min = 10 - nonFullSlot.itemSlot.item.type.cost;

            var realMin = Mathf.Min(min, amount);

            var newCoinIndex = nonFullSlot.itemSlot.item.type.cost + realMin - 1;

            containerView.removeItem(nonFullSlot.itemSlot.item);
            containerView.applyItem(nonFullSlot, new Item(coinTypes[newCoinIndex]));
            return amount - realMin;
        }

        if (emptySlot != null)
        {
            var realMin = Mathf.Min(10, amount);
            var newCoinIndex = realMin - 1;
            print(newCoinIndex);
            print(coinTypes[newCoinIndex]);
            containerView.applyItem(emptySlot, new Item(coinTypes[newCoinIndex]));
            return amount - realMin;
        }

        return amount;
    }

    public void trySell()
    {
        if (Cursor.instance.itemInHand != null)
        {
            addMoneyToAvailableSlotRec(Cursor.instance.itemInHand.type.cost);
            Cursor.instance.removeItemFromHand();
        }
    }
}