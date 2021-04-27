using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSection_Wheel : MonoBehaviour
{
    public TypeOfInk inkColor;
    public float offsetAnimation = 40;
    public RectTransform parent;

    public bool isSelected;
    public bool executeAnimation;
    public float lerpSpeed = 10;

    RectTransform own;
    public Vector2 selectedPos, notSelectedPos, posToAnim;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<RectTransform>();
        own = GetComponent<RectTransform>();
        Vector2 dir = own.position - parent.position;
        dir.Normalize();
        selectedPos = parent.anchoredPosition + (dir * offsetAnimation);
        notSelectedPos = parent.anchoredPosition;
    }
    private void OnEnable()
    {
        if (parent==null)
        {
            parent = transform.parent.GetComponent<RectTransform>();
        }
        parent.anchoredPosition = notSelectedPos;
    }
    void HaveToAnimate()
    {
        if (isSelected)
        {
            Vector2 distToPos = parent.anchoredPosition - selectedPos;
            if (distToPos.magnitude > 0.1)
            {
                executeAnimation = true;
                posToAnim = selectedPos;
            }
            else executeAnimation = false;
        }
        else
        {
            Vector2 distToPos = parent.anchoredPosition - notSelectedPos;
            if (distToPos.magnitude > 0.1)
            {
                executeAnimation = true;
                posToAnim = notSelectedPos;
            }
            else executeAnimation = false;
        }
    }
    void Update()
    {
        HaveToAnimate();
        if (executeAnimation)
        {
            PerformAnimation();
        }
    }
    void PerformAnimation()
    {
        
        parent.anchoredPosition = Vector2.Lerp(parent.anchoredPosition, posToAnim,lerpSpeed * Time.deltaTime);
    }
}
