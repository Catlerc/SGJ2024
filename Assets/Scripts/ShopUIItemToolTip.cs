using System;
using UnityEngine;

public class ShopUIItemToolTip: MonoBehaviour
{
    public ButItemLine line;
    private void OnMouseOver()
    {
        Cursor.instance.updateToolTip(line.item);
        Cursor.instance.overShopItem = true;
    }
}