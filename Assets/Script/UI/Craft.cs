using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Craft : MonoBehaviour
{

    [SerializeField]
    Image itemImg;
    [SerializeField]
    Image basicSprite;
    [SerializeField]
    TextMeshProUGUI itemPos;
    [SerializeField]
    TextMeshProUGUI chanceText;
    [SerializeField]
    ParticleSystem[] BasicParticles;
    [SerializeField]
    ParticleSystem[] SuccessParticles;
    int chance;
    Item _item;
    public int CurIndex;

    public void init()
    {
        setting();
    }
    
    public void Next()
    {
        if(Inventory.Instance.InventoryItemList.Count!=0)
        {
            CurIndex++;
            if (CurIndex >= Inventory.Instance.InventoryItemList.Count)
            {
                CurIndex = 0;

            }

            setting();
        }
        else
        {
            itemImg.GetComponent<Image>().sprite = basicSprite.GetComponent<Image>().sprite;
        }


    }
    public void prev()
    {
        if (Inventory.Instance.InventoryItemList.Count != 0)
        {
            CurIndex--;
            if (CurIndex < 0)
            {

                CurIndex = Inventory.Instance.InventoryItemList.Count - 1;
            }


            setting();
        }
        else
        {
            itemImg.GetComponent<Image>().sprite = basicSprite.GetComponent<Image>().sprite;
        }
          


    }
    void setting()
    {
        _item = Inventory.Instance.Inven_slots[CurIndex]._item;
        itemImg.GetComponent<Image>().sprite = _item.itemImage;

        itemPos.text = _item.Starpos + "성 -> " + (_item.Starpos + 1).ToString() + "성";
        chance = 100 - _item.Starpos * 10;
        chanceText.text = "성공확률 : " + chance + "%";
    }
    public void Starpos()
    {
        StartCoroutine(starpos());
    }
    IEnumerator starpos()
    {
        ItemManager.itemmanager.RemoveEffect(Inventory.Instance.Inven_slots[CurIndex]._item);

        int randomvalue = Random.Range(0, 100);
        for(int i=0;i< BasicParticles.Length;i++)
        {
            BasicParticles[i].Play();
        }
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < BasicParticles.Length; i++)
        {
            BasicParticles[i].Stop();
        }
        if (Inventory.Instance.Inven_slots[CurIndex]._item.isEquip==true)
        {
            
        }
        if(randomvalue< chance)
        {
            for (int i = 0; i < SuccessParticles.Length; i++)
            {
                SuccessParticles[i].Play();
            }
            _item.Starpos++;
        }
        else
        {
            _item.Starpos--;
            if(_item.Starpos<0)
            {
                _item.Starpos = 0;
            }
        }
        
        Inventory.Instance.Inven_slots[CurIndex]._item.Starpos= _item.Starpos;
        ItemManager.itemmanager.SetEfect(Inventory.Instance.Inven_slots[CurIndex]._item);
       
        setting();

        yield return null;
    }
}
//