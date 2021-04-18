using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class DrawMgr : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler
{
    
    public Texture2D MaskTexture;
    public float MinDistance;
    public DrawableItem_SO SelectedObject;

    Canvas canvas;
    Image OwnImage;
    List<RectTransform> points;
    RectTransform lastPoint;
    List<RectTransform> toRemove;
    bool pressOnImage;
    float ScaleFactor;
    Vector2 MouseClickOnCanvas;
    


    public void OnPointerDown(PointerEventData eventData)
    {

        MouseClickOnCanvas = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        pressOnImage = true;
        Color c = PickColor();
        if (c == Color.black)
        {
            lastPoint.anchoredPosition = RelativeMouseToImage();
        }
        

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (pressOnImage)
        {
            BoundsExit();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        pressOnImage = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        //SET IMAGE
        canvas = GetComponentInParent<Canvas>();
        OwnImage = GetComponent<Image>();
        OwnImage.sprite = SelectedObject.ImageToDisplay;
        MaskTexture = SelectedObject.Mask;


        List<RectTransform> PointToCreate = SelectedObject.GetCheckpoints();
        CreatePointsOnScene(PointToCreate);
        RectTransform[] p = GetComponentsInChildren<RectTransform>();
        ScaleFactor = canvas.scaleFactor;
        toRemove = new List<RectTransform>();
        points = new List<RectTransform>();
        foreach (RectTransform i in p)
        {
            if (i.tag == "Checkpoint")
            {
                points.Add(i);
            }
            else
            {
                lastPoint = i;
            }
        }
        
    }
    void CreatePointsOnScene(List<RectTransform> list)
    {
        foreach (RectTransform p in list)
        {
            RectTransform newP = Instantiate(p);
            newP.SetParent(transform);
            newP.anchoredPosition = p.anchoredPosition;
            newP.tag = p.tag;
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
                    //Vector2 pos = new Vector2(t.position.x, t.position.y);
                    float d = Vector3.Distance(t.anchoredPosition, RelativeMouseToImage());
                    if (d <= MinDistance)
                    {
                        Debug.Log("preso check " + t.name);
                        toRemove.Add(t);
                    }
                }
                foreach (RectTransform rectTransform in toRemove)
                {
                    points.Remove(rectTransform);
                }

                if (points.Count <=0)
                {
                    if (Vector2.Distance(lastPoint.anchoredPosition,RelativeMouseToImage())<= MinDistance)
                    {
                        pressOnImage = false;
                        Debug.Log("preso ultimo");

                        Debug.Log("disegno completato");
                    }


                }
            }
            else
            {

                BoundsExit();
            }
        }
    }
    public void BoundsExit()
    {
        pressOnImage = false;
        Debug.Log("fuori");
    }


    #region utility
    Color PickColor()
    {
        MouseClickOnCanvas = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 clickNorm = RelativeMouseToImage();


        clickNorm.x /= OwnImage.rectTransform.rect.width;
        clickNorm.y /= OwnImage.rectTransform.rect.height;

        Color c = MaskTexture.GetPixel((int)(clickNorm.x * MaskTexture.width), (int)(clickNorm.y * MaskTexture.height));
        return c;
    }

    Vector2 RelativeMouseToImage()
    {
        Vector2 res = Vector2.zero;
        res = (MouseClickOnCanvas / ScaleFactor )- new Vector2(Camera.main.WorldToScreenPoint(OwnImage.transform.position).x, Camera.main.WorldToScreenPoint(OwnImage.transform.position).y);
        return res;
    } 
    #endregion
}
