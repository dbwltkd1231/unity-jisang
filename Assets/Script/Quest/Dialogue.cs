using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//Ŀ����Ŭ������ �׳��� �����Ҽ����⶧���� serialziable �ʿ���.
public class Dialogue 
{
    [Tooltip("���ġ�� �ɸ��� �̸�")]
    public string name;
    [Tooltip("��� ����")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;//�̺�Ʈ �̸�
    public Vector2 line;//�������� ������ ����
    public Dialogue[] dialogues;
}
