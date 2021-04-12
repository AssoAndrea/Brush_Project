using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AIMove : MonoBehaviour
{
    [SerializeField] float SpeedX;
    [SerializeField] float SpeedY;
    [SerializeField] int DirectionX = 1;
    [SerializeField] int DirectionY = 1;
    [SerializeField] float Distance;
    [SerializeField] float FloatingY;
    [SerializeField] bool IsFlying;
    [Tooltip("List of Layer Name this GO may collide with")]
    [SerializeField] string[] CollidableLayersName;

    [Header ("Configutator Animator")]
    [Tooltip ("The Animator Parameter Name which is used for velocity")]
    [SerializeField] string VelocityParameterName;
    //[Tooltip ("The Animator Parameter Distance which is used for distance")]
    //[SerializeField] string DistanceParameterName;
    [Tooltip ("If TRUE, Animator has Patrolling parameter")]
    [SerializeField] bool HasPatrolling;
    [Tooltip ("If TRUE, Animator has FollowPlayer parameter")]
    [SerializeField] bool HasFollowPlayer;
    [Tooltip ("If TRUE, Animator has Hitted parameter to handle the Die Animation")]
    [SerializeField] bool HasHitted;

    //[SerializeField] AnimationClip Idle;
    //[SerializeField] AnimationClip Patrolling;

    bool collidableX, collidableY;
    Vector2 startPosition;
    Animator anim;
    Rigidbody2D rb2d;
    SpriteRenderer sr;
    float maxY, minY;

    // Start is called before the first frame update
    void Start()
    {
        collidableX = false;

        startPosition = transform.position;
        minY = startPosition.y - FloatingY ;
        maxY = startPosition.y + FloatingY ;
        
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (IsFlying)
        {
            rb2d.gravityScale = 0;
        }
        
        if (DirectionX == 1)
        {
            sr.flipX = !sr.flipX;
        }
    }

    private void FixedUpdate()
    {
        if (collidableX)
        {
            DirectionX = -DirectionX;
            sr.flipX = !sr.flipX;
            collidableX = false;
        }

        #region comment code
        //transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, 0);
        //Vector2 dir = (Speed * 1) + (Physics2D.gravity * rb2d.gravityScale);
        //rb2d.MovePosition(rb2d.position + Speed * Time.fixedDeltaTime);
        #endregion
        if (IsFlying)
        {
            #region COMMENTED handle vertical collision
            //if (collidableY && collidableX)
            //{
            //    DirectionY = -DirectionY;
            //    DirectionX = -DirectionX;

            //    collidableY = false;
            //    collidableX = false;

            //    sr.flipX = !sr.flipX;
            //}
            //else if (collidableY)
            //{
            //    DirectionY = -DirectionY;
            //    collidableY = false;
            //    DirectionX = -DirectionX;
            //    collidableX = false;
            //}
            //else if (collidableX)
            //{
            //    DirectionX = -DirectionX;
            //    sr.flipX = !sr.flipX;
            //    collidableX = false;
            //}
            //else
            //{
            #endregion
            if (rb2d.position.y >= maxY)
                {
                    Debug.Log("rb2d.position.y >= maxY");
                    DirectionY *= -1;
                }
                else if (rb2d.position.y <= minY)
                {
                    Debug.Log("rb2d.position.y <= minY");
                    DirectionY = -DirectionY;
                }
                else
                {
                    Debug.Log("T'ho fregato!");
                }
            //}
            rb2d.velocity = new Vector2(DirectionX * SpeedX, DirectionY * SpeedY);
        }
        else
        {
            rb2d.velocity = new Vector2(DirectionX * SpeedX, rb2d.velocity.y);
        }

        // anim.vel = rb2d.vel
        anim.SetFloat(VelocityParameterName, rb2d.velocity.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int layerIndex = 0; layerIndex < CollidableLayersName.Length; layerIndex++)
        {
            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(CollidableLayersName[layerIndex])))
            {
                Debug.Log($"I ({this.name}) collided with {collision.gameObject.name} in layer {LayerMask.LayerToName(collision.gameObject.layer) }");
                collidableX = true;

                #region COMMENTED handle collision Y
                //if (IsFlying)
                //{
                //    //Ray2D rayHorz = new Ray2D(rb2d.position, new Vector2(DirectionX, 0) );
                //    //Ray2D rayVert = new Ray2D(rb2d.position, new Vector2(0, DirectionY) );

                //    bool hor = Physics2D.Raycast(rb2d.position, new Vector2(DirectionX, 0), rb2d.transform.localScale.x / 2 + 0.2f) ;
                //    bool vert = Physics2D.Raycast(rb2d.position, new Vector2(0, DirectionY), rb2d.transform.localScale.x / 2 + 0.2f) ;

                //    if ( vert )
                //    {
                //        collidableY = true;
                //    }
                //    else if ( hor)
                //    {
                //        collidableX = true;
                //    }
                //    else
                //    {
                //        collidableY = true;
                //        collidableX = true;
                //    }
                //}
                //else
                //{
                //    collidableX = true;
                //}
                #endregion

                break;
            }
        }
    }
}
