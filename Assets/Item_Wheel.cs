using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Wheel : MonoBehaviour
{
    public RectTransform sectionPrefab;
    public Inventory_SO playerInventory;
    public RectTransform centerPrefab;
    public RectTransform SpriteOnSectionPrefab;
    public TypeOfInk colorOfInk;
    public Color FillColor;
    public float WheelRotationOffset = 45;

    public DrawableItem[] itemsOnWheel;
    int NumberOfItem;
    List<ItemSection> pointsToCheck = new List<ItemSection>();
    ItemSection selectedPoint;

    void WheelCreation()
    {
        NumberOfItem = itemsOnWheel.Length;
        float degreeSection = 360 / NumberOfItem;
        float FillAmount = degreeSection / 360;
        for (int i = 0; i < NumberOfItem; i++)
        {
            RectTransform img = Instantiate(sectionPrefab, transform);
            Vector3 dirr;

            float zDegRot = -(i * degreeSection) + WheelRotationOffset;
            img.GetComponent<Image>().fillAmount = degreeSection / 360;
            img.rotation = Quaternion.Euler(img.rotation.eulerAngles.x, img.rotation.eulerAngles.y, zDegRot);

            img.name = "Section " +itemsOnWheel[i].item.name;
            dirr = img.transform.up;
            if (itemsOnWheel[i].isLocked) FillColor = playerInventory.colorsInventory.LockedColor;
            
            img.GetComponent<Image>().color = FillColor;

            RectTransform point = Instantiate(centerPrefab, img.transform);
            point.name = "center of " + img.name;
            ItemSection scr = point.GetComponent<ItemSection>();
            scr.ParentOfWheel = img.GetComponent<RectTransform>();



            point.transform.localRotation = Quaternion.Euler(0, 0, point.transform.localRotation.eulerAngles.z - degreeSection / 2);
            point.position += point.transform.up * 2.6f;
            if (itemsOnWheel[i].isLocked)
            {
                RectTransform imgOnSection = Instantiate(SpriteOnSectionPrefab, point.transform);
                imgOnSection.SetParent(img.transform);
                imgOnSection.GetComponent<Image>().sprite = playerInventory.colorsInventory.LockedSprite;
            }
            else
            {
                RectTransform imgOnSection = Instantiate(SpriteOnSectionPrefab, point.transform);
                imgOnSection.SetParent(img.transform);
                imgOnSection.GetComponent<Image>().sprite = itemsOnWheel[i].item.ImageToDisplay;
            }
            Vector3 dd = point.position - img.position;
            dd.Normalize();
            scr.dir = point.position - img.position;
            scr.item = itemsOnWheel[i].item;
            scr.ParentOfWheel = img;
            pointsToCheck.Add(scr);
            scr.newParentAfter = transform;
            scr.InitScr();


        }
    }

    private Vector2 RectToScreen(RectTransform rect)
    {
        return Camera.main.WorldToScreenPoint(rect.transform.position);
    }
    private void CheckWheel()
    {
        ItemSection minDistPoint = null;
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
            if (isItemUnlocked(minDistPoint.item))
            {
                selectedPoint = minDistPoint;
                selectedPoint.isSelected = true;
                playerInventory.SetItemToDraw(minDistPoint.item);
            }


        }
    }

    bool isItemUnlocked(DrawableItem_SO itemToCheck)
    {
        foreach (DrawableItem item in itemsOnWheel)
        {
            if (item.item.name == itemToCheck.name)
            {
                if (item.isLocked)
                {
                    Debug.Log(item.item.name + "is locked");
                    return false;
                }
                
            }
        }
        return true;
    }
    void Switch_Color()
    {
        switch (colorOfInk)
        {
            case TypeOfInk.White:
                itemsOnWheel = playerInventory.drawableObjects.WhiteItem;
                playerInventory.ItemToDraw = playerInventory.drawableObjects.WhiteItem[0].item;
                break;
            case TypeOfInk.Red:
                itemsOnWheel = playerInventory.drawableObjects.RedItem;
                playerInventory.ItemToDraw = playerInventory.drawableObjects.RedItem[0].item;
                break;
            case TypeOfInk.Green:
                itemsOnWheel = playerInventory.drawableObjects.GreenItem;
                playerInventory.ItemToDraw = playerInventory.drawableObjects.GreenItem[0].item;
                break;
            case TypeOfInk.Blue:
                break;
            case TypeOfInk.Orange:
                break;
            case TypeOfInk.Purple:
                break;
            case TypeOfInk.Yellow:
                break;
            case TypeOfInk.Last:
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Switch_Color();

        WheelCreation();
        //Debug.Log(pointsToCheck.Count);
    }
    // Update is called once per frame
    void Update()
    {
        CheckWheel();
    }
}
