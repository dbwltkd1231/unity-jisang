using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Status : MonoBehaviour
{
    public static Status Instance;
    [SerializeField]
    public TextMeshProUGUI LevelValue;
    [SerializeField]
    TextMeshProUGUI MaxHpValue;
    [SerializeField]
    TextMeshProUGUI AttValue;
    [SerializeField]
    TextMeshProUGUI DefValue;
    [SerializeField]
    TextMeshProUGUI CrtValue;
    [SerializeField]
    TextMeshProUGUI SpdValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
  
    public void RefreshStats()
    {
        LevelValue.text= PlayerManager.Instance.playerstats.Lv.ToString();
        MaxHpValue.text = PlayerManager.Instance.playerstats.TotalMaxHp().ToString();
        AttValue.text = PlayerManager.Instance.playerstats.TotalAtt().ToString();
        DefValue.text = PlayerManager.Instance.playerstats.TotalDef().ToString();
        CrtValue.text = Mathf.Round((PlayerManager.Instance.playerstats.TotalCrt())).ToString()+"%";
        SpdValue.text = Mathf.Round(PlayerManager.Instance.playerstats.TotalSpd()).ToString();
    }

}

