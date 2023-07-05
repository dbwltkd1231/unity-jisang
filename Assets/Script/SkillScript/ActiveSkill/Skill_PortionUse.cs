using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PortionUse : Skill_Base
{

    private void Start()
    {
        
        keycode= UiManager.uimanager.PortionSlot.GetComponent<HotKey>().keyCode;
        //포션키는 연결되는 핫키가 고정이기때문.
        ConnectedHotKey =UiManager.uimanager.PortionSlot.GetComponent<HotKey>();
        skilling = true;
    }
    private void Update()
    {

        if (Input.GetButtonDown(keycode)&& skilling==true)
        {
           
            PlayerManager.Instance.HpRezen(SkillMag);
            skilling = false;
        }

        if(skilling==false)
        {
            time += Time.deltaTime;
            if (ConnectedHotKey != null)
            {
                ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = time / CoolTime;
            }
            if (time > CoolTime)
            {
                time = 0;
                skilling = true;
            }
        }
            
        
    }


}
