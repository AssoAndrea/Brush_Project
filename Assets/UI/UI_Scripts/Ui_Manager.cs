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
    public Color_Wheel ColorWheel;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance) instance = this;
        inventory.ResetInventory();
    }
    void Update()
    {
        InputHandler();
    }
    private void InputHandler()
    {
        if (!inventory.DrawSpaceOpen)
        {
            ColorWheel.gameObject.SetActive(Input.GetKey(KeyCode.Q));
        }
        else
        {
            ColorWheel.gameObject.SetActive(false);
        }
    }

    public void ItemDrawComplete()
    {
        HideDrawSpace();
        Player.WeaponHandle = inventory.ItemToDraw.itemType;
    }
    public void ShowDrawSpace()
    {
        DrawSpace.gameObject.SetActive(true);
    }

    public void HideDrawSpace()
    {
        DrawSpace.gameObject.SetActive(false);
    }
    public void SetItem()
    {
        ImageToDraw.SetObjectToDraw(inventory.ItemToDraw);
    }

 
}
