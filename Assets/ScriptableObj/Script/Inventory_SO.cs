using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Player/Inventory")]
public class Inventory_SO : ScriptableObject
{
    [Header("Inventory")]
    public Color[] AviableColors;
    public TypeOfInk InkToUse;
    public bool DrawSpaceOpen;
    public DrawableObjectsInventory drawableObjects;
    public ColorsInventory colorsInventory;
    public DrawableItem_SO ItemToDraw;
    public bool IsPlacing;

    [Header("Events")]
    public GameEvent UpdateInkEvent;
    public GameEvent SelectedInkEvent;
    public GameEvent SelectedItemEvent;

    [Header("Ink Value")]
    public float InkLoseWhileDraw = 5;
    public float StartRedInk;
    public float StartWhiteInk;
    public float StartGreenInk;
    public float maxRedInk, maxWhiteInk, maxGreenInk;
    public float currRedInk, currWhiteInk, currGreenInk;

    public void SetInk(TypeOfInk ink)
    {
        InkToUse = ink;
        UpdateInkEvent.Raise();
    }

    public void SetItemToDraw(DrawableItem_SO item)
    {
        ItemToDraw = item;
        SelectedItemEvent.Raise();
    }
    public void SetDefaultItem()
    {
        switch (InkToUse)
        {
            case TypeOfInk.White:
                ItemToDraw = drawableObjects.WhiteItem[0].item;
                break;
            case TypeOfInk.Red:
                ItemToDraw = drawableObjects.RedItem[0].item;
                break;
            case TypeOfInk.Green:
                ItemToDraw = drawableObjects.GreenItem[0].item;
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
        SelectedItemEvent.Raise();
    }

    public void AddInk(TypeOfInk ink, float amount)
    {
        switch (ink)
        {
            case TypeOfInk.White:
                float newVal = currWhiteInk + amount;
                if (newVal > maxWhiteInk)
                {
                    newVal = maxWhiteInk;
                }
                else if(newVal < 0)
                {
                    newVal = 0;
                }
                currWhiteInk = newVal;
                break;
            case TypeOfInk.Red:
                newVal = currRedInk + amount;
                if (newVal > maxRedInk)
                {
                    newVal = maxWhiteInk;
                }
                else if (newVal < 0)
                {
                    newVal = 0;
                }
                currRedInk = newVal;
                break;
            case TypeOfInk.Green:
                newVal = currGreenInk + amount;
                if (newVal > maxGreenInk)
                {
                    newVal = maxGreenInk;
                }
                else if (newVal < 0)
                {
                    newVal = 0;
                }
                currGreenInk = newVal;
                break;
            case TypeOfInk.Last:
                break;
            default:
                break;
        }

        UpdateInkEvent.Raise();
    }
    public float GetCurrentInk()
    {
        switch (InkToUse)
        {
            case TypeOfInk.White:
                return currWhiteInk;
            case TypeOfInk.Red:
                return currRedInk;
            case TypeOfInk.Green:
                return currGreenInk;
            default:
                return currGreenInk;
        }
    }
    public void SetInkVal(TypeOfInk ink,float val)
    {
        switch (ink)
        {
            case TypeOfInk.White:
                currWhiteInk = val;
                break;
            case TypeOfInk.Red:
                currRedInk = val;
                break;
            case TypeOfInk.Green:
                currGreenInk = val;
                break;
        }
        UpdateInkEvent.Raise();
    }
    public void RemoveInkWhileDraw()
    {
        AddInk(InkToUse, -InkLoseWhileDraw * Time.deltaTime);
        UpdateInkEvent.Raise();
    }

    //SOLO A SCOPO DEBUG
    public void ResetInventory()
    {
        currRedInk = StartRedInk;
        currGreenInk = StartGreenInk;
        currWhiteInk = StartWhiteInk;
        DrawSpaceOpen = false;

        UpdateInkEvent.Raise();
    }
    



}
