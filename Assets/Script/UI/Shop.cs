using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject recheck;
    [SerializeField]
    GameObject failObj;
    public Item CurrentCheckedItem;
    public void RecheckStart(ShopSlot slot)
    {
        CurrentCheckedItem = slot.GetComponent<ShopSlot>()._item;
        recheck.SetActive(true);
    }
    public void RecheckEnd()
    {
        CurrentCheckedItem = null;
        recheck.SetActive(false);
    }
    //아이템 구매
    public void Purchase()
    {

        if(PlayerManager.Instance.playerstats.Coin>= CurrentCheckedItem.Cost)
        {
            AudioManager.Instance.BuySound();//구매성공 사운드 재생
            PlayerManager.Instance.UseCoin(CurrentCheckedItem.Cost);//플레이어 소지 골드 차감
            var newitem = Instantiate(CurrentCheckedItem);
     
            Inventory.Instance.AddItem(newitem);
     
            RecheckEnd();//구매 재확인 알림판 비활성화
        }
        else
        {
            StartCoroutine(notifyFail());//골드 부족 경고알림판 2초동안 활성화
        }
        
    }

    IEnumerator notifyFail()
    {
        AudioManager.Instance.DenySound();
        failObj.SetActive(true);
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(1f));
        failObj.SetActive(false);
        yield return null;
    }
}
