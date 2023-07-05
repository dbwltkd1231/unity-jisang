using UnityEngine;
using System;
[CreateAssetMenu]
[Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public bool isEquip;
    public Sprite itemImage;
    public int Cost;
    public string itemDes;
    public int itemMag;
    public string itemImagePath;
    public int Starpos;
    public UiManager.ItemType itemtype;
    public UiManager.ItemDetailType itemdetailtype;

    

}
