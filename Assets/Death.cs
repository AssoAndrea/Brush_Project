using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public Image DeathImage;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("Damaged"))
        {
            DeathImage.gameObject.SetActive(true);
            DeathImage.color = new Color(DeathImage.color.r, DeathImage.color.g, DeathImage.color.b, DeathImage.color.a + 1 * Time.deltaTime);
            if (DeathImage.color.a >= 1)
            {
                DeathImage.color = new Color(DeathImage.color.r, DeathImage.color.g, DeathImage.color.b,1);
                anim.SetBool("Damaged", false);
                SceneManager.LoadScene(2);

            }
        }
        else if (DeathImage.color.a > 0)
        {
            DeathImage.color = new Color(DeathImage.color.r, DeathImage.color.g, DeathImage.color.b, DeathImage.color.a+-1*Time.deltaTime);
        }
        else
        {
            DeathImage.gameObject.SetActive(false);
        }

    }
}
