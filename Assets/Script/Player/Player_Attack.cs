using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_Attack : MonoBehaviour
{
    [Header("공격 배율"),SerializeField]
    float mag = 1;
   
    [HideInInspector]
    Animator anim;
    [SerializeField]
    ParticleSystem[] SlashEffect;
    [Header("공격 트랜스폼"), SerializeField]
    Transform SwordRayTrs;
    [Header("공격 범위"), SerializeField]
    Vector3 AttackDIst;
    [Header("최대 타격 수"), SerializeField]
    int Maxtarget = 3;
    [SerializeField]
    Player_Move pm;

 
    LayerMask MonseterLayer;

    
   
   
    public bool ComboIng = false;

    public int ComboCount = 4;//
    int damage;
 
    //UI관련
    bool isClick = false;
  
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
       
    }
    void Start()
    {
        MonseterLayer = 1 << 7;
        ComboCount = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick&&pm.IsMoving==false&&PlayerManager.Instance.SkillIng==false)
        {
            
            attack();
        }
        else
        {
            ComboCount = 4;
            anim.SetBool("ComboIng", false);
            anim.ResetTrigger("Combo4");
            anim.ResetTrigger("Combo3");
            anim.ResetTrigger("Combo2");
            anim.ResetTrigger("Combo1");
            i = 0;
        }
        //키보드 활용 공격
        if (Input.GetButton("Fire1"))
        {
          
            isClick = true;
        }
        else
        {
            isClick = false;
        }
    }
    int i = 0;

    //애니메이션 이벤트함수
    public void NormalAttack()
    {
        SlashEffect[i].Play();

        i++;
       
        damage = (int)Mathf.Round(PlayerManager.Instance.playerstats.TotalAtt() * mag);
        Collider[] hits = Physics.OverlapBox(SwordRayTrs.position, AttackDIst, Quaternion.identity,MonseterLayer);
        if(hits.Length>0)
        {
            transform.LookAt(hits[0].transform);
        }
       
        for (int i = 0; i < (hits.Length>Maxtarget ? Maxtarget:hits.Length); i++)
        {
            hits[i].transform.GetComponent<Monster>().Hitted(damage);
            CameraManager.Instance.ShakeCamera();

        }
        ComboCount--;
        if(ComboCount<1)
        {
            ComboCount = 4;
            i = 0;
        }
       
    }
   public void HitSound()
    {
        AudioManager.Instance.HitSound();
    }
    //update문에서 발동되는 함수.
    void attack()
    {
       
        if (ComboCount == 4)
        {
            anim.SetBool("ComboIng", true);
            anim.SetTrigger("Combo1");
        }
        else if (ComboCount == 3)
        {
          
            anim.SetTrigger("Combo2");
        }
        else if (ComboCount == 2)
        {
            anim.SetTrigger("Combo3");
        }
        else if (ComboCount == 1)
        {
            anim.SetTrigger("Combo4");
            anim.SetBool("ComboIng", false);
            anim.ResetTrigger("Combo3");
            anim.ResetTrigger("Combo2");
            anim.ResetTrigger("Combo1");
           

        }
        
    }

    //버튼 활용 공격
    public void attackDown()
    {

        isClick = true;

    }
    public void attackUp()
    {

        isClick = false;

    }
 
}
