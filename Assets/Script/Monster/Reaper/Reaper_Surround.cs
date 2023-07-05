using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper_Surround : FSMSingleton<Reaper_Surround>, IFSMState<Reaper_FSM>
{
    Vector3 randompos;
   
    public void Enter(Reaper_FSM e)
    {
        e.reaper.anim.SetBool("Walk", true);

        int randomX = Random.Range(-10, 10);
        int randomZ = Random.Range(-10, 10);


        randompos = new Vector3(e.reaper.OriginPos.x + randomX, 0, e.reaper.OriginPos.z + randomZ);//·£´ýÆ÷Áö¼Ç 
    }

    public void Execute(Reaper_FSM e)
    {

        if (e.CheckPlayer() == true)
        {
            e.ChangeState(Reaper_Chase._Inst);
        }
        else
        {
            e.reaper.nav.SetDestination(randompos);
      
            if (Vector3.Distance(e.transform.position,randompos)<1f)
            {
                e.reaper.anim.SetBool("Walk", false);
                e.reaper.time += Time.deltaTime;
                if (e.reaper.time > e.reaper.CheckTime)
                {
                    e.reaper.time = 0;
                    e.ChangeState(Reaper_Idle._Inst);
                }
                
            }
        }
    }

    public void Exit(Reaper_FSM e)
    {
        e.reaper.anim.SetBool("Walk", false);
    }
}
