using TMPro;
using UnityEngine;

public class ButItemLine : MonoBehaviour
{
    public Item item;

    public TextMeshPro text;
    public SpriteRenderer itemIcon;
    public BuyButton buyButton;

    public void init(Item item)
    {
        buyButton.item = item;
        this.item = item;
        text.text = item.type.cost.ToString();
        itemIcon.sprite = item.type.image;
    }
}