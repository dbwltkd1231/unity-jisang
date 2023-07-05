using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using System;
public class GameDataManager : MonoBehaviour
{
    private DatabaseReference reference;

    public Dictionary<string, string> player_keycode;
    public Dictionary<int, string> Skill_keycode;
    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void Init(bool load)
    {
        StartCoroutine(init(load));
    }
    IEnumerator init(bool load)
    {

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Game");
        if (load == true)
        {
            //JsonLoad();
        }
       // Status.Instance.RefreshStats();
       // PlayerManager.Instance.Init();
        yield return null;
    }

    public void LogIn()
    {

    }

    public void Save()
    {
        Save_OptionData newSave_OptionData=new Save_OptionData();

        AudioManager.Instance.audioMixer.GetFloat("Master", out newSave_OptionData.MasterVol);
        AudioManager.Instance.audioMixer.GetFloat("BGM", out newSave_OptionData.BgmVol);
        AudioManager.Instance.audioMixer.GetFloat("Effect", out newSave_OptionData.EffectVol);



    }

}
[Serializable]
public class Save_OptionData
{
    public float MasterVol;
    public float BgmVol;
    public float EffectVol;

    public Dictionary<string,string> player_Keycode=new Dictionary<string, string>();
    
    public Dictionary<int, string> Skill_Keycode=new Dictionary<int, string>();

}
