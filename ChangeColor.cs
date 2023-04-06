using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject[] objects;  // objects 배열에 오브젝트들을 할당하여 저장
    public Color colorToChange = new Color(1f,0f,0f,0f);  // 색상 변경할 색상을 할당
    //추가로 워터컨트롤러에서 용량이 올라갈 때 무슨 재료가 용량이 올라가는지, 그 재료의 색을 파악해서 10마다 색을 변경할 수 있게 만들어야함
    public int startObjectIndex = 1;  // 시작 오브젝트의 인덱스를 할당

    // 변수 값을 저장하는 변수
    private float variableValue = 0f;

    // 색상이 변경되고 있는지 여부를 저장하는 변수
    private bool isChangingColor = false;

    private WaterController waterController;

    private void Start()
    {
        // OtherScript 컴포넌트를 가진 GameObject를 찾아서 otherScript 변수에 할당합니다.
        waterController = GameObject.FindObjectOfType<WaterController>();
        variableValue= waterController.currentWaterAmount;

        /*colorToChange = GetComponent<SpriteRenderer>().color;
        colorToChange.a = 0.9f;*/

    }
    private void Update()
    {
        waterController = GameObject.FindObjectOfType<WaterController>();
        variableValue = waterController.currentWaterAmount;

        // 변수 값이 100 이상이면 100으로 고정하고 isChangingColor를 true로 설정
        if (variableValue >= 100f)
        {
            variableValue = 100f;
            isChangingColor = true;
        }

        // 색상 변경 중이면 모든 오브젝트의 색상을 colorToChange로 변경
        if (isChangingColor)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].GetComponent<SpriteRenderer>().color = colorToChange;
            }
        }
        // 색상 변경 중이 아니면
        else if (variableValue > 0)
        {
            // startObjectIndex 이상부터 시작하여 (variableValue / incrementValue)만큼의 오브젝트의 색상을 변경
            for (int i = startObjectIndex; i < objects.Length; i++)
            {
                if ((i - startObjectIndex) < (variableValue / 10))
                {
                    objects[i].GetComponent<SpriteRenderer>().color = colorToChange;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
