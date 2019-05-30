using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class TaskButton : MonoBehaviour
{
    private int[] date;

    public GameObject[] buttons;

    private void OnEnable()
    {
        date = new int[5];
        Load();
        CheckDay();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }

    private void CheckDay()
    {
        string day = System.DateTime.Now.ToString();

        string[] actualDay = day.Split('/');

        for (int i =0; i< 5;i++)
        {
            if(date[i] != int.Parse(actualDay[1]))
            {
                buttons[i].SetActive(true);
            }
            else
            {
                buttons[i].SetActive(false);
            }
        }
    }

    public void ButtonPressed(int buttonNum)
    {
        string day = System.DateTime.Now.ToString();

        string[] actualDay = day.Split('/');

        date[buttonNum] = int.Parse(actualDay[1]);
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/taskDates.dat", FileMode.Create);

        TaskDates data = new TaskDates();

        data.dailyDate = date;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/taskDates.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/taskDates.dat", FileMode.Open);

            TaskDates data = (TaskDates)bf.Deserialize(file);
            file.Close();

            date = data.dailyDate;
        }
    }

}

[Serializable]
class TaskDates
{
    public int[] dailyDate;
    public int[] weeklyDate;
}
