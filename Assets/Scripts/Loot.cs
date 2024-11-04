using System;
using System.Collections;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public SpriteRenderer itemRenderer;
    public Item item;
    public GameObject arrow;

    private void OnMouseUpAsButton()
    {
        if (this.item.type is WinNote)
        {
            Player.instance.walking = false;
            
            Player.instance.winScreen.SetActive(true);
            Player.instance.musicObj.SetActive(false);
        }
        else
        {
            Cursor.instance.overLoot = this;
        }
    }

    public void init(Item item)
    {
        this.item = item;
        itemRenderer.sprite = item.type.image;
        StartCoroutine(dropAnim());
        if (this.item.type is WinNote)
        {
            Player.instance.walking = false;
            Player.instance.stop = true;
        }
    }

    IEnumerator dropAnim()
    {
        var time = 0f;
        var maxTime = 1f;
        while (time < maxTime)
        {
            time += Time.deltaTime;
            var t = time / maxTime;
            itemRenderer.transform.localPosition = new Vector3(0, Mathf.Sin((0.25f + t * 0.75f) * Mathf.PI) / 5 - t, 0);
            itemRenderer.transform.rotation = Quaternion.Euler(0, 0, 360 * t);
            yield return new WaitForEndOfFrame();
        }

        if (this.item.type is WinNote)
        {
            arrow.SetActive(true);
        }
    }
}