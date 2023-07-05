using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
  
    public List<Save_Skilldata> Hotkeys = new List<Save_Skilldata>();
    public List<SaveItem> Inven_Item2 = new List<SaveItem>();


    public int playerCurrentExp;
    public int playerLv;
    public int coin;

}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public bool Load;

 
   
    Dictionary<string, object> PlayerData;
    private DatabaseReference reference;

   
    SaveData loadData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }

        }
     
        reference = FirebaseDatabase.DefaultInstance.RootReference;
       
    }

    public void Init(bool load)
    {
        StartCoroutine(init(load));
    }
    IEnumerator init(bool load)
    {
  
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Game");
        if (load==true)
        {
            JsonLoad();
        }
        Status.Instance.RefreshStats();
        PlayerManager.Instance.Init();
        yield return null;
    }
    public void LogIn()
    {
        loadData = new SaveData();

        FirebaseDatabase.DefaultInstance
       .GetReference("Player").Child("player")
       .GetValueAsync().ContinueWithOnMainThread(task =>
       {
           if (task.IsFaulted)
           {
               // Handle the error...
           }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;


               foreach (var data in snapshot.Children)
               {
                   if (data.Key == "coin")
                   {

                       loadData.coin = int.Parse(data.Value.ToString());
                   }
                   else if (data.Key == "playerLv")
                   {

                       loadData.playerLv = int.Parse(data.Value.ToString());
                   }
                   else if (data.Key == "playerCurrentExp")
                   {

                       loadData.playerCurrentExp = int.Parse(data.Value.ToString());

                   }
               }
            
               for (int i = 0; i < snapshot.Child("Hotkeys").ChildrenCount; i++)
               {
                   Save_Skilldata tmp = new Save_Skilldata();
                   foreach (var data in snapshot.Child("Hotkeys").Child(i.ToString()).Children)
                   {

                       if (data.Key == "Hotkey_ID")
                       {
                           // Debug.Log(loadData.Hotkeys[i].Hotkey_ID);
                           tmp.Hotkey_ID = int.Parse(data.Value.ToString());
                       }
                       else if (data.Key == "SkillTree_ID")
                       {
                           tmp.SkillTree_ID = int.Parse(data.Value.ToString());
                       }
                       else if (data.Key == "Skill_ID")
                       {
                           tmp.Skill_ID = int.Parse(data.Value.ToString());
                       }
                   }
                   loadData.Hotkeys.Add(tmp);
               }

               for (int i = 0; i < snapshot.Child("Inven_Item2").ChildrenCount; i++)
               {
                   SaveItem tmp = new SaveItem();
                   foreach (var data in snapshot.Child("Inven_Item2").Child(i.ToString()).Children)
                   {
                       if (data.Key == "Cost")
                       {
                           tmp.Cost = int.Parse(data.Value.ToString());
                       }
                       else if (data.Key == "imagePath")
                       {
                           tmp.imagePath = data.Value.ToString();
                       }
                       else if (data.Key == "isEquip")
                       {
                           tmp.isEquip =Convert.ToBoolean(data.Value.ToString());
                       }
                       else if (data.Key == "itemDes")
                       {
                           tmp.itemDes = data.Value.ToString();
                       }
                       else if (data.Key == "itemMag")
                       {
                           tmp.itemMag = int.Parse(data.Value.ToString());
                       }
                       else if (data.Key == "itemName")
                       {
                           tmp.itemName = data.Value.ToString();
                       }
                       else if (data.Key == "itemdetailtype")
                       {
                           tmp.itemdetailtype = (UiManager.ItemDetailType)(int.Parse(data.Value.ToString()));
                       }
                       else if (data.Key == "itemtype")
                       {
                           tmp.itemtype = (UiManager.ItemType)(int.Parse(data.Value.ToString()));
                       }
                       else if (data.Key == "Starpos")
                       {
                           tmp.Starpos = int.Parse(data.Value.ToString());
                       }

                   }
                   loadData.Inven_Item2.Add(tmp);
               }
           }
       });
     
    }


    public void JsonLoad()
    {

        PlayerManager.Instance.playerstats.Lv = loadData.playerLv;
        PlayerManager.Instance.playerstats.currentExp = loadData.playerCurrentExp;
        PlayerManager.Instance.playerstats.Coin = loadData.coin;

        for (int i = 0; i < loadData.Hotkeys.Count; i++)
        {
            if (loadData.Hotkeys[i].Skill_ID != 0)
            {
                SkillManager.Instance.HotkeyList[loadData.Hotkeys[i].Hotkey_ID].SetKey(SkillManager.Instance.FindSkill(loadData.Hotkeys[i].Skill_ID), SkillManager.Instance.FIndSkillSlot(loadData.Hotkeys[i].SkillTree_ID));
            }
        }
        for (int i = 0; i < loadData.Inven_Item2.Count; i++)
        {

            Item newitem = ScriptableObject.CreateInstance("Item") as Item;

            newitem.itemName = loadData.Inven_Item2[i].itemName;
            newitem.Cost = loadData.Inven_Item2[i].Cost;
            newitem.isEquip = loadData.Inven_Item2[i].isEquip;

            newitem.itemImage = Resources.Load<Sprite>(loadData.Inven_Item2[i].imagePath);



            newitem.itemDes = loadData.Inven_Item2[i].itemDes;
            newitem.itemMag = loadData.Inven_Item2[i].itemMag;
            newitem.itemtype = loadData.Inven_Item2[i].itemtype;
            newitem.itemdetailtype = loadData.Inven_Item2[i].itemdetailtype;
            newitem.Starpos = loadData.Inven_Item2[i].Starpos;
            Inventory.Instance.AddItem(newitem);
          
            if (newitem.isEquip == true)
            {
                Equip.Instance.EquipItem(newitem, Inventory.Instance.Inven_slots[i]);
            }

        }
        
    }

    public void JsonSave()
    {
   
        QuestSystem.Instance.Save();
        SaveData saveData = new SaveData();
    
        for (int i = 0; i < Inventory.Instance.InventoryItemList.Count; i++)
        {
            SaveItem newitem = new SaveItem();
           
           
            newitem.itemName = Inventory.Instance.InventoryItemList[i].itemName;
            newitem.Cost = Inventory.Instance.InventoryItemList[i].Cost;
            newitem.isEquip = Inventory.Instance.InventoryItemList[i].isEquip;
            newitem.imagePath = ItemManager.itemmanager.FindItem(newitem.itemName);
            newitem.itemDes = Inventory.Instance.InventoryItemList[i].itemDes;
            newitem.itemMag = Inventory.Instance.InventoryItemList[i].itemMag;
            newitem.itemtype = Inventory.Instance.InventoryItemList[i].itemtype;
            newitem.itemdetailtype = Inventory.Instance.InventoryItemList[i].itemdetailtype;
            newitem.Starpos = Inventory.Instance.InventoryItemList[i].Starpos;
            if(newitem.imagePath==null)
            {
                Debug.Log("경로저장실패");
            }
            saveData.Inven_Item2.Add(newitem);
          
        }
        for(int i=0;i<SkillManager.Instance.HotkeyList.Count;i++)
        {
            Save_Skilldata newData = new Save_Skilldata();
            newData.Hotkey_ID = SkillManager.Instance.HotkeyList[i].ID;
            if (SkillManager.Instance.HotkeyList[i].Skill != null)
            {
                newData.SkillTree_ID = SkillManager.Instance.HotkeyList[i].ConnectedSkillSlot.ID;
                newData.Skill_ID = SkillManager.Instance.HotkeyList[i].Skill.ID;
            }
            saveData.Hotkeys.Add(newData);

        }
      
        saveData.playerLv = PlayerManager.Instance.playerstats.Lv;
        saveData.playerCurrentExp = PlayerManager.Instance.playerstats.currentExp;
        saveData.coin = PlayerManager.Instance.playerstats.Coin;

        

        string json = JsonUtility.ToJson(saveData, true);
       
        reference.Child("Player").Child("player").SetRawJsonValueAsync(json);
    
        StartCoroutine(CheckSave());
    }
    IEnumerator CheckSave()
    {
        UiManager.uimanager.saveCheck.SetActive(true);
        yield return new WaitForSeconds(2f);
        UiManager.uimanager.saveCheck.SetActive(false);
        yield return null;
             
    }
}
[Serializable]
public class Save_Skilldata
{
    public int Skill_ID;
    public int SkillTree_ID;
    public int Hotkey_ID;

}
[Serializable]
public class SaveItem
{
    public string itemName;
    public bool isEquip;
    public int Starpos;
    public string imagePath;

    public int Cost;
    public string itemDes;
    public int itemMag;

    public UiManager.ItemType itemtype;
    public UiManager.ItemDetailType itemdetailtype;
}
