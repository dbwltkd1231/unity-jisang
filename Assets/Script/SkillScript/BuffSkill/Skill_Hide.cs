using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Hide : BuffBase
{
    



    public override void Execute()
    {
        if (ConnectedHotKey != null)
        {
            ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = time / CoolTime;
        }
       
        currentTime = DurationTime;
        SkillManager.Instance.UseBuff(this);
        StartCoroutine(Activation());


    }

    IEnumerator Activation()
    {
        SkillManager.Instance.HideMaterialSetValue(0.4f);
        PlayerManager.Instance.HideUse(true);
   
        while (currentTime>0)
        {
            currentTime -=1f;
  
            yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(1f));
        }
        DeActivation();

    }
    public void DeActivation()
    {
        for(int i=0;i< SkillManager.Instance.Skill_Hide_Materials.Length;i++)
        {
            SkillManager.Instance.HideMaterialSetValue(1f);
        }
        
        
        skilling = false;
        PlayerManager.Instance.HideUse(false);
    }


}
