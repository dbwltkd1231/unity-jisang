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
    
    public void FreshSlot()//�κ��丮 ������
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
           //���Ժ���

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
        if(SelectedSlot != null&& SelectedSlot._item.isEquip==false)//����������񰡾ƴϰ� ���õȽ����� �ִٸ� ���õȽ����� ������ ����.
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
