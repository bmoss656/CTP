using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class InventoryManager : MonoBehaviour
{

    private int currency;

    private void OnEnable()
    {
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


    void Start ()
    {
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/inventoryInfo.dat", FileMode.Create);

        InventoryData data = new InventoryData();
        data.currency = currency;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/inventoryInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/inventoryInfo.dat", FileMode.Open);

            InventoryData data = (InventoryData)bf.Deserialize(file);
            file.Close();

            currency = data.currency;
        }
    }

    public int GetCurrency()
    {
        return currency;
    }

    public void AddCurrency(int add)
    {
        currency += add;
        Save();
    }

    public void MinusCurrency(int minus)
    {
        currency -= minus;
        Save();
    }
}

[Serializable]
class InventoryData
{
    public int currency;
}

