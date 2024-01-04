using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MakingManager : MonoBehaviour
{
    static public MakingManager mm;

    public TextMeshProUGUI text;//������ ���� ������ �ؽ�Ʈ
    private bool isPouring = false; // �� �ξ����� ������ üũ�ϴ� ����

    private float pouringSpeed = 15f; // �ʴ� �ξ����� ���� ��
    public float currentWaterAmount = 0f; // ���ſ� ��� ���� ��

    private float interval = 1f;
    private float timer = 0f;

    public float milkAmount = 0f; //������ ��
    public float alcholAmount = 0f; //���� ��
    public float juiceAmount = 0f;  // �ֽ��� ��
    public float lemonAmount = 0f; //�������� ��

    //���� ����
    public bool PutIce = false;
    public bool coldness = false; //������ ������ ���� ������ ����.
    public float iceAmount = 0f; // ����
    public GameObject icePrefab; // ������ ���� ������Ʈ ������
    public Button icebutton; //���� ��ư
    // ��� ���þ��� ����
    public bool isLime = false; //����
    private Button limeButton;
    public bool isSugar = false; //����
    private Button sugarButton;
    public GameObject sugarEffect;
    public Transform sugarSpawnPoint;

    public bool isGum = false; //����
    private Button gumButton;
    public bool isFeed = false; //���
    private Button feedButton;

    //������ �ر� ����
    public Image r1Color, r2Color, r3Color, r4Color, r5Color, r6Color,
        r7Color, r8Color, r9Color, r10Color, r11Color, r12Color; //������ �� ����

    public TextMeshProUGUI recipe1Method, recipe2Method, recipe3Method, recipe4Method, recipe5Method, recipe6Method,
        recipe7Method, recipe8Method, recipe9Method, recipe10Method, recipe11Method, recipe12Method; // ������ �ؽ�Ʈ

    //���� ĳ���� ȣ���� 
    public TextMeshProUGUI chocoFriendlyText;
    //�� ���� ����
    private Color milkColor = Color.white;
    private Color alcholColor = Color.green;
    private Color juiceColor = Color.red;
    private Color lemonColor = Color.blue;

    // ColorMixer �ʱ� ��ġ�� �����ϴ� ����
    public Transform otherObjectTransform;
    private Vector2 initialPosition = Vector2.zero;

    //������ ����
    public TextMeshProUGUI recipeText;
    //������ ������ ��ƼŬ ���� ����
    public GameObject particlePrefab; // ��ƼŬ ������
    public Transform particleSpawnPoint; // ��ƼŬ ���� ��ġ
    private bool isEffectMade = false;
    public GameObject[] iceImg;
    public GameObject[] liqImg;
    public GameObject[] limeImg;
    public GameObject dogfeedImg;
    public GameObject gumImg;
    // �� ����
    public TextMeshProUGUI tmpText;
    public GameObject coin;
    public int nowRate;
    public TextMeshProUGUI tmpText2;
    public GameObject friendship;
    private float coinUpdateInterval = 1f; // ���� ������Ʈ ����
    private float lastCoinUpdateTime = 0f; // ������ ���� ������Ʈ �ð�

    // ȿ���� ����
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
        //������ �ر�

    }

    void Update()
    {
        timer += Time.deltaTime;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(2, 2), 0);

        // �浹�� ��� ������Ʈ�� ��ȸ�ϸ� ó��
        foreach (Collider2D collider in colliders)
        {
            if (timer >= interval)
            {
                timer = 0f;
            }
            if (isPouring)
            {

                // �±׺��� ���� �þ
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

        // isPouring�� true�̰� ����� �ð��� ���� ������Ʈ ������ �ʰ��� ��� ������ ������Ʈ
        /*
        if (isPouring && Time.time - lastCoinUpdateTime >= coinUpdateInterval)
        {
            lastCoinUpdateTime = Time.time; // ������ ���� ������Ʈ �ð� ����
            GameManager.GM.SetCoin(0);
            
        }
        */
        //���� ���� ���μҸ� �̻�� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���ſ� ����� ��, �� �ξ����� ����
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
        // ���ſ��� ����� �� �ξ����� ���� �����
        if (collision.gameObject.tag == "ing" || collision.gameObject.tag == "ice")
        {
            isPouring = false;
            audioSoure.Stop();
        }

    }


    private void WritingAmount() //TMPro�� ����ϴ� �Լ�
    {
        text.text = currentWaterAmount.ToString("0") + "/100" + "\n" + milkAmount.ToString("0") +
            "\n" + alcholAmount.ToString("0") + "\n" + juiceAmount.ToString("0") + "\n" + lemonAmount.ToString("0");
    }

    //�� ����, ���� �þ ������ ������� �Լ�
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

            // ColorMixer ������Ʈ�� y ũ�⸦ �����ϴ� �ڵ�
            float mixerScale = currentWaterAmount * 0.02f;
            Vector3 originalScale = targetObject.transform.localScale;
            Vector3 newScale = new Vector3(originalScale.x, mixerScale, originalScale.z);
            Vector3 positionOffset = new Vector3(0, (newScale.y - originalScale.y) / 2, 0);
            targetObject.transform.localScale = newScale;
            targetObject.transform.position += positionOffset;
        }

    }

    //ClearButton�� ���� �Լ�
    public void ClearAll()
    {
        // ��� ��� ���� 0���� �ʱ�ȭ
        currentWaterAmount = 0f;
        milkAmount = 0f;
        alcholAmount = 0f;
        juiceAmount = 0f;
        lemonAmount = 0f;
        //���� �ʱ�ȭ
        iceAmount = 0;
        coldness = false;
        PutIce = false;
        //GameObject go = GameObject.Find("IcePrefab(Clone)");
        Destroy(GameObject.Find("IcePrefab(Clone)"));

        // ColorMixer ������Ʈ�� ������ �ʱ�ȭ
        GameObject targetObject = GameObject.Find("ColorMixer");
        targetObject.transform.position = initialPosition;
        SpriteRenderer targetRenderer = targetObject.GetComponent<SpriteRenderer>();
        targetRenderer.color = Color.clear;

        // ColorMixer ������Ʈ�� ũ�⸦ �ʱ�ȭ
        Vector3 originalScale = new Vector3(1.28f, 0.02f, 1);
        targetObject.transform.localScale = originalScale;
        targetObject.transform.position = initialPosition;

        // TMPro �� �ʱ�ȭ
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
    /// �� �ý���
    /// </summary>

    public void JuiceRate()
    {
        int targetID;

        //IDSP���� �о�� ���� ������ ������
        float val0, val1, val2, val3, val4;

        GameObject rs = GameObject.Find("Rating");
        IDsearchParse IDSP = rs.GetComponent<IDsearchParse>();
        RatingSystem rss = rs.GetComponent<RatingSystem>();
        targetID = rss.nowID;
        IDSP.SearchWant(targetID);

        // �Ľ̵� ������ ������
        val0 = IDSP.needAmount;
        val1 = IDSP.milkWant;
        val2 = IDSP.juiceWant;
        val3 = IDSP.alcholWant;
        val4 = IDSP.lemonWant;


        // ���� ��� �� ���� �˻�
        float errorRange = 0.1f;

        // ���� ����
        float ratio1 = val1 / (val1 + val2 + val3 + val4);
        float ratio2 = val2 / (val1 + val2 + val3 + val4);
        float ratio3 = val3 / (val1 + val2 + val3 + val4);
        float ratio4 = val4 / (val1 + val2 + val3 + val4);

        // milkAmount, juiceAmount, alcholAmount, lemonAmount�� ���� ���
        float totalAmount = milkAmount + juiceAmount + alcholAmount + lemonAmount;
        float ratioM = milkAmount / totalAmount;
        float ratioJ = juiceAmount / totalAmount;
        float ratioA = alcholAmount / totalAmount;
        float ratioL = lemonAmount / totalAmount;

        if (Mathf.Abs(ratio1 - ratioM) <= errorRange
            && Mathf.Abs(ratio2 - ratioJ) <= errorRange
            && Mathf.Abs(ratio3 - ratioA) <= errorRange
            && Mathf.Abs(ratio4 - ratioL) <= errorRange
            && Mathf.Abs(val0 - totalAmount) <= 6f// ������ ���� val0�� 10% �������� ���� �ִ��� �˻�
            && IDSP.coldness == coldness
            && IDSP.isLime == isLime
            && IDSP.isSugar == isSugar
            && IDSP.isGum == isGum
            && IDSP.isFeed == isFeed)
        {
            coin.SetActive(true);
            tmpText.text = "+" + 100.ToString();
            GameManager.GM.SetCoin(100);
            //�ܰ��̸� ģ�е� ����
            //GameManager.GM.SetFame(4);
            nowRate = 100;
            if (rss.nowID > 99 && rss.nowID < 200) //������ ��� ������ ����(���� ȣ������ ������ ���̵� ���ص� 
            {
                friendship.SetActive(true);
                tmpText2.text = "+" + 4.ToString();
                rss.SetChocoF(4);
                chocoFriendlyText.text = "���� \n ȣ���� :" + rss.GetChocoF();
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
            if (rss.nowID > 99 && rss.nowID < 200) //������ ��� ������ ����(���� ȣ������ ������ ���̵� ���ص� 
            {
                friendship.SetActive(true);
                tmpText2.text = "+" + 2.ToString();
                rss.SetChocoF(2);
                chocoFriendlyText.text = "���� \n ȣ���� :" + rss.GetChocoF();
            }
        }
        else if (Mathf.Abs(val0 - totalAmount) <= 6f)

        {
            coin.SetActive(true);
            tmpText.text = "+" + 20.ToString();
            nowRate = 20;
            GameManager.GM.SetCoin(20);
            if (rss.nowID > 99 && rss.nowID < 200) //������ ��� ������ ����(���� ȣ������ ������ ���̵� ���ص� 
            {
                friendship.SetActive(true);
                tmpText2.text = "+" + 1.ToString();
                rss.SetChocoF(1);
                chocoFriendlyText.text = "���� \n ȣ���� :" + rss.GetChocoF();
            }
        }
        else
        {
            coin.SetActive(true);
            tmpText.text = "+" + 0.ToString();
            nowRate = 0;
            if (rss.nowID > 99 && rss.nowID < 200) //������ ��� ������ ����(���� ȣ������ ������ ���̵� ���ص� 
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

        tmpText.text = string.Empty; // �Ǵ� �ٽ� �ʱ�ȭ�� �ؽ�Ʈ�� ����
        coin.SetActive(false);
        friendship.SetActive(false);
        tmpText2.text = string.Empty;
        // �ٽ� �ʱ�ȭ�� �ؽ�Ʈ�� ǥ���ϰ� �ʹٸ� �Ʒ��� �ڵ带 �߰�
        tmpText.gameObject.SetActive(true);
    }


    // ������ ���� �Լ�
    private Dictionary<string, bool> recipeSuccessMap = new Dictionary<string, bool>(); //������ ���ʹ߻�����
    
    void CheckingRecipe()
    {
        // �� ��Ḷ���� ���� ���
        float total = milkAmount + alcholAmount + juiceAmount + lemonAmount;
        float milkRatio = milkAmount / total;
        float alcoholRatio = alcholAmount / total;
        float juiceRatio = juiceAmount / total;
        float lemonRatio = lemonAmount / total;

        // �� ��� ������ ���� ������ 10%�� ����
        float errorRange = 0.1f;

        // ���� ������ ��Ÿ���� bool ����
        bool hasIce = iceAmount > 0;

        if (total > 20)
        {
            // ������ �������� ���Ͽ�
            if (Mathf.Abs(milkRatio - 1f / 7f) <= errorRange
                && Mathf.Abs(alcoholRatio - 1f / 7f) <= errorRange
                && Mathf.Abs(juiceRatio - 5f / 7f) <= errorRange
                && Mathf.Abs(lemonRatio) <= errorRange
                && !hasIce)
            {
                recipeText.text = "���� ��ŰŸ";
                if (!recipeSuccessMap.ContainsKey("���� ��ŰŸ")) // �ش� �������� ù ��° ���� �ÿ��� ����Ʈ ����
                {
                    MakeEffect();
                    recipeSuccessMap.Add("���� ��ŰŸ", true); // �ش� �������� ù ��° ���� ���θ� true�� ����

                }

                SpriteRenderer spriteRenderer = iceImg[0].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r1Color.color = Color.white;
                SpriteRenderer sprite = liqImg[0].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(1,1,5,0); // ������

                recipe1Method.text = "���� ��ŰŸ\n��Ÿ�� 1: �ֽ� 5: ���� 1+����";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
                && Mathf.Abs(alcoholRatio - 1f / 3f) <= errorRange
                && Mathf.Abs(juiceRatio - 2f / 3f) <= errorRange
                && Mathf.Abs(lemonRatio) <= errorRange
                && hasIce)
            {
                recipeText.text = "��ũ�� ��Ʈ����";
                if (!recipeSuccessMap.ContainsKey("��ũ�� ��Ʈ����")) // �ش� �������� ù ��° ���� �ÿ��� ����Ʈ ����
                {
                    MakeEffect();
                    recipeSuccessMap.Add("��ũ�� ��Ʈ����", true); // �ش� �������� ù ��° ���� ���θ� true�� ����
                }

                SpriteRenderer spriteRenderer = iceImg[1].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r2Color.color = Color.white;

                SpriteRenderer sprite = liqImg[1].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 1, 2, 0);

                recipe2Method.text = "��ũ�� ��Ʈ����\n��Ÿ��1:�ֽ�2+����";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
                && Mathf.Abs(alcoholRatio - 3f / 6f) <= errorRange
                && Mathf.Abs(juiceRatio - 2f / 6f) <= errorRange
                && Mathf.Abs(lemonRatio - 1f / 6f) <= errorRange
                && !hasIce)
            {
                recipeText.text = "ġ����ũ�� �׸���";

                if (!recipeSuccessMap.ContainsKey("ġ����ũ�� �׸���"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("ġ����ũ�� �׸���", true);
                }

                r3Color.color = Color.white;

                SpriteRenderer sprite = liqImg[2].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 3, 2, 1);

                recipe3Method.text = "ġ����ũ�� �׸���\n��Ÿ��3:�ֽ�2:ź���1";
            }
            else if (Mathf.Abs(milkRatio - 2f / 4f) <= errorRange
                && Mathf.Abs(alcoholRatio - 1f / 4f) <= errorRange
                && Mathf.Abs(juiceRatio - 1f / 4f) <= errorRange
                && Mathf.Abs(lemonRatio) <= errorRange
                && hasIce)
            {
                recipeText.text = "�ǳ��ݸ�";
                if (!recipeSuccessMap.ContainsKey("�ǳ��ݸ�"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("�ǳ��ݸ�", true);
                }
                SpriteRenderer spriteRenderer = iceImg[2].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r4Color.color = Color.white;

                SpriteRenderer sprite = liqImg[3].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(2, 1, 1, 0);
                recipe4Method.text = "�ǳ��ݸ�\n��Ÿ��1:�꽺1:����2 + ����";
            }
            else if (Mathf.Abs(milkRatio - 1f / 6f) <= errorRange
                && Mathf.Abs(alcoholRatio - 5f / 6f) <= errorRange
                && Mathf.Abs(juiceRatio) <= errorRange
                && Mathf.Abs(lemonRatio) <= errorRange
                && hasIce)
            {
                recipeText.text = "����Ƽ��ź �׸���";
                if (!recipeSuccessMap.ContainsKey("����Ƽ��ź �׸���"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("����Ƽ��ź �׸���", true);
                }
                SpriteRenderer spriteRenderer = iceImg[3].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r5Color.color = Color.white;

                SpriteRenderer sprite = liqImg[4].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(1, 5, 0, 0);
                recipe5Method.text = "����Ƽ��ź �׸���\n��Ÿ��5:����1+����";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
               && Mathf.Abs(alcoholRatio - 1f / 4f) <= errorRange
               && Mathf.Abs(juiceRatio) <= errorRange
               && Mathf.Abs(lemonRatio - 3f / 4f) <= errorRange
               && hasIce && isLime)
            {
                recipeText.text = "���� �ҵ���";
                if (!recipeSuccessMap.ContainsKey("���� �ҵ���"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("���� �ҵ���", true);
                }
                //����
                SpriteRenderer Renderer = limeImg[0].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer spriteRenderer = iceImg[4].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r6Color.color = Color.white;

                SpriteRenderer sprite = liqImg[5].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 1, 0, 3);
                recipe6Method.text = "���� �ҵ���\n��Ÿ��1:ź���3+����+����";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
             && Mathf.Abs(alcoholRatio - 3f / 4f) <= errorRange
             && Mathf.Abs(juiceRatio) <= errorRange
             && Mathf.Abs(lemonRatio - 1f / 4f) <= errorRange
             && hasIce && isLime && isSugar)
            {
                recipeText.text = "���� ��� ��Ƽ��";
                if (!recipeSuccessMap.ContainsKey("���� ��� ��Ƽ��"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("���� ��� ��Ƽ��", true);

                }
                SpriteRenderer Renderer = limeImg[1].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer spriteRenderer = iceImg[5].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r7Color.color = Color.white;

                SpriteRenderer sprite = liqImg[6].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 3, 0, 1);
                recipe7Method.text = "���� ��� ��Ƽ��\n��Ÿ��3:ź���1+����+����+����";
            }

            else if (Mathf.Abs(milkRatio) <= errorRange
            && Mathf.Abs(alcoholRatio - 4f / 5f) <= errorRange
            && Mathf.Abs(juiceRatio - 1f / 5f) <= errorRange
            && Mathf.Abs(lemonRatio) <= errorRange
             && isLime)
            {
                recipeText.text = "����ī ��Ƽ��";
                if (!recipeSuccessMap.ContainsKey("����ī ��Ƽ��"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("����ī ��Ƽ��", true);
                }
                SpriteRenderer Renderer = limeImg[2].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                r8Color.color = Color.white;

                SpriteRenderer sprite = liqImg[7].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 4, 1, 0);
                recipe8Method.text = "����ī ��Ƽ��\n��Ÿ��4:�ֽ�1+����";
            }
            else if (Mathf.Abs(milkRatio - 1f / 6f) <= errorRange
             && Mathf.Abs(alcoholRatio - 2f / 6f) <= errorRange
            && Mathf.Abs(juiceRatio - 3f / 6f) <= errorRange
             && Mathf.Abs(lemonRatio) <= errorRange
             && isSugar)
            {
                recipeText.text = "���� �´� ��ġ";
                if (!recipeSuccessMap.ContainsKey("���� �´� ��ġ"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("���� �´� ��ġ", true);
                }
                r9Color.color = Color.white;

                SpriteRenderer sprite = liqImg[8].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(1, 2, 3, 0);
                recipe9Method.text = "���� �´� ��ġ\n����1:��Ÿ��2:�ֽ�3+����";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
            && Mathf.Abs(alcoholRatio - 1f / 5f) <= errorRange
           && Mathf.Abs(juiceRatio - 1f / 5f) <= errorRange
            && Mathf.Abs(lemonRatio - 3f / 5f) <= errorRange
            && hasIce && isLime)
            {
                recipeText.text = "�̵��� ��𿹵�";
                if (!recipeSuccessMap.ContainsKey("�̵��� ��𿹵�"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("�̵��� ��𿹵�", true);
                }
                SpriteRenderer Renderer = limeImg[3].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer spriteRenderer = iceImg[6].GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                r10Color.color = Color.white;

                SpriteRenderer sprite = liqImg[9].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 1, 1, 3); recipe10Method.text = "�̵��� ��𿹵�\n��Ÿ��1:�ֽ�1:ź���3 + ����+����";
            }
            else if (Mathf.Abs(milkRatio - 1f / 8f) <= errorRange
            && Mathf.Abs(alcoholRatio - 3f / 8f) <= errorRange
           && Mathf.Abs(juiceRatio - 1f / 8f) <= errorRange
            && Mathf.Abs(lemonRatio - 3f / 8f) <= errorRange
            && hasIce && isLime && isGum && isSugar)
            {
                recipeText.text = "�� ���Ϸ��� ������ �Ͽ��";
                if (!recipeSuccessMap.ContainsKey("�� ���Ϸ��� ������ �Ͽ��"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("�� ���Ϸ��� ������ �Ͽ��", true);
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
                recipe11Method.text = "�� ���Ϸ��� ������ �Ͽ��\n����1:��Ÿ��3:�ֽ�1:ź���3+����+����+����+����";
            }
            else if (Mathf.Abs(milkRatio) <= errorRange
            && Mathf.Abs(alcoholRatio - 2f / 8f) <= errorRange
             && Mathf.Abs(juiceRatio - 1f / 8f) <= errorRange
             && Mathf.Abs(lemonRatio - 5f / 8f) <= errorRange
             && isFeed && isLime)
            {
                recipeText.text = "��� �Ƹ޸�ĭ �����극";
                if (!recipeSuccessMap.ContainsKey("��� �Ƹ޸�ĭ �����극"))
                {
                    MakeEffect();
                    recipeSuccessMap.Add("��� �Ƹ޸�ĭ �����극", true);
                }
                SpriteRenderer Renderer = limeImg[5].GetComponent<SpriteRenderer>();
                Renderer.color = Color.white;

                SpriteRenderer feedR = dogfeedImg.GetComponent<SpriteRenderer>();
                feedR.color = Color.white;

                r12Color.color = Color.white;

                SpriteRenderer sprite = liqImg[11].GetComponent<SpriteRenderer>();
                sprite.color = RecipeMixColors(0, 2, 1, 5);
                recipe12Method.text = "��� �Ƹ޸�ĭ �����극\n��Ÿ��2:�ֽ�1:ź���5+����+���";
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
        // �� ����ġ�� ���� ������ ����
        Color mixedColor = weight1 * milkColor + weight2 * alcoholColor + weight3 * juiceColor + weight4 * lemonColor;

        return mixedColor;
    }

    //�մ��� ���ϴ� �Ͱ� ���ϴ� �Լ�
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
        // ����� GameObject�� �̿��Ͽ� ��ƼŬ ������ ����
        Destroy(partEffect);
        isEffectMade = false;
    }

    public void CreateIce() //���� �Լ�
    {


        if (PutIce == false)
        {
            // ���� ����
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
        else Debug.Log("������ �̹� �ֽ��ϴ�");

        UpdateColor();
    }

    public void PutSugar() // ���� �߰�
    {
        isSugar = true;
        Instantiate(sugarEffect, sugarSpawnPoint.position, sugarSpawnPoint.rotation);
        sugarButton.interactable = false;
    }
    public void PutLime()
    {
        //��Ȱ��ȭ�� ������Ʈ�� Ȱ��ȭ �ϱ� ���ؼ� �ڽĿ�����Ʈ�� ����� Find��
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
        //��Ȱ��ȭ�� ������Ʈ�� Ȱ��ȭ �ϱ� ���ؼ� �ڽĿ�����Ʈ�� ����� Find��
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
        //��Ȱ��ȭ�� ������Ʈ�� Ȱ��ȭ �ϱ� ���ؼ� �ڽĿ�����Ʈ�� ����� Find��
        isFeed = true;
        GameObject feed = GameObject.Find("feed");
        Transform feedTrans = feed.transform;
        Transform feedImgtrans = feedTrans.Find("feedImg");
        GameObject feedImg = feedImgtrans.gameObject;
        feedImg.SetActive(true);
        feedButton.interactable = false;
    }

}
