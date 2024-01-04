using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogueReader : MonoBehaviour
{
    [SerializeField] private TextAsset dialogueData; // ��ȭ �����͸� ��� �ִ� CSV ����
    private Dictionary<int, TalkData[]> dialogueDict = new Dictionary<int, TalkData[]>(); // �̺�Ʈ ��ȣ�� ���� ��ȭ �����͸� �����ϴ� ��ųʸ�

    void Awake()
    {
        // CSV ������ �о�ͼ� dictionary�� ����
        string[] lines = dialogueData.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length < 3) continue; // �ʿ��� ���� ������ continue
            if (!int.TryParse(values[0], out int eventNum)) continue; // �̺�Ʈ ��ȣ�� �������� �ƴϸ� continue

            // ��ȭ ������ ����
            string speaker = values[1];
            string text = values[2].Replace("\\n", "\n").Replace("'",",");
            TalkData talkData = new TalkData(speaker, new string[] { text });

            if (!dialogueDict.ContainsKey(eventNum))
                dialogueDict[eventNum] = new TalkData[] { talkData }; // ���ο� �̺�Ʈ ��ȣ�� ��� ��ȭ ������ �迭�� �����ϰ� ��ȭ ������ �߰�
            else
                dialogueDict[eventNum] = dialogueDict[eventNum].Concat(new TalkData[] { talkData }).ToArray(); // ���� �̺�Ʈ ��ȣ�� ��� ��ȭ ������ �迭�� �߰�

            Debug.Log($"Read dialogue: eventNum={eventNum}, speaker={speaker}, text={text}");
        }
    }


    // �̺�Ʈ ��ȣ�� �ش��ϴ� ��ȭ ������ ��ȯ
    public TalkData[] GetDialogueByEventNum(int eventNum)
    {
        if (dialogueDict.ContainsKey(eventNum))
        {
            return dialogueDict[eventNum];
        }
        else
        {
            Debug.LogWarning($"Cannot find dialogue for event number {eventNum}");
            return null;

        }
    }
}