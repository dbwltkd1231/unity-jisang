using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MssiveSlashCtrl : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] myobjs;
    [SerializeField]
    Transform[] parents;

    double damage;
    void Start()
    {
        myobjs = GetComponentsInChildren<ParticleSystem>();
    
    
    }

   
    public void SkillOn(int skillDamage)
    {
        damage = skillDamage/(double)100*PlayerManager.Instance.playerstats.TotalAtt();
        int i = 0;
        for (; i < 6; i++)
        {
            myobjs[i].startDelay = 0.2f;

           // parents[0].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));
           
            myobjs[i].Play();
           
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage,0.2f));
        for (; i < 12; i++)
        {
            myobjs[i].startDelay = 0.5f;

            //parents[1].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));
          
            myobjs[i].Play();
           
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 0.5f));
        for (; i < 18; i++)
        {
            myobjs[i].startDelay = 0.8f;

            //parents[2].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));
           
            myobjs[i].Play();
            
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 0.8f));
        for (; i < 24; i++)
        {
            myobjs[i].startDelay = 1.2f;

           // parents[3].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));

            myobjs[i].Play();
           
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 1.2f));
        for (; i < 30; i++)
        {
            myobjs[i].startDelay = 1.5f;

           // parents[4].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));
            myobjs[i].Play();
            

        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 1.5f));
        for (; i < 36; i++)
        {
            myobjs[i].startDelay = 1.8f;

           // parents[5].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));

            myobjs[i].Play();
           
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 1.8f));
        for (; i < 42; i++)
        {
            myobjs[i].startDelay = 3.2f;

           /// parents[6].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));

            myobjs[i].Play();
           
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 3.2f));
        for (; i < 48; i++)
        {
            myobjs[i].startDelay = 3.5f;

           // parents[7].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));

            myobjs[i].Play();
           
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 3.5f));
        for (; i < 54; i++)
        {
            myobjs[i].startDelay = 3.8f;

            //parents[8].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));

            myobjs[i].Play();
            
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 3.8f));
        //
        for (; i < 60; i++)
        {
            myobjs[i].startDelay = 5.5f;
           // parents[9].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));

            myobjs[i].Play();
           
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 5.5f));
        for (; i < 66; i++)
        {
            myobjs[i].startDelay = 5.9f;

           // parents[10].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));

            myobjs[i].Play();
          
        }
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 5.9f));
        for (; i < 72; i++)
        {
            myobjs[i].startDelay = 6.3f;
            //parents[11].gameObject.transform.localRotation = Quaternion.Euler(new Vector3(Random.RandomRange(-15, 15), 0, Random.RandomRange(-90, 90)));

            myobjs[i].Play();
            
        }
        
        StartCoroutine(SkillManager.Instance.SkillAttack((int)damage, 6.3f));

    }


   
}
