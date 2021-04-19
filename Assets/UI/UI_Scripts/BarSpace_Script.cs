using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSpace_Script : MonoBehaviour
{
    [Header("Remaining Ink")]
    public Image redInkFill;
    public Image whiteInkFill;
    public Image greenInkFill;
    public Inventory_SO inventory;



    // Start is called before the first frame update
    void Start()
    {
        UpdateInkUI();
    }
    public void UpdateInkUI()
    {
        redInkFill.fillAmount = inventory.currRedInk / inventory.maxRedInk;
        whiteInkFill.fillAmount = inventory.currWhiteInk / inventory.maxWhiteInk;
        greenInkFill.fillAmount = inventory.currGreenInk / inventory.maxGreenInk;
        Debug.Log(redInkFill.fillAmount);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.AddInk(TypeOfInk.Red, 5);
        }

    }
}
