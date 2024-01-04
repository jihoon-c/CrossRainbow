using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public Vector3 spawnPosition = new Vector3(4.6f, -0.44f, 0f); // ������ ��ġ�� Vector3�� ����
    private GameObject spawnedObject; // ������ ������Ʈ�� �����ϴ� ����

    //����
    //upDownCheck   : true == up, false == down
    bool upDownCheck = false;
    Animator recipeAnimator;

    void Start()
    {
        recipeAnimator = prefabToSpawn.GetComponent<Animator>();
    }
    void Update()
    {

    }

    public void ToggleObject()
    {
        // ������ ������Ʈ�� ������ �����Ѵ�
        /*if (spawnedObject == null)
        {
            spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
        // ������ ������Ʈ�� ������ �����Ѵ�
        else
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }*/

        //����
        //Recipe�� UI�� �������� �Ǵ��Ͽ� UI-Image�� ������ Recipe Object�� �̿��ϰ� �ֽ��ϴ�.
        upDownCheck = !upDownCheck;

        if(upDownCheck == true)
        {
            recipeAnimator.SetTrigger("RecipeUp");
        }
        else if (upDownCheck == false)
        {
            recipeAnimator.SetTrigger("RecipeDown");
        }
    }
}
