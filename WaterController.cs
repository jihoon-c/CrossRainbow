using UnityEngine;
using TMPro;

public class WaterController : MonoBehaviour
{
    public Transform cupA; // ���� A
    public Transform cupB; // ���� B
    public TextMeshProUGUI text; // TMPro ������Ʈ
    public GameObject particlePrefab; // �� ȿ��

    private bool isPouring = false; // �� �ξ����� ������ üũ�ϴ� ����
    private float pouringSpeed = 15f; // �ʴ� �ξ����� ���� ��
    public float currentWaterAmount = 0f; // ���� B�� ��� ���� ��

    private float interval = 1f;
    private float timer = 0f;

    private NewClear newclear;

    private void Start()
    {
        newclear = GameObject.FindObjectOfType<NewClear>();

    }
    private void Update()
    {
        timer += Time.deltaTime;

        // �� �ξ����� ���̸� ���� ���� ������Ʈ
        if (isPouring)
        {
            currentWaterAmount += pouringSpeed * Time.deltaTime;

            if (timer >= interval)
            {
                timer = 0f;
                SpawnParticleAtTop();
            }
            

            // ���� B�� ���� á���� �� �ξ����� ���� �����
            if (currentWaterAmount >= 100f)
            {
                currentWaterAmount = 100f;
                isPouring = false;

            }

            // ���� B�� ��� ���� ���� �ؽ�Ʈ�� ǥ��
            text.text = currentWaterAmount.ToString("0") + "/100";
        }
        if (newclear.clicked) // �����ؾ���
        {
            ResetWaterAmount();
            text.text = currentWaterAmount.ToString("0") + "/100";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� A�� ���� B�� ����� ��, �� �ξ����� ����
        if (collision.transform == cupB)
        {
            isPouring = true;
            cupA.rotation = Quaternion.Euler(0f, 0f, -20f); // ���� A�� ����δ�
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���� A�� ���� B���� ����� �� �ξ����� ���� �����
        if (collision.transform == cupB)
        {
            isPouring = false;
            cupA.rotation = Quaternion.Euler(0f, 0f, 0f); // ���� A�� ������� ������
        }
    }

    public void SpawnParticleAtTop()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x+1f, transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2f, transform.position.z);
        GameObject particle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
        particle.transform.SetParent(transform);
    }

   
    public void ResetWaterAmount() //���¹�ư ������ ���½�ų �Լ� (�������)
    {
        currentWaterAmount = 0f;
    }
}
