using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Reward/Money", fileName = "PointReward_")]
public class Reword_Money : Reward
{
    public override void Give(Quest quest)
    {
        PlayerManager.Instance.GetCoin(Quantity);
    }
}
