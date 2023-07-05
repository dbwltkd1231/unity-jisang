using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper_Idle : FSMSingleton<Reaper_Idle>, IFSMState<Reaper_FSM>
{
    
    public void Enter(Reaper_FSM e)
    {
        if (Vector3.Distance(e.transform.position, e.reaper.OriginPos) > 1f)
        {
            e.reaper.anim.SetBool("Walk", true);
          
        }
        else
        {
            e.reaper.anim.SetBool("Walk", false);
        }
    }

    public void Execute(Reaper_FSM e)
    {
        if (Vector3.Distance(e.transform.position, e.reaper.OriginPos) > 1f)
        {
            e.reaper.nav.SetDestination(e.reaper.OriginPos);

        }
        else
        {
            e.reaper.anim.SetBool("Walk", false);
        }



        if (e.CheckPlayer()==true)
        {
            e.ChangeState(Reaper_Chase._Inst);
        }
        else
        {
           
            e.reaper.time += Time.deltaTime;
            if(e.reaper.time > e.reaper.CheckTime)
            {
                e.reaper.time = 0;
                e.ChangeState(Reaper_Surround._Inst);
               
            }
        }
    }

    public void Exit(Reaper_FSM e)
    {
        e.reaper.anim.SetBool("Walk", false);
    }

 
}
