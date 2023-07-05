using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SixShot : Skill_Base
{
   
    [SerializeField]
    GameObject SreenYellow;//암전오브젝트
   
    [SerializeField]
    GameObject GroundEffect;
    [SerializeField]
    GameObject SlashEffect;
    
   
    double damage;
    public override void Start()
    {

        SreenYellow.SetActive(false);

    }

    public override void Execute()
    {
        StartCoroutine(SixShot());
    }

    public override void SkillBttnOn()
    {
        StartCoroutine(SixShot());
    }
    void SixShotExit()
    {
        GameManager.TimeReset();
        PlayerManager.Instance.anim.SetBool("Attention", false);
       
        SreenYellow.SetActive(false);
        PlayerManager.Instance.SkillIng = false;
   
    }
    IEnumerator SixShot()
    {
        PlayerManager.Instance.SkillIng = true;
        GroundEffect.SetActive(true);
        GroundEffect.transform.position = PlayerManager.Instance.transform.position;
        SlashEffect.transform.position = PlayerManager.Instance.transform.position;
        damage = PlayerManager.Instance.playerstats.TotalAtt() * (double)SkillMag/100;


        PlayerManager.Instance.anim.SetBool("Attention", true);
        ParticleSystem[] GroundPrefabs = GroundEffect.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < GroundPrefabs.Length; i++)
        {
            GroundPrefabs[i].Play();
        }
        GameManager.TimeSlow(0.1f);
        SreenYellow.SetActive(true);


        Collider[] Targets = Physics.OverlapSphere(PlayerManager.Instance.transform.position, 10f, 1 << 7);
        GameObject[] targets = new GameObject[Targets.Length];

        if (targets.Length < 1)
        {
            SixShotExit();
            GroundEffect.SetActive(false);
            yield break;
        }

        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(1f));

        for (int i = 0; i < (Targets.Length <= 6 ? Targets.Length : 6); i++)
        {
            targets[i] = Targets[i].gameObject;

        }

        //PlayerManager.Instance.transform.position = originpos;
        SlashEffect.SetActive(true);

        
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(1.2f));
        SlashEffect.GetComponent<SlashDanceCtrl>().SKillOn();
        

        SixShotExit();
        PlayerManager.Instance.anim.SetTrigger("Skill_Shot");

        for (int count = 0; count < 6; count++)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].gameObject)
                {
                    targets[i].gameObject.GetComponent<Monster>().Hitted((int)damage);
                }
                
            }
            yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(0.4f));
        }
        SlashEffect.SetActive(false);
        GroundEffect.SetActive(false);
        yield return null;
    }
}
