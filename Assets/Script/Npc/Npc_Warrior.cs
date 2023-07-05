using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Npc_Warrior : NPC, IPointerClickHandler
{

    public UnityEngine.Events.UnityEvent Warrior;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Canvas.SetActive(false);
        CommuniteObj.SetActive(true);
        SetDialogue();
        showDialogue();
        Warrior.Invoke();
    }

    public override void SetDialogue()
    {
        OKBttn.SetActive(true);
        NameText.text = NpcName;
        if (QuestSystem.Instance.CompletedQuests.Count == 2)
        {
            //if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[0].Tasks[0].State == TaskState.Running)
            if (QuestSystem.Instance.ActiveQuests.Count == 0)
            {
                QuestGiver.QuestReceive(2);
                QuestManager.Instance.DialoguStart = 20;
                QuestManager.Instance.DialoguEnd = 24;


                //시작해

            }
            else if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[1].Tasks[0].State == TaskState.Running)
            {
                QuestManager.Instance.DialoguStart = 25;
                QuestManager.Instance.DialoguEnd = 25;

                //빨리해줘
            }

            else if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[2].Tasks[0].State == TaskState.Running)
            {
               
                OKBttn.SetActive(true);
                QuestManager.Instance.DialoguStart = 26;
                QuestManager.Instance.DialoguEnd = 29;
                QuestSystem.Instance.ActiveQuests[0].Complete();

                //퀘스트 끝
                //고마워

            }
            else
            {
                QuestManager.Instance.DialoguStart = 29;
                QuestManager.Instance.DialoguEnd = 29;
                //평상시
            }
        }

        else
        {
            QuestManager.Instance.DialoguStart = 29;
            QuestManager.Instance.DialoguEnd = 29;
            //평상시
        }
       

    }

}