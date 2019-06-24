using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



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

    private void Awake()
    {
        Load();
    }

    private void OnEnable()
    {
        m_instance = this;
        Load();
    }
    private void OnDisable()
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    //private void Awake()
    //{
    //    if(control == null)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        control = this;
    //    }
    //    else if(control != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    private void Start()
    {

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

        Debug.Log(DateTime.Today);
    }

    public void GiveExp(float xp)
    {
        experience += xp;
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
    }

}

[Serializable]
class PlayerData
{
    public float experience;
    public string type;
    public EnvironmentState state;
}
