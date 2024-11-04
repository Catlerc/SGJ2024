using TMPro;
using UnityEngine;

public class ButItemLine : MonoBehaviour
{
    public ItemType item;

    public TextMeshPro text;
    public SpriteRenderer itemIcon;
    public BuyButton buyButton;

    public void init(ItemType item)
    {
        buyButton.item = item;
        this.item = item;
        text.text = item.cost.ToString();
        itemIcon.sprite = item.image;
    }
}   