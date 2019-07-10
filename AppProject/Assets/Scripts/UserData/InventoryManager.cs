using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class InventoryManager : MonoBehaviour
{
    private static InventoryManager m_instance;
    public static InventoryManager instance { get { return m_instance; } }

    private int currency;
    public List<string> heldItems;
    private int curInvSize;


    private void OnEnable()
    {
        m_instance = this;
        heldItems = new List<string>();   
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


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/inventoryInfo.dat", FileMode.Create);

        InventoryData data = new InventoryData();
        data.currency = currency;
        data.heldItems = new List<string>(heldItems.Count);
        for(int i = 0;i<heldItems.Count;i++)
        {
            data.heldItems.Add(heldItems[i]);
        }
        //data.heldItems = heldItems;
        data.curInvSize = curInvSize;

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
            heldItems = new List<string>(data.heldItems.Count);
            for (int i = 0; i < data.heldItems.Count; i++)
            {
                heldItems.Add(data.heldItems[i]);
            }
            curInvSize = data.curInvSize;
        }
        else
        {
            currency = 0;
            heldItems = new List<string>();
            curInvSize = 0;
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

    public void AddItem(string name)
    {
        //Items are represented using the name of the item, which can then be loaded from resources
        heldItems.Add(name);
    }

    public void TakeItem(int num)
    {
        heldItems.RemoveAt(num);
    }

    public string GetItem(int num)
    {
        //Fetching an item will return null string if item doesnt exist
        if(heldItems.Count > num)
        {
            return heldItems[num];
        }
        else
        {
            return "NULL";
        }
    }
}

[Serializable]
class InventoryData
{
    public int currency;
    public List<string> heldItems;
    public int curInvSize;
}

