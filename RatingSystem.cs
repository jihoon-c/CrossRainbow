using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class RatingSystem : MonoBehaviour
{
    
    public static RatingSystem rs;
    public int nowID;
    MakingManager makingManager;
    public Button button; //�ִ� ��ư ��Ȱ��ȭ ���ؼ�
    //�� ���� ĳ���ͺ� ģ�е�
    public int chocoF=0;
    public int hotdogF = 0;
    public int coockieF = 0;
    public int leoF = 0;
    public int janggunF = 0;

    // �� ��� ����
    public GameObject OrderImage;
    public bool coldness;
    public GameObject iceImg;

    //



    //�� ���� �մ��� ģ�е��� ���� �̻��� ��� �̺�Ʈ ����

    //
    private void Awake()
    {
       
        makingManager = GameObject.Find("cup").GetComponent<MakingManager>();
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameManager.GM.SetFame(100);
            GameManager.GM.SetCoin(1000);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.GM.SetFame(10);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.GM.SetCoin(1000);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {


            if (Time.timeScale == 1)
            {
                Time.timeScale = 5;
            }
            else if (Time.timeScale == 5)
            {
                Time.timeScale = 1;
            }

        }

        

    }





    private void OnTriggerEnter2D(Collider2D other)
    {
        string checkNPC = "NPC";
       

        if (other.CompareTag(checkNPC))
        {
            NormalObjData nod = other.gameObject.GetComponent<NormalObjData>();
            if (nod != null && other.GetComponent<NPC_AI>().eve == false)
            {
                // ���� ���Ḧ ���� npc�� id�� ������
                nowID = nod.id; //nowID�� ���� �� ��ư�� ����
                Debug.Log(nowID);
                Order();
                button.interactable = true;
            }
        }

        else if(other.name == "NPC_Regular_Prefab_1(Clone)")
        {
            nowID = Random.Range(100, 104);          
        }
        else if (other.name == "NPC_Regular_Prefab_2(Clone)")
        {
            nowID = Random.Range(200, 204);
            // �ֵ��� ���̵� ����
        }
        else if (other.name == "NPC_Regular_Prefab_3(Clone)")
        {
            nowID = Random.Range(300, 304);
            // ��Ű�� ���̵� ����
        }
        else if (other.name == "NPC_Regular_Prefab_4(Clone)")
        {
            nowID = Random.Range(300, 304);
            // ������ ���̵� ����
        }
        else if (other.name == "NPC_Regular_Prefab_5(Clone)")
        {
            nowID = Random.Range(400, 404);
            // �屺�� ���̵� ����
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // �浹�� ����� ��ü�� �÷��̾��� ��� ��ư�� �ٽ� ��Ȱ��ȭ�մϴ�.
        if (other.CompareTag("Player"))
        {
            button.interactable = false;
        }
    }


    //ģ�е� ��� �� ��
    public void SetChocoF(int value) { chocoF += value; }
    public int GetChocoF() {return chocoF; }
    public void SetHotdogF(int value) { hotdogF += value; }
    public void SetCoockieF(int value) { coockieF += value; }
    public void SetLeoF(int value) { leoF += value; }
    public void SetJanggunF(int value) { janggunF += value; }


    public void Order()
    {
        RatingSystem rs = GameObject.Find("Rating").GetComponent<RatingSystem>();
        SpriteRenderer sprOrderImage = OrderImage.GetComponent<SpriteRenderer>();
        Transform Transform = OrderImage.GetComponent<Transform>();
        IDsearchParse IDSP = GameObject.Find("Rating").GetComponent<IDsearchParse>();
        IDSP.SearchWant(nowID);
        //ID�� �°� �� ����
        int result = rs.nowID;

        // ���� ��ȯ 
        float originalY = sprOrderImage.transform.localPosition.y; // ���� y ��ġ �� ����
        float lengthRatio = IDSP.needAmount / 100.0f; // IDSP�� needAmount�� 0~1 ������ ������ ��ȯ

        Vector3 originalScale = sprOrderImage.transform.localScale;
        Vector3 newScale = new Vector3(originalScale.x, originalScale.y * (lengthRatio), originalScale.z);
        Vector3 positionOffset = new Vector3(0, (newScale.y) / 2, 0);
        sprOrderImage.transform.localScale = newScale;

        Vector3 newPosition = sprOrderImage.transform.localPosition;
        newPosition.y = originalY - (originalScale.y - newScale.y) / 2; // ���� y ��ġ���� ����� ������ ���ݸ�ŭ �̵�
        sprOrderImage.transform.localPosition = newPosition;

        //�پ�� ���̴� square ������Ʈ�� ��ũ��Ʈ���� ������Ŵ


        //����, ����, ��� �̹��� �߰��Ǵ� ��� �߰� ����

        sprOrderImage.color = IDSP.bubbleColor(result);

        if (IDSP.coldness == false) //������ �ִ� ��� �ӽ�
        {
            iceImg.SetActive(false);
            //sprOrderImage.sprite = Resources.Load<Sprite>("Order/iceLiq");
        }
        else
        {
            iceImg.SetActive(true);
        }
    }



    public void Test_SatisfactionUp()
    {
        GameManager.GM.SetFame(1000);
        GameManager.GM.SetCoin(1000);
    }
}
