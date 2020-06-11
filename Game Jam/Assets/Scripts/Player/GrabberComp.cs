using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberComp : MonoBehaviour
{
    public static KeyComp keyInHand;                        // The current key component that the player is holding.
    private Vector3 originalGrabberPosition;

    // Start is called before the first frame update
    void Start()
    {
        keyInHand = null;
        originalGrabberPosition = gameObject.transform.localPosition;

        // Ignore Collision between player and grabber
        GameObject parentObj = gameObject.transform.parent.gameObject;
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), parentObj.GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        // Update grabber object position base on where the player is moving horizontally
        float x = PlayerMovement.isFacingRight ? originalGrabberPosition.x : -originalGrabberPosition.x;
        gameObject.transform.localPosition = new Vector3(x, originalGrabberPosition.y, originalGrabberPosition.z);

        // When the player releases the Left Shift button, release the key
        if(Input.GetKeyUp(KeyCode.LeftShift) && keyInHand)
        {
            keyInHand = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only grab a key when the player is currently not holding another one, and while they are holding Left Shift button
        if(collision.gameObject.GetComponent<KeyComp>() && Input.GetKey(KeyCode.LeftShift) && !keyInHand)
        {
            keyInHand = collision.gameObject.GetComponent<KeyComp>();
            
            Debug.Log("*Key Pick-up Sound*");
        }

        // Ignore collisions with the map itself
        if(collision.gameObject.name == "Tilemap")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.collider);
        }
    }
}
