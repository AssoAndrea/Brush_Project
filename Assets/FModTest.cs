using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FModTest : MonoBehaviour
{
    [FMODUnity.ParamRef]
    public string param;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(param, 0);
        }
    }
}
