using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocoEvent : MonoBehaviour
{
    /*
    public GameObject sem;
    public SceneEventManager scenEventManager;

    private bool hasTriggered = false; // �浹 �̺�Ʈ�� �� ���� ����ǵ��� üũ�ϴ� ����

    void Start()
    {
        sem = GameObject.Find("SceneEventManager");
        scenEventManager = sem.GetComponent<SceneEventManager>();
        scenEventManager.ChocoAppear();
        scenEventManager.hasChocoSpawned = true;
        scenEventManager.StopAnother();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) // �̹� �浹 �̺�Ʈ�� ����� ���
            return;

        if (other.CompareTag("NPC")) 
        {
            scenEventManager.StopAnother();

            hasTriggered = true; // �浹 �̺�Ʈ�� ����Ǿ����� üũ
            GameObject event1Box = GameObject.Find("ChocoEvent1Collider");
            event1Box.SetActive(false);
        }
    }
    */
}
