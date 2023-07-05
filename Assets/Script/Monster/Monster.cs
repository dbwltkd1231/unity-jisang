using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public enum MonsterState
{
    Idle,
    Chase,
    Attack,
    Surround,
    Dead,
    None
}
public abstract class Monster :MonoBehaviour
{
    [Header("획득 경험치"),SerializeField]
    public int monsterExp;
    [Header("부활대기시간"), SerializeField]
    public int rezenTIme;
    [SerializeField]
    public Slider hpslider;
    [SerializeField]
    public int maxHp;
    [Header("공격력"), SerializeField]
    public int Power;
   
    [Header("이동속도"),SerializeField]
    public float Speed;
    [Header("떨어뜨리는 코인 최소치"), SerializeField]
    public int minCoin;
    [Header("떨어뜨리는 코인 최대치"), SerializeField]
    public int maxCoin;
    [Header("탐색범위"), SerializeField]
    public float FindDist;
    [Header("공격범위"), SerializeField]
    public float AttackDist;
    [Header("순찰간격"), SerializeField]
    public float CheckTime = 20f;

    public float time;
    public NavMeshAgent nav;
    public Animator anim;
    public int currentHp;


    public Vector3 randompos;
    
    public bool Surrounding;
    
    public bool Dead;
    
    
    public Transform target;
   
    public Vector3 OriginPos;
    public UnityEngine.Events.UnityEvent onDead;
    public virtual void Hitted(int damage)
    {
        int randomvalue = Random.Range(0, 100);
        int damageValue = damage;
        if(randomvalue<PlayerManager.Instance.playerstats.TotalCrt())
        {
            damageValue = damageValue * 2;
            BattleManger.Instance.SendCrtDamageText(damageValue, this.transform);
        }
        else
        {
            BattleManger.Instance.SendDamageText(damageValue, this.transform);
        }
       

        currentHp = currentHp - damageValue;
        if (currentHp < 1)
        {
            PlayerManager.Instance.GetExp(monsterExp);
            onDead.Invoke();
        }
        else
        {
            BattleManger.Instance.SendHitEffect(this.transform);
            hpslider.value = currentHp;
        }
    }
    public virtual void Init()
    {
        if (hpslider == null)
        {
            hpslider = GetComponentInChildren<Slider>();
        }

        if (nav == null)
        {
            nav = GetComponent<NavMeshAgent>();
            nav.speed = Speed;
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        currentHp = maxHp;
        hpslider.maxValue = maxHp;
        hpslider.value = currentHp;
        Dead = false;
        OriginPos = transform.position;//시작위치 저장
        Surrounding = false;

    }
    

 

    public bool PlayerHide;
    public void UpdateData(bool HideOn)
    {
        PlayerHide = HideOn;

    }
    public void UpdateData(int tmp)
    {
    }
   
  
}




