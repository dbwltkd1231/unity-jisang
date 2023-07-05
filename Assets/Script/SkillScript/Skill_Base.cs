using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Base : MonoBehaviour
{
    [Header("��ų �̹���"),SerializeField]
    public Sprite SkillImg;
    [Header("��ų �䱸����"), SerializeField]
    public int ReqLv;
    [Header("��ų ����"), SerializeField]
    public int SkillMag;
    [Header("��ų ��Ÿ��"), SerializeField]
    public int CoolTime;
    [Header("��ų �̸�"), SerializeField]
    public string SkillName;
    [Header("���ӽð�"), SerializeField]
    public float DurationTime = 10f;
    [Header("��ų����"), SerializeField]
    public string SkillDes;
    [Header("��ų�̹��� ���"), SerializeField]
    public string SKillImgPath;
    [Header("ID"), SerializeField]
    public int ID;

    public float currentTime;
    public HotKey ConnectedHotKey;
    public float time;//��Ÿ�������
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
