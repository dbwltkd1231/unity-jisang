using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper_Attack : FSMSingleton<Reaper_Attack>, IFSMState<Reaper_FSM>
{
    float time;

    
    public void Enter(Reaper_FSM e)
    {
        
    }
    public void Execute(Reaper_FSM e)
    {
        e.reaper.nav.SetDestination(e.transform.position);

        if(e.reaper.PlayerHide!=true)
        {
            if (Vector3.Distance(e.transform.position, PlayerManager.Instance.transform.position) < e.reaper.AttackDist && e.reaper.attackEnd == true)
            {
                Vector3 dir = PlayerManager.Instance.transform.position - e.transform.position;
                e.transform.rotation = Quaternion.Lerp(e.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2f);
                time += Time.deltaTime;
                if (time > e.reaper.attackDelay)
                {
                    int value = Random.RandomRange(0, 10);
                    if (value < 6)
                    {
                        e.reaper.anim.SetTrigger("Attack1");
                    }
                    else if (value < 8)
                    {
                        e.reaper.anim.SetTrigger("Attack2");
                        time = 0;
                        e.reaper.attackEnd = false;
                    }
                }
            }
            else if (Vector3.Distance(e.transform.position, PlayerManager.Instance.transform.position) < e.reaper.AttackDist && e.reaper.attackEnd == false)
            {
                Vector3 dir = PlayerManager.Instance.transform.position - e.transform.position;
                e.transform.rotation = Quaternion.Lerp(e.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2f);
            }
            else if (e.reaper.attackEnd == true && Vector3.Distance(e.transform.position, PlayerManager.Instance.transform.position) >= e.reaper.AttackDist)
            {

                e.ChangeState(Reaper_Chase._Inst);
            }
        }
        
        else
        {
           
            e.ChangeState(Reaper_Chase._Inst);
        }

        
    }

    public void Exit(Reaper_FSM e)
    {
        time = 0;
    }


}