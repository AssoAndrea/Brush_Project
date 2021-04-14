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
    private SpriteRenderer SR;
    private float moveInput;
    private bool isGrounded;        
    private Animator anim;
    public LayerMask Ground;
    public LayerMask Enemy;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
 
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);
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
        
        anim.SetInteger("Speed", (int)rb.velocity.x);

        if (IsDamaged)
        {
            anim.SetTrigger("Damaged");
            IsDamaged = false;
        }


        //Flippo la sprite se il player va a sinistra

        if (rb.velocity.x < -.1f)
            SR.flipX = true;
        else if(rb.velocity.x > .1f)
            SR.flipX = false;
    }    

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == 6)
        {
            IsDamaged = true;
        }
    }

}
