using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsSkill;
    public bool IsUi;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (Instance != this)
                Destroy(this.gameObject); 
        }
    }

   
    public void OnScreenSetting(bool flag)
    {
        if(flag==true)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(1920, 1080, false);
        }
    }
    public void ReturnLobby()
    {
        SceneManager.LoadScene("Loby");
    }
    public void ExitGame()
    {
        Application.Quit();
    }


    public static void TimeSlow(float Scale)
    {
        Time.timeScale = Scale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public static void TimeReset()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public  IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
    
    public IEnumerator CooltimeCal(Image CoolImg,float cooltime)
    {
        CoolImg.fillAmount = 1;
        yield return StartCoroutine(WaitForRealSeconds(cooltime / 5));//
        CoolImg.fillAmount = 0.8f;
        yield return StartCoroutine(WaitForRealSeconds(cooltime / 5));
        CoolImg.fillAmount = 0.6f;
        yield return StartCoroutine(WaitForRealSeconds(cooltime / 5));
        CoolImg.fillAmount = 0.4f;
        yield return StartCoroutine(WaitForRealSeconds(cooltime / 5));
        CoolImg.fillAmount = 0.2f;
        yield return StartCoroutine(WaitForRealSeconds(cooltime / 5));
        CoolImg.fillAmount = 0;
        yield return null;
    }
}
public enum RewardType
{
    None,
    Exp,
    Money
}
