using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public Canvas StartCanvas;
    public Canvas ControlsCanvas;
   

    // Start is called before the first frame update
    void Awake()
    {
        StartCanvas = GetComponent<Canvas>();
    }

    public void Start()
    {
        SoundManager.instance.PlayBackground();
    }

    public void StartGameButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SoundManager.instance.PlayBackground();
    }

    //Opens Controls screen
    public void ControlsButtonPressed()
    {
        StartCanvas.enabled = false;
        ControlsCanvas.enabled = true;
    }

    //Exits Controls screen
    public void BackButtonPressed()
    {
        StartCanvas.enabled = true;
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
