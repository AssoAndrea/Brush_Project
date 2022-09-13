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
    List<RectTransform> checkpoints;
    RectTransform lastPoint;
    List<RectTransform> toRemove;
    List<RectTransform> originalList = new List<RectTransform>();
    bool pressOnImage;
    float ScaleFactor;
    Vector2 MouseClickOnCanvas;

    public GameObject debug;

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

    //CHECKPOINT CREATION AND SETTINGS
    #region INIZIALIZZAZIONE 
    private void SetImageCheckPoints()
    {
        List<RectTransform> PointToCreate = SelectedObject.GetCheckpoints();
        CreatePointsOnScene(PointToCreate);
        RectTransform[] points = GetComponentsInChildren<RectTransform>();
        ScaleFactor = canvas.scaleFactor;
        toRemove = new List<RectTransform>();
        this.checkpoints = new List<RectTransform>();
        foreach (RectTransform point in points)
        {
            if (point.tag == "Checkpoint")
            {
                this.checkpoints.Add(point);
                originalList.Add(point);
            }
            else
            {
                lastPoint = point;
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

            //Check if mouse is over the black mask and if user pass all checkpoints
            if (color == Color.black)
            {
                foreach (RectTransform rectTransform in checkpoints)
                {
                    float distance = Vector3.Distance(rectTransform.anchoredPosition, RelativeMouseToImage());
                    if (distance <= MinDistance)
                    {
                        toRemove.Add(rectTransform);
                    }
                }
                foreach (RectTransform rectTransform in toRemove)
                {
                    checkpoints.Remove(rectTransform);
                }
                toRemove.Clear();

                if (checkpoints.Count <= 0) //Completed
                {
                    if (Vector2.Distance(lastPoint.anchoredPosition, RelativeMouseToImage()) <= MinDistance)
                    {
                        pressOnImage = false;
                        brush.canDraw = false;
                        DrawComplete.Invoke();
                        Game_Manager.instance.inventory.AddInk(Game_Manager.instance.inventory.InkToUse, -Game_Manager.instance.inventory.ItemToDraw.InkToRemove);
                        Game_Manager.instance.inventory.UpdateInkEvent.Raise();
                        Game_Manager.instance.inventory.DrawSpaceOpen = false;
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
        #region Debug
        //if (Input.GetKeyDown(KeyCode.P) )
        //{
        //    Debug.Log("DrawComplete.Invoke()");
        //    pressOnImage = false;
        //    brush.canDraw = false;
        //    DrawComplete.Invoke();
        //}
        #endregion
    }
    public void OnFail()
    {
        toRemove.Clear();
        pressOnImage = false;
        checkpoints.Clear();
        brush.canDraw = false;
        foreach (RectTransform tr in originalList)
        {
            checkpoints.Add(tr);
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
        Color color = PickColor();
        if (color == Color.black)
        {
            lastPoint.anchoredPosition = RelativeMouseToImage();
        }


    }
    #endregion

    #region UnityCallback
   
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


        Color color = MaskTexture.GetPixel((int)(clickNorm.x * MaskTexture.width), (int)(clickNorm.y * MaskTexture.height));
        return color;
    }

    Vector2 RelativeMouseToImage()
    {
        Vector2 res = Vector2.zero;
        res = MouseClickOnCanvas /*/ ScaleFactor )*/- new Vector2(Camera.main.WorldToScreenPoint(OwnImage.transform.position).x, Camera.main.WorldToScreenPoint(OwnImage.transform.position).y);
        res/= ScaleFactor;
        return res;
    } 
    #endregion
}
