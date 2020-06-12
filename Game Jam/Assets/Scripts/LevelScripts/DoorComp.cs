﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorComp : MonoBehaviour
{
    public bool isUnlocked = false;
    public bool goToNextScene = true;
    public Sprite doorOpenSprite;
    public Sprite doorCloseSprite;
    public bool playSoundOnce = false;

    private bool alreadyPlayed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = (isUnlocked ? doorOpenSprite : doorCloseSprite);
        GetComponent<BoxCollider2D>().isTrigger = isUnlocked;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(playSoundOnce && alreadyPlayed) { return; }

            Debug.Log("*Door Lock Sound Plays*");
            alreadyPlayed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isUnlocked && collision.gameObject.tag == "Player")
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        if(goToNextScene && isUnlocked)
        {
            PlayerHealthComp.Resurrect();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
