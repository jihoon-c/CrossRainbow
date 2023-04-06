using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class NewClear : MonoBehaviour
{
    public bool clicked = false;
    public GameObject[] objectsWithTag; // 삭제할 오브젝트를 담을 배열
    public TextMeshProUGUI textMesh; // 초기화할 TMpro

    private WaterController waterController;

    private void Start()
    {
        // OtherScript 컴포넌트를 가진 GameObject를 찾아서 otherScript 변수에 할당합니다.
        waterController = GameObject.FindObjectOfType<WaterController>();
    }

    public void OnClickButton()
    {
        objectsWithTag = GameObject.FindGameObjectsWithTag("ice"); // YourTag에 해당하는 모든 오브젝트를 찾아 배열에 저장

        for (int i = 0; i < objectsWithTag.Length; i++)
        {
            Destroy(objectsWithTag[i]); // 배열에 저장된 모든 오브젝트 삭제
        }

        // 다른 함수에서 사용하는 변수 값 초기화
        ResetValue();
        //코루틴
        clicked = true;
        StartCoroutine(AutoResetBool());

        // TMpro 초기화
        textMesh.text = "0/100";
    }

    private void ResetValue()
    {
        // otherScript 변수를 통해 myValue 값을 가져와서 0으로 초기화합니다.
        waterController.currentWaterAmount = 0f;
    }

    private IEnumerator AutoResetBool()
    {
        yield return new WaitForSeconds(0.01f);
        clicked = false;
    }
}
