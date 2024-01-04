using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ingredients : MonoBehaviour
{
    public GameObject objectPrefab;
    private GameObject currentObject;
    private Quaternion originalRotation; // 기존 회전값을 저장하기 위한 변수 추가


    void Start()
    {
        originalRotation = Quaternion.identity; // 초기 회전값을 저장
    }

    void Update()
    {
        if(NPCManager.N.GetNPCManagerCalculating() == true) { return; }

        // 마우스 왼쪽 버튼을 누르면 오브젝트 복제 생성
        if (Input.GetMouseButtonDown(0))
        {

            // 현재 마우스 위치의 오브젝트와 이미 생성된 복제 오브젝트와 충돌 판정
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (hitCollider != null && hitCollider.gameObject == gameObject && currentObject == null)
            {
                // 복제된 오브젝트가 없다면 생성
                currentObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
                currentObject.GetComponent<SpriteRenderer>().sortingOrder = 1; // 마우스 커서 위에 오도록 레이어 설정
                originalRotation = currentObject.transform.rotation; // 기존 회전값 저장

                // 클릭 시 투명
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;
                color.a = 0;
                spriteRenderer.color = color;
            }
            else if (currentObject != null)
            {
                // 복제된 오브젝트가 있다면 삭제
                Destroy(currentObject);
                currentObject = null;

                // 클릭 시 투명 되돌아옴
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;
                color.a = 255;
                spriteRenderer.color = color;
            }
        }

        // 마우스 오른쪽 버튼을 누르면 복제된 오브젝트 삭제
        if (Input.GetMouseButtonDown(1))
        {
            if (currentObject != null)
            {
                Destroy(currentObject);
                currentObject = null;

                // 클릭 시 투명 되돌아옴
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;
                color.a = 255;
                spriteRenderer.color = color;
            }
        }

        // 복제된 오브젝트가 있다면 마우스 커서 위치로 이동
        if (currentObject != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            currentObject.transform.position = mousePosition;

            // cup 태그의 오브젝트와 충돌할 때 y축으로 20도 회전
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentObject.transform.position, 0.1f);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "cup")
                {
                    currentObject.transform.rotation = Quaternion.Euler(0f, 0f, 20f);
                }
            }

            // cup과 닿지 않았을 때 기존 회전값으로 돌아오도록 처리
            if (currentObject.transform.rotation != originalRotation)
            {
                Collider2D[] hitCollidersWithoutCup = Physics2D.OverlapCircleAll(currentObject.transform.position, 0.1f);
                bool isCollidingWithCup = false;
                foreach (Collider2D hitCollider in hitCollidersWithoutCup)
                {
                    if (hitCollider.gameObject.tag == "cup")
                    {
                        isCollidingWithCup = true;
                        break;
                    }
                }

                if (!isCollidingWithCup)
                {
                    currentObject.transform.rotation = Quaternion.Lerp(currentObject.transform.rotation, originalRotation, Time.deltaTime * 5f);
                }
            }
        }

    }

}
        

        