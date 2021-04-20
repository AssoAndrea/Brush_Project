using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Scorpion,Snake,Bee}

public class EnemyDamege : MonoBehaviour
{
    public EnemyType Type;
    bool Hitted = false;
    Rigidbody2D rb;
    Collider2D collider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Type == EnemyType.Scorpion)
        {
            if (collision.tag == "Hammer")
            {
                Hitted = true;
                rb.AddForce(Vector3.up * 150);
                rb.freezeRotation = false;
                rb.AddTorque(300);
                Destroy(collider);

            }
        }
        else if (Type == EnemyType.Snake)
        {
            if (collision.tag == "Sword")
            {
                Hitted = true;
                rb.AddForce(Vector3.up * 150);
                rb.freezeRotation = false;
                rb.AddTorque(300);
                Destroy(collider);

            }
        }
        else if (Type == EnemyType.Bee)
        {
            if (collision.tag == "Bow")
            {
                Hitted = true;
                rb.AddForce(Vector3.up * 150);
                rb.freezeRotation = false;
                rb.AddTorque(300);
                Destroy(collider);
            }
        }
    }

    private void Update()
    {
        if (Hitted)
        {
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        Color c = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a-(.5f*Time.deltaTime));
        if (GetComponent<SpriteRenderer>().color.a < 0.05f)
        {
            Destroy(gameObject);
        }
    }
}
