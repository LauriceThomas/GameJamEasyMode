using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool isPlayerDead;                //variable to be set in PlayerHealth
    public LayerMask GroundLayerMask;               //helps ground check to ignore player collider
    public Rigidbody2D playerRb;
    public BoxCollider2D boxCollider2d;
    public float aliveSpeed;
    public float ghostModeSpeed;
    public float jumpForce;
    public static bool isFacingRight;       //Will be used to flip key sprite and grabber positions in Grabber Comp.

    //Variables to control player's RigidBody2D GravityScale and Drag values while dead or alive
    public float ghostModeGravityScale = 0;
    public float ghostModeDrag = 1f;
    public float aliveModeGravityScale = 10;
    public float aliveModeGravityDrag = 0.05f;

    void Awake()
    {
        
        playerRb = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        aliveSpeed = 10f;
        jumpForce = 20f;
        ghostModeSpeed = 6f;
        isPlayerDead = false;
        isFacingRight = true;
    }

    private void FixedUpdate()
    {
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isPlayerDead = PlayerHealthComp.isInDeathMode;

        //ghostMode controls and values
        if (isPlayerDead)
        {
            //light weight ghost values
            playerRb.gravityScale = ghostModeGravityScale;
            playerRb.drag = ghostModeDrag;

            //float up and down controls
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                playerRb.velocity = Vector2.up * ghostModeSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                playerRb.velocity = Vector2.down * ghostModeSpeed;
            }

            //GhostMode left and right controls 
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                playerRb.velocity = new Vector2(-ghostModeSpeed, playerRb.velocity.y);
                isFacingRight = false;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                playerRb.velocity = new Vector2(ghostModeSpeed, playerRb.velocity.y);
                isFacingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        //living controls and values
        else
        {
            //heavy knight values
            playerRb.gravityScale = aliveModeGravityScale;
            playerRb.drag = aliveModeGravityDrag;

            //jump controls
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                if (CheckIfGrounded())
                {
                    playerRb.velocity = Vector2.up * jumpForce;
                }
            }

            //Alive left and right controls 
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                playerRb.velocity = new Vector2(-aliveSpeed, playerRb.velocity.y);
                isFacingRight = false;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                playerRb.velocity = new Vector2(aliveSpeed, playerRb.velocity.y);
                isFacingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                playerRb.velocity = new Vector2(0, playerRb.velocity.y);
            }
        }

       
    }

    public bool CheckIfGrounded()
    {
        float extendBox = 0.05f;
        float boxAngle = 0f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center,
            boxCollider2d.bounds.size, boxAngle, Vector2.down, extendBox, GroundLayerMask);

        return raycastHit.collider != null;
    }

    //called after Reset button is pressed
    public void ResetValues()
    {
        playerRb = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        aliveSpeed = 10f;
        jumpForce = 20f;
        ghostModeSpeed = 6f;
        isPlayerDead = false;
        isFacingRight = true;
        PlayerHealthComp.Resurrect();
    }
}
