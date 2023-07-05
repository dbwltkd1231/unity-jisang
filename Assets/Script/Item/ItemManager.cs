using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemManager : MonoBehaviour
{
    [Header("아이템데이터 저장경로"), SerializeField]
    string csv_FileName;
    [Header("상점 슬롯"), SerializeField]
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
        if(item.itemdetailtype==UiManager.ItemDetailType.Armor)//체력
        {
            tmp = item.itemMag + item.Starpos*10;
            PlayerManager.Instance.MaxHpInc(tmp);
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Weapon)//공격력
        {
            tmp = item.itemMag + item.Starpos * 3;
            PlayerManager.Instance.playerstats.itemAtt += tmp;
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Boots)//속도
        {
            tmp = item.Starpos * 10;
            PlayerManager.Instance.playerstats.itemSpd += item.itemMag;
            PlayerManager.Instance.MaxHpInc(tmp);

        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Hat)//방어력
        {
            tmp = item.itemMag + item.Starpos * 2;
            PlayerManager.Instance.playerstats.itemDef += tmp;
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Acc)//크확
        {
            tmp = item.itemMag + item.Starpos * 3;
            PlayerManager.Instance.playerstats.itemCrt += tmp;
        }

        Status.Instance.RefreshStats();
    }

    public void RemoveEffect(Item item)
    {
        int tmp = 0;
        if (item.itemdetailtype == UiManager.ItemDetailType.Armor)//체력
        {
            tmp = item.itemMag + item.Starpos * 10;
            PlayerManager.Instance.MaxHpInc(-tmp);
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Weapon)//공격력
        {
            tmp = item.itemMag + item.Starpos * 3;
            PlayerManager.Instance.playerstats.itemAtt -= tmp;
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Boots)//속도
        {
            tmp = item.Starpos * 10;
            PlayerManager.Instance.playerstats.itemSpd -= item.itemMag;
            PlayerManager.Instance.MaxHpInc(-tmp);
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Hat)//방어력
        {
            tmp = item.itemMag+item.Starpos * 2;
            PlayerManager.Instance.playerstats.itemDef -= tmp;
        }
        else if (item.itemdetailtype == UiManager.ItemDetailType.Acc)//크확
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
            if(data[i]["이름"]=="")
            {
                break;
            }
            //Item item = new Item();
            Item item = ScriptableObject.CreateInstance("Item") as Item;
            item.itemName = data[i]["이름"].ToString();
            item.Cost = (int)data[i]["가격"];
            item.itemDes = (string)data[i]["설명"];

           
            

            string itemtype = data[i]["타입"].ToString();
            item.itemtype = (UiManager.ItemType)Enum.Parse(typeof(UiManager.ItemType), itemtype);

            string itemdetailtype = data[i]["세부타입"].ToString();
            item.itemdetailtype = (UiManager.ItemDetailType)Enum.Parse(typeof(UiManager.ItemDetailType), itemdetailtype);
  
            item.itemDes = data[i]["설명"].ToString();
            item.itemMag = (int)data[i]["수치"];
            item.itemImagePath= data[i]["이미지"].ToString();
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
