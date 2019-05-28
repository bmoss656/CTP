﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class TaskSaving : MonoBehaviour
{

    public string[] electronics;
    public ReadToggle readEle;
    public ReadToggle readHours;

    public int hours;

    public bool assignedTasks = false;
    public bool doneTutorial = false;

    public GameObject tut;
    public GameObject Tasks;

    private void OnEnable()
    {
        Load();
        if(doneTutorial)
        {
            tut.SetActive(false);
            Tasks.SetActive(true);
        }
        else
        {
            tut.SetActive(true);
            Tasks.SetActive(false);
        }
    }


    private void OnDisable()
    {
        Save();
    }

    public void Save()
    {
        electronics = new string[5];
        electronics = readEle.electronics;
        hours = readHours.hours;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/taskInfo.dat", FileMode.Create);

        TaskInfo data = new TaskInfo();
        data.electronics = electronics;
        data.hours = hours;
        data.assignedTasks = assignedTasks;
        data.doneTutorial = doneTutorial;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/taskInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/taskInfo.dat", FileMode.Open);

            TaskInfo data = (TaskInfo)bf.Deserialize(file);
            file.Close();

            electronics = data.electronics;
            hours = data.hours;
            assignedTasks = data.assignedTasks;
            doneTutorial = data.doneTutorial;
            
        }
    }

    public void SetTutorial(bool set)
    {
        doneTutorial = set;
    }

}

[Serializable]
class TaskInfo
{
    public string[] electronics;
    public int hours;
    public bool assignedTasks;
    public bool doneTutorial;
}