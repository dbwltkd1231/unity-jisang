using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : EliteMonster,IObserver
{

    [Header("스킬1 Prefab"), SerializeField]
    public GameObject Elite_Skill_Prefab;
    [Header("스킬2 Particle"), SerializeField]
    public ParticleSystem[] Skill2Particles;
    [Header("공격1 영역"), SerializeField]
    public Reaper_AttackArea[] AttackAreas1;
    [Header("공격2 영역"), SerializeField]
    public Reaper_AttackArea[] AttackAreas2;

   

    public override void Init()
    {
        base.Init(); 
        attackEnd = true;
    }
   
    public  void Start()
    {
        PlayerManager.Instance.ResisterMonster(this); 
        
    }
    public override void Hitted(int damage)
    {
        base.Hitted(damage);
        if(currentHp<1)
        {
            AttackAreaSetActive(AttackAreas1, false);
            AttackAreaSetActive(AttackAreas2, false);
            MonsterManager.Instance.ReceiveReaper(this.gameObject);
            
        }
    }
    public void Attack1_Start()
    {
        AttackAreas1[0].transform.localPosition = new Vector3(-0.3f, 0,5.5f);
        AttackAreaSetActive(AttackAreas1, true);
    }
    public void Attack1_End()
    {
        GameObject skillprefab = Instantiate(Elite_Skill_Prefab, AttackAreas1[0].transform.position, Quaternion.identity);
        Destroy(skillprefab, 1f);
        if (AttackAreas1[0].GetComponent<Reaper_AttackArea>().AttackSuccess == true)
        {
            PlayerManager.Instance.GetDamage(Power);
        }
        AttackAreaSetActive(AttackAreas1,false);

      
    }
    public void Attack2_Start()
    {

        AttackAreas2[0].transform.localPosition = new Vector3(-0.3f, 0, 6.1f);
        AttackAreas2[1].transform.localPosition = new Vector3(-0.3f, 0, 11.6f);
        AttackAreas2[2].transform.localPosition = new Vector3(3.8f, 0, 10.1f);
        AttackAreas2[3].transform.localPosition = new Vector3(-5.4f, 0, 9f);

        AttackAreaSetActive(AttackAreas2, true);

        for (int i=0;i< Skill2Particles.Length;i++)
        {
            Skill2Particles[i].gameObject.SetActive(true);
        }
      
    }
  
    public void Attack2_Mid()
    {
      
        for (int i = 0; i < Skill2Particles.Length; i++)
        {
            Skill2Particles[i].Play();
        }
        for (int i = 0; i < 4; i++)
        {
            if (AttackAreas2[i].GetComponent<Reaper_AttackArea>().AttackSuccess == true)
            {
                PlayerManager.Instance.GetDamage(Power/2);
            }
        }
    }
    public void Attack2_End()
    {
  
        for (int i = 0; i < Skill2Particles.Length; i++)
        {
            Skill2Particles[i].gameObject.SetActive(false);
        }

        AttackAreaSetActive(AttackAreas2, false);

    }

    void AttackAreaSetActive(Reaper_AttackArea[] attackArea, bool TF)
    {
        for (int i = 0; i < attackArea.Length; i++)
        {
            attackArea[i].transform.gameObject.SetActive(TF);
            if (TF == true)
            {
                attackArea[i].transform.SetParent(null);
              
            }
            else
            {
                attackArea[i].transform.SetParent(this.transform);
                attackEnd = true;
            }
        }
    }



}

//5초마다 적탐색 -> 없으면 가만히,있으면 해당 위치로 쫒아감->공격가능?공격 주변에있음? 다시쫒아감 주변에없음?원래자리로 돌아감