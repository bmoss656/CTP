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

        //audSource = GetComponent<AudioSource>();

        BackgroundMusic();
    }

 
	
	private void BackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        effectsSource.clip = clip;
        effectsSource.Play();
    }



}
