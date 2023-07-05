using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper_Chase : FSMSingleton<Reaper_Chase>, IFSMState<Reaper_FSM>
{
    
    public void Enter(Reaper_FSM e)
    {
        e.reaper.anim.SetBool("Walk", true);
    }

    public void Execute(Reaper_FSM e)
    {

        if (e.CheckPlayer() == true)
        {
            e.reaper.nav.SetDestination(PlayerManager.Instance.transform.position);
            if(Vector3.Distance(e.transform.position, PlayerManager.Instance.transform.position)<e.reaper.AttackDist)
            {
                e.ChangeState(Reaper_Attack._Inst);
            }

        }
        else
        {
            e.ChangeState(Reaper_Idle._Inst);
        }
    }

    public void Exit(Reaper_FSM e)
    {
        e.reaper.anim.SetBool("Walk", false);
    }
}
