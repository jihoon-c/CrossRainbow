using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakingManager : MonoBehaviour
{
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

    void Start()
    {
        initialPosition = otherObjectTransform.position;
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
        // ���ſ� ����� ��, �� �ξ����� ����
        if (collision.gameObject.tag == "ing")
        {
            isPouring = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���ſ��� ����� �� �ξ����� ���� �����
        if (collision.gameObject.tag == "ing")
        {
            isPouring = false;
        }
    }

    private void WritingAmount() //TMPro�� ����ϴ� �Լ�
    {
        text.text = currentWaterAmount.ToString("0") + "/100" + "\n" +"milk : "+ milkAmount.ToString("0") +
            "\n" +"alchol : "+ alcholAmount.ToString("0") + "\n" + "juice : "+ juiceAmount.ToString("0") + "\n" +"lemon : "+ lemonAmount.ToString("0");
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

        // ColorMixer ������Ʈ�� ������ �ʱ�ȭ
        GameObject targetObject = GameObject.Find("ColorMixer");
        targetObject.transform.position = initialPosition;
        SpriteRenderer targetRenderer = targetObject.GetComponent<SpriteRenderer>();
        targetRenderer.color = Color.clear;

        // ColorMixer ������Ʈ�� ũ�⸦ �ʱ�ȭ
        Vector3 originalScale = new Vector3(1, 0.02f, 1);
        targetObject.transform.localScale = originalScale;
        targetObject.transform.position = initialPosition;

        // TMPro �� �ʱ�ȭ
        WritingAmount();
    }

    //������ ���� �Լ�
    void CheckingRecipe()
    {
        // �� ��Ḷ���� ���� ���
        float total = milkAmount + alcholAmount + juiceAmount + lemonAmount;
        float milkRatio = milkAmount / total;
        float alcoholRatio = alcholAmount / total;
        float juiceRatio = juiceAmount / total;
        float lemonRatio = lemonAmount / total;

        // ������ �������� ���Ͽ�
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
        // ����� GameObject�� �̿��Ͽ� ��ƼŬ ������ ����
        Destroy(partEffect);
    }
}
