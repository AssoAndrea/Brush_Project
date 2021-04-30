using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSection : MonoBehaviour
{
    public DrawableItem_SO item;
    public float offsetAnimation = 40;
    public RectTransform ParentOfWheel;
    public Transform newParentAfter;

    public bool isSelected;
    public bool executeAnimation;
    public float lerpSpeed = 10;

    public Vector2 selectedPos, notSelectedPos, posToAnim, dir;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void InitScr()
    {

        selectedPos = ParentOfWheel.anchoredPosition + (dir * offsetAnimation);

        notSelectedPos = ParentOfWheel.anchoredPosition;
        ParentOfWheel.anchoredPosition = notSelectedPos;

        transform.SetParent(newParentAfter);

    }
    private void OnEnable()
    {
        if (ParentOfWheel != null)
        {
            ParentOfWheel.anchoredPosition = notSelectedPos;
        }
    }
    void HaveToAnimate()
    {
        if (isSelected)
        {
            Vector2 distToPos = ParentOfWheel.anchoredPosition - selectedPos;
            if (distToPos.magnitude > 0.1)
            {
                executeAnimation = true;
                posToAnim = selectedPos;
            }
            else executeAnimation = false;
        }
        else
        {
            Vector2 distToPos = ParentOfWheel.anchoredPosition - notSelectedPos;
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

        ParentOfWheel.anchoredPosition = Vector2.Lerp(ParentOfWheel.anchoredPosition, posToAnim, lerpSpeed * Time.deltaTime);
    }
}
