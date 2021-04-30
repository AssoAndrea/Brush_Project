using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Wheel_Mgr : MonoBehaviour
{
    public RectTransform ColorWheel;
    public RectTransform RedItemWheel;
    public RectTransform GreenItemWheel;
    public RectTransform WhiteItemWheel;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OpenItemWheel()
    {
        switch (Game_Manager.instance.inventory.InkToUse)
        {
            case TypeOfInk.White:
                ShowWhiteItemWheel();
                break;
            case TypeOfInk.Red:
                ShowRedItemWheel();
                break;
            case TypeOfInk.Green:
                ShowGreenItemWheel();
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
    public void CloseItemWheel()
    {
        RedItemWheel.gameObject.SetActive(false);
        GreenItemWheel.gameObject.SetActive(false);
        WhiteItemWheel.gameObject.SetActive(false);
    }
    public void CloseAllWheel()
    {
        ColorWheel.gameObject.SetActive(false);
        RedItemWheel.gameObject.SetActive(false);
        GreenItemWheel.gameObject.SetActive(false);
        WhiteItemWheel.gameObject.SetActive(false);
    }
    public void ShowColorWheel()
    {
        CloseAllWheel();
        ColorWheel.gameObject.SetActive(true);
    }
    public void ShowRedItemWheel()
    {
        CloseAllWheel();
        RedItemWheel.gameObject.SetActive(true);
    }
    public void ShowGreenItemWheel()
    {
        CloseAllWheel();
        GreenItemWheel.gameObject.SetActive(true);
    }
    public void ShowWhiteItemWheel()
    {
        CloseAllWheel();
        WhiteItemWheel.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
