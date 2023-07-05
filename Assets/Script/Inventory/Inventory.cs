using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   
    [SerializeField]
    public List<Slot> Inven_slots;
    [SerializeField]
    Transform SlotParent;
 

    public static Inventory Instance;
    public Slot SelectedSlot;

    public List<Item> InventoryItemList;

    void Awake()
    {
   
        if (Instance == null)
        {
            Instance = this; 
         
        }
        else
        {
            if (Instance != this) 
                Destroy(this.gameObject); 
        }
       
       
    }
    
    public void FreshSlot()//인벤토리 재정렬
    {
        int i = 0;
        for (; i < InventoryItemList.Count; i++)
        {
            Inven_slots[i].GetItem(InventoryItemList[i]);
            
        }
        for (; i < Inven_slots.Count; i++)
        {
            Inven_slots[i].GetItem(null);

        }
    }

    public void AddItem(Item _item)
    {
        if (InventoryItemList.Count < Inven_slots.Count)
        {
            InventoryItemList.Add(_item);
            FreshSlot();
        }
        else
        {
            return;
           //슬롯부족

        }
    }

    private void Start()
    {
        StartCoroutine(freshSlotDelay());
        SelectedSlot = null;
    }
    IEnumerator freshSlotDelay()
    {
        yield return new WaitForSeconds(2f);
        FreshSlot();
        yield return null;
    }
    public void Unselect(Slot Selectedslot)
    {
        Selectedslot.image.color = new Color(1, 1, 1, 1);
        SelectedSlot = null;
    }
    public void SellItem()
    {
        if(SelectedSlot != null&& SelectedSlot._item.isEquip==false)//장착중인장비가아니고 선택된슬롯이 있다면 선택된슬롯의 아이템 제거.
        {
            InventoryItemList.Remove(SelectedSlot.GetComponent<Slot>()._item);
           
            Unselect(SelectedSlot);
            FreshSlot();
        }
    }
    public void EquipItem()
    {
        if (SelectedSlot != null&& SelectedSlot.GetComponent<Slot>()._item.itemtype==UiManager.ItemType.Equip)
        {
            Item equipitem = SelectedSlot.GetComponent<Slot>()._item;
            equipitem.isEquip = true;

            Equip.Instance.EquipItem(equipitem, SelectedSlot);
            Unselect(SelectedSlot);
        }
      
    }

}
