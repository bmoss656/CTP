using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


//Unity vector3 is not serializable for saving so needed to create own holder
[Serializable]
public struct Vector3Holder
{
    public float x;
    public float y;
    public float z;

    public void Fill(Vector3 vec)
    {
        x = vec.x;
        y = vec.y;
        z = vec.z;
    }

    public Vector3 vector
    { get { return new Vector3(x, y, z); } }
}

public class PlacedObjectManager : MonoBehaviour
{
    public List<string> placedItems;
    public List<Vector3Holder> itemLocations;
    public List<Vector3Holder> itemRotations;
    private int itemCount = 0;

    public bool isHouse = false;

    private void OnEnable()
    {
        Load();
        SpawnItems();
    }

    private void OnDisable()
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void AddItem(string objName, Vector3 position, Vector3 rotation)
    {
        placedItems.Add(objName);
        Vector3Holder holder1 = new Vector3Holder();
        holder1.Fill(position);
        itemLocations.Add(holder1);
        Vector3Holder holder2 = new Vector3Holder();
        holder2.Fill(rotation);
        itemRotations.Add(holder2);
        itemCount++;
        if (isHouse)
        {
            SetSize();
        }
    }

    public void DeleteItem(int itemIndex)
    {
        placedItems.RemoveAt(itemIndex);
        itemLocations.RemoveAt(itemIndex);
        itemRotations.RemoveAt(itemIndex);
        itemCount--;
    }

    private void SpawnItems()
    {
        if (itemCount > 0)
        {
            for (int i = 0; i < itemCount; i++)
            {
                Instantiate(Resources.Load("Items/" + placedItems[i]) as GameObject, itemLocations[i].vector, Quaternion.Euler(itemRotations[i].vector), transform);
            }
        }
        if (isHouse)
        {
            SetSize();
        }
    }

    public void SetPosition(Vector3 pos, Vector3 rot, int itemIndex)
    {
        Vector3Holder holder1 = new Vector3Holder();
        holder1.Fill(pos);
        itemLocations[itemIndex] = holder1;

        Vector3Holder holder2 = new Vector3Holder();
        holder2.Fill(rot);
        itemRotations[itemIndex] = holder2;
    }

    public void SetSize()
    {
        for(int i = 0; i< transform.childCount; i++)
        {
            transform.GetChild(i).transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Save()
    {
        
        if (!isHouse)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/outsidePlacedData.dat", FileMode.Create);

            OutsidePlacedData data = new OutsidePlacedData();
            data.itemCount = itemCount;

            data.placedItems = new List<string>(placedItems.Count);
            for (int i = 0; i < placedItems.Count; i++)
            {
                data.placedItems.Add(placedItems[i]);
            }

            data.itemLocations = new List<Vector3Holder>(itemLocations.Count);
            for (int i = 0; i < itemLocations.Count; i++)
            {
                data.itemLocations.Add(itemLocations[i]);
            }

            data.itemRotations = new List<Vector3Holder>(itemRotations.Count);
            for (int i = 0; i < itemRotations.Count; i++)
            {
                data.itemRotations.Add(itemRotations[i]);
            }

            bf.Serialize(file, data);
            file.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/housePlacedData.dat", FileMode.Create);

            HousePlacedData data = new HousePlacedData();
            data.itemCount = itemCount;

            data.placedItems = new List<string>(placedItems.Count);
            for (int i = 0; i < placedItems.Count; i++)
            {
                data.placedItems.Add(placedItems[i]);
            }

            data.itemLocations = new List<Vector3Holder>(itemLocations.Count);
            for (int i = 0; i < itemLocations.Count; i++)
            {
                data.itemLocations.Add(itemLocations[i]);
            }

            data.itemRotations = new List<Vector3Holder>(itemRotations.Count);
            for (int i = 0; i < itemRotations.Count; i++)
            {
                data.itemRotations.Add(itemRotations[i]);
            }

            bf.Serialize(file, data);
            file.Close();
        }

        
    }

    public void Load()
    {
        if (!isHouse)
        {
            if (File.Exists(Application.persistentDataPath + "/outsidePlacedData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/outsidePlacedData.dat", FileMode.Open);

                OutsidePlacedData data = (OutsidePlacedData)bf.Deserialize(file);
                file.Close();

                itemCount = data.itemCount;
                placedItems = new List<string>(data.placedItems.Count);
                for (int i = 0; i < data.placedItems.Count; i++)
                {
                    placedItems.Add(data.placedItems[i]);
                }

                itemLocations = new List<Vector3Holder>(data.itemLocations.Count);
                for (int i = 0; i < data.itemLocations.Count; i++)
                {
                    itemLocations.Add(data.itemLocations[i]);
                }

                itemRotations = new List<Vector3Holder>(data.itemRotations.Count);
                for (int i = 0; i < data.itemRotations.Count; i++)
                {
                    itemRotations.Add(data.itemRotations[i]);
                }
            }
            else
            {
                placedItems = new List<string>();
                itemLocations = new List<Vector3Holder>();
                itemRotations = new List<Vector3Holder>();
                itemCount = 0;
            }
        }
        else
        {
            if (File.Exists(Application.persistentDataPath + "/housePlacedData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/housePlacedData.dat", FileMode.Open);

                HousePlacedData data = (HousePlacedData)bf.Deserialize(file);
                file.Close();

                itemCount = data.itemCount;
                placedItems = new List<string>(data.placedItems.Count);
                for (int i = 0; i < data.placedItems.Count; i++)
                {
                    placedItems.Add(data.placedItems[i]);
                }

                itemLocations = new List<Vector3Holder>(data.itemLocations.Count);
                for (int i = 0; i < data.itemLocations.Count; i++)
                {
                    itemLocations.Add(data.itemLocations[i]);
                }

                itemRotations = new List<Vector3Holder>(data.itemRotations.Count);
                for (int i = 0; i < data.itemRotations.Count; i++)
                {
                    itemRotations.Add(data.itemRotations[i]);
                }
            }
            else
            {
                placedItems = new List<string>();
                itemLocations = new List<Vector3Holder>();
                itemRotations = new List<Vector3Holder>();
                itemCount = 0;
            }
        }
    }
}

[Serializable]
class OutsidePlacedData
{
    public List<string> placedItems;
    public List<Vector3Holder> itemLocations;
    public List<Vector3Holder> itemRotations;
    public int itemCount;
}

[Serializable]
class HousePlacedData
{
    public List<string> placedItems;
    public List<Vector3Holder> itemLocations;
    public List<Vector3Holder> itemRotations;
    public int itemCount;
}
