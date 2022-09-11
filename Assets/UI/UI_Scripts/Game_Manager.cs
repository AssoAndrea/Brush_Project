using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    public Inventory_SO inventory;

    [Header("DEBUG KEYCODE")]
    [Space(20)]
    public KeyCode OpenAndCloseInventory = KeyCode.I;
    public KeyCode SelectBow = KeyCode.Alpha1;
    public KeyCode SelectSword = KeyCode.Alpha2;
    public KeyCode SelectHammer = KeyCode.Alpha3;


    [Header("Events")]
    [Space(20)]
    public UnityEvent onDrawSpaceOpen;
    public UnityEvent onDrawSpaceClose;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        inventory.InkToUse = TypeOfInk.Red;
        inventory.SelectedInkEvent.Raise();
        inventory.ItemToDraw = inventory.drawableObjects.RedItem[0].item;
        inventory.SelectedItemEvent.Raise();
        SoundMgr.PlayOneShot("MainMusic");

    }
    void ItemSelection()
    {
        if (Input.GetKeyDown(SelectBow))
        {
            RedDrawableItem itemToPick = new RedDrawableItem();
            foreach (RedDrawableItem item in inventory.drawableObjects.RedItem)
            {
                if (item.type == RedItem.Bow)
                {
                    itemToPick = item;
                    break;
                }
            }
            inventory.ItemToDraw = itemToPick.item;
            //inventory.InkToUse = TypeOfInk.Red;
            //inventory.SelectedInkEvent.Raise();
            inventory.SelectedItemEvent.Raise();
        }
        if (Input.GetKeyDown(SelectSword))
        {
            RedDrawableItem itemToPick = new RedDrawableItem();
            foreach (RedDrawableItem item in inventory.drawableObjects.RedItem)
            {
                if (item.type == RedItem.Sword)
                {
                    itemToPick = item;
                    break;
                }
            }
            inventory.ItemToDraw = itemToPick.item;
            //inventory.InkToUse = TypeOfInk.Red;
            //inventory.SelectedInkEvent.Raise();
            inventory.SelectedItemEvent.Raise();
        }
        if (Input.GetKeyDown(SelectHammer))
        {
            RedDrawableItem itemToPick = new RedDrawableItem();
            foreach (RedDrawableItem item in inventory.drawableObjects.RedItem)
            {
                if (item.type == RedItem.Hammer)
                {
                    itemToPick = item;
                    break;
                }
            }
            inventory.ItemToDraw = itemToPick.item;
            //inventory.InkToUse = TypeOfInk.Red;
            //inventory.SelectedInkEvent.Raise();
            inventory.SelectedItemEvent.Raise();
        }
    }
    // Update is called once per frame
    void Update()
    {
        ItemSelection();

        if (Input.GetKeyDown(OpenAndCloseInventory))
        {
            if (inventory.DrawSpaceOpen)
            {
                Ui_Manager.instance.HideDrawSpace();
                onDrawSpaceClose.Invoke();
            }
            else
            {
                Ui_Manager.instance.ShowDrawSpace();

                onDrawSpaceOpen.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
