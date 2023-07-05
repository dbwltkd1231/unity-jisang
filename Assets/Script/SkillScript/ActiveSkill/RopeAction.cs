using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class RopeAction : MonoBehaviour
{
    //레이캐스트 라인렌더러 스프링조인트
    public Transform Hand;
    RaycastHit hit;
    [Header("로프액션 쿨타임"), SerializeField]
    float cooltime;

    public LayerMask GrapplingObj;

    SpringJoint sj;
    Rigidbody rb;
    LineRenderer lr;
    Animator anim;
    bool ropeing = false;


    float time;
    bool RopeCoolOn;
    Ray ray;
    private Vector3 dir;
    float dist;
    void Start()
    {
      
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        RopeCoolOn = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (RopeCoolOn && ropeing == false)
            {
                
                PlayerManager.Instance.Sword.SetActive(false);
                anim.SetBool("Rope", true);
                RopeCoolOn = false;
            }
            else if (ropeing == true)
            {
   
                PlayerManager.Instance.Sword.SetActive(true);
                anim.SetBool("Rope", false);

                dir.Normalize();
                dir.y = 2;
                dist = Mathf.Clamp(dist, 2, 5);
                rb.velocity = dir * dist;
                ropeing = false;
                lr.positionCount = 0;
                StartCoroutine(CoolCal());

            }
        }
        if(ropeing==true)
        {
            lr.SetPosition(0, Hand.position);
        }

    }

    IEnumerator CoolCal()
    {
        yield return new WaitForSeconds(cooltime);
        RopeCoolOn = true;
    }

    
    void RopeShot()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray);
        if (Physics.Raycast(ray, out hit,100f,GrapplingObj))
        {
            Vector3 targetPosition = new Vector3(hit.transform.position.x, PlayerManager.Instance.transform.position.y, hit.transform.position.z);
            PlayerManager.Instance.transform.LookAt(targetPosition);


            lr.positionCount = 2;
            lr.SetPosition(0, Hand.position);
            lr.SetPosition(1, hit.point);
            lr.SetWidth(0.2f, 0.2f);

            //sj = this.gameObject.AddComponent<SpringJoint>();
          //  sj.autoConfigureConnectedAnchor = false;
            //sj.connectedAnchor = hit.point;


            dist = Vector3.Distance(Hand.position, hit.point);

          //  sj.maxDistance = dist;
           // sj.minDistance = dist * 0.5f;

            dir = hit.transform.position-this.transform.position;

            
            
        }
    }
    public void DrawRope()
    {
        RopeShot();
        ropeing = true;
    }
}

//effect06