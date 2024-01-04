using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkData
{
    public string speaker; // 대화하는 사람의 이름
    public string[] text; // 대화 내용

    public TalkData(string speaker, string[] text)
    {
        this.speaker = speaker;
        this.text = text;
    }
}


