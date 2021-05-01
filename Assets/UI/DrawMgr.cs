using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class DrawMgr : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler
{
    
    public float MinDistance;
    public DrawableItem_SO SelectedObject;
    public PaintLine brush;

    [Header("Events")]
    public UnityEvent DrawFail;
    public UnityEvent DrawComplete;

    Texture2D MaskTexture;
    Canvas canvas;
    Image OwnImage;
    List<RectTransform> points;
    RectTransform lastPoint;
    List<RectTransform> toRemove;
    List<RectTransform> originalList = new List<RectTransform>();
    bool pressOnImage;
    float ScaleFactor;
    Vector2 MouseClickOnCanvas;

    //saved ink
    float startInk;



    public void SetObjectToDraw(DrawableItem_SO item)
    {
        SelectedObject = item;
        if (OwnImage == null)
        {
            InitComponent();
        }
        OwnImage.sprite = SelectedObject.ImageToDisplay;
        MaskTexture = SelectedObject.Mask;
    }

    //DOVE VENGONO CREATI I PUNTI E SETTATI I CHECKPOINT
    #region INIZIALIZZAZIONE 
    private void SetImageCheckPoints()
    {
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
                originalList.Add(i);
            }
            else
            {
                lastPoint = i;
            }
        }
    }

    void InitComponent()
    {
        canvas = GetComponentInParent<Canvas>();
        OwnImage = GetComponent<Image>();
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
    #endregion 


    private void ResetPoints()
    {
        foreach (RectTransform rectTransform in originalList)
        {
            Destroy(rectTransform.gameObject);
        }
        originalList.Clear();
        Destroy(lastPoint.gameObject);
    }


    
    private void DrawLogic()
    {
        if (pressOnImage)
        {
            brush.canDraw = true;
            Color color = PickColor();

            if (color == Color.black)
            {
                foreach (RectTransform t in points)
                {
                    //Vector2 pos = new Vector2(t.position.x, t.position.y);
                    float d = Vector3.Distance(t.anchoredPosition, RelativeMouseToImage());
                    if (d <= MinDistance)
                    {
                        //Debug.Log("preso check " + t.name);
                        toRemove.Add(t);
                    }
                }
                foreach (RectTransform rectTransform in toRemove)
                {
                    points.Remove(rectTransform);
                }

                if (points.Count <= 0)
                {
                    if (Vector2.Distance(lastPoint.anchoredPosition, RelativeMouseToImage()) <= MinDistance)
                    {
                        pressOnImage = false;
                        brush.canDraw = false;
                        DrawComplete.Invoke();
                        Debug.Log("disegno completato");
                    }

                }
            }
            else
            {
                OnFail();
            }
        }
        else
        {
            brush.canDraw = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        DrawLogic();
        if (pressOnImage)
        {
            Game_Manager.instance.inventory.RemoveInkWhileDraw();
        }

        if (Input.GetKeyDown(KeyCode.P) )
        {
            Debug.Log("DrawComplete.Invoke()");
            pressOnImage = false;
            brush.canDraw = false;
            DrawComplete.Invoke();
        }
        
    }
    public void OnFail()
    {
        pressOnImage = false;
        points.Clear();
        brush.canDraw = false;
        foreach (RectTransform tr in originalList)
        {
            points.Add(tr);
        }
        Game_Manager.instance.inventory.SetInkVal(Game_Manager.instance.inventory.InkToUse, startInk);
        Debug.Log("fuori");
        DrawFail.Invoke();
    }

    #region Pointer CallBack
    public void OnPointerExit(PointerEventData eventData)
    {
        if (pressOnImage)
        {
            OnFail();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pressOnImage)
        {
            OnFail();
        }
        pressOnImage = false;
    }
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
    #endregion

    #region UnityCallback
    void Start()
    {

    }
   
    private void OnEnable()
    {

        OwnImage.sprite = SelectedObject.ImageToDisplay;
        MaskTexture = SelectedObject.Mask;
        startInk = Game_Manager.instance.inventory.GetCurrentInk();

        SetImageCheckPoints();
    }
    private void OnDisable()
    {
        ResetPoints();
    } 
    #endregion

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
