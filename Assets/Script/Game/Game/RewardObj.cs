using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardObj : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;


    public void SendMessage(RewardType type,int value)
    {
        string rewardtype;
        if (type==RewardType.Exp)
        {
            rewardtype = "����ġ";
        }
        else if (type == RewardType.Money)
        {
            rewardtype = "���";
        }
        else
        {
            rewardtype = "???";
        }
        text.text = rewardtype + "�� ȹ���ϼ̽��ϴ� (+" + value + " )";

    }

   
}
