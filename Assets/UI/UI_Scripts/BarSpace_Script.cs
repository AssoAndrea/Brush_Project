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


    float redInk;
    float whiteInk;
    float greenInk;
    // Start is called before the first frame update
    void Start()
    {
        redInk = 1;
        whiteInk = 1;
        greenInk = 1;
    }


    public void SetRedInkVal(float value) => UpdateInkSlider(redInkFill, value);
    public void SetWhiteInkVal(float value) => UpdateInkSlider(whiteInkFill, value);
    public void SetGreenInkVal(float value) => UpdateInkSlider(greenInkFill, value);

    public void UpdateInkSlider(Image slider, float val) => slider.fillAmount = val;
}
