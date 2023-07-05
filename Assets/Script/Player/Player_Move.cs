using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{

  
    [Header("������"), SerializeField]
    float JumpPower = 5f;
    [Header("ȸ���ӵ�"), SerializeField]
    public float rotateSpeed = 90f;
    [SerializeField]//lowclimb������ Ʈ������
    Transform LegTr;
    [SerializeField]
    FollowCamera camera;

    public bool IsMoving;

    float coyotetime = 0.1f;
    bool jumpcount = false;//���� ���Է�
    bool isEvade = false;
    Rigidbody rb;
    bool isground;
    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
        isground = true;
    }
    private void FixedUpdate()
    {
        if(PlayerManager.Instance.SkillIng==false)
        {
            move();
        }
        
       
    }

    void move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if(isGround()==true)
            {
                AudioManager.Instance.LandingSound();
            }
            this.transform.position += transform.forward * PlayerManager.Instance.playerstats.TotalSpd() * Time.unscaledDeltaTime;

            Vector3 tmp = camera.transform.forward;
            tmp.y = 0;
            transform.forward = tmp;
            PlayerManager.Instance.anim.SetBool("Move", true);
            LowClimb();//������� ������
            IsMoving = true;
        }
        
        else if (Input.GetKey(KeyCode.S))
        {
            if (isGround() == true)
            {
                AudioManager.Instance.LandingSound();
            }
            Vector3 tmp = camera.transform.forward;
            tmp.y = 0;
            transform.forward = tmp;
            this.transform.position -=transform.forward * PlayerManager.Instance.playerstats.TotalSpd()/2 * Time.unscaledDeltaTime;
            PlayerManager.Instance.anim.SetBool("Move", true);
            //PlayerManager.Instance.anim.SetBool("BackMove", true);
            IsMoving = true;

        }
        else
        {
            PlayerManager.Instance.anim.SetBool("Move", false);
            IsMoving = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            IsMoving = true;
            transform.position -= transform.right * Time.unscaledDeltaTime * PlayerManager.Instance.playerstats.TotalSpd() / 2;
           
            PlayerManager.Instance.anim.SetBool("MoveRight", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            IsMoving = true;
            transform.position += transform.right * Time.unscaledDeltaTime * PlayerManager.Instance.playerstats.TotalSpd() / 2;
            
            PlayerManager.Instance.anim.SetBool("MoveRight", true);

        }
        else
        {
            IsMoving = false;
            PlayerManager.Instance.anim.SetBool("MoveRight", false);
        }
      


        if (Input.GetKey(KeyCode.Space) && isGround() == true)
        {
            rb.velocity = Vector3.up * JumpPower;
            AudioManager.Instance.JumpSound();
            isground = false;


        }
        else if(isground==false&& isGround()==true)
         {
            PlayerManager.Instance.anim.SetTrigger("JumpDown");
            isground = true;
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift) && isGround() == true&& isEvade==false)
        {
            rb.velocity = transform.forward * 12f;
            isEvade = true;
            PlayerManager.Instance.SkillIng = true;
            PlayerManager.Instance.anim.SetTrigger("Evade");
        }

        if (isGround() == false)
        {
           
           
            coyotetime -= Time.deltaTime;
            if (coyotetime > 0f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = Vector3.up * JumpPower;

                }
            }
        }
        else
        {
            coyotetime = 0.1f;
        }

   
        if (jumpcount == true && isGround()==true)
        {
            rb.velocity = Vector3.up * 3f;
            jumpcount = false;
        }
        
       
        

        
    }

    float time = 0;
    void LowClimb()
    {
        Ray ray = new Ray(LegTr.position, transform.forward);
        Debug.DrawRay(LegTr.position, transform.forward/2, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            if(hit.collider!=null)//�ε����� ��ֹ��������� ��¦�÷��ش�.
            {
                time = Time.deltaTime;
                transform.position += new Vector3(0, time > 0.5f ? 0.5f : time, 0);
            }
            
        }
        else
        {
            time = 0;
        }
    }

    bool isGround()
    {
        Ray ray = new Ray(LegTr.position, -transform.up);
        Debug.DrawRay(LegTr.position, -transform.up/2, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f))
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    public void JumpDownEnd()
    {

        AudioManager.Instance.LandingSound();
    }
    public void EvadeOff()
    {
        rb.velocity = Vector3.zero;
        PlayerManager.Instance.SkillIng = false;
        isEvade = false;
    }

   
}

//�������϶��̵� �Ұ��� + �̵����ϋ� ���ݰ���

//roof corner A 10   ceiling 2X682 ceiling 2X681 ceiling 2X683 ceiling 2X680
//roof corner A 4 ceiling 2X480 ~ceiling 2X483     ceiling 2X480  ceiling 2X483 ceiling 2X486 ceiling 2X485