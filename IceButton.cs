using UnityEngine;

public class IceButton : MonoBehaviour
{
    [SerializeField] private GameObject objectToInstantiate; // 생성할 오브젝트
    
    [SerializeField] private float minX = 3.3f; // x 위치의 최소값
    [SerializeField] private float maxX = 5.3f; // x 위치의 최대값



    public void click()
    {
        // 마우스로 클릭시 실행되는 함수
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, 1.28f, -2f);
        Instantiate(objectToInstantiate, spawnPos, Quaternion.identity);

    }
}
