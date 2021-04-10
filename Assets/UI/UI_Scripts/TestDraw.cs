using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestDraw : MonoBehaviour, IPointerClickHandler
{
    public Image img;
    public Sprite spr;
    public Image imgtochange;

    public void OnPointerClick(PointerEventData eventData)
    {
        Color c = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite.texture.GetPixel((int)(eventData.position.x - img.rectTransform.position.x), (int)(eventData.position.y - img.rectTransform.position.y));
        Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        imgtochange.color = c;
    }

    // Start is called before the first frame update
    void Start()
    {
        spr = img.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

        } 
    }
}
