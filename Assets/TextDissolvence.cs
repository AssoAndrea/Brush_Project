using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDissolvence : MonoBehaviour
{
    public GameObject Player;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha=Vector2.Distance(this.transform.position, Player.transform.position);
        if (alpha < 3)
        {
            alpha = 1;
        }
        else if(alpha>=4)
        {
            alpha=0;
        }
        else
        {
            alpha = 1-(alpha % 1);
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
