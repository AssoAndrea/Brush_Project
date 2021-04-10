using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class colorPicker : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler
{
    public Texture2D texture;
    public Image Palette;
    public Image img2;


    List<RectTransform> points;
    List<RectTransform> toRemove;
    bool pressOnImage;
    PointerEventData evDt;
    


    public void OnPointerDown(PointerEventData eventData)
    {
        evDt = eventData;
        pressOnImage = true;
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (pressOnImage)
        {
            pressOnImage = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressOnImage = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        texture = Palette.sprite.texture;
        RectTransform[] p = GetComponentsInChildren<RectTransform>();
        toRemove = new List<RectTransform>();
        points = new List<RectTransform>();
        foreach (RectTransform i in p)
        {
            if (i.tag == "Checkpoint")
            {
                points.Add(i);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pressOnImage)
        {
            Color color = PickColor();
            if (color == Color.black)
            {
                foreach (RectTransform t in points)
                {
                    Vector2 pos = new Vector2(t.position.x, t.position.y);
                    float d = Vector3.Distance(t.position, evDt.position);
                    if (d < 10f)
                    {
                        Debug.Log("preso check" + t.name);
                        toRemove.Add(t);
                    }
                }
                foreach (RectTransform rectTransform in toRemove)
                {
                    points.Remove(rectTransform);
                }

                if (points.Count <=0)
                {
                    pressOnImage = false;
                    Debug.Log("disegno completato");
                }
            }
            else if(color == Color.red)
            {
                Debug.Log("sei fuori");
                pressOnImage = false;
            }
        }
    }

    Color PickColor()
    {
        Vector2 clickNorm = (evDt.position - new Vector2(Palette.rectTransform.position.x, Palette.rectTransform.position.y));
        clickNorm.x /= Palette.rectTransform.rect.width;
        clickNorm.y /= Palette.rectTransform.rect.height;
        Color c = texture.GetPixel((int)(clickNorm.x * texture.width), (int)(clickNorm.y * texture.height));
        img2.color = c;
        return c;
    }
    
}
