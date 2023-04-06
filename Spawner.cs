using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // ������ ������Ʈ ������
    public Vector2 startPos; // ù ��° ������Ʈ ���� ��ġ
    public Vector2 offset; // ���� ��ġ ����

    private Vector2 currentPos; // ���� ���� ��ġ

    private void Start()
    {
        currentPos = startPos;
    }

    private void SpawnObject()
    {
        // �������� ���� ��ġ�� �����մϴ�.
        GameObject obj = Instantiate(prefab, currentPos, Quaternion.identity);

        // ���� ��ġ�� ���ݸ�ŭ �����մϴ�.
        currentPos += offset;
    }
}
