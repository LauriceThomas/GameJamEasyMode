using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberComp : MonoBehaviour
{
    public static KeyComp keyInHand;                        // The current key component that the player is holding.

    // Start is called before the first frame update
    void Start()
    {
        keyInHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        // When the player releases the Left Shift button, release the key
        if (Input.GetKeyUp(KeyCode.LeftShift) && keyInHand)
        {
            keyInHand = null;
        }
    }

    // Only grab a key when the player is currently not holding another one, and while they are holding Left Shift button
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<KeyComp>() && Input.GetKey(KeyCode.LeftShift) && !keyInHand)
        {
            if (collision.gameObject.tag == "ResKey")
            {
                PlayerHealthComp.Resurrect();
            }

            keyInHand = collision.gameObject.GetComponent<KeyComp>();
            SoundManager.instance.PlaySound(SoundManager.SoundName.key);
        }
    }
}