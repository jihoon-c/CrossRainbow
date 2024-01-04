using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocoEvent : MonoBehaviour
{
    /*
    public GameObject sem;
    public SceneEventManager scenEventManager;

    private bool hasTriggered = false; // 충돌 이벤트가 한 번만 실행되도록 체크하는 변수

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
        if (hasTriggered) // 이미 충돌 이벤트가 실행된 경우
            return;

        if (other.CompareTag("NPC")) 
        {
            scenEventManager.StopAnother();

            hasTriggered = true; // 충돌 이벤트가 실행되었음을 체크
            GameObject event1Box = GameObject.Find("ChocoEvent1Collider");
            event1Box.SetActive(false);
        }
    }
    */
}
