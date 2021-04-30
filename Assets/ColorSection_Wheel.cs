using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSection_Wheel : MonoBehaviour
{
    public TypeOfInk inkColor;
    public float offsetAnimation = 40;
    public RectTransform par;
    public Transform newParentAfter;

    public bool isSelected;
    public bool executeAnimation;
    public float lerpSpeed = 10;

    RectTransform own;
    public Vector2 selectedPos, notSelectedPos, posToAnim,dir;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void InitScr()
    {
        own = GetComponent<RectTransform>();

        selectedPos = par.anchoredPosition + (dir * offsetAnimation);

        notSelectedPos = par.anchoredPosition;
        par.anchoredPosition = notSelectedPos;

        transform.SetParent(newParentAfter);

    }
    private void OnEnable()
    {
        if (par != null)
        {
            par.anchoredPosition = notSelectedPos;
        }
    }
    void HaveToAnimate()
    {
        if (isSelected)
        {
            Vector2 distToPos = par.anchoredPosition - selectedPos;
            if (distToPos.magnitude > 0.1)
            {
                executeAnimation = true;
                posToAnim = selectedPos;
            }
            else executeAnimation = false;
        }
        else
        {
            Vector2 distToPos = par.anchoredPosition - notSelectedPos;
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
        
        par.anchoredPosition = Vector2.Lerp(par.anchoredPosition, posToAnim,lerpSpeed * Time.deltaTime);
    }
}
