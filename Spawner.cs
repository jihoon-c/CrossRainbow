using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // 생성할 오브젝트 프리팹
    public Vector2 startPos; // 첫 번째 오브젝트 생성 위치
    public Vector2 offset; // 생성 위치 간격

    private Vector2 currentPos; // 현재 생성 위치

    private void Start()
    {
        currentPos = startPos;
    }

    private void SpawnObject()
    {
        // 프리팹을 생성 위치에 생성합니다.
        GameObject obj = Instantiate(prefab, currentPos, Quaternion.identity);

        // 생성 위치를 간격만큼 증가합니다.
        currentPos += offset;
    }
}
