using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color_Wheel : MonoBehaviour
{
    public RectTransform sectionPrefab;
    public Inventory_SO playerInventory;
    public Canvas Canvas;

    float ScaleFactor;
    int NumberOfColors;
    List<ColorSection_Wheel> pointsToCheck = new List<ColorSection_Wheel>();
    ColorSection_Wheel selectedPoint;
    int selectedIndex = -1;
    List<Image> imagesOfWheel = new List<Image>();



    // Start is called before the first frame update
    void Start()
    {
        ScaleFactor = Canvas.scaleFactor;
        NumberOfColors = playerInventory.AviableColors.Length;
        Debug.Log(NumberOfColors);
        float degreeSection = 360 / NumberOfColors;
        float FillAmount = degreeSection / 360;
        for (int i = 0; i < NumberOfColors; i++)
        {
            RectTransform img = Instantiate(sectionPrefab);
            img.transform.SetParent(transform);
            img.anchoredPosition = Vector2.zero;
            img.localScale = Vector3.one;
            img.localPosition = new Vector3(img.localPosition.x, img.localPosition.y, 0);
            img.rotation = Quaternion.Euler(img.rotation.eulerAngles.x, img.rotation.eulerAngles.y, -i * degreeSection);
            img.GetComponent<Image>().fillAmount = degreeSection / 360;
            img.GetComponent<Image>().color = playerInventory.AviableColors[i];

            ColorSection_Wheel point = img.GetComponentInChildren<ColorSection_Wheel>();
            point.inkColor = (TypeOfInk)i;
            point.name = point.name + point.inkColor.ToString();
            pointsToCheck.Add(point);
            imagesOfWheel.Add(img.GetComponent<Image>());
        }
        
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
                selectedIndex = i;
            }
            pointsToCheck[i].isSelected = false;
        }
        if (minDistPoint != null)
        {
            selectedPoint = minDistPoint;
            selectedPoint.isSelected = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckWheel();
        if (Input.GetMouseButtonDown(0))
        {
            playerInventory.SetInk((TypeOfInk)selectedIndex);
        }

    }
}
