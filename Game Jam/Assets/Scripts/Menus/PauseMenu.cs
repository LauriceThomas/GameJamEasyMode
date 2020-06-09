using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Canvas PauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        PauseCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //checks for pause input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //on/off switch for pause screen
            PauseCanvas.enabled = !PauseCanvas.enabled;

            //pauses or unpauses frame dependent movement
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    //Restarts current level
    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //not setup
    public void ControlsButtonPressed()
    {
        Debug.Log("Controls Pressed");
    }

    //not setup
    public void ExitButtonPressed()
    {
        Debug.Log("Exit Pressed");
    }
}
