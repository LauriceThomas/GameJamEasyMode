using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSound
{
    public AudioClip audioClip;
    public bool loop = false;

    [Range(min: 0, max: 1)]
    public float volume;
    
    [HideInInspector]
    public AudioSource audioSource;
}

[System.Serializable]
public class SoundEffect : BaseSound
{
    public SoundManager.SoundName soundName;
}

[System.Serializable]
public class BackgroundTheme : BaseSound
{
    public int levelIndex;
}

public class SoundManager : MonoBehaviour
{
    public enum SoundName
    {
        alive, dead, doorOpen, doorLock
    };

    public static SoundManager instance;
    public SoundEffect[] soundEffects;
    public BackgroundTheme[] backgroundThemes;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Add a new Audio Source Component to game object for each sound effect with all information set on Unity
        foreach(var s in soundEffects)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
            s.audioSource.loop = s.loop;
        }

        // Do the same with background themes
        foreach(var b in backgroundThemes)
        {
            b.audioSource = gameObject.AddComponent<AudioSource>();
            b.audioSource.clip = b.audioClip;
            b.audioSource.volume = b.volume;
            b.audioSource.playOnAwake  = false;
            b.audioSource.loop= b.loop;
        }
    }

    // Plays the first audio clip that matches with the level index. Will notify if there's no audio clips, or more than one for a scene  
    public void PlayBackground(int levelIndex)
    {
        StopAllBackgroundMusic();

        BackgroundTheme[] backgroundTheme = backgroundThemes
            .Where(x => x.levelIndex == levelIndex).ToArray();

        if(backgroundTheme == null || backgroundTheme.Length == 0)
        {
            Debug.Log("No background theme found for Scene: " + levelIndex);
            return;
        }
        
        if(backgroundTheme.Length > 1)
        {
            Debug.Log("More than one background theme was set for scene: " + levelIndex);
        }

        backgroundTheme[0].audioSource.Play();
    }

    // Looks for first specified sound-effect by enum name
    public void PlaySound(SoundName soundName)
    {
        SoundEffect soundCombo = soundEffects.FirstOrDefault(x => x.soundName == soundName);

        if(soundCombo == null) { return; }

        soundCombo.audioSource.Play();
    }

    public void StopAllBackgroundMusic()
    {
        foreach(var b in backgroundThemes)
        {
            b.audioSource.Stop();
        }
    }
}
