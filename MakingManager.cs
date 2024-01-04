using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MakingManager : MonoBehaviour
{
    static public MakingManager mm;

    public TextMeshProUGUI text;//비율과 양을 보여줄 텍스트
    private bool isPouring = false; // 물 부어지는 중인지 체크하는 변수

    private float pouringSpeed = 15f; // 초당 부어지는 물의 양
    public float currentWaterAmount = 0f; // 물컵에 담긴 물의 양

    private float interval = 1f;
    private float timer = 0f;

    public float milkAmount = 0f; //우유의 양
    public float alcholAmount = 0f; //술의 양
    public float juiceAmount = 0f;  // 주스의 양
    public float lemonAmount = 0f; //레몬즙의 양

    //얼음 관련
    public bool PutIce = false;
    public bool coldness = false; //얼음의 유무에 따라 차가움 증가.
    public float iceAmount = 0f; // 얼음
    public GameObject icePrefab; // 생성할 얼음 오브젝트 프리팹
    public Button icebutton; //얼음 버튼
    // 양과 관련없는 토핑
    public bool isLime = false; //라임
    private Button limeButton;
    public bool isSugar = false; //설탕
    private Button sugarButton;
    public GameObject sugarEffect;
    public Transform sugarSpawnPoint;

    public bool isGum = false; //개껌
    private Button gumButton;
    public bool isFeed = false; //사료
    private Button feedButton;

    //레시피 해금 관련
    public Image r1Color, r2Color, r3Color, r4Color, r5Color, r6Color,
        r7Color, r8Color, r9Color, r10Color, r11Color, r12Color; //레시피 색 변경

    public TextMeshProUGUI recipe1Method, recipe2Method, recipe3Method, recipe4Method, recipe5Method, recipe6Method,
        recipe7Method, recipe8Method, recipe9Method, recipe10Method, recipe11Method, recipe12Method; // 제조법 텍스트

    //메인 캐릭터 호감도 
    public TextMeshProUGUI chocoFriendlyText;
    //색 변경 관련
    private Color milkColor = Color.white;
    private Color alcholColor = Color.green;
    private Color juiceColor = Color.red;
    private Color lemonColor = Color.blue;

    // ColorMixer 초기 위치를 저장하는 변수
    public Transform otherObjectTransform;
    private Vector2 initialPosition = Vector2.zero;

    //레시피 관련
    public TextMeshProUGUI recipeText;
    //레시피 성공시 파티클 생성 관련
    public GameObject particlePrefab; // 파티클 프리팹
    public Transform particleSpawnPoint; // 파티클 생성 위치
    private bool isEffectMade = false;
    public GameObject[] iceImg;
    public GameObject[] liqImg;
    public GameObject[] limeImg;
    public GameObject dogfeedImg;
    public GameObject gumImg;
    // 평가 관련
    public TextMeshProUGUI tmpText;
    public GameObject coin;
    public int nowRate;
    public TextMeshProUGUI tmpText2;
    public GameObject friendship;
    private float coinUpdateInterval = 1f; // 코인 업데이트 간격
    private float lastCoinUpdateTime = 0f; // 마지막 코인 업데이트 시간

    // 효과음 관련
    AudioSource audioSoure;
    public AudioClip audioLiquid;
    public AudioClip audioIce;

    private void Awake()
    {
        icebutton = GameObject.Find("iceButton").GetComponent<Button>();
        limeButton = GameObject.Find("limeButton").GetComponent<Button>();
        sugarButton = GameObject.Find("sugarButton").GetComponent<Button>();
        gumButton = GameObject.Find("gumButton").GetComponent<Button>();
        feedButton = GameObject.Find("feedButton").GetComponent<Button>();
        audioSoure = GetComponent<AudioSource>();

        
    }
    void Start()
    {
        initialPosition = otherObjectTransform.position;
        iceAmount = 0;
        //레시피 해금

    }

    void Update()
    {
        timer += Time.deltaTime;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(2, 2), 0);

        // 충돌한 모든 오브젝트를 순회하며 처리
        foreach (Collider2D collider in colliders)
        {
            if (timer >= interval)
            {
                timer = 0f;
            }
            if (isPouring)
            {

                // 태그별로 양이 늘어남
                if (collider.gameObject.name == "Milk(Clone)")
                {
                    milkAmount += pouringSpeed * Time.deltaTime;
                }
                else if (collider.gameObject.name == "Alchol(Clone)")
                {
                    alcholAmount += pouringSpeed * Time.deltaTime;
                }
                else if (collider.gameObject.name == "Juice(Clone)")
                {
                    juiceAmount += pouringSpeed * Time.deltaTime;
                }
                else if (collider.gameObject.name == "Lemon(Clone)")
                {
                    lemonAmount += pouringSpeed * Time.deltaTime;

                }

                if (currentWaterAmount >= 90f)
                {
                    //currentWaterAmount = 100f;
                    //isPouring = false;
                    icebutton.interactable = false;
                }

                if (currentWaterAmount >= 100f)
                {
                    currentWaterAmount = 100f;
                    isPouring = false;
                    audioSoure.Stop();
                }
            }

            WritingAmount();
        }
        currentWaterAmount = milkAmount + alcholAmount + juiceAmount + lemonAmount + iceAmount;
        UpdateColor();
        CheckingRecipe();

        // isPouring이 true이고 경과한 시간이 코인 업데이트 간격을 초과한 경우 코인을 업데이트
        /*
        if (isPouring && Time.time - lastCoinUpdateTime >= coinUpdateInterval)
        {
            lastCoinUpdateTime = Time.time; // 마지막 코인 업데이트 시간 갱신
            GameManager.GM.SetCoin(0);
            
        }
        */
        //음료 제조 코인소모 미사용 예정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 물컵에 닿았을 때, 물 부어지기 시작
        if (collision.gameObject.tag == "ing" || collision.gameObject.tag == "ice")
        {
            if (currentWaterAmount < 100f)
            {
                isPouring = true;
                audioSoure.clip = audioLiquid;
                audioSoure.Play();
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 물컵에서 벗어나면 물 부어지는 것을 멈춘다
        if (collision.gameObject.tag == "ing" || collision.gameObject.tag == "ice")
        {
            isPouring = false;
            audioSoure.Stop();
        }

    }


    private void WritingAmount() //TMPro에 출력하는 함수
    {
        text.text = currentWaterAmount.ToString("0") + "/100" + "\n" + milkAmount.ToString("0") +
            "\n" + alcholAmount.ToString("0") + "\n" + juiceAmount.ToString("0") + "\n" + lemonAmount.ToString("0");
    }

    //색 변경, 양이 늘어날 때마다 길어지는 함수
    private void UpdateColor()
    {
        GameObject targetObject = GameObject.Find("ColorMixer");
        SpriteRenderer targetRenderer = targetObject.GetComponent<SpriteRenderer>();

        float totalAmount = milkAmount + alcholAmount + juiceAmount + lemonAmount;
        if (totalAmount > 0)
        {
            Color mixedColor = milkColor * (milkAmount / totalAmount) +
                alcholColor * (alcholAmount / totalAmount) +
                juiceColor * (juiceAmount / totalAmount) +
                lemonColor * (lemonAmount / totalAmount);
            targetRenderer.color = mixedColor;

            // ColorMixer 오브젝트의 y 크기를 수정하는 코드
            float mixerScale = currentWaterAmount * 0.02f;
            Vector3 originalScale = targetObject.transform.localScale;
            Vector3 newScale = new Vector3(originalScale.x, mixerScale, originalScale.z);
            Vector3 positionOffset = new Vector3(0, (newScale.y - originalScale.y) / 2, 0);
            targetObject.transform.localScale = newScale;
            targetObject.transform.position += positionOffset;
        }

    }

    //ClearButton을 위한 함수
    public void ClearAll()
    {
        // 모든 재료 양을 0으로 초기화
        currentWaterAmount = 0f;
        milkAmount = 0f;
        alcholAmount = 0f;
        juiceAmount = 0f;
        lemonAmount = 0f;
        //얼음 초기화
        iceAmount = 0;
        coldness = false;
        PutIce = false;
        //GameObject go = GameObject.Find("IcePrefab(Clone)");
        Destroy(GameObject.Find("IcePrefab(Clone)"));

        // ColorMixer 오브젝트의 색상을 초기화
        GameObject targetObject = GameObject.Find("ColorMixer");
        targetObject.transform.position = initialPosition;
        SpriteRenderer targetRenderer = targetObject.GetComponent<SpriteRenderer>();
        targetRenderer.color = Color.clear;

        // ColorMixer 오브젝트의 크기를 초기화
        Vector3 originalScale = new Vector3(1.28f, 0.02f, 1);
        targetObject.transform.localScale = originalScale;
        targetObject.transform.position = initialPosition;

        // TMPro 값 초기화
        WritingAmount();

        if (icebutton.interactable == false)
        {
            icebutton.interactable = true;
        }

        if (sugarButton.interactable == false)
        {
            sugarButton.interactable = true;
        }
        isSugar = false;

        if (limeButton.interactable == false)
        {
            limeButton.interactable = true;
        }
        isLime = false;
        GameObject lime = GameObject.Find("lime");
        Transform limeTrans = lime.transform;
        Transform limeImgtrans = limeTrans.Find("limeImg");
        GameObject limeImg = limeImgtrans.gameObject;
        limeImg.SetActive(false);
        limeButton.interactable = true;

        if (gumButton.interactable == false)
        {
            gumButton.interactable = true;
        }
        isGum = false;
        GameObject gum = GameObject.Find("gum");
        Transform gumTrans = gum.transform;
        Transform gumImgtrans = gumTrans.Find("gumImg");
        GameObject gumImg = gumImgtrans.gameObject;
        gumImg.SetActive(false);
        gumButton.interactable = true;

        if (feedButton.interactable == false)
        {
            feedButton.interactable = true;
        }
        isFeed = false;
        GameObject feed = GameObject.Find("feed");
        Transform feedTrans = feed.transform;
        Transform feedImgtrans = feedTrans.Find("feedImg");
        GameObject feedImg = feedImgtrans.gameObject;
        feedImg.SetActive(false);
        feedButton.interactable = true;

    }

    /// <summary>
    /// 평가 시스템
    /// </summary>

    public void JuiceRate()
    {
        int targetID;

        //IDSP에서 읽어온 값을 저장할 변수임
        float val0, val1, val2, val3, val4;

        GameObject rs = GameObject.Find("Rating");
        IDsearchParse IDSP = rs.GetComponent<IDsearchParse>();
        RatingSystem rss = rs.GetComponent<RatingSystem>();
        targetID = rss.nowID;
        IDSP.SearchWant(targetID);

        // 파싱된 값들을 가져옴
        val0 = IDSP.needAmount;
        val1 = IDSP.milkWant;
        val2 = IDSP.juiceWant;
        val3 = IDSP.alcholWant;
        val4 = IDSP.lemonWant;


        // 비율 계산 및 조건 검사
        float errorRange = 0.1f;

        // 비율 값들
        float ratio1 = val1 / (val1 + val2 + val3 + val4);
        float ratio2 = val2 / (val1 + val2 + val3 + val4);
        float ratio3 = val3 / (val1 + val2 + val3 + val4);
        float ratio4 = val4 / (val1 + val2 + val3 + val4);

        // milkAmount, juiceAmount, alcholAmount, lemonAmount의 비율 계산
        float totalAmount = milkAmount + juiceAmount + alcholAmount + lemonAmount;
        float ratioM = milkAmount / totalAmount;
        float ratioJ = juiceAmount / totalAmount;
        float ratioA = alcholAmount / totalAmount;
        float ratioL = lemonAmount / totalAmount;

        if (Mathf.Abs(ratio1 - ratioM) <= errorRange
            && Mathf.Abs(ratio2 - ratioJ) <= errorRange
            && Mathf.Abs(ratio3 - ratioA) <= errorRange
            && Mathf.Abs(ratio4 - ratioL) <= errorRange
            && Mathf.Abs(val0 - totalAmount) <= 6f// 음료의 양이 val0과 10% 오차범위 내에 있는지 검사
            && IDSP.coldness == coldness
            && IDSP.isLime == isLime
            && IDSP.isSugar == isSugar
            && IDSP.isGum == isGum
            && IDSP.isFeed == isFeed)
        {
            coin.SetActive(true);
            tmpText.text = "+" + 100.ToString();
            GameManager.GM.SetCoin(100);
            //단골이면 친밀도 증가
            //GameManager.GM.SetFame(4);
            nowRate = 100;
            if (rss.nowID > 99 && rss.nowID < 200) //초코인 경우 만족도 증가(초코 호감도가 증가해 아이디가 변해도 
            {
                friendship.SetActive(true);
                tmpText2.text = "+" + 4.ToString();
                rss.SetChocoF(4);
                chocoFriendlyText.text = "초코 \n 호감도 :" + rss.GetChocoF();
            }
        }
        else if (Mathf.Abs(ratio1 - ratioM) <= errorRange
            && Mathf.Abs(ratio2 - ratioJ) <= errorRange
            && Mathf.Abs(ratio3 - ratioA) <= errorRange
            && Mathf.Abs(ratio4 - ratioL) <= errorRange
           )
        {
            coin.SetActive(true);
            tmpText.text = "+" + 50.ToString();
            nowRate = 50;
            GameManager.GM.SetCoin(50);
            if (rss.nowID > 99 && rss.nowID < 200) //초코인 경우 만족도 증가(초코 호감도가 증가해 아이디가 변해도 
            {
                friendship.SetActive(true);
                tmpText2.text = "+" + 2.ToString();
                rss.SetChocoF(2);
                chocoFriendlyText.text = "초코 \n 호감도 :" + rss.GetChocoF();
            }
        }
        else if (Mathf.Abs(val0 - totalAmount) <= 6f)

        {
            coin.SetActive(true);
            tmpText.text = "+" + 20.ToString();
            nowRate = 20;
            GameManager.GM.SetCoin(20);
            if (rss.nowID > 99 && rss.nowID < 200) //초코인 경우 만족도 증가(초코 호감도가 증가해 아이디가 변해도 
            {
                friendship.SetActive(true);
                tmpText2.text = "+" + 1.ToString();
                rss.SetChocoF(1);
                chocoFriendlyText.text = "초코 \n 호감도 :" + rss.GetChocoF();
            }
        }
        else
        {
            coin.SetActive(true);
            tmpText.text = "+" + 0.ToString();
            nowRate = 0;
            if (rss.nowID > 99 && rss.nowID < 200) //초코인 경우 만족도 증가(초코 호감도가 증가해 아이디가 변해도 
            {
                friendship.SetActive(true);
                tmpText2.text = "+" + 0.ToString();

            }
        }

        StartCoroutine(ResetTextAfterDelay(tmpText, 2.0f));
    }



    private IEnumerator ResetTextAfterDelay(TextMeshProUGUI tmpText, float delay)
    {
        yield return new WaitForSeconds(delay);

        tmpText.text = string.Empty; // 또는 다시 초기화할 텍스트를 설정
        coin.SetActive(false);
        friendship.SetActive(false);
        tmpText2.text = string.Empty;
        // 다시 초기화된 텍스트를 표시하고 싶다면 아래의 코드를 추가
        tmpText.gameObject.SetActive(true);
    }


    // 레시피 조사 함수
    private Dictionary<string, bool> recipeSuccessMap = new Dictionary<string, bool>(); //레시피 최초발생여부
    
    void CheckingRecipe()
    {
        // 각 재료마다의 비율 계산
        float total = milkAmount + alcholAmount + juiceAmount + lemonAmount;
        float milkRatio = milkAmount / total;
        float alcoholRatio = alcholAmount / total;
        float juiceRatio = juiceAmount / total;
        float lemonRatio = lemonAmount / total;

        // 각 재료 비율의 오차 범위를 10%로 지정
        float errorRange = 0.1f;

        // 얼음 유무를 나타내는 bool 변수
        bool hasIce = iceAmount > 0;

        if (total > 20)
        {
            // 레시피 제조법에 대하여
            if (Mathf.Abs(milkRatio - 1f / 7f) <= errorRange
                && Mathf.Abs(alcoholRatio - 1f / 7f) <= errorRange
                && Mathf.Abs(juiceRatio - 5f / 7f) <= errorRange
                && Mathf.Abs(lemonRatio) <= errorRange
                && !hasIce)
            {
                recipeText.text = "레드 아키타";
                if (!recipeSuccessMap.ContainsKey("레드 아키타")) // 해당 레시피의 첫 번째 성공 시에만 이펙트 생성
                {
                    MakeEffect();
                    recipeSuccessMap.Add("레드 아키타", true); // 해당 레시피의 첫 번째 성공 여부를 true로 변경

                }

                SpriteRenderer spriteRenderer = iceImg[0].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r1Color.color = Color.white;
                SpriteRenderer sprite = liqImg[0].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(1,1,5,0); // 빨강색

                recipe1Method.text = "레드 아키타\n넥타르 1: 주스 5: 우유 1+얼음";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
                && Mathf.Abs(alcoholRatio - 1f / 3f) <= errorRange
                && Mathf.Abs(juiceRatio - 2f / 3f) <= errorRange
                && Mathf.Abs(lemonRatio) <= errorRange
                && hasIce)
            {
                recipeText.text = "스크루 리트리버";
                if (!recipeSuccessMap.ContainsKey("스크루 리트리버")) // 해당 레시피의 첫 번째 성공 시에만 이펙트 생성
                {
                    MakeEffect();
                    recipeSuccessMap.Add("스크루 리트리버", true); // 해당 레시피의 첫 번째 성공 여부를 true로 변경
                }

                SpriteRenderer spriteRenderer = iceImg[1].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r2Color.color = Color.white;

                SpriteRenderer sprite = liqImg[1].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 1, 2, 0);

                recipe2Method.text = "스크루 리트리버\n넥타르1:주스2+얼음";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
                && Mathf.Abs(alcoholRatio - 3f / 6f) <= errorRange
                && Mathf.Abs(juiceRatio - 2f / 6f) <= errorRange
                && Mathf.Abs(lemonRatio - 1f / 6f) <= errorRange
                && !hasIce)
            {
                recipeText.text = "치마요크셔 테리어";

                if (!recipeSuccessMap.ContainsKey("치마요크셔 테리어"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("치마요크셔 테리어", true);
                }

                r3Color.color = Color.white;

                SpriteRenderer sprite = liqImg[2].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 3, 2, 1);

                recipe3Method.text = "치마요크셔 테리어\n넥타르3:주스2:탄산수1";
            }
            else if (Mathf.Abs(milkRatio - 2f / 4f) <= errorRange
                && Mathf.Abs(alcoholRatio - 1f / 4f) <= errorRange
                && Mathf.Abs(juiceRatio - 1f / 4f) <= errorRange
                && Mathf.Abs(lemonRatio) <= errorRange
                && hasIce)
            {
                recipeText.text = "피나콜리";
                if (!recipeSuccessMap.ContainsKey("피나콜리"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("피나콜리", true);
                }
                SpriteRenderer spriteRenderer = iceImg[2].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r4Color.color = Color.white;

                SpriteRenderer sprite = liqImg[3].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(2, 1, 1, 0);
                recipe4Method.text = "피나콜리\n넥타르1:쥬스1:우유2 + 얼음";
            }
            else if (Mathf.Abs(milkRatio - 1f / 6f) <= errorRange
                && Mathf.Abs(alcoholRatio - 5f / 6f) <= errorRange
                && Mathf.Abs(juiceRatio) <= errorRange
                && Mathf.Abs(lemonRatio) <= errorRange
                && hasIce)
            {
                recipeText.text = "러스티베탄 테리어";
                if (!recipeSuccessMap.ContainsKey("러스티베탄 테리어"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("러스티베탄 테리어", true);
                }
                SpriteRenderer spriteRenderer = iceImg[3].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r5Color.color = Color.white;

                SpriteRenderer sprite = liqImg[4].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(1, 5, 0, 0);
                recipe5Method.text = "러스티베탄 테리어\n넥타르5:우유1+얼음";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
               && Mathf.Abs(alcoholRatio - 1f / 4f) <= errorRange
               && Mathf.Abs(juiceRatio) <= errorRange
               && Mathf.Abs(lemonRatio - 3f / 4f) <= errorRange
               && hasIce && isLime)
            {
                recipeText.text = "하이 불도그";
                if (!recipeSuccessMap.ContainsKey("하이 불도그"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("하이 불도그", true);
                }
                //라임
                SpriteRenderer Renderer = limeImg[0].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer spriteRenderer = iceImg[4].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r6Color.color = Color.white;

                SpriteRenderer sprite = liqImg[5].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 1, 0, 3);
                recipe6Method.text = "하이 불도그\n넥타르1:탄산수3+얼음+라임";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
             && Mathf.Abs(alcoholRatio - 3f / 4f) <= errorRange
             && Mathf.Abs(juiceRatio) <= errorRange
             && Mathf.Abs(lemonRatio - 1f / 4f) <= errorRange
             && hasIce && isLime && isSugar)
            {
                recipeText.text = "레몬 드롭 말티즈";
                if (!recipeSuccessMap.ContainsKey("레몬 드롭 말티즈"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("레몬 드롭 말티즈", true);

                }
                SpriteRenderer Renderer = limeImg[1].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer spriteRenderer = iceImg[5].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r7Color.color = Color.white;

                SpriteRenderer sprite = liqImg[6].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 3, 0, 1);
                recipe7Method.text = "레몬 드롭 말티즈\n넥타르3:탄산수1+얼음+라임+설탕";
            }

            else if (Mathf.Abs(milkRatio) <= errorRange
            && Mathf.Abs(alcoholRatio - 4f / 5f) <= errorRange
            && Mathf.Abs(juiceRatio - 1f / 5f) <= errorRange
            && Mathf.Abs(lemonRatio) <= errorRange
             && isLime)
            {
                recipeText.text = "보드카 말티즈";
                if (!recipeSuccessMap.ContainsKey("보드카 말티즈"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("보드카 말티즈", true);
                }
                SpriteRenderer Renderer = limeImg[2].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                r8Color.color = Color.white;

                SpriteRenderer sprite = liqImg[7].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 4, 1, 0);
                recipe8Method.text = "보드카 말티즈\n넥타르4:주스1+라임";
            }
            else if (Mathf.Abs(milkRatio - 1f / 6f) <= errorRange
             && Mathf.Abs(alcoholRatio - 2f / 6f) <= errorRange
            && Mathf.Abs(juiceRatio - 3f / 6f) <= errorRange
             && Mathf.Abs(lemonRatio) <= errorRange
             && isSugar)
            {
                recipeText.text = "시추 온더 비치";
                if (!recipeSuccessMap.ContainsKey("시추 온더 비치"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("시추 온더 비치", true);
                }
                r9Color.color = Color.white;

                SpriteRenderer sprite = liqImg[8].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(1, 2, 3, 0);
                recipe9Method.text = "시추 온더 비치\n우유1:넥타르2:주스3+설탕";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
            && Mathf.Abs(alcoholRatio - 1f / 5f) <= errorRange
           && Mathf.Abs(juiceRatio - 1f / 5f) <= errorRange
            && Mathf.Abs(lemonRatio - 3f / 5f) <= errorRange
            && hasIce && isLime)
            {
                recipeText.text = "미도리 사모예드";
                if (!recipeSuccessMap.ContainsKey("미도리 사모예드"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("미도리 사모예드", true);
                }
                SpriteRenderer Renderer = limeImg[3].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer spriteRenderer = iceImg[6].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r10Color.color = Color.white;

                SpriteRenderer sprite = liqImg[9].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 1, 1, 3); recipe10Method.text = "미도리 사모예드\n넥타르1:주스1:탄산수3 + 얼음+라임";
            }
            else if (Mathf.Abs(milkRatio - 1f / 8f) <= errorRange
            && Mathf.Abs(alcoholRatio - 3f / 8f) <= errorRange
           && Mathf.Abs(juiceRatio - 1f / 8f) <= errorRange
            && Mathf.Abs(lemonRatio - 3f / 8f) <= errorRange
            && hasIce && isLime && isGum && isSugar)
            {
                recipeText.text = "롱 아일랜드 아프간 하운드";
                if (!recipeSuccessMap.ContainsKey("롱 아일랜드 아프간 하운드"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("롱 아일랜드 아프간 하운드", true);
                }
                SpriteRenderer Renderer = limeImg[4].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer gumR = gumImg.GetComponent<SpriteRenderer>();
                gumR.color = Color.white;

                SpriteRenderer spriteRenderer = iceImg[7].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r11Color.color = Color.white;

                SpriteRenderer sprite = liqImg[10].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(1, 3, 1, 3);
                recipe11Method.text = "롱 아일랜드 아프간 하운드\n우유1:넥타르3:주스1:탄산수3+얼음+라임+설탕+개껌";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
            && Mathf.Abs(alcoholRatio - 2f / 8f) <= errorRange
             && Mathf.Abs(juiceRatio - 1f / 8f) <= errorRange
             && Mathf.Abs(lemonRatio - 5f / 8f) <= errorRange
             && isFeed && isLime)
            {
                recipeText.text = "쿠바 아메리칸 볼리브레";
                if (!recipeSuccessMap.ContainsKey("쿠바 아메리칸 볼리브레"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("쿠바 아메리칸 볼리브레", true);
                }
                SpriteRenderer Renderer = limeImg[5].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer feedR = dogfeedImg.GetComponent<SpriteRenderer>();
                feedR.color = Color.white;

                r12Color.color = Color.white;

                SpriteRenderer sprite = liqImg[11].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 2, 1, 5);
                recipe12Method.text = "쿠바 아메리칸 볼리브레\n넥타르2:주스1:탄산수5+라임+사료";
            }
            else
            {
                recipeText.text = "";
                DestroyEffect();
            }
        }
        else
        {
            recipeText.text = "";
            DestroyEffect();
        }
    }

    public Color RecipeMixColors(float milkWeight, float alcoholWeight, float juiceWeight, float lemonWeight)
    {
        Color milkColor = Color.white;
        Color alcoholColor = Color.green;
        Color juiceColor = Color.red;
        Color lemonColor = Color.blue;

        float totalValue = milkWeight + alcoholWeight + juiceWeight + lemonWeight;

        float weight1 = milkWeight / totalValue;
        float weight2 = alcoholWeight / totalValue;
        float weight3 = juiceWeight / totalValue;
        float weight4 = lemonWeight / totalValue;
        // 각 가중치에 따라 색상을 섞음
        Color mixedColor = weight1 * milkColor + weight2 * alcoholColor + weight3 * juiceColor + weight4 * lemonColor;

        return mixedColor;
    }

    //손님이 원하는 것과 비교하는 함수
    /*private void RateJuice()
    {
        float funiturePrice = GameManager.GM.GetFurniturePrice();
    }
    */

    void MakeEffect()
    {
        if (!isEffectMade)
        {
            Instantiate(particlePrefab, particleSpawnPoint.position, particleSpawnPoint.rotation);
            isEffectMade = true;
        }

    }
    void DestroyEffect()
    {
        GameObject partEffect = GameObject.Find("RecipeSucess(Clone)");
        // 저장된 GameObject를 이용하여 파티클 프리팹 삭제
        Destroy(partEffect);
        isEffectMade = false;
    }

    public void CreateIce() //얼음 함수
    {


        if (PutIce == false)
        {
            // 얼음 생성
            audioSoure.clip = audioIce;
            audioSoure.Play();

            Vector3 spawnPos = new Vector3(5f, -2.5f, 0f);
            Quaternion spawnRot = new Quaternion(0f, 0f, -20f, 0f);
            GameObject ice = Instantiate(icePrefab, spawnPos, spawnRot);
            PutIce = true;
            iceAmount += 10;
            coldness = true;
            UpdateColor();
        }
        else Debug.Log("얼음은 이미 있습니다");

        UpdateColor();
    }

    public void PutSugar() // 설탕 추가
    {
        isSugar = true;
        Instantiate(sugarEffect, sugarSpawnPoint.position, sugarSpawnPoint.rotation);
        sugarButton.interactable = false;
    }
    public void PutLime()
    {
        //비활성화된 오브젝트를 활성화 하기 위해서 자식오브젝트로 만들고 Find함
        isLime = true;
        GameObject lime = GameObject.Find("lime");
        Transform limeTrans = lime.transform;
        Transform limeImgtrans = limeTrans.Find("limeImg");
        GameObject limeImg = limeImgtrans.gameObject;
        limeImg.SetActive(true);
        limeButton.interactable = false;
    }
    public void PutGum()
    {
        //비활성화된 오브젝트를 활성화 하기 위해서 자식오브젝트로 만들고 Find함
        isGum = true;
        GameObject gum = GameObject.Find("gum");
        Transform gumTrans = gum.transform;
        Transform gumImgtrans = gumTrans.Find("gumImg");
        GameObject gumImg = gumImgtrans.gameObject;
        gumImg.SetActive(true);
        gumButton.interactable = false;
    }

    public void PutFeed()
    {
        //비활성화된 오브젝트를 활성화 하기 위해서 자식오브젝트로 만들고 Find함
        isFeed = true;
        GameObject feed = GameObject.Find("feed");
        Transform feedTrans = feed.transform;
        Transform feedImgtrans = feedTrans.Find("feedImg");
        GameObject feedImg = feedImgtrans.gameObject;
        feedImg.SetActive(true);
        feedButton.interactable = false;
    }

}
