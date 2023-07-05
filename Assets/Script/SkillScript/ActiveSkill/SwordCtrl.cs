using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCtrl : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] effects;
    public int Skilldamage;
    Transform Parent;
    bool Active;
    Transform Target;
    float lifetime;
  

    // Update is called once per frame
    void Update()
    {
        if(Active==true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, 0.5f);
            lifetime += Time.deltaTime;
            if(lifetime>8f)
            {
                SkillExit();
                lifetime = 0;
              
            }
        }
    }
    public void Shoot(Transform target,int damage,Transform parent)
    {
        for(int i=0;i< effects.Length;i++)
        {
            effects[i].Play();
        }
        Parent = parent;
        Skilldamage = damage;
        transform.LookAt(target);
        Target = target;
        Active = true;
    }
    void SkillExit()
    {
        Active = false;
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].Stop();
        }
        this.transform.SetParent(Parent);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
  
        if (other.CompareTag("Monster"))
        {
            
            other.GetComponent<Monster>().Hitted(Skilldamage);
            SkillExit();


        }
      
    }

    //진짜맞추지말고 빨리날리고 대충없애고 데미지 타게팅으로주자
}
