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
    [Header("���� ����"),SerializeField]
    public EquipSlot[] Equip_slots;

    //���������� ����Ʈ
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
                
                if(Equip_slots[i]._item == newitem)//������������ �������Ѵٸ�?-> Ż��
                {
                    UnEquipItem(Equip_slots[i]);
                }
                else if (Equip_slots[i]._item != null)//������ �������� �����ߴٸ�?->���� ������ Ż���� ����
                {
                    UnEquipItem(Equip_slots[i]);
                    Equip_slots[i].EquipItem(newitem, ConnectedSlot);
                }
                else//����ִ� ���������̶��.
                {
                    Equip_slots[i].EquipItem(newitem, ConnectedSlot);
                }

                if(newitem.itemdetailtype==UiManager.ItemDetailType.Use)//�Һ�������� �����Ұ�� �Һ񽽷Կ��� �߰��� ����
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



