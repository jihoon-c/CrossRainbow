using UnityEngine;

public class DragAndSnap : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isDragging;

    void OnMouseDown()
    {
        isDragging = true;
        startPosition = transform.position;
    }

    void OnMouseUp()
    {
        isDragging = false;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity; //기울어진거 초기화
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            transform.position = mousePosition;
        }
    }
}
