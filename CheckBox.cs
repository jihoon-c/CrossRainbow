using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 오브젝트 프리팹
    public GameObject prefabToSpawn2; // 생성할 오브젝트 프리팹
    
    public List<string> targetTags; // 충돌 대상 태그 리스트
    public TextMeshProUGUI countText; // 카운트를 출력할 TMP 객체
    private int count = 0; // 충돌한 횟수
    private Dictionary<string, int> countDict = new Dictionary<string, int>(); // 충돌한 횟수를 저장할 딕셔너리

    // 마지막 문자가 : 이면 지우는 메서드 (언제 검사하고 지워야하는지 생각)
    //컵의 크기에 따라서 최대 넣을 수 있는 양이 달라짐
    // 최대 넣을 양만큼 넣었다면 더이상 재료가 추가되지 않음

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("alchol"))
        {
            if (!countDict.ContainsKey(collision.gameObject.tag))
            {
                countDict[collision.gameObject.tag] = 0;
            }
            countDict[collision.gameObject.tag]++;
            Instantiate(prefabToSpawn, Vector2.zero, Quaternion.identity);

            string countStr = "";
            foreach (KeyValuePair<string, int> pair in countDict)
            {
                countStr += pair.Key + " " + pair.Value + " : ";
            }
            countText.text = countStr;
        }

        else if (collision.gameObject.CompareTag("water"))
        {
            if (!countDict.ContainsKey(collision.gameObject.tag))
            {
                countDict[collision.gameObject.tag] = 0;
            }
            countDict[collision.gameObject.tag]++;
            Instantiate(prefabToSpawn2, Vector2.zero, Quaternion.identity);

            string countStr = "";
            foreach (KeyValuePair<string, int> pair in countDict)
            {
                countStr += pair.Key + " " + pair.Value + " : ";
            }
            countText.text = countStr;
        }
    }

}
