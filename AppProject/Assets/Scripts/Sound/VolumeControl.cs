using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public bool music;

    public void ChangeVolume()
    {
        if(music)
        {
            SoundManager.Instance.musicSource.volume = GetComponent<Slider>().value;
        }
        else
        {
            SoundManager.Instance.effectsSource.volume = GetComponent<Slider>().value;
        }
    }
	
}
