using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public Vector3 spawnPosition = new Vector3(2.5f, -0.44f, 0f); // ������ ��ġ�� Vector3�� ����
    private GameObject spawnedObject; // ������ ������Ʈ�� �����ϴ� ����

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void ToggleObject()
    {
        // ������ ������Ʈ�� ������ �����Ѵ�
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
        // ������ ������Ʈ�� ������ �����Ѵ�
        else
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }
}
