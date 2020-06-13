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

            SoundManager.instance.PlaySound(SoundManager.SoundName.doorLock);
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

            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            int nextScene = currentScene + 1 >= sceneCount ? 0 : currentScene + 1;
            
            SceneManager.LoadScene(nextScene);
            SoundManager.instance.PlayBackground(nextScene);
        }
    }
}
