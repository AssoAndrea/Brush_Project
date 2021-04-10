using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    static public Game_Manager instance;
    public BarSpace_Script barSpace;
    // Start is called before the first frame update
    void Start()
    {
        if (!instance) instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
