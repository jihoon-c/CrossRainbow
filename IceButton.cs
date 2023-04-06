using UnityEngine;

public class IceButton : MonoBehaviour
{
    [SerializeField] private GameObject objectToInstantiate; // ������ ������Ʈ
    
    [SerializeField] private float minX = 3.3f; // x ��ġ�� �ּҰ�
    [SerializeField] private float maxX = 5.3f; // x ��ġ�� �ִ밪



    public void click()
    {
        // ���콺�� Ŭ���� ����Ǵ� �Լ�
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, 1.28f, -2f);
        Instantiate(objectToInstantiate, spawnPos, Quaternion.identity);

    }
}
