using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorComp : MonoBehaviour
{
    public bool isUnlocked = false;
    public bool goToNextScene = true;
    public int nextScene;
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
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        if(goToNextScene && isUnlocked)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void UpdateCollisionWithPlayer()
    {
        // Collision between player and door is ignored only when the door is unlocked and the door does not complete the level
        GameObject player = GameObject.Find("Player Character");
        GameObject playerGrabber = GameObject.Find("Grabber Child");

        BoxCollider2D doorCollider = gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D grabberCollider = playerGrabber.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(doorCollider, playerCollider, isUnlocked && !goToNextScene);
        Physics2D.IgnoreCollision(doorCollider, grabberCollider, isUnlocked && !goToNextScene);
    }
}
