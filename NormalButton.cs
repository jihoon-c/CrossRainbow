using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalButton : MonoBehaviour
{
    GameObject myObject;
    NormalObjData myScript;
    int myValue;
    RatingSystem rs;

    //
    //음료 점수 측정
    public MakingManager mm;
    float ma, ja, aa, la;
    bool coldness;

    void start()
    {
        // "MyObject"라는 이름을 가진 오브젝트를 찾아서 변수에 저장합니다.
        myObject = GameObject.FindWithTag("NPC");

        // 해당 오브젝트에 붙어 있는 "MyScript" 스크립트를 가져옵니다.
        myScript = myObject.GetComponent<NormalObjData>();

        // MyScript 스크립트에서 값을 가져옵니다.
        myValue = myScript.id;
    }
    public void ClickRate()
    {
        StartCoroutine(DelayedExecution(10f));
    }
    private IEnumerator DelayedExecution(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // 지정된 시간만큼 대기

        // 실행할 동작 또는 함수 호출
        //ClickNoraml();
    }

}
