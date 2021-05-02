using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsScript : MonoBehaviour
{
    public GameObject Player;

    bool isFreezed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFreezed)
        {
            isFreezed = true;
        }
        else if (!isFreezed)
        {
            if (Input.GetKey(KeyCode.T))
                transform.gameObject.GetComponent<SpriteRenderer>().size += new Vector2(0, 0.01f);
            else if (Input.GetKey(KeyCode.G))
                transform.gameObject.GetComponent<SpriteRenderer>().size -= new Vector2(0, 0.01f);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            Player.gameObject.GetComponent<PlayerMove>().onStairs = true;        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            Player.gameObject.GetComponent<PlayerMove>().onStairs = false;
    }
}
