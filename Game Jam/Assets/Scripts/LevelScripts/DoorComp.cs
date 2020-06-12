using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorComp : MonoBehaviour
{
    public bool isUnlocked = false;
    public bool goToNextScene = true;
    public Sprite doorOpenSprite;
    public Sprite doorCloseSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = (isUnlocked ? doorOpenSprite : doorCloseSprite);
        UpdateCollisionWithPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!CanCompleteLevel())
            {
                Debug.Log("*Door Lock Sound Plays*");
            }
        }
    }

    public bool CanCompleteLevel()
    {
        if(goToNextScene && isUnlocked)
        {
            PlayerHealthComp.Resurrect();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
            return true;
        }

        return false;
    }

    public void UpdateCollisionWithPlayer()
    {
        // Collision between player and door is ignored only when the door is unlocked and the door does not complete the level
        GameObject player = GameObject.Find("TaveraLPlayerTest");
        GameObject playerGrabber = GameObject.Find("Grabber Child");

        BoxCollider2D doorCollider = gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D grabberCollider = playerGrabber.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(doorCollider, playerCollider, isUnlocked && !goToNextScene);
        Physics2D.IgnoreCollision(doorCollider, grabberCollider, isUnlocked && !goToNextScene);
    }
}
