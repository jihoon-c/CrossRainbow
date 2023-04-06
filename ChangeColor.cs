using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject[] objects;  // objects �迭�� ������Ʈ���� �Ҵ��Ͽ� ����
    public Color colorToChange = new Color(1f,0f,0f,0f);  // ���� ������ ������ �Ҵ�
    //�߰��� ������Ʈ�ѷ����� �뷮�� �ö� �� ���� ��ᰡ �뷮�� �ö󰡴���, �� ����� ���� �ľ��ؼ� 10���� ���� ������ �� �ְ� ��������
    public int startObjectIndex = 1;  // ���� ������Ʈ�� �ε����� �Ҵ�

    // ���� ���� �����ϴ� ����
    private float variableValue = 0f;

    // ������ ����ǰ� �ִ��� ���θ� �����ϴ� ����
    private bool isChangingColor = false;

    private WaterController waterController;

    private void Start()
    {
        // OtherScript ������Ʈ�� ���� GameObject�� ã�Ƽ� otherScript ������ �Ҵ��մϴ�.
        waterController = GameObject.FindObjectOfType<WaterController>();
        variableValue= waterController.currentWaterAmount;

        /*colorToChange = GetComponent<SpriteRenderer>().color;
        colorToChange.a = 0.9f;*/

    }
    private void Update()
    {
        waterController = GameObject.FindObjectOfType<WaterController>();
        variableValue = waterController.currentWaterAmount;

        // ���� ���� 100 �̻��̸� 100���� �����ϰ� isChangingColor�� true�� ����
        if (variableValue >= 100f)
        {
            variableValue = 100f;
            isChangingColor = true;
        }

        // ���� ���� ���̸� ��� ������Ʈ�� ������ colorToChange�� ����
        if (isChangingColor)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].GetComponent<SpriteRenderer>().color = colorToChange;
            }
        }
        // ���� ���� ���� �ƴϸ�
        else if (variableValue > 0)
        {
            // startObjectIndex �̻���� �����Ͽ� (variableValue / incrementValue)��ŭ�� ������Ʈ�� ������ ����
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
