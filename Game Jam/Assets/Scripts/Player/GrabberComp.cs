using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberComp : MonoBehaviour
{
    [HideInInspector]
    public bool isHoldingKey = false;           // Should prevent holding more than one key.
    
    private KeyComp keyInHand;                  // The current key component that the player is holding.
    private SpriteRenderer keySprite;           // Will be used to flip the sprite when player face left or right.
    private Vector3 originalGrabberPosition;

    // Start is called before the first frame update
    void Start()
    {
        isHoldingKey = false;
        originalGrabberPosition = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Update grabber object position base on where the player is moving horizontally
        float x = PlayerMovement.isFacingRight ? originalGrabberPosition.x : -originalGrabberPosition.x;
        gameObject.transform.localPosition = new Vector3(x, originalGrabberPosition.y, originalGrabberPosition.z);

        // Update key sprite to also face where ever the player is facing
        if(keySprite)
        {
            keySprite.flipY = !PlayerMovement.isFacingRight;
        }

        // When the player releases the Left Shift button, release the key
        if(Input.GetKeyUp(KeyCode.LeftShift) && isHoldingKey)
        {
            ClearKeyFromGrabber();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only grab a key when the player is currently not holding another one, and while they are holding Left Shift button
        if(collision.gameObject.GetComponent<KeyComp>() && Input.GetKey(KeyCode.LeftShift) && !isHoldingKey)
        {
            var keyObject = collision.gameObject;
            keyInHand = keyObject.GetComponent<KeyComp>();
            keySprite = keyObject.GetComponent<SpriteRenderer>();
            keyInHand.isBeingHold = true;
            isHoldingKey = true;
        }
    }

    // Used to release key, or before the key is destroyed upon unlocking the door
    public void ClearKeyFromGrabber()
    {
        keyInHand.isBeingHold = false;
        isHoldingKey = false;
        keySprite = null;
        keyInHand = null;
    }
}
