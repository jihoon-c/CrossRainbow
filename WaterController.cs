using UnityEngine;
using TMPro;

public class WaterController : MonoBehaviour
{
    public Transform cupA; // 물컵 A
    public Transform cupB; // 물컵 B
    public TextMeshProUGUI text; // TMPro 오브젝트
    public GameObject particlePrefab; // 물 효과

    private bool isPouring = false; // 물 부어지는 중인지 체크하는 변수
    private float pouringSpeed = 15f; // 초당 부어지는 물의 양
    public float currentWaterAmount = 0f; // 물컵 B에 담긴 물의 양

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

        // 물 부어지는 중이면 물의 양을 업데이트
        if (isPouring)
        {
            currentWaterAmount += pouringSpeed * Time.deltaTime;

            if (timer >= interval)
            {
                timer = 0f;
                SpawnParticleAtTop();
            }
            

            // 물컵 B가 가득 찼으면 물 부어지는 것을 멈춘다
            if (currentWaterAmount >= 100f)
            {
                currentWaterAmount = 100f;
                isPouring = false;

            }

            // 물컵 B에 담긴 물의 양을 텍스트로 표시
            text.text = currentWaterAmount.ToString("0") + "/100";
        }
        if (newclear.clicked) // 수정해야함
        {
            ResetWaterAmount();
            text.text = currentWaterAmount.ToString("0") + "/100";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 물컵 A가 물컵 B에 닿았을 때, 물 부어지기 시작
        if (collision.transform == cupB)
        {
            isPouring = true;
            cupA.rotation = Quaternion.Euler(0f, 0f, -20f); // 물컵 A를 기울인다
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 물컵 A가 물컵 B에서 벗어나면 물 부어지는 것을 멈춘다
        if (collision.transform == cupB)
        {
            isPouring = false;
            cupA.rotation = Quaternion.Euler(0f, 0f, 0f); // 물컵 A를 원래대로 돌린다
        }
    }

    public void SpawnParticleAtTop()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x+1f, transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2f, transform.position.z);
        GameObject particle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
        particle.transform.SetParent(transform);
    }

   
    public void ResetWaterAmount() //리셋버튼 누르면 리셋시킬 함수 (만드는중)
    {
        currentWaterAmount = 0f;
    }
}
