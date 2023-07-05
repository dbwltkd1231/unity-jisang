using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class NPC : MonoBehaviour
{
   

    [SerializeField]
     public QuestGiver QuestGiver;
    [SerializeField]
    public GameObject CommuniteObj;
    [SerializeField]
    public TextMeshProUGUI NameText;
    [SerializeField]
    public TextMeshProUGUI DialogueText;
    [SerializeField]
    public GameObject Questmark;
   
    [SerializeField]
    public GameObject Canvas;
    [SerializeField]
    public GameObject OKBttn;
    [SerializeField]
    public string NpcName;

    [SerializeField]
    public DialogueEvent dialogue;
   
    

    public Dialogue[] GetDialogues(int x,int y)
    {
        dialogue.dialogues = QuestManager.Instance.GetDialogue(x,y);
        return dialogue.dialogues;
    }

    public virtual void SetDialogue()
    {
        QuestGiver.SetQuestMark();
    }

  
    public void showDialogue()
    {
        
        if (QuestManager.Instance.y >= GetDialogues(QuestManager.Instance.DialoguStart, QuestManager.Instance.DialoguEnd)[QuestManager.Instance.x].contexts.Length)
        {
            QuestManager.Instance.y = 0;
            QuestManager.Instance.x++;
        }

        if (QuestManager.Instance.x >= GetDialogues(QuestManager.Instance.DialoguStart, QuestManager.Instance.DialoguEnd).Length)
        {
            QuestManager.Instance.x = 0;
            Canvas.SetActive(true);
            CommuniteObj.SetActive(false);
           
        }
        else
        {
            NameText.text = GetDialogues(QuestManager.Instance.DialoguStart, QuestManager.Instance.DialoguEnd)[QuestManager.Instance.x].name;  
            StartCoroutine(SpecialDialgoue(GetDialogues(QuestManager.Instance.DialoguStart, QuestManager.Instance.DialoguEnd)[QuestManager.Instance.x].contexts[QuestManager.Instance.y]));
            QuestManager.Instance.y++;
        }
    }
    IEnumerator SpecialDialgoue(string text)
    {
        int i = 0;
        string tmp="";
        for(; i<text.Length;i++)
        {
            tmp += text[i];
            DialogueText.text = tmp;
            yield return null;
        }
       
    }
    
}

