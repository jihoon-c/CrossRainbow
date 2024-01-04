using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TalkObject : MonoBehaviour
{
    public TMP_Text dialogueText; // 대화 내용을 표시할 TMPro(TextMeshPro)
    public TMP_Text speakerText; // 대화하는 사람의 이름을 표시할 TMPro(TextMeshPro)
    public GameObject image1; // 이미지1을 담은 GameObject
    public GameObject panel; // 대화 패널을 담은 GameObject
    public GameObject nextTalkObject;

    private DialogueReader dialogueReader; // 대화 데이터를 관리하는 DialogueReader 클래스
    private TalkData[] dialogue; // 현재 대화 데이터 배열
    private int currentIndex; // 현재 대화 인덱스
    private int currentEventId; // 현재 이벤트 번호
    public  GameObject eventImg1, eventImg2, eventImg3, eventImg4;

    private void Start()
    {
        dialogueReader = FindObjectOfType<DialogueReader>(); // DialogueReader 클래스를 찾아옴
        SceneEventFunc();
    }

    public void OnMouseDown()
    {
        SceneEventFunc();
    }

    public void SceneEventFunc()
    {
        ObjData objData = GetComponent<ObjData>();

        if (objData != null)
        {
            int eventId = objData.id;

            if (eventId != currentEventId)
            {
                dialogue = dialogueReader.GetDialogueByEventNum(eventId);

                if (dialogue != null && dialogue.Length > 0)
                {
                    currentIndex = 0; // 첫 번째 대화로 초기화
                    currentEventId = eventId; // 현재 이벤트 번호 업데이트
                    ShowCurrentDialogue();
                }
                else
                {
                    Debug.LogWarning($"Cannot find dialogue for event number {eventId}");
                }
            }
            else
            {
                ShowNextDialogue();
            }
        }

    }

    private void ShowCurrentDialogue()
    {
        if (currentIndex >= dialogue.Length)
        {
            dialogue = null; // 대화 종료
            currentEventId = 0; // 현재 이벤트 번호 초기화
            DisableDialogueUI(); // 대화가 종료되면 UI를 비활성화
            return;
        }

        TalkData currentTalkData = dialogue[currentIndex];
        string speaker = currentTalkData.speaker;
        //string face = currentTalkData.face;
        string[] texts = currentTalkData.text;


        speakerText.text = speaker .Replace("1","").Replace("2", "").Replace("3", "").Replace("4", "");

        // 대화 내용을 배열 형태로 표시
        dialogueText.text = string.Join("\n", texts);

        EnableDialogueUI(); // 대화가 시작되면 UI를 활성화


        SpriteRenderer spriteRenderer;
        spriteRenderer = image1.GetComponent<SpriteRenderer>();
        // image1의 스프라이트 변경을 위한 선언

        // speaker(2열의 내용)에 따라 이미지를 활성화
        if (speaker == "견왕")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/DogKing");
        }
        else if (speaker == "로이")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Loi_S");
        }
        else if (speaker == "로이1")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Loi_S_1");
        }
        else if (speaker == "로이2")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Loi_S_2");
        }
        else if (speaker == "로이3")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Loi_S_3");
        }
        else if (speaker == "초코")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S");
        }
        else if (speaker == "초코1")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S");
        }
        else if (speaker == "초코2")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S_2");
        }
        else if (speaker == "초코3")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S_3");
        }
        else if (speaker == "초코4")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S_4");
        }
        else
        {
            // 다른 경우에는 이미지를 모두 비활성화
            image1.SetActive(false);
        }

        //특정 이벤트 실행을 할 때 대사 중 발췌해서 이벤트 실행시키면 됩니다

        //1 
        if (dialogueText.text.Contains("머무는 도시가 있다"))
        {
            eventImg1.SetActive(true);
        }
        if (dialogueText.text.Contains("이들은 생전의 가족을"))
        {
            eventImg1.SetActive(false);
            eventImg2.SetActive(true);
        }
        if (dialogueText.text.Contains("이 평화가"))
        {
            eventImg2.SetActive(false);
            eventImg4.SetActive(true);
        }
        if (dialogueText.text.Contains("어째서 이렇게"))
        {
            eventImg4.SetActive(false);
        }


        if (dialogueText.text.Contains("저번에 마셔본"))
        {
            eventImg3.SetActive(true);
        }
        if (dialogueText.text.Contains("잠시만요"))
        {
            eventImg3.SetActive(false);
        }
        /*
           if (dialogueText.text.Contains("…"))
        {
            SceneManager.LoadScene("MainScene");
        }
        */

        //11
        if (dialogueText.text.Contains("그렇게 말하니까 무척이나"))
        {
            //ChocoEvent1();
        }

        


        currentIndex++; // 다음 대화 인덱스로 진행
    }

    private void ShowNextDialogue()
    {
        if (dialogue == null || currentIndex >= dialogue.Length)
        {
            DisableDialogueUI(); // 대화가 종료되면 UI를 비활성화
            return; // 대화가 종료되었거나 모든 대화를 출력한 경우 더 이상 진행하지 않음
        }

        ShowCurrentDialogue();
    }

    private void DisableDialogueUI()
    {
        dialogueText.text = ""; // 대화 텍스트 초기화
        speakerText.text = ""; // 대화하는 사람 이름 초기화
        image1.SetActive(false); // 이미지1 비활성화
        panel.SetActive(false); // 대화 패널 비활성화

        if (SceneManager.GetActiveScene().name == "Prologue")
        SceneManager.LoadScene("MainScene");

        nextTalkObject.SetActive(false);
        Time.timeScale = 1;

        //GameObject sem = GameObject.Find("SceneEventManager");
        //SceneEventManager sems = sem.GetComponent<SceneEventManager>();
        //sems.ResumePausedScripts();
    }

    private void EnableDialogueUI()
    {
        // 패널 활성화
        panel.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        speakerText.gameObject.SetActive(true);
    }

    public void ChocoEvent1()
    {
        GameObject sem = GameObject.Find("SceneEventManager");
        SceneEventManager sems = sem.GetComponent<SceneEventManager>();
        sems.ResumePausedScripts();
        Debug.Log("초코 이벤트1 실행");
    }
}