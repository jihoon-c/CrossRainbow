using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakingManager : MonoBehaviour
{
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

    void Start()
    {
        initialPosition = otherObjectTransform.position;
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

                if (currentWaterAmount >= 100f)
                {
                    currentWaterAmount = 100f;
                    isPouring = false;
                }
            }
            WritingAmount();
        }
        currentWaterAmount = milkAmount + alcholAmount + juiceAmount + lemonAmount;
        UpdateColor();
        CheckingRecipe();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 물컵에 닿았을 때, 물 부어지기 시작
        if (collision.gameObject.tag == "ing")
        {
            isPouring = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 물컵에서 벗어나면 물 부어지는 것을 멈춘다
        if (collision.gameObject.tag == "ing")
        {
            isPouring = false;
        }
    }

    private void WritingAmount() //TMPro에 출력하는 함수
    {
        text.text = currentWaterAmount.ToString("0") + "/100" + "\n" +"milk : "+ milkAmount.ToString("0") +
            "\n" +"alchol : "+ alcholAmount.ToString("0") + "\n" + "juice : "+ juiceAmount.ToString("0") + "\n" +"lemon : "+ lemonAmount.ToString("0");
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

        // ColorMixer 오브젝트의 색상을 초기화
        GameObject targetObject = GameObject.Find("ColorMixer");
        targetObject.transform.position = initialPosition;
        SpriteRenderer targetRenderer = targetObject.GetComponent<SpriteRenderer>();
        targetRenderer.color = Color.clear;

        // ColorMixer 오브젝트의 크기를 초기화
        Vector3 originalScale = new Vector3(1, 0.02f, 1);
        targetObject.transform.localScale = originalScale;
        targetObject.transform.position = initialPosition;

        // TMPro 값 초기화
        WritingAmount();
    }

    //레시피 조사 함수
    void CheckingRecipe()
    {
        // 각 재료마다의 비율 계산
        float total = milkAmount + alcholAmount + juiceAmount + lemonAmount;
        float milkRatio = milkAmount / total;
        float alcoholRatio = alcholAmount / total;
        float juiceRatio = juiceAmount / total;
        float lemonRatio = lemonAmount / total;

        // 레시피 제조법에 대하여
        if (milkRatio == 1f / 3f && alcoholRatio == 1f / 3f && juiceRatio == 1f / 3f && lemonRatio == 0f)
        {
            recipeText.text = "Recipe 1";
            MakeEffect();
        }
        else if (milkRatio == 1f / 3f && alcoholRatio == 2f / 3f && juiceRatio == 0f && lemonRatio == 0f)
        {
            recipeText.text = "Recipe 2";
        }
        else
        {
            recipeText.text = "";
            DestroyEffect();
        }
    }

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
    }
}
