using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool isPlayerDead;    //variable to be set in PlayerHealth
    public float speed;                 //player movement speed
    private float h;                    //horizontal axis
    private float v;                    //vertical axis

    
    void Start()
    {
        h = 0f;
        v = 0f;
        isPlayerDead = false;
    }

 
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        //allow up/down float controls but no jump
        if (isPlayerDead)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {

            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {

            }
        
    }
        //jump enabled but not up/down float
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                CheckIfGrounded();
            }
        }

        //left and right controls stay the same either way
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {

        }

    }

    public void CheckIfGrounded()
    {

    }
}
