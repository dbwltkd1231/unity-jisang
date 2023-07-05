using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_OneShot : Skill_Base
{

    [Header("CrosshairPrefab"), SerializeField]
    GameObject CrossHair;
    [Header("검기 프리팹"), SerializeField]
    GameObject SwordForcePrefab;
    [SerializeField]
    LayerMask layermask;
   


    Monster target;
    Image crosshair;
    bool FindTarget;
  
    RaycastHit hit;

    float FindTime = 0;
   
    void Start()
    {

        crosshair = CrossHair.GetComponent<Image>();

    }

    //1단계_ 과녁을 활성화
    public void OheShot_Inst()
    {
        CrossHair.SetActive(true);
        CrossHair.transform.SetParent(GameObject.Find("Canvas").transform);
        CrossHair.transform.localPosition = new Vector3(0, 0, 0);
        GameManager.TimeSlow(0.1f);
    }

    //3단계_ 일정시간이 초과되거나 적탐색에 성공했을때 공격스킬 발동
    public void OheShot()
    {
        skilling = false;
        
        GameManager.TimeReset();
        PlayerManager.Instance.anim.SetBool("Attention", false);
        PlayerManager.Instance.SkillIng = false;
        CrossHair.transform.SetParent(this.transform);
        CrossHair.SetActive(false);
        if(FindTarget==true)
        {
            
            SwordForcePrefab.transform.parent = null;
            SwordForcePrefab.transform.position = PlayerManager.Instance.Sword.transform.position;
            SwordForcePrefab.SetActive(true);
            int damage = PlayerManager.Instance.playerstats.TotalAtt() * SkillMag / 100;
            SwordForcePrefab.GetComponent<SwordCtrl>().Shoot(target.transform, damage, this.transform);
        }
    }
 
    public override void Execute()
    {
        if (ConnectedHotKey != null&&skilling==false)
        {
            ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = time / CoolTime;
            OheShot_Inst();
            skilling = true;
        }

       

    }

    //2단계_ 과녁을 마우스를통해 조절하며 적을탐색
    private void FixedUpdate()
    {

        if (skilling == true)
        {
            PlayerManager.Instance.SkillIng = true;
            PlayerManager.Instance.anim.SetBool("Attention", true);

            CrossHair.transform.position = Input.mousePosition;
            time += Time.unscaledDeltaTime;
            if(Input.GetMouseButtonDown(0))
            {
                time = 11;
            }
            if(time>10f)
            {
                time = 0;
                OheShot();
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            if (Physics.Raycast(ray, out hit, layermask))
            {
                if (hit.collider.CompareTag("Monster"))
                {
                    crosshair.fillAmount = (FindTime / 3f);
                    FindTime += Time.unscaledDeltaTime;
                    if (FindTime >3)
                    {
                        crosshair.fillAmount = 0;
                        PlayerManager.Instance.anim.SetTrigger("Skill_Shot");
                        target = hit.collider.GetComponent<Monster>();
                        FindTarget = true;
                        FindTime = 0;
                        OheShot();
                    }
                }
                else
                {
                    FindTime = 0;
                }
            }
            
        }
    }

}
