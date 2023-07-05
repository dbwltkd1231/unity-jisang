using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : Monster
{
   

    public MonsterState monsterstate;
    public override void Init()
     {
        currentHp = maxHp;
        base.Init();
        time = CheckTime;
        monsterstate = MonsterState.Idle;
        StartCoroutine(FindTarget(FindDist));
        hpslider.value = currentHp;


    }

    IEnumerator FindTarget(float FindDist)
    {
        while (true)
        {
            Collider[] Targetpool = Physics.OverlapSphere(transform.position, FindDist, 1 << 10);
            if (Targetpool.Length > 0 && PlayerHide != true)
            {
                target = Targetpool[0].GetComponent<PlayerManager>().transform;

                if (Vector3.Distance(target.transform.position, this.transform.position) < AttackDist)
                {
                    transform.LookAt(target);
                    Surrounding = false;
                    anim.SetBool("Walk", false);
                    monsterstate = MonsterState.Attack;
                }
                else if (Vector3.Distance(target.transform.position, this.transform.position) < FindDist)
                {
                    Surrounding = false;
                    monsterstate = MonsterState.Chase;
                    anim.ResetTrigger("Attack");
                    anim.SetBool("Walk", true);

                }
            }
            else
            {
                anim.ResetTrigger("Attack");
                monsterstate = MonsterState.Idle;
            }


            yield return new WaitForSeconds(1f);

        }
        yield return null;
    }


}
