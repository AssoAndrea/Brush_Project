using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remaining_Color_Scr : MonoBehaviour
{

    public Image ImageToFill;
    public Inventory_SO inventory;

    Color color;
    Image image;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

    }
    void SetColor(Color c)
    {

        //set c
        if (image== null)
        {
            image = GetComponent<Image>();
        }

        color = c;
        image.color = c;
        ImageToFill.color = c;
    }
    void SetFill(float val)
    {
        ImageToFill.fillAmount = val;
    }
    private void OnEnable()
    {
        UpdateBar();
    }
    public void UpdateBar()
    {
        switch (inventory.InkToUse)
        {
            case TypeOfInk.White:
                SetColor(Color.white);
                SetFill(inventory.currWhiteInk / inventory.maxWhiteInk);
                break;
            case TypeOfInk.Red:
                SetColor(Color.red);
                SetFill(inventory.currRedInk / inventory.maxRedInk);
                break;
            case TypeOfInk.Green:
                SetColor(Color.green);
                SetFill(inventory.currGreenInk/ inventory.maxGreenInk);
                break;
        }
    }
}
