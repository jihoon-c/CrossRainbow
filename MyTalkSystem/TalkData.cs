using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkData
{
    public string speaker; // ��ȭ�ϴ� ����� �̸�
    public string[] text; // ��ȭ ����

    public TalkData(string speaker, string[] text)
    {
        this.speaker = speaker;
        this.text = text;
    }
}


