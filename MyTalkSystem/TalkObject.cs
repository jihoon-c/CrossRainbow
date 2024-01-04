using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TalkObject : MonoBehaviour
{
    public TMP_Text dialogueText; // ��ȭ ������ ǥ���� TMPro(TextMeshPro)
    public TMP_Text speakerText; // ��ȭ�ϴ� ����� �̸��� ǥ���� TMPro(TextMeshPro)
    public GameObject image1; // �̹���1�� ���� GameObject
    public GameObject panel; // ��ȭ �г��� ���� GameObject
    public GameObject nextTalkObject;

    private DialogueReader dialogueReader; // ��ȭ �����͸� �����ϴ� DialogueReader Ŭ����
    private TalkData[] dialogue; // ���� ��ȭ ������ �迭
    private int currentIndex; // ���� ��ȭ �ε���
    private int currentEventId; // ���� �̺�Ʈ ��ȣ
    public  GameObject eventImg1, eventImg2, eventImg3, eventImg4;

    private void Start()
    {
        dialogueReader = FindObjectOfType<DialogueReader>(); // DialogueReader Ŭ������ ã�ƿ�
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
                    currentIndex = 0; // ù ��° ��ȭ�� �ʱ�ȭ
                    currentEventId = eventId; // ���� �̺�Ʈ ��ȣ ������Ʈ
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
            dialogue = null; // ��ȭ ����
            currentEventId = 0; // ���� �̺�Ʈ ��ȣ �ʱ�ȭ
            DisableDialogueUI(); // ��ȭ�� ����Ǹ� UI�� ��Ȱ��ȭ
            return;
        }

        TalkData currentTalkData = dialogue[currentIndex];
        string speaker = currentTalkData.speaker;
        //string face = currentTalkData.face;
        string[] texts = currentTalkData.text;


        speakerText.text = speaker .Replace("1","").Replace("2", "").Replace("3", "").Replace("4", "");

        // ��ȭ ������ �迭 ���·� ǥ��
        dialogueText.text = string.Join("\n", texts);

        EnableDialogueUI(); // ��ȭ�� ���۵Ǹ� UI�� Ȱ��ȭ


        SpriteRenderer spriteRenderer;
        spriteRenderer = image1.GetComponent<SpriteRenderer>();
        // image1�� ��������Ʈ ������ ���� ����

        // speaker(2���� ����)�� ���� �̹����� Ȱ��ȭ
        if (speaker == "�߿�")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/DogKing");
        }
        else if (speaker == "����")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Loi_S");
        }
        else if (speaker == "����1")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Loi_S_1");
        }
        else if (speaker == "����2")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Loi_S_2");
        }
        else if (speaker == "����3")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Loi_S_3");
        }
        else if (speaker == "����")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S");
        }
        else if (speaker == "����1")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S");
        }
        else if (speaker == "����2")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S_2");
        }
        else if (speaker == "����3")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S_3");
        }
        else if (speaker == "����4")
        {
            image1.SetActive(true);
            spriteRenderer.sprite = Resources.Load<Sprite>("Standing/Choco_S_4");
        }
        else
        {
            // �ٸ� ��쿡�� �̹����� ��� ��Ȱ��ȭ
            image1.SetActive(false);
        }

        //Ư�� �̺�Ʈ ������ �� �� ��� �� �����ؼ� �̺�Ʈ �����Ű�� �˴ϴ�

        //1 
        if (dialogueText.text.Contains("�ӹ��� ���ð� �ִ�"))
        {
            eventImg1.SetActive(true);
        }
        if (dialogueText.text.Contains("�̵��� ������ ������"))
        {
            eventImg1.SetActive(false);
            eventImg2.SetActive(true);
        }
        if (dialogueText.text.Contains("�� ��ȭ��"))
        {
            eventImg2.SetActive(false);
            eventImg4.SetActive(true);
        }
        if (dialogueText.text.Contains("��°�� �̷���"))
        {
            eventImg4.SetActive(false);
        }


        if (dialogueText.text.Contains("������ ���ź�"))
        {
            eventImg3.SetActive(true);
        }
        if (dialogueText.text.Contains("��ø���"))
        {
            eventImg3.SetActive(false);
        }
        /*
           if (dialogueText.text.Contains("��"))
        {
            SceneManager.LoadScene("MainScene");
        }
        */

        //11
        if (dialogueText.text.Contains("�׷��� ���ϴϱ� ��ô�̳�"))
        {
            //ChocoEvent1();
        }

        


        currentIndex++; // ���� ��ȭ �ε����� ����
    }

    private void ShowNextDialogue()
    {
        if (dialogue == null || currentIndex >= dialogue.Length)
        {
            DisableDialogueUI(); // ��ȭ�� ����Ǹ� UI�� ��Ȱ��ȭ
            return; // ��ȭ�� ����Ǿ��ų� ��� ��ȭ�� ����� ��� �� �̻� �������� ����
        }

        ShowCurrentDialogue();
    }

    private void DisableDialogueUI()
    {
        dialogueText.text = ""; // ��ȭ �ؽ�Ʈ �ʱ�ȭ
        speakerText.text = ""; // ��ȭ�ϴ� ��� �̸� �ʱ�ȭ
        image1.SetActive(false); // �̹���1 ��Ȱ��ȭ
        panel.SetActive(false); // ��ȭ �г� ��Ȱ��ȭ

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
        // �г� Ȱ��ȭ
        panel.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        speakerText.gameObject.SetActive(true);
    }

    public void ChocoEvent1()
    {
        GameObject sem = GameObject.Find("SceneEventManager");
        SceneEventManager sems = sem.GetComponent<SceneEventManager>();
        sems.ResumePausedScripts();
        Debug.Log("���� �̺�Ʈ1 ����");
    }
}