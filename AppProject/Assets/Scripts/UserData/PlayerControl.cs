using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    private static PlayerControl m_instance;
    public static PlayerControl instance { get { return m_instance; } }

    public static PlayerControl control;
    public enum PlayerType { EMPTY, PLAYER1, PLAYER2, PLAYER3 };

    public float experience = 0;
    public float maxExp = 10000;
    public string pType = "empty";
    public PlayerType type = PlayerType.EMPTY;
    public string lastLogonDate;


    private int maxDataFiles = 0;
    //Names of all files that get saved, used to reset app in reset function
    private readonly string[] fileNames = { "/playerInfo", "/taskDates", "/taskInfo",
                                    "/inventoryInfo", "/outsidePlacedData",
                                    "/housePlacedData", "/weeklyTaskDates" , "/gameSettings"};

    private void Awake()
    {
        Load();
    }

    private void OnEnable()
    {
        m_instance = this;
        Load();
        maxDataFiles = 7;

    }
    private void OnDisable()
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Start()
    {
        //Would be used for loading in different player types if assets were avaliable
        if (pType == "player1")
        {
            type = PlayerType.PLAYER1;
            pType = "player1";
        }
        else if (pType == "player2")
        {
            type = PlayerType.PLAYER2;
            pType = "player2";
        }
        else if (pType == "player3")
        {
            type = PlayerType.PLAYER3;
            pType = "player3";
        }
        else
        {
            type = PlayerType.EMPTY;
        }
    }

    public void GiveExp(float xp)
    {
        experience += xp;
        //Cap exp at 10000
        if(experience > 10000)
        {
            experience = 10000;
        }
    }

    public void LoseExp(float xp)
    {
        experience -= xp;
        if(experience < 0)
        {
            experience = 0;
        }
    }

    public void SetPlayerType(string pt)
    {
        if(pt == "player1")
        {
            type = PlayerType.PLAYER1;
            pType = "player1";
        }
        else if(pt == "player2")
        {
            type = PlayerType.PLAYER2;
            pType = "player2";
        }
        else if (pt == "player3")
        {
            type = PlayerType.PLAYER3;
            pType = "player3";
        }

    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Create);

        PlayerData data = new PlayerData();
        data.experience = experience;
        data.type = pType;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            experience = data.experience;
            pType = data.type;
        }
        else
        {
            experience = 2000;
            pType = "Player1";
        }
    }

    public void ResetSaveData()
    {
        //Delete all saved data
        for (int i = 0; i< maxDataFiles; i++)
        {
            if (File.Exists(Application.persistentDataPath + fileNames[i] + ".dat"))
            {
                File.Delete(Application.persistentDataPath + fileNames[i] + ".dat");
            }
        }
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}

[Serializable]
class PlayerData
{
    public float experience;
    public string type;
}
