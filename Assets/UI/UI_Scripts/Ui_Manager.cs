using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Manager : MonoBehaviour
{
    public static Ui_Manager instance;
    public DrawMgr ImageToDraw;
    public WeaponSystem Player;
    public DrawSpace_Mgr DrawSpace;
    public Inventory_SO inventory;
    public Selection_Wheel_Mgr Wheel_Mgr;
    public KeyCode openColorWheel, openItemWheel;

    public Canvas canvas;
    public bool oneWheelAlreadyOpen = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!instance) instance = this;
        inventory.ResetInventory();
    }
    void Start()
    {

    }
    void Update()
    {
        InputHandler();
    }
    private void InputHandler()
    {
        if (!inventory.DrawSpaceOpen)
        {
            if (Input.GetKeyDown(openColorWheel))
            {
                Wheel_Mgr.ShowColorWheel();
                oneWheelAlreadyOpen = true;
            }
            else if (Input.GetKeyUp(openColorWheel))
            {
                Wheel_Mgr.CloseAllWheel();
                oneWheelAlreadyOpen = false;
            }
            if (Input.GetKeyDown(openItemWheel))
            {
                Wheel_Mgr.OpenItemWheel();
                oneWheelAlreadyOpen = true;
            }
            else if (Input.GetKeyUp(openItemWheel))
            {
                Wheel_Mgr.CloseItemWheel();
                ShowDrawSpace();
                oneWheelAlreadyOpen = false;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                HideDrawSpace();
            }
        }

    }

    public void ItemDrawComplete()
    {
        inventory.DrawSpaceOpen = false;
        HideDrawSpace();
        switch (inventory.InkToUse)
        {
            case TypeOfInk.White:
                break;
            case TypeOfInk.Red:
                RedInkItem_SO redItem = (RedInkItem_SO)inventory.ItemToDraw;
                Player.WeaponHandle = redItem.TypeOfItem;
                break;
            case TypeOfInk.Green:
                break;
            case TypeOfInk.Last:
                break;
            default:
                break;
        }
        //Player.WeaponHandle = inventory.ItemToDraw.itemType;
    }
    public void ShowDrawSpace()
    {
        if (inventory.GetCurrentInk() > inventory.ItemToDraw.InkToRemove)
        {
            inventory.DrawSpaceOpen = true;
            DrawSpace.gameObject.SetActive(true);
        }
    }

    public void HideDrawSpace()
    {
        inventory.DrawSpaceOpen = false;

        DrawSpace.gameObject.SetActive(false);
    }
    public void SetItem()
    {
        ImageToDraw.SetObjectToDraw(inventory.ItemToDraw);
    }

 
}
