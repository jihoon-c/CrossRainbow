using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������Ʈ ������
    public GameObject prefabToSpawn2; // ������ ������Ʈ ������
    
    public List<string> targetTags; // �浹 ��� �±� ����Ʈ
    public TextMeshProUGUI countText; // ī��Ʈ�� ����� TMP ��ü
    private int count = 0; // �浹�� Ƚ��
    private Dictionary<string, int> countDict = new Dictionary<string, int>(); // �浹�� Ƚ���� ������ ��ųʸ�

    // ������ ���ڰ� : �̸� ����� �޼��� (���� �˻��ϰ� �������ϴ��� ����)
    //���� ũ�⿡ ���� �ִ� ���� �� �ִ� ���� �޶���
    // �ִ� ���� �縸ŭ �־��ٸ� ���̻� ��ᰡ �߰����� ����

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
