using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixButton : MonoBehaviour
{
    public GameObject[] objects;

    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    public void OnClick()
    {
        // 오브젝트 인스턴스들의 색을 가져와서 섞음
        Color mixedColor = MixColors(objects);

        // 인스턴스들의 색을 변경
        ChangeColor(objects, mixedColor);

    }
    public Color MixColors(GameObject[] objects)
    {
        int count = objects.Length;
        if (count == 0) return Color.white;

        float r = 0f, g = 0f, b = 0f, a = 0f;
        foreach (GameObject obj in objects)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                Color color = renderer.color;
                r += color.r;
                g += color.g;
                b += color.b;
                a += color.a;
            }
        }

        return new Color(r / count, g / count, b / count, a / count);
    }

    public void ChangeColor(GameObject[] objects, Color color)
    {
        foreach (GameObject obj in objects)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = color;
            }
        }
    }
}
