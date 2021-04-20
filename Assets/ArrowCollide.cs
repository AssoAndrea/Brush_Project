using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollide : MonoBehaviour
{
    public SpriteRenderer sr;
    public Collider2D coll;
    Rigidbody2D rb;
    bool Hitted = false;
    Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startRot = transform.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Hitted)
        {
            Color c = sr.color;
            sr.color = new Color(c.r, c.g, c.b, c.a - (.5f * Time.deltaTime));
            if (sr.color.a < 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(coll);
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
        transform.rotation = startRot;
        Hitted = true;
    }
}
