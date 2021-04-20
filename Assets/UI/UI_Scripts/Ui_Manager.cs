using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Manager : MonoBehaviour
{
    public static Ui_Manager instance;
    public DrawMgr DrawSpace;
    public Inventory_SO inventory;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance) instance = this;
        inventory.ResetInventory();
    }



    public void ShowDrawSpace()
    {
        instance.DrawSpace.gameObject.SetActive(true);
    }

    public void HideDrawSpace()
    {
        instance.DrawSpace.gameObject.SetActive(false);
    }
    public void SetItem()
    {
        DrawSpace.SetObjectToDraw(inventory.ItemToDraw);
    }
    public void Prova()
    {
        Debug.Log("chiamato");
    }
 
}
