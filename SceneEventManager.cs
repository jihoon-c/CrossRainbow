using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneEventManager : MonoBehaviour
{
    public bool hasChocoSpawned = false; // ���ڰ�  ���ʷ� �����ߴ��� ����
    public GameObject nextTalkObject;
    public TalkObject talkObjectScript;

    private List<MonoBehaviour> pausedScripts = new List<MonoBehaviour>(); // �Ͻ������� ��ũ��Ʈ���� ������ ����Ʈ

    private void Update()
    {

    }

    public void ChocoAppear()
    {
        
        if (talkObjectScript != null)
        {
            // TalkObject ��ũ��Ʈ�� OnMouseDown �Լ� ȣ��
            //talkObjectScript.SceneEventFunc(11);
        }

        //GameObject nextTalkObject = GameObject.Find("NextTalk");
        Time.timeScale = 0;
        nextTalkObject.SetActive(true);
        talkObjectScript.SceneEventFunc();
    }

    public void StopAnother()
    {


        // ��Ȱ��ȭ 
        //��� ���� ��Ȱ��ȭ

        /*
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("ing");

        foreach (GameObject obj in targetObjects)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false; // ��ũ��Ʈ ���� ����
                pausedScripts.Add(script); // �Ͻ������� ��ũ��Ʈ�� ����Ʈ�� �߰�
            }
        }
        //npc�Ŵ��� ��Ȱ��ȭ
        GameObject npcManagerObject = GameObject.Find("NPCManager");

        if (npcManagerObject != null)
        {
            npcManagerObject.SetActive(false);
        }

        //iceButton ��Ȱ��ȭ
        Button icebutton = GameObject.Find("ice").GetComponent<Button>();
        icebutton.interactable = false;

        Button recipeButton = GameObject.Find("RecipeButton").GetComponent<Button>();
        recipeButton.interactable = false;

        //NPC�� �Ͻ����� 
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npc in npcs)
        {
            MonoBehaviour[] scripts = npc.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                Animator animator = npc.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.enabled = false; // Animator ��Ȱ��ȭ
                }
                script.enabled = false; // ��ũ��Ʈ ���� ����
                pausedScripts.Add(script); // �Ͻ������� ��ũ��Ʈ�� ����Ʈ�� �߰�
            }
        }
        */

    }

    public void ResumePausedScripts()
    {
        /*
        foreach (MonoBehaviour script in pausedScripts)
        {
            script.enabled = true; // �Ͻ������� ��ũ��Ʈ�� �ٽ� Ȱ��ȭ
        }

        pausedScripts.Clear(); // �Ͻ������� ��ũ��Ʈ ����Ʈ�� ���
        */

        
    }
}
