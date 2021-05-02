using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AIMove : MonoBehaviour
{
    [Tooltip("Speed on axes X and Y. If IsFlying is false Speed.y must be equal zero")]
    [SerializeField] Vector2 Speed;
    [SerializeField] int DirectionX = 1;
    [SerializeField] int DirectionY = 1;
    [Tooltip("Range on Y axis from the start position of gameobject")]
    [SerializeField] float FloatingY = 0;
    [Tooltip("Range on X axis from the start position of gameobject")]
    [SerializeField] float RangeX = 0;
    [SerializeField] bool IsFlying;
    [Tooltip("If it checked, it will able to modify at runtime the RangeX value.\n" +
        "NOTE: could generate ugly artefacts")]
    [SerializeField] bool ActivateOffsetX;
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
    float maxX, minX;
    Vector2 checkOffset;

    // Start is called before the first frame update
    void Start()
    {
        collidableX = false;
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

        // Verifico che il movimento sull'asse orizzontale sia positivo
        if (RangeX < 0)
        {
            RangeX = -RangeX;
        }

        startPosition = transform.position;

        // definisco le posizioni massime VERTICALI in cui il GameObject può muoversi
        minY = startPosition.y - FloatingY;
        maxY = startPosition.y + FloatingY;

        // definisco le posizioni massime ORIZZONTALI in cui il GameObject può muoversi
        //minX = startPosition.x - RangeX;
        //maxX = startPosition.x + RangeX;
        UpdateHorizontalOffset(startPosition.x, out minX, out maxX);

        checkOffset = new Vector2(RangeX, FloatingY);
    }

    private void UpdateHorizontalOffset(float StartPositionX, out float minX, out float maxX)
    {
        minX = StartPositionX - RangeX;
        maxX = StartPositionX + RangeX;
    }

    private void FixedUpdate()
    {
        if (ActivateOffsetX && minX != checkOffset.x)
        {
            Debug.Log("offset.X was modified");
            UpdateHorizontalOffset(startPosition.x, out minX, out maxX);
        }

        /* Gestione del cambio di direzione sull'asse X
         *
         * Cause Possibili:
         *  - Collisione
         *  - Il GameObject ha un RangeX > 0 e 
         *       la posizione del suo Rigidbody ha superato uno dei limiti (inferiore o maggiore)
         *       definiti da minX e maxX
         */
        if (collidableX || ( RangeX > 0 && ( rb2d.position.x >= maxX || rb2d.position.x <= minX) ) )
        {
            if (collidableX) collidableX = false;

            DirectionX = -DirectionX;
            sr.flipX = !sr.flipX;
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
                    //Debug.Log("rb2d.position.y >= maxY");
                    DirectionY *= -1;
                }
                else if (rb2d.position.y <= minY)
                {
                    //Debug.Log("rb2d.position.y <= minY");
                    DirectionY = -DirectionY;
                }
                else
                {
                    //Debug.Log("T'ho fregato!");
                }
            //}

            rb2d.velocity = new Vector2(DirectionX * Speed.x, DirectionY * Speed.y);            
        }
        else
        {
            rb2d.velocity = new Vector2(DirectionX * Speed.x, rb2d.velocity.y);
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
