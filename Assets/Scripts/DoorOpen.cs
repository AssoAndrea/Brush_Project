using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject Button;

    bool isOpen;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.GetComponent<ButtonScript>().Activated)
            isOpen = true;

        if (isOpen)
            anim.SetBool("open", true);
            
    }
}
