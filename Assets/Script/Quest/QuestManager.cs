using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("NPC ��ȭ���� ���"), SerializeField]
    string csv_FileName;
    [Header("������ ��ȭ ��ũ��Ʈ ���۰�"), SerializeField]
    public int DialoguStart;
    [Header("������ ��ȭ ��ũ��Ʈ ����"), SerializeField]
    public int DialoguEnd;

    

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();
   
    public int x;
    public int y;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);
            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);
            }
           
        }
    }

    public Dialogue[] GetDialogue(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for (int i = 0; i <= _EndNum - _StartNum; i++)
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }
        return dialogueList.ToArray();
    }

}
