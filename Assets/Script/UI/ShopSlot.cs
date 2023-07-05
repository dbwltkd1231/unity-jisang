using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] Image image;
    [SerializeField] public Item _item;

  
    public void GetItemInShop(Item item)
    {
        _item = item;
        image.sprite = _item.itemImage;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowInfo.showinfo.Active(_item, this.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShowInfo.showinfo.NoActive();
    }




}
