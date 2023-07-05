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
            rewardtype = "∞Ê«Ëƒ°";
        }
        else if (type == RewardType.Money)
        {
            rewardtype = "∞ÒµÂ";
        }
        else
        {
            rewardtype = "???";
        }
        text.text = rewardtype + "∏¶ »πµÊ«œºÃΩ¿¥œ¥Ÿ (+" + value + " )";

    }

   
}
