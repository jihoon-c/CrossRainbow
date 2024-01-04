using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneEventManager : MonoBehaviour
{
    public bool hasChocoSpawned = false; // 초코가  최초로 등장했는지 여부
    public GameObject nextTalkObject;
    public TalkObject talkObjectScript;

    private List<MonoBehaviour> pausedScripts = new List<MonoBehaviour>(); // 일시정지된 스크립트들을 저장할 리스트

    private void Update()
    {

    }

    public void ChocoAppear()
    {
        
        if (talkObjectScript != null)
        {
            // TalkObject 스크립트의 OnMouseDown 함수 호출
            //talkObjectScript.SceneEventFunc(11);
        }

        //GameObject nextTalkObject = GameObject.Find("NextTalk");
        Time.timeScale = 0;
        nextTalkObject.SetActive(true);
        talkObjectScript.SceneEventFunc();
    }

    public void StopAnother()
    {


        // 비활성화 
        //재료 제작 비활성화

        /*
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("ing");

        foreach (GameObject obj in targetObjects)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false; // 스크립트 실행 정지
                pausedScripts.Add(script); // 일시정지된 스크립트를 리스트에 추가
            }
        }
        //npc매니저 비활성화
        GameObject npcManagerObject = GameObject.Find("NPCManager");

        if (npcManagerObject != null)
        {
            npcManagerObject.SetActive(false);
        }

        //iceButton 비활성화
        Button icebutton = GameObject.Find("ice").GetComponent<Button>();
        icebutton.interactable = false;

        Button recipeButton = GameObject.Find("RecipeButton").GetComponent<Button>();
        recipeButton.interactable = false;

        //NPC들 일시정지 
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npc in npcs)
        {
            MonoBehaviour[] scripts = npc.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                Animator animator = npc.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.enabled = false; // Animator 비활성화
                }
                script.enabled = false; // 스크립트 실행 정지
                pausedScripts.Add(script); // 일시정지된 스크립트를 리스트에 추가
            }
        }
        */

    }

    public void ResumePausedScripts()
    {
        /*
        foreach (MonoBehaviour script in pausedScripts)
        {
            script.enabled = true; // 일시정지된 스크립트를 다시 활성화
        }

        pausedScripts.Clear(); // 일시정지된 스크립트 리스트를 비움
        */

        
    }
}
