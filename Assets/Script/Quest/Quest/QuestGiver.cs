using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public static QuestGiver Instance;

    [SerializeField]
    public Quest[] quests;
   
   
    [SerializeField]
    Material[] QuestMarkMaterial;

    [SerializeField]
    NPC[] Npcs;
    NPC CurNpc;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
   
 
  
    public void QuestReceive(int value)//퀘스트갱신함수
    {
        QuestSystem.Instance.Register(quests[value]);
       

    }
   

    public void SetQuestMark()
    {
        if(CurNpc != null)
        {
            CurNpc.Questmark.SetActive(false);
        }
        

        if (QuestSystem.Instance.CompletedQuests.Count == 0)
        {
            CurNpc = Npcs[0];
        }
        else if (QuestSystem.Instance.CompletedQuests.Count == 1)
        {
            CurNpc = Npcs[1];
        }
        else if (QuestSystem.Instance.CompletedQuests.Count == 2)
        {
            CurNpc = Npcs[2];
        }
        else
        {
            CurNpc = null;
        }

        if(QuestSystem.Instance.ActiveQuests.Count >0)
        {
            /*
              if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[0].Tasks[0].State == TaskState.Running)
            {
                CurNpc.Questmark.SetActive(true);
                CurNpc.Questmark.GetComponent<MeshRenderer>().material = QuestMarkMaterial[0];
            }
            else 
              */
            if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[2].Tasks[0].State == TaskState.Running)
            {
                CurNpc.Questmark.SetActive(true);
                CurNpc.Questmark.GetComponent<MeshRenderer>().material = QuestMarkMaterial[1];
            }
        }
        else if (QuestSystem.Instance.ActiveQuests.Count ==0&& CurNpc!=null)
        {
            CurNpc.Questmark.SetActive(true);
            CurNpc.Questmark.GetComponent<MeshRenderer>().material = QuestMarkMaterial[0];
        }







    }
    
}
