using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemManager : MonoBehaviour
{
    [Header("�����۵����� ������"), SerializeField]
    string csv_FileName;
    [Header("���� ����"), SerializeField]
    ShopSlot[] shopslot;

    public static ItemManager itemmanager;
    private void Awake()
    {
        if(itemmanager==null)
        {
            itemmanager = this;
        }
        CreateItem(csv_FileName);
    }
  
    public void SetEfect(Item item)
    {
        int tmp = 0;
        if(item.itemdetailtype==UiManager.ItemDetailType.Armor)//ü��
        {
            tmp = item.itemMag + item.Starpos*10;
            PlayerManager.Instance.MaxHpInc(tmp);
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Weapon)//���ݷ�
        {
            tmp = item.itemMag + item.Starpos * 3;
            PlayerManager.Instance.playerstats.itemAtt += tmp;
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Boots)//�ӵ�
        {
            tmp = item.Starpos * 10;
            PlayerManager.Instance.playerstats.itemSpd += item.itemMag;
            PlayerManager.Instance.MaxHpInc(tmp);

        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Hat)//����
        {
            tmp = item.itemMag + item.Starpos * 2;
            PlayerManager.Instance.playerstats.itemDef += tmp;
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Acc)//ũȮ
        {
            tmp = item.itemMag + item.Starpos * 3;
            PlayerManager.Instance.playerstats.itemCrt += tmp;
        }

        Status.Instance.RefreshStats();
    }

    public void RemoveEffect(Item item)
    {
        int tmp = 0;
        if (item.itemdetailtype == UiManager.ItemDetailType.Armor)//ü��
        {
            tmp = item.itemMag + item.Starpos * 10;
            PlayerManager.Instance.MaxHpInc(-tmp);
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Weapon)//���ݷ�
        {
            tmp = item.itemMag + item.Starpos * 3;
            PlayerManager.Instance.playerstats.itemAtt -= tmp;
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Boots)//�ӵ�
        {
            tmp = item.Starpos * 10;
            PlayerManager.Instance.playerstats.itemSpd -= item.itemMag;
            PlayerManager.Instance.MaxHpInc(-tmp);
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Hat)//����
        {
            tmp = item.itemMag+item.Starpos * 2;
            PlayerManager.Instance.playerstats.itemDef -= tmp;
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Acc)//ũȮ
        {
            tmp = item.itemMag + item.Starpos * 3;
            PlayerManager.Instance.playerstats.itemCrt -= tmp;
        }

        Status.Instance.RefreshStats();
    }
    public void CreateItem(string _CSVFileName)
    {
        List<Dictionary<string, object>> data = CSVReader.Read(_CSVFileName);
        
       for(int i=0;i<data.Count;i++)
        {
            if(data[i]["�̸�"]=="")
            {
                break;
            }
            //Item item = new Item();
            Item item = ScriptableObject.CreateInstance("Item") as Item;
            item.itemName = data[i]["�̸�"].ToString();
            item.Cost = (int)data[i]["����"];
            item.itemDes = (string)data[i]["����"];

           
            

            string itemtype = data[i]["Ÿ��"].ToString();
            item.itemtype = (UiManager.ItemType)Enum.Parse(typeof(UiManager.ItemType), itemtype);

            string itemdetailtype = data[i]["����Ÿ��"].ToString();
            item.itemdetailtype = (UiManager.ItemDetailType)Enum.Parse(typeof(UiManager.ItemDetailType), itemdetailtype);
  
            item.itemDes = data[i]["����"].ToString();
            item.itemMag = (int)data[i]["��ġ"];
            item.itemImagePath= data[i]["�̹���"].ToString();
            item.itemImage = Resources.Load<Sprite>(item.itemImagePath);
            shopslot[i].GetItemInShop(item);

        }
        
    }

    public string FindItem(string name)
    {
        for(int i=0;i<shopslot.Length;i++)
        {
            if(shopslot[i]._item.itemName==name)
            {
                return shopslot[i]._item.itemImagePath;
            }
        }
        return string.Empty;
    }
    



}
