using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CupController : MonoBehaviour
{
    public TextMeshProUGUI Viyul;
    public TextMeshProUGUI Money;
    public float money = 1000; //재화

    private float currentVolume = 0f;
    private float milkAmount = 0f;
    private float alcholAmount = 0f;
    private float cAmount = 0f;
    private float dAmount = 0f;

    public Transform MilkT;
    public Transform AlcholT;
    public Transform JuiceT;
    public Transform ChureT;

    private SpriteRenderer cupRenderer;
    private Color aColor;
    private Color bColor;
    private Color cColor;
    private Color dColor;

    private void Start()
    {
        cupRenderer = GetComponent<SpriteRenderer>();
        aColor = GetIngredientColor("Milk");
        bColor = GetIngredientColor("Alchol");
        cColor = GetIngredientColor("Juice");
        dColor = GetIngredientColor("Chure");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
        if (otherRb != null)
        {
            // Rotate the other object by 90 degrees around the y-axis
            otherRb.transform.Rotate(new Vector3(0f, -20f, 0f));
        }
    
        if (collision.CompareTag("Milk"))
        {
            milkAmount += 5f;
            money -= 10;
        }
        else if (collision.CompareTag("Alchol"))
        {
            alcholAmount += 5f;
            money -= 5;
        }
        else if (collision.CompareTag("Juice"))
        {
            cAmount += 5f;
            money -= 10;
        }
        else if (collision.CompareTag("Chure"))
        {
            dAmount += 5f;
            money -= 20;
        }

        currentVolume = milkAmount + alcholAmount + cAmount + dAmount;

        if (currentVolume > 100f)
        {
            // 현재 볼륨이 최대 볼륨인 100을 초과하면 각 재료의 오버플로를 같은 양만큼 줄입니다.
            float overflow = currentVolume - 100f;
            milkAmount -= overflow / 4f;
            alcholAmount -= overflow / 4f;
            cAmount -= overflow / 4f;
            dAmount -= overflow / 4f;
            currentVolume = 100f;
        }

        if (currentVolume % 5 == 0&& currentVolume < 100f)
        {
            UpdateCupSize();
            MixColors();
        }
        
        // 텍스트 업데이트
        UpdateTextMeshPro();
        UpdateMoney();
    }


    private void UpdateTextMeshPro()
    {
        Viyul.text =  currentVolume.ToString("F1") + "/" + "100"; 
    }

    private void UpdateMoney()
    {
        Money.text = money.ToString() + "$";
    }

    private Color GetIngredientColor(string tag)
    {
        switch (tag)
        {
            case "Milk":
                return Color.white;
            case "Alchol":
                return Color.green;
            case "Juice":
                return Color.blue;
            case "Chure":
                return Color.red;
            default:
                return Color.white;
        }
    }

    private void MixColors()
    {
        float totalAmount = milkAmount + alcholAmount + cAmount + dAmount;

        if (totalAmount > 0f && currentVolume % 10 == 0)
        {
            float aRatio = milkAmount / totalAmount;
            float bRatio = alcholAmount / totalAmount;
            float cRatio = cAmount / totalAmount;
            float dRatio = dAmount / totalAmount;

            Color mixedColor = new Color(
                aRatio * aColor.r + bRatio * bColor.r + cRatio * cColor.r + dRatio * dColor.r,
                aRatio * aColor.g + bRatio * bColor.g + cRatio * cColor.g + dRatio * dColor.g,
                aRatio * aColor.b + bRatio * bColor.b + cRatio * cColor.b + dRatio * dColor.b);

            cupRenderer.color = mixedColor;
        }
    }

    private void UpdateCupSize()
    {
        float currentHeight = transform.localScale.y;
        float newHeight = currentHeight + 0.07f;

        Vector3 centerOffset = new Vector3(0f, (newHeight - currentHeight) / 2f, 0f);
        transform.position += centerOffset;
        transform.localScale = new Vector3(transform.localScale.x, newHeight, transform.localScale.z);
    }
    
    private void CupRotate()
    {
        //.rotation = Quaternion.Euler(0f, 0f, -20f); // 물컵 A를 기울인다
    }
}


