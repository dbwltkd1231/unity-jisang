using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper_FSM : FSM<Reaper_FSM>
{
 
    public Reaper reaper;


    private void Start()
    {
        reaper = GetComponent<Reaper>();
        for (int i=0;i< reaper.AttackAreas1.Length; i++)
        {
            reaper.AttackAreas1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < reaper.AttackAreas2.Length; i++)
        {
            reaper.AttackAreas2[i].gameObject.SetActive(false);
        }


        InitState(this, Reaper_Idle._Inst);

    }
    private void Update()
    {
        FSMUpdate();
    }

    public bool CheckPlayer()
    {
        if(Vector3.Distance(PlayerManager.Instance.transform.position,this.transform.position)<reaper.FindDist&& reaper.PlayerHide != true)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }

    
}
