using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClearButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        // 클릭 이벤트 발생 시 실행할 함수
        RemoveObjectsWithTag();
        ResetText();

    }

    void RemoveObjectsWithTag()
    {
        GameObject[] objectsToRemove = GameObject.FindGameObjectsWithTag("liq1").Concat(
            GameObject.FindGameObjectsWithTag("liq2")).Concat(GameObject.FindGameObjectsWithTag("ice")).ToArray();

        foreach (GameObject obj in objectsToRemove)
        {
            Destroy(obj);
        }
    }

    private void ResetText()
    {
        TMP_Text[] texts = FindObjectsOfType<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            text.text = "";
        }
    }
}
