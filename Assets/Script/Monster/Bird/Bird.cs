using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : NormalMonster, IObserver
{
    [SerializeField]
    GameObject Core;
    [SerializeField]
    GameObject parent;


    public UnityEngine.Events.UnityEvent Eagle;
    bool hit;


    public override void Hitted(int damage)
    {
        base.Hitted(damage);
        if (currentHp < 1)
        {
            currentHp = maxHp;
            Eagle.Invoke();
            anim.ResetTrigger("Attack");
            anim.SetTrigger("isDead");
            StartCoroutine(resion());
        }
        

    }
    IEnumerator resion()
    {
     
        yield return new WaitForSeconds(2f);
        parent.gameObject.SetActive(false);

    }
    public  void Start()
    {
        anim = GetComponent<Animator>();
        OriginPos = this.transform.position;
        Init();
        hit = false;
        GameObject.Find("Player").GetComponent<PlayerManager>().ResisterMonster(this);
    }
    public void Update()
    {
        switch (monsterstate)
        {
            case MonsterState.Idle:
                transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                parent.transform.RotateAround(Core.transform.position, Vector3.down / 4f, 22f * Time.deltaTime);

                break;
            case MonsterState.Chase:
                transform.LookAt(target);
                Vector3 dir = target.transform.position - this.transform.position;
                dir.Normalize();
                parent.transform.position += dir * Speed * Time.deltaTime;
                break;
            case MonsterState.Attack:
                transform.LookAt(target);
                anim.SetTrigger("Attack");

                break;

        }
    }

    public void BirdAttack()
    {
        Collider[] Targetpool = Physics.OverlapSphere(transform.position, AttackDist);//플레이어가 거리에 들어와있는지 한번더 체크
        foreach (Collider targetCol in Targetpool)
        {
            if (targetCol.CompareTag("Player"))
            {
                targetCol.GetComponent<PlayerManager>().GetDamage(Power);
            }
        }
    }
    public void Leave()
    {
        hit = true;
    }


}
