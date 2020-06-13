using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    public Canvas PauseCanvas;
    public Canvas ControlsCanvas;
    public PlayerMovement playerMove;
    private bool isRestarting;
    
    // Start is called before the first frame update
    void Awake()
    {
        PauseCanvas = GetComponent<Canvas>();
        //ControlsCanvas = FindObjectOfType<Canvas>();
        isRestarting = false;
        playerMove = FindObjectOfType<PlayerMovement>();
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRestarting == true)
        {
            SetRestartValues();
        }
        
        //checks for pause input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if(Time.timeScale == 1)
            {
                PauseGame();
            }
            //resumes
            else
            {
                ResumeGame();
            }
        }
    }

    //Restores time scale
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseCanvas.enabled = false;
        ControlsCanvas.enabled = false;
    }

    //Stops Time scale
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseCanvas.enabled = true;
    }

 
    public void SetRestartValues()
    {
        PauseCanvas = GetComponent<Canvas>();
        //ControlsCanvas = FindObjectOfType<Canvas>();
        playerMove = FindObjectOfType<PlayerMovement>();
        isRestarting = false;
        playerMove.ResetValues();
        ResumeGame();
    }

    //Restarts current level
    public void RestartButtonPressed()
    {
        isRestarting = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Opens Controls screen
    public void ControlsButtonPressed()
    {
        PauseCanvas.enabled = false;
        ControlsCanvas.enabled = true;
    }

    //Exits Controls screen
    public void BackButtonPressed()
    {
        PauseCanvas.enabled = true;
        ControlsCanvas.enabled = false;
    }

    //Will exit game in build or editor
    public void ExitButtonPressed()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
		        Application.Quit();
        #endif
    }
}
