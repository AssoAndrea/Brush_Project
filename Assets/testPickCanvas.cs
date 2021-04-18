using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class testPickCanvas : MonoBehaviour, IPointerClickHandler
{
    Vector2 clickNorm = Vector2.zero;
    public Canvas canvas;
    public Texture2D texture;
    float ScaleFactor;
    public Image img;

    public void OnPointerClick(PointerEventData eventData)
    {
        ScaleFactor = canvas.scaleFactor;
        Debug.Log(ScaleFactor);
        RectTransform tr = GetComponent<RectTransform>();
        Vector3 worldPos = tr.position;
        Vector3 mousePos = Input.mousePosition;
        Vector2 on2DWoldPos = new Vector2(worldPos.x, worldPos.y);
        Vector2 on2DmousePos = new Vector2(mousePos.x, mousePos.y);
        //Debug.Log(on2DmousePos - on2DWoldPos);
        Vector2 clickNorm = on2DmousePos/ScaleFactor - tr.anchoredPosition;
        clickNorm.x /= tr.rect.width;
        clickNorm.y /= tr.rect.height;

        Color c = texture.GetPixel((int)(clickNorm.x * texture.width), (int)(clickNorm.y * texture.height));
        img.color = c;


    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
