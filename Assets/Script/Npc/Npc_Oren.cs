using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Npc_Oren : NPC, IPointerClickHandler
{

    public UnityEngine.Events.UnityEvent Master;

   

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
       
        Canvas.SetActive(false);
        CommuniteObj.SetActive(true);
        SetDialogue();
        showDialogue();
        Master.Invoke();
    }

    public override void SetDialogue()
    {
        base.SetDialogue();
        OKBttn.SetActive(true);
        
        if(QuestSystem.Instance.CompletedQuests.Count==0)
        {
            if(QuestSystem.Instance.ActiveQuests.Count==0)
            {
                QuestGiver.QuestReceive(0);
                QuestManager.Instance.DialoguStart = 1;
                QuestManager.Instance.DialoguEnd = 4;
            }
            else if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[1].Tasks[0].State == TaskState.Running)
            {
                QuestManager.Instance.DialoguStart = 5;
                QuestManager.Instance.DialoguEnd = 6;
            }

            else if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[1].Tasks[0].State == TaskState.Complete)
            {
                QuestManager.Instance.DialoguStart = 7;
                QuestManager.Instance.DialoguEnd = 9;
                QuestSystem.Instance.ActiveQuests[0].Complete();
            }
            else
            {
                QuestManager.Instance.DialoguStart = 10;
                QuestManager.Instance.DialoguEnd = 10;
            }
        }
        else
        {
            QuestManager.Instance.DialoguStart = 10;
            QuestManager.Instance.DialoguEnd = 10;
        }

    }

    

}

