using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystemSave : MonoBehaviour
{
    [SerializeField]
    private Quest quest;
    [SerializeField]
    private Category category;
    [SerializeField]
    private TaskTarget target;

    void Start()
    {
        var questSystem = QuestSystem.Instance;

        if(questSystem.ActiveQuests.Count==0)
        {
            var newQuest = questSystem.Register(quest);
        }
        else
        {
            questSystem.onQuestCompleted += (quest) =>
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
            };
        }
    }

    
}
