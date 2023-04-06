using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class NewClear : MonoBehaviour
{
    public bool clicked = false;
    public GameObject[] objectsWithTag; // ������ ������Ʈ�� ���� �迭
    public TextMeshProUGUI textMesh; // �ʱ�ȭ�� TMpro

    private WaterController waterController;

    private void Start()
    {
        // OtherScript ������Ʈ�� ���� GameObject�� ã�Ƽ� otherScript ������ �Ҵ��մϴ�.
        waterController = GameObject.FindObjectOfType<WaterController>();
    }

    public void OnClickButton()
    {
        objectsWithTag = GameObject.FindGameObjectsWithTag("ice"); // YourTag�� �ش��ϴ� ��� ������Ʈ�� ã�� �迭�� ����

        for (int i = 0; i < objectsWithTag.Length; i++)
        {
            Destroy(objectsWithTag[i]); // �迭�� ����� ��� ������Ʈ ����
        }

        // �ٸ� �Լ����� ����ϴ� ���� �� �ʱ�ȭ
        ResetValue();
        //�ڷ�ƾ
        clicked = true;
        StartCoroutine(AutoResetBool());

        // TMpro �ʱ�ȭ
        textMesh.text = "0/100";
    }

    private void ResetValue()
    {
        // otherScript ������ ���� myValue ���� �����ͼ� 0���� �ʱ�ȭ�մϴ�.
        waterController.currentWaterAmount = 0f;
    }

    private IEnumerator AutoResetBool()
    {
        yield return new WaitForSeconds(0.01f);
        clicked = false;
    }
}
