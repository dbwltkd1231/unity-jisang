using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_Attack : MonoBehaviour
{
    [Header("���� ����"),SerializeField]
    float mag = 1;
   
    [HideInInspector]
    Animator anim;
    [SerializeField]
    ParticleSystem[] SlashEffect;
    [Header("���� Ʈ������"), SerializeField]
    Transform SwordRayTrs;
    [Header("���� ����"), SerializeField]
    Vector3 AttackDIst;
    [Header("�ִ� Ÿ�� ��"), SerializeField]
    int Maxtarget = 3;
    [SerializeField]
    Player_Move pm;

 
    LayerMask MonseterLayer;

    
   
   
    public bool ComboIng = false;

    public int ComboCount = 4;//
    int damage;
 
    //UI����
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
        //Ű���� Ȱ�� ����
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

    //�ִϸ��̼� �̺�Ʈ�Լ�
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
    //update������ �ߵ��Ǵ� �Լ�.
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

    //��ư Ȱ�� ����
    public void attackDown()
    {

        isClick = true;

    }
    public void attackUp()
    {

        isClick = false;

    }
 
}
