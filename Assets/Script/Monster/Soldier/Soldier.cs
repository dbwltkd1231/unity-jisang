using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Soldier : NormalMonster,IObserver
{


    public override void Hitted(int damage)
    {
        base.Hitted(damage);
        if (currentHp < 1)
        {
            MonsterManager.Instance.ReceiveSoldier(this.gameObject);
            int randomvalue = Random.Range(minCoin, maxCoin);
            MonsterManager.Instance.SendCoin(this.transform.position, randomvalue);
            anim.SetBool("Walk", false);
            anim.ResetTrigger("Attack");
        }

    }


    public override void Init()
    {
        base.Init();

        hpslider.transform.parent.localPosition = new Vector3(0, -0.53f, 0);
        PlayerManager.Instance.ResisterMonster(this);



    }
    public void Attack()
    {
        
        Collider[] Targetpool = Physics.OverlapSphere(transform.position, AttackDist);//플레이어가 거리에 들어와있는지 한번더 체크
        foreach (Collider targetCol in Targetpool)
        {
            if (targetCol.CompareTag("Player"))
            {
                PlayerManager.Instance.GetDamage(Power);
                //targetCol.GetComponent<PlayerManager>().GetDamage(Power);
            }
        }
           
           
    }
    
 

    public  void Update()
    {
        switch (monsterstate)
        {
            case MonsterState.Idle:
                if (Surrounding == false)
                {
                    if (Vector3.Distance(transform.position, OriginPos) >= 1f)//내가원래위치에 없으면 원래자리로 돌아온다.
                    {

                        nav.SetDestination(OriginPos);

                    }
                    else if (Vector3.Distance(transform.position, OriginPos) < 1f)
                    {
                        nav.SetDestination(transform.position);
                        anim.SetBool("Walk", false);

                        time -= Time.deltaTime;//상태전환 대기
                        if (time < 0f)
                        {
                            int randomX = Random.Range(-15, 15);
                            int randomZ = Random.Range(-15, 15);


                            randompos = new Vector3(transform.position.x + randomX, 0, transform.position.z + randomZ);//랜덤포지션 미리 설정
                            time = CheckTime;//상태전환대기시간 초기화
                            transform.LookAt(randompos);
                            anim.SetBool("Walk", true);
                            Surrounding = true;
                        }
                    }
                }
                else if (Surrounding == true)
                {

                    if (Vector3.Distance(transform.position, randompos) >= 1f)//내가원래위치에 없으면 원래자리로 돌아온다.
                    {

                        nav.SetDestination(randompos);
                    }
                    else if (Vector3.Distance(transform.position, randompos) < 1f)
                    {
                        nav.SetDestination(transform.position);
                        anim.SetBool("Walk", false);

                        time -= Time.deltaTime;//상태전환 대기
                        if (time < 0f)
                        {
                            time = CheckTime;//상태전환대기시간 초기화
                            transform.LookAt(OriginPos);
                            Surrounding = false;
                            anim.SetBool("Walk", true);
                        }
                    }
                }
                break;
            case MonsterState.Chase:
                nav.SetDestination(target.transform.position);
                break;
            case MonsterState.Attack:
                nav.SetDestination(transform.position);
                anim.SetTrigger("Attack");
                break;
            case MonsterState.Surround:

                break;
        }

    }


}
