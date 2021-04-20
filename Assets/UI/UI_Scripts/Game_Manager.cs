using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class Game_Manager : MonoBehaviour
{
    public Inventory_SO inventory;

    [Header("Events")]
    [Space(20)]
    public UnityEvent onDrawSpaceOpen;
    public UnityEvent onDrawSpaceClose;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            DrawableItem itemToPick = new DrawableItem();
            foreach (DrawableItem item in inventory.drawableObjects.RedItem)
            {
                if (item.type == RedInkItem.Arco)
                {
                    itemToPick = item;
                    break;
                }
            }
            inventory.ItemToDraw = itemToPick.item;
            inventory.InkToUse = TypeOfInk.Red;
            inventory.SelectedItemEvent.Raise();
            inventory.SelectedInkEvent.Raise();
        }
        if (Input.GetKeyDown(KeyCode.I))
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
