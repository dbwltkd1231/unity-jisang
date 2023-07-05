using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Quest/Reward/Exp", fileName = "ExpReward_")]
public class Reward_Exp : Reward
{
    public override void Give(Quest quest)
    {
        PlayerManager.Instance.GetExp(Quantity);
    }
}
