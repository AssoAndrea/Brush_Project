using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestDraw01 : MonoBehaviour, IPointerDownHandler//IPointerClickHandler
{
    public RawImage img;    
    public Image imgtochange;
    public RenderTexture DrawOnThisRT;    
    public Texture2D destination;
    public Texture2D TextureToDraw;  
    
    PointerEventData evDt;
    bool pressOnImage;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        evDt = eventData;
        pressOnImage = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressOnImage = false;
    }

    // Start is called before the first frame update
    void Start()
    {      
        //destination = new Texture2D(1, 1);

        RenderTexture.active = DrawOnThisRT;
        GL.Clear(true, true, Color.white);
        RenderTexture.active = null;
    }    

    // Update is called once per frame
    void Update()
    {       
        if (pressOnImage)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector2 clickNorm = evDt.position - new Vector2(img.rectTransform.position.x, img.rectTransform.position.y);
                clickNorm.x /= img.rectTransform.rect.width;
                clickNorm.y /= img.rectTransform.rect.height;

                Debug.Log(clickNorm);

                RenderTexture.active = DrawOnThisRT;
                GL.PushMatrix();
                GL.LoadPixelMatrix(0, DrawOnThisRT.width, 0, DrawOnThisRT.height);

                //destination.SetPixel(0, 0, imgtochange.color);
                //destination.Apply();              

                Graphics.DrawTexture(new Rect(clickNorm.x * DrawOnThisRT.width - 15, clickNorm.y * DrawOnThisRT.height - 15, 30, 30), TextureToDraw);

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
}
