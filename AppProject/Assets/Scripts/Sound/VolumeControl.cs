using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class VolumeControl : MonoBehaviour
{

    public Slider musicSlider;
    public Slider soundsSlider;

    private float musicVol;
    private float soundVol;

    private void Start()
    {
        Load();
        ChangeVolume(true);
        ChangeVolume(false);
        musicSlider.value = musicVol;
        soundsSlider.value = soundVol;
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }

    private void Update()
    {
        musicVol = musicSlider.value;
        soundVol = soundsSlider.value;
    }

    public void ChangeVolume(bool music)
    {
        //Change volume of the music and effects using sliders
        if (SoundManager.Instance)
        {
            if (music)
            {
                SoundManager.Instance.musicSource.volume = musicVol;
            }
            else
            {
                SoundManager.Instance.effectsSource.volume = soundVol;
            }
        }
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameSettings.dat", FileMode.Create);

        GameSettings data = new GameSettings();

        Debug.Log(musicVol + " MUSic");
        Debug.Log(soundVol + " soundav");

        data.musicVol = musicVol;

        data.soundVol = soundVol;


        bf.Serialize(file, data);
        file.Close();
    }


    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/gameSettings.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/gameSettings.dat", FileMode.Open);

            GameSettings data = (GameSettings)bf.Deserialize(file);
            file.Close();

            Debug.Log(data.musicVol + " Music");
            musicVol = data.musicVol;

            Debug.Log(data.soundVol + " sound");
            soundVol = data.soundVol;

        }
        else
        {

            musicVol = 0.75f;

            soundVol = 0.5f;

        }
    }
}

[Serializable]
class GameSettings
{
    public float musicVol;
    public float soundVol;
}