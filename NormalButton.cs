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
    //���� ���� ����
    public MakingManager mm;
    float ma, ja, aa, la;
    bool coldness;

    void start()
    {
        // "MyObject"��� �̸��� ���� ������Ʈ�� ã�Ƽ� ������ �����մϴ�.
        myObject = GameObject.FindWithTag("NPC");

        // �ش� ������Ʈ�� �پ� �ִ� "MyScript" ��ũ��Ʈ�� �����ɴϴ�.
        myScript = myObject.GetComponent<NormalObjData>();

        // MyScript ��ũ��Ʈ���� ���� �����ɴϴ�.
        myValue = myScript.id;
    }
    public void ClickRate()
    {
        StartCoroutine(DelayedExecution(10f));
    }
    private IEnumerator DelayedExecution(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // ������ �ð���ŭ ���

        // ������ ���� �Ǵ� �Լ� ȣ��
        //ClickNoraml();
    }

}
