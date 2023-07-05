using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class QuestSystem : MonoBehaviour
{
    #region Save Path
    private const string kSaveRootPath = "questSystem";
    private const string kActiveQuestsSavePath = "activeQuests";
    private const string kCompletedQuestsSavePath = "completedQuests";
    private const string kActiveAchievementsSavePath = "activeAchievement";
    private const string kCompletedAchievementsSavePath = "completedAchievement";
    #endregion

    #region Events
    public delegate void QuestRegisteredHandler(Quest newQuest);
    public delegate void QuestCompletedHandler(Quest quest);
    public delegate void QuestCanceledHandler(Quest quest);
    #endregion

    public static QuestSystem Instance;
    private DatabaseReference reference;
    

    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();

    private List<Quest> activeAchievements = new List<Quest>();
    private List<Quest> completedAchievements = new List<Quest>();

    private QuestDatabase questDatatabase;
    private QuestDatabase achievementDatabase;

    public event QuestRegisteredHandler onQuestRegistered;
    public event QuestCompletedHandler onQuestCompleted;
    public event QuestCanceledHandler onQuestCanceled;

    public event QuestRegisteredHandler onAchievementRegistered;
    public event QuestCompletedHandler onAchievementCompleted;
   
    public IReadOnlyList<Quest> ActiveQuests => activeQuests;
    public IReadOnlyList<Quest> CompletedQuests => completedQuests;
    public IReadOnlyList<Quest> ActiveAchievements => activeAchievements;
    public IReadOnlyList<Quest> CompletedAchievements => completedAchievements;

    
    [SerializeField]
    GameObject QuestTrackerViewPrefab;
    QuestSaveData FirebaseActiveQuestData;
    QuestSaveData FirebaseCompletedQuestData;
    GameObject QuestTrackerObj;
    private void Awake()
    {
        questDatatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        achievementDatabase = Resources.Load<QuestDatabase>("AchievementDatabase");
        /*
        if (!Load())
        {
            foreach (var achievement in achievementDatabase.Quests)
                Register(achievement);
        }
        */
       if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
       else if (Instance != null)
        {
            if(Instance != this)
            {
                Destroy(this.gameObject);
            }
           
        }

        
       
    }

    private void Start()
    {
        //LogIn();
    }

    public Quest Register(Quest quest)
    {
        var newQuest = quest.Clone();

        if (newQuest is Achievement)
        {
            newQuest.onCompleted += OnAchievementCompleted;

            activeAchievements.Add(newQuest);

            newQuest.OnRegister();
            onAchievementRegistered?.Invoke(newQuest);
        }
        else
        {
            newQuest.onCompleted += OnQuestCompleted;
            newQuest.onCanceled += OnQuestCanceled;

            activeQuests.Add(newQuest);

            newQuest.OnRegister();
            onQuestRegistered?.Invoke(newQuest);
        }
        //QuestTrackerObj.GetComponent<QuestTrackerView>().init();
        return newQuest;
    }

    public void ReceiveReport(string category, object target, int successCount)
    {
        ReceiveReport(activeQuests, category, target, successCount);
        ReceiveReport(activeAchievements, category, target, successCount);
    }

    public void ReceiveReport(Category category, TaskTarget target, int successCount)
        => ReceiveReport(category.CodeName, target.Value, successCount);

    private void ReceiveReport(List<Quest> quests, string category, object target, int successCount)
    {
        foreach (var quest in quests.ToArray())
            quest.ReceiveReport(category, target, successCount);
    }

    public void CompleteWaitingQuests()
    {
        foreach (var quest in activeQuests.ToList())
        {
            if (quest.IsComplatable)
                quest.Complete();
        }
    }

    public bool ContainsInActiveQuests(Quest quest) => activeQuests.Any(x => x.CodeName == quest.CodeName);

    public bool ContainsInCompleteQuests(Quest quest) => completedQuests.Any(x => x.CodeName == quest.CodeName);

    public bool ContainsInActiveAchievements(Quest quest) => activeAchievements.Any(x => x.CodeName == quest.CodeName);

    public bool ContainsInCompletedAchievements(Quest quest) => completedAchievements.Any(x => x.CodeName == quest.CodeName);

    public void Init(bool load)
    {
        StartCoroutine(init(load));
    }
    IEnumerator init(bool load)
    {
        if(load==true)
        {
           // LogIn();
        }
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Game");

        if (load==true)
        {
            Load();
        }
        QuestTrackerObj = Instantiate(QuestTrackerViewPrefab);
        QuestTrackerObj.transform.SetParent(GameObject.Find("Canvas").transform);
        QuestTrackerObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(590.73f, 110.8f);
        QuestTrackerObj.GetComponent<QuestTrackerView>().init();
       
        QuestGiver.Instance.SetQuestMark();
        yield return null;
    }
    public void Save()
    {
        var root = new JObject();
        root.Add(kActiveQuestsSavePath, CreateSaveDatas(activeQuests));
        root.Add(kCompletedQuestsSavePath, CreateSaveDatas(completedQuests));
        root.Add(kActiveAchievementsSavePath, CreateSaveDatas(activeAchievements));
        root.Add(kCompletedAchievementsSavePath, CreateSaveDatas(completedAchievements));


        reference.Child("Player").Child("quest").SetRawJsonValueAsync(root.ToString());

    }

    public void LogIn()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        activeQuests.Clear();
        completedQuests.Clear();
        FirebaseActiveQuestData = new QuestSaveData();
        FirebaseCompletedQuestData = new QuestSaveData();
     

        FirebaseDatabase.DefaultInstance
  .GetReference("Player").Child("quest")
  .GetValueAsync().ContinueWithOnMainThread(task =>
  {
      if (task.IsFaulted)
      {
          // Handle the error...
      }
      else if (task.IsCompleted)
      {
          DataSnapshot snapshot = task.Result;
          for (int i = 0; i < snapshot.Child("activeQuests").ChildrenCount; i++)
          {
              FirebaseActiveQuestData.taskSuccessCounts = new int[1];
              foreach (var data in snapshot.Child("activeQuests").Child(i.ToString()).Children)
              {


                  if (data.Key == "codeName")
                  {
                      FirebaseActiveQuestData.codeName = data.Value.ToString();
                  }
                  else if (data.Key == "state")
                  {
                      FirebaseActiveQuestData.state = (QuestState)int.Parse(data.Value.ToString());
                  }
                  else if (data.Key == "taskGroupIndex")
                  {
                      FirebaseActiveQuestData.taskGroupIndex = int.Parse(data.Value.ToString());
                  }

              }
              List<int> tmpList = new List<int>();
              foreach (var data2 in snapshot.Child("activeQuests").Child(i.ToString()).Child("taskSuccessCounts").Children)
              {
                  tmpList.Add(int.Parse(data2.Value.ToString()));

              }
          
              FirebaseActiveQuestData.taskSuccessCounts = tmpList.ToArray();
          }

          for (int i = 0; i < snapshot.Child("completedQuests").ChildrenCount; i++)
          {

              FirebaseCompletedQuestData.taskSuccessCounts = new int[1];

              foreach (var data in snapshot.Child("completedQuests").Child(i.ToString()).Children)
              {


                  if (data.Key == "codeName")
                  {
                      FirebaseCompletedQuestData.codeName = data.Value.ToString();
                  }
                  else if (data.Key == "state")
                  {
                      FirebaseCompletedQuestData.state = (QuestState)int.Parse(data.Value.ToString());
                  }
                  else if (data.Key == "taskGroupIndex")
                  {
                      FirebaseCompletedQuestData.taskGroupIndex = int.Parse(data.Value.ToString());
                  }

              }
              List<int> tmpList = new List<int>();
              foreach (var data2 in snapshot.Child("completedQuests").Child(i.ToString()).Child("taskSuccessCounts").Children)
              {
                  tmpList.Add(int.Parse(data2.Value.ToString()));


              }
          
              FirebaseCompletedQuestData.taskSuccessCounts = tmpList.ToArray();
          }

      }

  });

    }
    public bool Load()
    {
        LoadSaveDatas(FirebaseActiveQuestData, questDatatabase, LoadActiveQuest);
        LoadSaveDatas(FirebaseCompletedQuestData, questDatatabase, LoadCompletedQuest);
        return true;
    }
    

    private JArray CreateSaveDatas(IReadOnlyList<Quest> quests)
    {
        var saveDatas = new JArray();
        foreach (var quest in quests)
        {
            //if (quest.IsSavable)
            saveDatas.Add(JObject.FromObject(quest.ToSaveData()));
        }
        return saveDatas;
    }

    private void LoadSaveDatas(JToken datasToken, QuestDatabase database, System.Action<QuestSaveData, Quest> onSuccess)
    {
        var datas = datasToken as JArray;
        foreach (var data in datas)
        {
            var saveData = data.ToObject<QuestSaveData>();
            var quest = database.FindQuestBy(saveData.codeName);
            onSuccess.Invoke(saveData, quest);
            Debug.Log(saveData.ToString());
        }
    }
    private void LoadSaveDatas(QuestSaveData questsavedata,QuestDatabase database, System.Action<QuestSaveData, Quest> onSuccess)
    {

        
        var quest = database.FindQuestBy(questsavedata.codeName);
        if(quest!=null)
        {
            onSuccess.Invoke(questsavedata, quest);
        }
     
       

    }

    private void LoadActiveQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = Register(quest);
        newQuest.LoadFrom(saveData);
        //QuestView.init();
        
    }

    private void LoadCompletedQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = quest.Clone();
        newQuest.LoadFrom(saveData);

        if (newQuest is Achievement)
            completedAchievements.Add(newQuest);
        else
            completedQuests.Add(newQuest);

        QuestGiver.Instance.SetQuestMark();
    }

    #region Callback
    private void OnQuestCompleted(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        onQuestCompleted?.Invoke(quest);
    }

    private void OnQuestCanceled(Quest quest)
    {
        activeQuests.Remove(quest);
        onQuestCanceled?.Invoke(quest);

        Destroy(quest, Time.deltaTime);
    }

    private void OnAchievementCompleted(Quest achievement)
    {
        activeAchievements.Remove(achievement);
        completedAchievements.Add(achievement);

        onAchievementCompleted?.Invoke(achievement);
    }
    #endregion
}
