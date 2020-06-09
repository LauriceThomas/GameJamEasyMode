using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyComp : MonoBehaviour
{
    [HideInInspector]
    public bool isBeingHold;                        // A flag for the key to whether be attached to the player or not
    
    [HideInInspector]
    public Transform grabberGameObject;             // The character's grabber. Where the key will be attached to in the character
    
    public GameObject doorToUnlock;                 // When the key touches the door, it should disappear and finish the level
    private Quaternion originalRotation;            // Used to revert key's rotation to original state when it is not being hold

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = gameObject.transform.rotation;
        grabberGameObject = GameObject.Find("Grabber Child").transform;
        isBeingHold = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeyTransform();
    }

    void UpdateKeyTransform()
    {
        // Checks if the character model has a grabber, and if it is currently being hold
        if (grabberGameObject && isBeingHold)
        {
            // The key's z value will be rotated 90 degrees (The key will face where the player is facing), 
            // and its position will constantly match the grabber's position
            gameObject.transform.position = grabberGameObject.position;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 90));
        }
        else
        {
            // The key is let go and it's rotation is reverted back to its original state.
            gameObject.transform.rotation = originalRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the key collides with the door it is supposed to unlock, destroy and clear the key from grabber comp
        if(collision.gameObject.tag == "Door" && doorToUnlock == collision.gameObject)
        {
            grabberGameObject.GetComponent<GrabberComp>().ClearKeyFromGrabber();
            Destroy(gameObject);

            // TODO Complete the level when door is unlocked
            
        }
    }
}
