using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color_Wheel : MonoBehaviour
{
    public RectTransform sectionPrefab;
    public Inventory_SO playerInventory;
    public Canvas Canvas;
    public RectTransform centerPrefab;
    public RectTransform SpriteOnSectionPrefab;
    public float WheelRotationOffset;

    float ScaleFactor;
    int NumberOfColors;
    List<ColorSection_Wheel> pointsToCheck = new List<ColorSection_Wheel>();
    ColorSection_Wheel selectedPoint;
    


    private void ResetTransform(ref RectTransform tr)
    {
        tr.anchoredPosition = Vector2.zero;
        tr.localScale = Vector3.one;
        tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y, 0);
    }
    void WheelCreation()
    {
        ScaleFactor = Canvas.scaleFactor;
        NumberOfColors = playerInventory.colorsInventory.colors.Length;
        float degreeSection = 360 / NumberOfColors;
        float FillAmount = degreeSection / 360;
        for (int i = 0; i < NumberOfColors; i++)
        {
            RectTransform img = Instantiate(sectionPrefab, transform);
            Vector3 dirr;

            float zDegRot = -(i * degreeSection) +WheelRotationOffset;
            img.GetComponent<Image>().fillAmount = degreeSection / 360;
            img.rotation = Quaternion.Euler(img.rotation.eulerAngles.x, img.rotation.eulerAngles.y, zDegRot);
            Color colorToApply;
            img.name = "Section " + playerInventory.colorsInventory.colors[i].inkType.ToString();
            dirr = img.transform.up;
            if (playerInventory.colorsInventory.colors[i].IsLocked) colorToApply = playerInventory.colorsInventory.LockedColor;
            else colorToApply = playerInventory.colorsInventory.colors[i].color;
            img.GetComponent<Image>().color = colorToApply;

            RectTransform point = Instantiate(centerPrefab, img.transform);
            point.name = "center of " + img.name;
            ColorSection_Wheel scr = point.GetComponent<ColorSection_Wheel>();
            scr.ParentOfWheel = img.GetComponent<RectTransform>();



            point.transform.localRotation = Quaternion.Euler(0, 0, point.transform.localRotation.eulerAngles.z - degreeSection / 2);
            point.position += point.transform.up * 2.6f;
            if (playerInventory.colorsInventory.colors[i].IsLocked)
            {
                RectTransform imgOnSection = Instantiate(SpriteOnSectionPrefab,point.transform);
                imgOnSection.SetParent(img.transform);
                imgOnSection.GetComponent<Image>().sprite = playerInventory.colorsInventory.LockedSprite;
            }
            Vector3 dd = point.position - img.position;
            dd.Normalize();
            scr.dir = point.position - img.position;
            scr.inkColor = playerInventory.colorsInventory.colors[i].inkType;
            scr.ParentOfWheel = img;
            pointsToCheck.Add(scr);
            scr.newParentAfter = transform;
            scr.InitScr();


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        WheelCreation();
    }

    private Vector2 RectToScreen(RectTransform rect)
    {
        return Camera.main.WorldToScreenPoint(rect.transform.position);
    }
    private void CheckWheel()
    {
        ColorSection_Wheel minDistPoint = null;
        float lastDist = float.MaxValue;
        for (int i = 0; i < pointsToCheck.Count; i++)
        {
            Vector2 mousePosV2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RectTransform tr = pointsToCheck[i].GetComponent<RectTransform>();
            float dist = (mousePosV2 - RectToScreen(tr)).magnitude;
            if (dist < lastDist)
            {
                lastDist = dist;
                minDistPoint = pointsToCheck[i];


                
            }
            pointsToCheck[i].isSelected = false;
        }
        if (minDistPoint != null)
        {
            if (playerInventory.colorsInventory.IsColorUnlocked(minDistPoint.inkColor))
            {
                selectedPoint = minDistPoint;
                selectedPoint.isSelected = true;
                playerInventory.SetInk(selectedPoint.inkColor);
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckWheel();

    }
}
