using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Base : MonoBehaviour
{
    [Header("스킬 이미지"),SerializeField]
    public Sprite SkillImg;
    [Header("스킬 요구레벨"), SerializeField]
    public int ReqLv;
    [Header("스킬 배율"), SerializeField]
    public int SkillMag;
    [Header("스킬 쿨타임"), SerializeField]
    public int CoolTime;
    [Header("스킬 이름"), SerializeField]
    public string SkillName;
    [Header("지속시간"), SerializeField]
    public float DurationTime = 10f;
    [Header("스킬설명"), SerializeField]
    public string SkillDes;
    [Header("스킬이미지 경로"), SerializeField]
    public string SKillImgPath;
    [Header("ID"), SerializeField]
    public int ID;

    public float currentTime;
    public HotKey ConnectedHotKey;
    public float time;//쿨타임저장용
    public string keycode;
    public bool skilling;


    public virtual void Execute()
    {
        
    }
    public void KeySet(string value)
    {
        this.keycode = value;
    }
    public virtual void SkillBttnOn()
    {

    }
    public virtual void Start()
    {
        
    }
    public void Update()
    {
        if (this.keycode != string.Empty && ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount == 1)
        {
            if (Input.GetButtonDown(this.keycode))
            {
                Execute();
                StartCoroutine(SkillManager.Instance.CoolTimeCal(ConnectedHotKey, CoolTime));
            }
        }
    }


}
