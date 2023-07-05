using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;
public class LobySceneManager : MonoBehaviour
{
    public static LobySceneManager Instance;
    [SerializeField]
    GameObject LoadBttn;

    private void Start()
    {
        AudioManager.Instance.BgmPlay(0);
        //QuestSystem.Instance.LogIn();
        LoadBttn.SetActive(false);
        init();

    }

    public async void init()
    {
        var task1 = await System.Threading.Tasks.Task.Run(() =>
        {
            QuestSystem.Instance.LogIn();
            DataManager.Instance.LogIn();
            return true;
        });

        LoadBttn.SetActive(true);

    }

    public void SaveGameStart()
    {
        
        QuestSystem.Instance.Init(true);
        DataManager.Instance.Init(true);
        SceneManager.LoadScene("LoadingScene");
    }
    public void NewGameStart()
    {
        QuestSystem.Instance.Init(false);
        DataManager.Instance.Init(false);
        SceneManager.LoadScene("LoadingScene");
        
    }

   
    
}
