using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UI = UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class TestDraw01 : MonoBehaviour, IPointerDownHandler,IEventSystemHandler
{
    public UI.RawImage img;
    public int BrushSize;
    
    public Canvas canvas;
    public UI.Image imgtochange;
    public RenderTexture DrawOnThisRT;    
    public Texture2D destination;
    public Texture2D TextureToDraw;

    //public MouseMoveEvent onMouseMove;
    
    PointerEventData evDt;
    Event ev;
    bool pressOnImage;

    float ScaleFactor;

    public void OnPointerDown(PointerEventData eventData)
    {
        evDt = eventData;
        pressOnImage = true;
    }

    public void UpdatePaint()
    {
        if (pressOnImage)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector2 clickNorm = evDt.position / ScaleFactor - new Vector2(img.rectTransform.position.x, img.rectTransform.position.y) / ScaleFactor;
                clickNorm.x /= (int)(img.rectTransform.rect.width);
                clickNorm.y /= (int)(img.rectTransform.rect.height);
                // Debug.Log(clickNorm);

                RenderTexture.active = DrawOnThisRT;
                GL.PushMatrix();
                GL.LoadPixelMatrix(0, DrawOnThisRT.width, 0, DrawOnThisRT.height);

                //destination.SetPixel(0, 0, imgtochange.color);
                //destination.Apply();              

                Graphics.DrawTexture(new Rect(clickNorm.x * DrawOnThisRT.width - BrushSize / 2, clickNorm.y * DrawOnThisRT.height - BrushSize / 2, BrushSize, BrushSize), TextureToDraw);

                GL.PopMatrix();
                RenderTexture.active = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            RenderTexture.active = DrawOnThisRT;
            GL.Clear(true, true, Color.white);
            RenderTexture.active = null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressOnImage = false;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        MouseMoveEvent ev = MouseMoveEvent.GetPooled();
        //destination = new Texture2D(1, 1);
        ScaleFactor = canvas.scaleFactor;
        RenderTexture.active = DrawOnThisRT;
        GL.Clear(true, true, Color.white);
        RenderTexture.active = null;
    }    
    
    // Update is called once per frame
    void Update()
    {       

    }
}
