using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //public Vector2 velocity;
    public float Speed;
    public float JumpForce;
    public Transform FeetPos;
    public float CheckRadius;
    public bool IsDamaged;  //if player is damaged, put this true
    
    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;        
    private bool isCollided;
    private Animator anim;
    public LayerMask Ground;
    public LayerMask Enemy;

    // Start is called before the first frame update
    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (!(!isGrounded && isCollided))
        {
            rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);
        }      
    }

    // Update is called once per frame
    void Update()
    {        
        isGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius,Ground);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * JumpForce;
            anim.SetTrigger("Jump");
        }
        
        if (Input.GetKey(KeyCode.A))
            transform.GetComponent<SpriteRenderer>().flipX = true;
        else if (Input.GetKey(KeyCode.D))
            transform.GetComponent<SpriteRenderer>().flipX = false;

        anim.SetInteger("Speed", (int)rb.velocity.x);

        if (IsDamaged)
        {
            anim.SetTrigger("Damaged");
            IsDamaged = false;
        }              
    }    

    void OnCollisionEnter2D(Collision2D other)
    {
        isCollided = true;

        if (other.collider.gameObject.layer == 6)
        {
            IsDamaged = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isCollided = false;
    }
}
