using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteMonster : Monster
{
    [Header("공격 딜레이"),SerializeField]
    public float attackDelay;
    [Header("공격이 끝났는지 확인"), SerializeField]
    public bool attackEnd;


}
