using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour
{
    [Header("스킬데이터 저장경로"), SerializeField]
    string csv_FileName;


    [Header("버프슬롯 프리팹"),SerializeField]
    GameObject BuffSlotPrefab;
    [Header("버프슬롯 Parent"), SerializeField]
    Transform BuffSlotParent;
    [Header("스킬 List"), SerializeField]
     List<Skill_Base> Skill_List;
    [Header("SkillSlot Transform"), SerializeField]
    public Transform SkillSlots;
    [Header("SkillSlot"), SerializeField]
    SkillSlot[] skillSlots;
    [Header("HotKye List"), SerializeField]
    public List<HotKey> HotkeyList;
    [Header("포션"), SerializeField]
    public Skill_Base Portion;

    SkillSlot[] SkillSlot_List;

    [SerializeField]
    public Material[] Skill_Hide_Materials;

    public static SkillManager Instance;

    public Queue<GameObject> BuffSlotQueue;

    void init()
    {
        LoadSkillData(csv_FileName);
        BuffInit();
        SkillSlotInit();
        HideMaterialSetValue(1f);
    }
    int compare(Skill_Base a, Skill_Base b)
    {
        return a.ReqLv < b.ReqLv ? -1 : 1;
    }
    //버프 프리팹 풀링
    void BuffInit()
    {
        BuffSlotQueue = ObjectPooling.Instance.CreateQueue();
        for (int i = 0; i < 3; i++)
        {
            ObjectPooling.Instance.CreatePoolObj(BuffSlotQueue, BuffSlotParent, BuffSlotPrefab);
        }
    }
    void SkillSlotInit()
    {
        SkillSlot_List = SkillSlots.GetComponentsInChildren<SkillSlot>();
        Skill_List.Sort(compare);

        for (int i = 0; i < Skill_List.Count; i++)
        {
            SkillSlot_List[i].GetComponent<SkillSlot>().GetSkill(Skill_List[i]);
        }

        for (int i = 0; i < skillSlots.Length; i++)
        {
            PlayerManager.Instance.ResisterSkillSlot(skillSlots[i]);
        }
    }
    //버프 프리팹에 이미지 및 쿨타임 표시
    public void UseBuff(BuffBase buff)
    {
        GameObject newBuffSlot=
        ObjectPooling.Instance.GetPoolObj(BuffSlotQueue, BuffSlotParent, BuffSlotPrefab);
        newBuffSlot.GetComponent<Image>().sprite = buff.SkillImg;
        StartCoroutine(newBuffSlot.GetComponent<BuffSlot>().BuffPrefabLife(buff.DurationTime));
    }
    //플레이어 매트리얼 투명도 조절함수
    public void HideMaterialSetValue(float value)
    {
        for (int i = 0; i < Skill_Hide_Materials.Length; i++)
        {
            Skill_Hide_Materials[i].SetFloat("_Transp", value);
        }
    }
    //버프 오브젝트 회수
    public void ReceiveBuffSlot(GameObject obj)
    {
        ObjectPooling.Instance.ReturnPoolObj(BuffSlotQueue, BuffSlotParent, obj);
    }
    //삭제하고 애니메이션 이벤트로 수정예정
    public IEnumerator SkillAttack(int Damage,float time)
    {
        yield return new WaitForSeconds(time);
        int damage = Damage;
        Collider[] Targets = Physics.OverlapSphere(PlayerManager.Instance.transform.position, 10f, 1 << 7);
        GameObject[] targets = new GameObject[Targets.Length];
        if (targets.Length >= 1)
        {
            for (int i = 0; i < (Targets.Length <= 6 ? Targets.Length : 6); i++)
            {
                targets[i] = Targets[i].gameObject;
                targets[i].gameObject.GetComponent<Monster>().Hitted(damage);
            }
        }
        yield return null;
    }
    //스킬 쿨타임계산
    public IEnumerator CoolTimeCal(HotKey ConnectedHotKey,float coolTime)
    {

        ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = 0;

        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(coolTime / 5));
        ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = 0.2f;
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(coolTime / 5));
        ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = 0.4f;
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(coolTime / 5));
        ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = 0.6f;
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(coolTime / 5));
        ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = 0.8f;
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(coolTime / 5));
        ConnectedHotKey.GetComponent<HotKey>().SkillImage.fillAmount = 1;
        yield return null;
    }
    public void LoadSkillData(string _CSVFileName)
    {
        List<Dictionary<string, object>> data = CSVReader.Read(_CSVFileName);
        for (int i = 0; i < data.Count; i++)
        {
            if ((string)data[i]["이름"] == ""||i>=Skill_List.Count)
            {
                break;
            }
            Skill_Base newSkill=null;
            Skill_List[i].ID = int.Parse(data[i]["ID"].ToString());
            Skill_List[i].SkillName = data[i]["이름"].ToString();
            Skill_List[i].CoolTime = (int)data[i]["쿨타임"];
            Skill_List[i].SkillMag = (int)data[i]["배율"];
            Skill_List[i].ReqLv = (int)data[i]["요구레벨"];
            Skill_List[i].SkillName = data[i]["이름"].ToString();
            Skill_List[i].SkillDes = data[i]["설명"].ToString();
            Skill_List[i].SKillImgPath = data[i]["이미지"].ToString();
            Skill_List[i].SkillImg = Resources.Load<Sprite>(Skill_List[i].SKillImgPath);
            Skill_List[i].DurationTime = (int)data[i]["지속시간"];


        }
    }
    public Skill_Base FindSkill(int id)
    {
        Skill_Base targetSkill=new Skill_Base();
        for (int i=0;i<Skill_List.Count;i++)
        {
            if(Skill_List[i].ID==id)
            {
                targetSkill= Skill_List[i];
            }
           
        }
        return targetSkill;
    }
    public SkillSlot FIndSkillSlot(int id)
    {
        SkillSlot slot = new SkillSlot();
        for (int i=0;i<SkillSlot_List.Length;i++)
        {
            if(skillSlots[i].ID==id)
            {
                slot= skillSlots[i];
            }
        }
        return slot;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
      
        }
        else
        {
            Destroy(this.gameObject);
        }
        init();
    }
   
    
    
   
}
