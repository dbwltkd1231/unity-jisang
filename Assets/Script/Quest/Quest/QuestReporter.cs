using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestReporter : MonoBehaviour
{
    [SerializeField]
    private Category category;
    [SerializeField]
    private TaskTarget target;
    [SerializeField]
    private int successCount;
    [SerializeField]
    private string[] colliderTags;

    QuestGiver questgiver;
    private void Start()
    {
        questgiver = GameObject.Find("QuestManager").GetComponent<QuestGiver>();
    }
    private void OnTriggerEnter(Collider other)
    {
        ReportIfPassCondition(other);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReportIfPassCondition(collision);
    }

    public void Report()
    {
       
        QuestSystem.Instance.ReceiveReport(category, target, successCount);
        questgiver.SetQuestMark();
    }

    private void ReportIfPassCondition(Component other)
    {
        if (colliderTags.Any(x => other.CompareTag(x)))
            Report();
    }
}


//���� ��������ũ��Ʈ�߰�
//Į������ Į�ȸ´����� ���� ����
//���� ����->����Ʈ����Ʈ �߰�