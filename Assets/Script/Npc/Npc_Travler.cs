using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Npc_Travler : NPC, IPointerClickHandler
{
    public UnityEngine.Events.UnityEvent Travler;
    [SerializeField]
    PlayableDirector playableDirector;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Canvas.SetActive(false);
        CommuniteObj.SetActive(true);
        SetDialogue();
        showDialogue();
        Travler.Invoke();
    }
    public override void SetDialogue()
    {
        base.SetDialogue();
        OKBttn.SetActive(true);
        NameText.text = NpcName;

        if (QuestSystem.Instance.CompletedQuests.Count == 1)
        {
            //if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[0].Tasks[0].State == TaskState.Running)
            if (QuestSystem.Instance.ActiveQuests.Count == 0)
            {
                QuestGiver.QuestReceive(1);

                QuestManager.Instance.DialoguStart = 11;
                QuestManager.Instance.DialoguEnd = 14;
                playableDirector.Play();
                StartCoroutine(StopTimeLine2());

                //시작해

            }
            else if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[1].Tasks[0].State == TaskState.Running)
            {
                QuestManager.Instance.DialoguStart = 15;
                QuestManager.Instance.DialoguEnd = 16;

                //빨리해줘
            }

            else if (QuestSystem.Instance.ActiveQuests[0].TaskGroups[2].Tasks[0].State == TaskState.Running)
            {
                QuestManager.Instance.DialoguStart = 17;
                QuestManager.Instance.DialoguEnd = 18;

                QuestSystem.Instance.ActiveQuests[0].Complete();

            

            }
            else
            {
                QuestManager.Instance.DialoguStart = 19;
                QuestManager.Instance.DialoguEnd = 19;
                //평상시
            }
        }
        else
        {
            QuestManager.Instance.DialoguStart = 19;
            QuestManager.Instance.DialoguEnd = 19;
            //평상시
        }

    }
    public void StopTimeLine()
    {
        playableDirector.Stop();
    }
    IEnumerator StopTimeLine2()
    {
        yield return new WaitUntil(() => CommuniteObj.activeSelf ==false);
        playableDirector.Stop();
    }
}
