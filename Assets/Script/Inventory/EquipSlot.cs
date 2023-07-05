using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;

public class EquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public UiManager.ItemDetailType itemdetailtype;

    public Image EquipSlotImage;
 
    public Item _item;

    public Slot ConnectedSlot;


    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowInfo.showinfo.Active(_item, this.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShowInfo.showinfo.NoActive();
    }
   public void EquipItem(Item item,Slot connectedslot)
    {
        _item = item;
      
        
        EquipSlotImage.sprite = _item.itemImage;
        ConnectedSlot= connectedslot;
        ConnectedSlot.EquipText.SetActive(true);
        Equip.Instance.EquipItems.Add(_item);
        ItemManager.itemmanager.SetEfect(_item);
        Status.Instance.RefreshStats();
    }
    public void UnEquipItem()
    {
   
        ItemManager.itemmanager.RemoveEffect(_item);
        _item.isEquip = false;
        EquipSlotImage.sprite = UiManager.uimanager.SlotBasicSprite;
        ConnectedSlot.EquipText.SetActive(false);
        
        _item = null;
        ConnectedSlot = null;

        Status.Instance.RefreshStats();

    }
  
}
