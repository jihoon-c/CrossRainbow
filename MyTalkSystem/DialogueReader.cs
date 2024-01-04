using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogueReader : MonoBehaviour
{
    [SerializeField] private TextAsset dialogueData; // 대화 데이터를 담고 있는 CSV 파일
    private Dictionary<int, TalkData[]> dialogueDict = new Dictionary<int, TalkData[]>(); // 이벤트 번호에 대한 대화 데이터를 저장하는 딕셔너리

    void Awake()
    {
        // CSV 파일을 읽어와서 dictionary에 저장
        string[] lines = dialogueData.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length < 3) continue; // 필요한 열이 없으면 continue
            if (!int.TryParse(values[0], out int eventNum)) continue; // 이벤트 번호가 정수형이 아니면 continue

            // 대화 데이터 생성
            string speaker = values[1];
            string text = values[2].Replace("\\n", "\n").Replace("'",",");
            TalkData talkData = new TalkData(speaker, new string[] { text });

            if (!dialogueDict.ContainsKey(eventNum))
                dialogueDict[eventNum] = new TalkData[] { talkData }; // 새로운 이벤트 번호일 경우 대화 데이터 배열을 생성하고 대화 데이터 추가
            else
                dialogueDict[eventNum] = dialogueDict[eventNum].Concat(new TalkData[] { talkData }).ToArray(); // 기존 이벤트 번호일 경우 대화 데이터 배열에 추가

            Debug.Log($"Read dialogue: eventNum={eventNum}, speaker={speaker}, text={text}");
        }
    }


    // 이벤트 번호에 해당하는 대화 데이터 반환
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