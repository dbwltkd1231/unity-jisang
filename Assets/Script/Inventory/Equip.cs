using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Equip : MonoBehaviour
{

    public static Equip Instance;
    /*
    [SerializeField]
    private Transform slotParent;
    */
    [Header("장착 슬롯"),SerializeField]
    public EquipSlot[] Equip_slots;

    //장착아이템 리스트
    public List<Item> EquipItems;
    

    public enum EquipSlotType
    {
        Armor,
        Weapon,
        Hat,
        Boots,
        Acc,
        Use
    }

    private void OnValidate()
    {
        //Equip_slots = slotParent.GetComponentsInChildren<EquipSlot>();
    }

    private void Awake()
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

   
        EquipItems = new List<Item>();
    }

    public void EquipItem(Item newitem,Slot ConnectedSlot)
    {
       
        for (int i=0;i< Equip_slots.Length;i++)
        {
            if(Equip_slots[i].itemdetailtype== newitem.itemdetailtype)
            {
                
                if(Equip_slots[i]._item == newitem)//같은아이템을 재장착한다면?-> 탈착
                {
                    UnEquipItem(Equip_slots[i]);
                }
                else if (Equip_slots[i]._item != null)//기존에 아이템이 존재했다면?->기존 아이템 탈착후 장착
                {
                    UnEquipItem(Equip_slots[i]);
                    Equip_slots[i].EquipItem(newitem, ConnectedSlot);
                }
                else//비어있는 장착슬롯이라면.
                {
                    Equip_slots[i].EquipItem(newitem, ConnectedSlot);
                }

                if(newitem.itemdetailtype==UiManager.ItemDetailType.Use)//소비아이템을 장착할경우 소비슬롯에도 추가로 적용
                {
                    UiManager.uimanager.PortionSlot.SkillImage.sprite = newitem.itemImage;
                    SkillManager.Instance.Portion.SkillMag =  newitem.itemMag;
                    
                }
            }
        }
        Inventory.Instance.FreshSlot();
    }
    public void UnEquipItem(EquipSlot equipslot)
    {
        if(equipslot._item.itemdetailtype==UiManager.ItemDetailType.Use)
        {
            UiManager.uimanager.PortionSlot.SkillImage.sprite = UiManager.uimanager.SlotBasicCircleSprite;
            SkillManager.Instance.Portion.SkillMag = 0;
        }
        equipslot.UnEquipItem();

        EquipItems.Remove(equipslot._item);

        

    }




}



