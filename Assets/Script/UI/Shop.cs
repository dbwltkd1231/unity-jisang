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
    //������ ����
    public void Purchase()
    {

        if(PlayerManager.Instance.playerstats.Coin>= CurrentCheckedItem.Cost)
        {
            AudioManager.Instance.BuySound();//���ż��� ���� ���
            PlayerManager.Instance.UseCoin(CurrentCheckedItem.Cost);//�÷��̾� ���� ��� ����
            var newitem = Instantiate(CurrentCheckedItem);
     
            Inventory.Instance.AddItem(newitem);
     
            RecheckEnd();//���� ��Ȯ�� �˸��� ��Ȱ��ȭ
        }
        else
        {
            StartCoroutine(notifyFail());//��� ���� ���˸��� 2�ʵ��� Ȱ��ȭ
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
