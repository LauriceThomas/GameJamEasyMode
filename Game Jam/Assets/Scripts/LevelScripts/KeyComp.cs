using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyComp : MonoBehaviour
{    
    public DoorComp doorToUnlock;
    
    //[HideInInspector]
    private Transform grabberTransform;              // The character's grabber. Where the key will be attached to in the character

    private Quaternion originalRotation;            // Used to revert key's rotation to original state when it is not being hold

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = gameObject.transform.rotation;
        grabberTransform = GameObject.Find("Grabber Child").transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeyTransform();
        UpdateCollisionWithPlayer();

        // Update key sprite to also face where ever the player is facing if this Key equals the keyInHand static value
        gameObject.GetComponent<SpriteRenderer>().flipY = GrabberComp.keyInHand == this && !PlayerMovement.isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the key collides with the door it is supposed to unlock, destroy and clear the key from grabber comp
        if(collision.gameObject.tag == "Door" && collision.gameObject.GetComponent<DoorComp>() == doorToUnlock)
        {
            // Remove key from the grabber and destroy the key
            GrabberComp.keyInHand = null;
            Destroy(gameObject);

            // TODO Maybe add a delay to play the sound fully and/or update the sprite of the door
            Debug.Log("*Door Open Sound*");

            // Update door status
            doorToUnlock.isUnlocked = true;
        }
    }

    void UpdateKeyTransform()
    {
        // Checks if the character model has a grabber, and if it is currently being hold
        if (grabberTransform && GrabberComp.keyInHand == this)
        {
            // The key's z value will be rotated 90 degrees (The key will face where the player is facing), 
            // and its position will constantly match the grabber's position
            gameObject.transform.position = grabberTransform.position;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 90));
        }
        else
        {
            // The key is let go and its rotation is reverted back to its original state.
            gameObject.transform.rotation = originalRotation;
        }
    }

    void UpdateCollisionWithPlayer()
    {
        // If the key is being hold, ignore collisions between the player and key. Else Reinstate collision
        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), GameObject.Find("TaveraLPlayerTest").GetComponent<BoxCollider2D>(), GrabberComp.keyInHand == this);
        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), grabberTransform.gameObject.GetComponent<BoxCollider2D>(), GrabberComp.keyInHand == this);
    }
}
