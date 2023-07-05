using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//커스텀클래스는 그냥은 수정할수없기때문에 serialziable 필요함.
public class Dialogue 
{
    [Tooltip("대사치는 케릭터 이름")]
    public string name;
    [Tooltip("대사 내용")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;//이벤트 이름
    public Vector2 line;//엑셀에서 추출한 라인
    public Dialogue[] dialogues;
}
