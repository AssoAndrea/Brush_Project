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
    }
    void ItemSelection()
    {
        if (Input.GetKeyDown(SelectBow))
        {
            DrawableItem itemToPick = new DrawableItem();
            foreach (DrawableItem item in inventory.drawableObjects.RedItem)
            {
                if (item.type == ItemType.Bow)
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
            DrawableItem itemToPick = new DrawableItem();
            foreach (DrawableItem item in inventory.drawableObjects.RedItem)
            {
                if (item.type == ItemType.Sword)
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
            DrawableItem itemToPick = new DrawableItem();
            foreach (DrawableItem item in inventory.drawableObjects.RedItem)
            {
                if (item.type == ItemType.Hammer)
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
                inventory.DrawSpaceOpen = false;
                onDrawSpaceClose.Invoke();
            }
            else
            {
                inventory.DrawSpaceOpen = true;
                onDrawSpaceOpen.Invoke();
            }
        }
    }
}
