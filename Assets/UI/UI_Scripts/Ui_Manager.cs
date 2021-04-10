using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Manager : MonoBehaviour
{
    public static Ui_Manager instance;
    public GameObject DrawSpace, Mask;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance) instance = this;
    }



    public static void ShowDrawSpace()
    {
        instance.DrawSpace.SetActive(true);
        instance.Mask.SetActive(true);
    }

    public static void HideDrawSpace()
    {
        instance.DrawSpace.SetActive(false);
        instance.Mask.SetActive(false);
    }
}
