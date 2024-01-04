using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹
    public Vector3 spawnPosition = new Vector3(4.6f, -0.44f, 0f); // 생성할 위치를 Vector3로 지정
    private GameObject spawnedObject; // 생성된 오브젝트를 저장하는 변수

    //지아
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
        // 생성된 오브젝트가 없으면 생성한다
        /*if (spawnedObject == null)
        {
            spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
        // 생성된 오브젝트가 있으면 삭제한다
        else
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }*/

        //지아
        //Recipe는 UI의 영역으로 판단하여 UI-Image로 생성한 Recipe Object를 이용하고 있습니다.
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
