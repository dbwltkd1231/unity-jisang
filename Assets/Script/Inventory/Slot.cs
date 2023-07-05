using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
[Serializable]
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] public Image image;
   
    [SerializeField] public GameObject EquipText;
   
    public Item _item;
   
    public void GetItem(Item item)
    {
        if(item!=null)
        {
            _item = item;
            image.sprite = _item.itemImage;
            if(item.isEquip==true)
            {
                EquipText.SetActive(true);
            }
        }
        else
        {
            image.sprite = UiManager.uimanager.SlotBasicSprite;
            EquipText.SetActive(false);
        }
        
    }
   

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowInfo.showinfo.Active(_item, this.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShowInfo.showinfo.NoActive();
    }
 
    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    //½½·ÔÅ¬¸¯-> ½½·Ô=>¼¿·ºÆ®½½·Ô or ¼¿·ºÆ®½½·Ô=>½½·Ô
    public void OnPointerClick(PointerEventData eventData)
    {
        if(Inventory.Instance.SelectedSlot==this)
        {
            Inventory.Instance.SelectedSlot = null;
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            if (Inventory.Instance.SelectedSlot!=null&& Inventory.Instance.SelectedSlot != this)
            {
                Inventory.Instance.SelectedSlot.image.color = new Color(1, 1, 1, 1);
            }
            
            Inventory.Instance.SelectedSlot = this;
            this.image.color = new Color(1, 0.92f, 0.016f, 1);
        }
        
        

    }

   
}
