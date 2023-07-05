using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public struct PlayerStats
{
    public int Lv;

    public int MaxHp;
    public int Att;
    public int Def;
    public int Spd;
    public int Crt;
    public float Hp;


    public int currentExp;
    public int Coin;

    public int itemMaxHp;
    public int itemAtt;
    public int itemSpd;
    public int itemCrt;
    public int itemDef;
    
    public int TotalAtt( )
    {
        return (Att + itemAtt);
    }
    public int TotalDef()
    {
        return (Def + itemDef);
    }
    public int TotalSpd()
    {
        return (Spd + itemSpd);
    }
    public int TotalCrt()
    {
        return (Crt + itemCrt);
    }
    public float TotalMaxHp()
    {
        return (MaxHp + itemMaxHp);
    }

}

public class PlayerManager : MonoBehaviour, ISubject
{
    public static PlayerManager Instance;

    [Header("플레이어 기본스탯 저장경로"), SerializeField]
    string csv_FileName;

    [Header("경험치 바"), SerializeField]
    Image ExeImage;
    [Header("체력 바"), SerializeField]
    Slider HpSlider;
    [Header("레벨 텍스트"), SerializeField]
    TextMeshProUGUI LvText;
    [Header("코인 텍스트"), SerializeField]
    TextMeshProUGUI CoinText;
    [Header("Sword"), SerializeField]
    public GameObject Sword;

    [Header("LevelUp Effect"), SerializeField]
    LevelUpParticle levelupObj;
    [Header("사망 포스트프로세스"),SerializeField]
    GameObject Death_PP;
    [Header("카메라"), SerializeField]
    Transform cameraTr;

    public PlayerStats playerstats;
    public Animator anim;
    public bool HideOn = false;
    public bool SkillIng = false;

    List<IObserver> List_Monster = new List<IObserver>();
 
    List<IObserver> List_SkillSlot = new List<IObserver>();
   
    int RequireExp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            if (Instance != this)
                Destroy(this.gameObject);
        }
        anim = GetComponent<Animator>();
        
    }
  
    public void ResisterMonster(IObserver observer)//옵저버등록
    {
        List_Monster.Add(observer);
    }

    public void RemoveMonster(IObserver observer)
    {
        List_Monster.Remove(observer);
    }
    public void ResisterSkillSlot(IObserver observer)//옵저버등록
    {
        List_SkillSlot.Add(observer);
    }

    public void RemoveSkillSlot(IObserver observer)
    {
        List_SkillSlot.Remove(observer);
    }
    public void NotifyMonster()
    {
        foreach (IObserver observer in List_Monster)
        {
            observer.UpdateData(HideOn);
        }
    }
    public void NotifySkillSlot()
    {
        foreach (IObserver observer in List_SkillSlot)
        {
            observer.UpdateData(playerstats.Lv);
        }
    }
    public void HideUse(bool SkillOn)
    {
        HideOn = SkillOn;
        NotifyMonster();
    }
  
    
    public void GetDamage(int value)
    {
        
        value -= playerstats.Def;
        if (value < 1)
        {
            value = 1;
        }
        BattleManger.Instance.SendDamageText(value, this.transform);
        playerstats.Hp -= value;//데미지받는함수라 hprezen 함수로 대체 불가능.
        if (playerstats.Hp < 1)
        {
            HideUse(true);
            SkillIng = true;
            Death_PP.SetActive(true);
            anim.SetTrigger("Death");
           GameManager.TimeSlow(0.1f);
           
        }
        HpSlider.value = playerstats.Hp;
       
    }
    public void Death()
    {
        StartCoroutine(DeathCor());
    }
    IEnumerator DeathCor()
    {
        yield return GameManager.Instance.WaitForRealSeconds(2f);
        HideUse(false);
        GameManager.TimeReset();
        SkillIng = false;
        transform.position = new Vector3(0, 0, 0);
        cameraTr.localPosition = new Vector3(0, 0, 0);
        HpRezen((int)playerstats.TotalMaxHp());
        Death_PP.SetActive(false);
        yield return null;
    }
    public void Init()
    {
        RequireExp = playerstats.Lv * 20;
        LvText.text = "Lv." + playerstats.Lv.ToString();
        ExeImage.fillAmount = (float)playerstats.currentExp / RequireExp;
        PlayerStatsSet(csv_FileName);
        
        MaxHpInc(0);
        NotifySkillSlot();
        ReloadCoin();
    }
    public void LevelUp(int value)
    {
        levelupObj.LevelUpEffect();//레벨업 이펙트 연출
        playerstats.Lv = value;
        RequireExp = playerstats.Lv * 20;
        playerstats.currentExp = 0;
        LvText.text = "Lv." + playerstats.Lv.ToString();

        PlayerStatsSet(csv_FileName);
        HpRezen(playerstats.MaxHp);//Hp를 최대치로 회복
        NotifySkillSlot();//스킬슬롯에 레벨상황을 전달.
    }
    public void GetExp(int value)
    {
        if(BattleManger.Instance!=null)
        {
            BattleManger.Instance.SendRewardText(value, RewardType.Exp);
        }
        playerstats.currentExp += value;
        if (playerstats.currentExp >= RequireExp)
        {
            LevelUp(playerstats.Lv + 1);
        }
       
        ExeImage.fillAmount = (float)playerstats.currentExp / RequireExp;
    }
    public void GetCoin(int value)
    {
        if (BattleManger.Instance != null)
        {
            BattleManger.Instance.SendRewardText(value, RewardType.Money);
        }
        
        playerstats.Coin += value;
        ReloadCoin();
    }
    public void UseCoin(int value)
    {
        playerstats.Coin -= value;
        ReloadCoin();
    }
    public void ReloadCoin()
    {
        CoinText.text = playerstats.Coin.ToString();
    }


   public void MaxHpInc(int value)
    {
        playerstats.itemMaxHp += value;
        HpSlider.maxValue = playerstats.TotalMaxHp();
        HpRezen(value);
      
    }
    public void HpRezen(int value)
    {
        playerstats.Hp += value;
        if (playerstats.Hp > playerstats.TotalMaxHp())
        {
            playerstats.Hp = playerstats.TotalMaxHp();
        }
        else if (playerstats.Hp < 1)
        {
            playerstats.Hp = 1;

        }
        HpSlider.value = playerstats.Hp;
    }


    public PlayerStats PlayerStatsSet(string _CSVFileName)
    {
        List<Dictionary<string,object>> data= CSVReader.Read(_CSVFileName);
        int index = int.Parse((playerstats.Lv - 1).ToString());
        string tmp = data[index]["최대체력"].ToString();
        playerstats.MaxHp = int.Parse(tmp);
        playerstats.Att = (int)data[index]["공격력"];
        playerstats.Def = (int)data[index]["방어력"];
        playerstats.Spd = (int)data[index]["속도"];
        playerstats.Crt = (int)data[index]["크리티컬확률"];
        
        return playerstats;
    }
}
