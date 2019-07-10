using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance {get { return instance; } }

    public AudioClip backgroundMusic;

    public AudioSource musicSource;
    public AudioSource effectsSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        BackgroundMusic();
    }
	
	private void BackgroundMusic()
    {
        //Play background music at beggining of app
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        //Play passed audio clip
        effectsSource.clip = clip;
        effectsSource.Play();
    }



}
