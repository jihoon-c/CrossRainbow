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
    public Button button; //주는 버튼 비활성화 위해서
    //각 메인 캐릭터별 친밀도
    public int chocoF=0;
    public int hotdogF = 0;
    public int coockieF = 0;
    public int leoF = 0;
    public int janggunF = 0;

    // 평가 출력 관련
    public GameObject OrderImage;
    public bool coldness;
    public GameObject iceImg;

    //



    //각 메인 손님의 친밀도고 일정 이상일 경우 이벤트 수행

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
                // 현재 음료를 받을 npc의 id를 가져옴
                nowID = nod.id; //nowID를 음료 평가 버튼에 넣음
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
            // 핫독의 아이디 저장
        }
        else if (other.name == "NPC_Regular_Prefab_3(Clone)")
        {
            nowID = Random.Range(300, 304);
            // 쿠키의 아이디 저장
        }
        else if (other.name == "NPC_Regular_Prefab_4(Clone)")
        {
            nowID = Random.Range(300, 304);
            // 레오의 아이디 저장
        }
        else if (other.name == "NPC_Regular_Prefab_5(Clone)")
        {
            nowID = Random.Range(400, 404);
            // 장군의 아이디 저장
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // 충돌이 종료된 객체가 플레이어인 경우 버튼을 다시 비활성화합니다.
        if (other.CompareTag("Player"))
        {
            button.interactable = false;
        }
    }


    //친밀도 상승 할 때
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
        //ID에 맞게 색 변경
        int result = rs.nowID;

        // 길이 변환 
        float originalY = sprOrderImage.transform.localPosition.y; // 원래 y 위치 값 저장
        float lengthRatio = IDSP.needAmount / 100.0f; // IDSP의 needAmount를 0~1 사이의 비율로 변환

        Vector3 originalScale = sprOrderImage.transform.localScale;
        Vector3 newScale = new Vector3(originalScale.x, originalScale.y * (lengthRatio), originalScale.z);
        Vector3 positionOffset = new Vector3(0, (newScale.y) / 2, 0);
        sprOrderImage.transform.localScale = newScale;

        Vector3 newPosition = sprOrderImage.transform.localPosition;
        newPosition.y = originalY - (originalScale.y - newScale.y) / 2; // 원래 y 위치에서 변경된 길이의 절반만큼 이동
        sprOrderImage.transform.localPosition = newPosition;

        //줄어든 길이는 square 오브젝트의 스크립트에서 복구시킴


        //라임, 개껌, 사료 이미지 추가되는 대로 추가 예정

        sprOrderImage.color = IDSP.bubbleColor(result);

        if (IDSP.coldness == false) //얼음이 있는 경우 임시
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
