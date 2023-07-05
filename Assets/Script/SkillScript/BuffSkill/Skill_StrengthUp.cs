using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_StrengthUp : BuffBase
{
    [SerializeField]
    GameObject[] skilleffects;
    int originAtt;
    int originItemAtt;

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
        originAtt=PlayerManager.Instance.playerstats.Att;
        originItemAtt = PlayerManager.Instance.playerstats.itemAtt;

        PlayerManager.Instance.playerstats.Att *= 2;
        PlayerManager.Instance.playerstats.itemAtt *= 2;
        for(int i=0;i< skilleffects.Length;i++)
        {
            skilleffects[i].SetActive(true);
            skilleffects[i].transform.SetParent(PlayerManager.Instance.transform,false);
        }
        while (currentTime > 0)
        {
            currentTime -= 1f;

            yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(1f));
        }
        DeActivation();

    }
    public void DeActivation()
    {
        PlayerManager.Instance.playerstats.Att = originAtt;
        PlayerManager.Instance.playerstats.itemAtt = originItemAtt;

        for (int i = 0; i < skilleffects.Length; i++)
        {
            skilleffects[i].transform.SetParent(this.transform,false);
            skilleffects[i].SetActive(false);
        }

        PlayerManager.Instance.HideUse(false);
    }
}
