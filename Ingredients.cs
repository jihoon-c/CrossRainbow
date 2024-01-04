using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ingredients : MonoBehaviour
{
    public GameObject objectPrefab;
    private GameObject currentObject;
    private Quaternion originalRotation; // ���� ȸ������ �����ϱ� ���� ���� �߰�


    void Start()
    {
        originalRotation = Quaternion.identity; // �ʱ� ȸ������ ����
    }

    void Update()
    {
        if(NPCManager.N.GetNPCManagerCalculating() == true) { return; }

        // ���콺 ���� ��ư�� ������ ������Ʈ ���� ����
        if (Input.GetMouseButtonDown(0))
        {

            // ���� ���콺 ��ġ�� ������Ʈ�� �̹� ������ ���� ������Ʈ�� �浹 ����
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (hitCollider != null && hitCollider.gameObject == gameObject && currentObject == null)
            {
                // ������ ������Ʈ�� ���ٸ� ����
                currentObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
                currentObject.GetComponent<SpriteRenderer>().sortingOrder = 1; // ���콺 Ŀ�� ���� ������ ���̾� ����
                originalRotation = currentObject.transform.rotation; // ���� ȸ���� ����

                // Ŭ�� �� ����
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;
                color.a = 0;
                spriteRenderer.color = color;
            }
            else if (currentObject != null)
            {
                // ������ ������Ʈ�� �ִٸ� ����
                Destroy(currentObject);
                currentObject = null;

                // Ŭ�� �� ���� �ǵ��ƿ�
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;
                color.a = 255;
                spriteRenderer.color = color;
            }
        }

        // ���콺 ������ ��ư�� ������ ������ ������Ʈ ����
        if (Input.GetMouseButtonDown(1))
        {
            if (currentObject != null)
            {
                Destroy(currentObject);
                currentObject = null;

                // Ŭ�� �� ���� �ǵ��ƿ�
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;
                color.a = 255;
                spriteRenderer.color = color;
            }
        }

        // ������ ������Ʈ�� �ִٸ� ���콺 Ŀ�� ��ġ�� �̵�
        if (currentObject != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            currentObject.transform.position = mousePosition;

            // cup �±��� ������Ʈ�� �浹�� �� y������ 20�� ȸ��
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentObject.transform.position, 0.1f);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "cup")
                {
                    currentObject.transform.rotation = Quaternion.Euler(0f, 0f, 20f);
                }
            }

            // cup�� ���� �ʾ��� �� ���� ȸ�������� ���ƿ����� ó��
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
        

        