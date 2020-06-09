using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool isPlayerDead;    //variable to be set in PlayerHealth
    public LayerMask GroundLayerMask;   //helps ground check to ignore player collider
    public Rigidbody2D playerRb;
    public BoxCollider2D boxCollider2d;
    public float speed;
    public float jumpForce;
    
    void Awake()
    {
        playerRb = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        speed = 15f;
        jumpForce = 35;
        isPlayerDead = false;
    }

    private void FixedUpdate()
    {
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
       
        //allow up/down float controls but no jump
        if (isPlayerDead)
        {

            playerRb.gravityScale = 0f;
            playerRb.drag = 1.4f;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                playerRb.velocity = Vector2.up * speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                playerRb.velocity = Vector2.down * speed;
            }
        }

        //jump enabled but not up/down float
        else
        {
            playerRb.gravityScale = 15f;
            playerRb.drag = 0.05f;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                if (CheckIfGrounded())
                {
                    playerRb.velocity = Vector2.up * jumpForce;
                }
            }
        }

        //left and right controls stay the same either way
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerRb.velocity = new Vector2(-speed, playerRb.velocity.y);
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerRb.velocity = new Vector2(speed, playerRb.velocity.y);
        }

        else if(!isPlayerDead)
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
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
}
