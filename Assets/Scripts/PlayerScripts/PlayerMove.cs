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
    public GameObject WeaponPos;
    
    private Rigidbody2D rb;
    private SpriteRenderer SR;
    private float moveInput;
    private float moveInputY;
    private bool isGrounded;        
    private Animator anim;
    AnimatorStateInfo animStateInfo;
    int currState, jumpStateHash;

    public bool onStairs;
    public LayerMask Ground;
    
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpStateHash = Animator.StringToHash("PlayerJump");
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        //rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        if (onStairs)
        {
            rb.velocity = new Vector2(moveInput * 3, moveInputY * 3);            
            rb.gravityScale = 0;
        }
        else
        {
            if (Game_Manager.instance.inventory.DrawSpaceOpen == false)
            {
                rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);
                rb.gravityScale = 1;
            }            
        }
    }

    // Update is called once per frame
    void Update()
    {
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currState = animStateInfo.shortNameHash;

        isGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius, Ground);        

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && Game_Manager.instance.inventory.DrawSpaceOpen == false)
        {
            onStairs = false;
            rb.velocity = Vector2.up * JumpForce;            
            anim.SetTrigger("Jump");
        }

        if (currState == jumpStateHash)
        {
            onStairs = false;
        }

        anim.SetInteger("Speed", (int)rb.velocity.x);

        if (IsDamaged)
        {
            anim.SetBool("Damaged",true);
            IsDamaged = false;
        }


        //Flippo la sprite se il player va a sinistra

        if (rb.velocity.x < -.1f)
        {
            transform.rotation =Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
        }
        else if (rb.velocity.x > .1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,0, transform.rotation.z));
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == 6)
        {
            IsDamaged = true;
        }

        if (other.collider.gameObject.layer == 10)
        {
            rb.velocity = Vector2.up * (JumpForce + 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            IsDamaged = true;
        }
    }
}
