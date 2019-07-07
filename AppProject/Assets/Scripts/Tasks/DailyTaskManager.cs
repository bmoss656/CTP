using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class DailyTaskManager : MonoBehaviour
{
    public int[] date;

    public GameObject[] buttons;
    public GameObject doneButton;
    private AudioClip taskSound;
    private PlayerControl pc;

    public int curDate;

    public int selectionCount = 0;
    public string[] tasksToSave;


    private void Start()
    {
        taskSound = Resources.Load("Sound/Success", typeof(AudioClip)) as AudioClip;
    }

    private void OnEnable()
    {
        pc = PlayerControl.instance;

        Load();
        GetComponent<TaskAssign>().SetStrings(tasksToSave);
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

        for (int i = 0; i < 5; i++)
        {
            if (date != null)
            {
                if (date[i] != int.Parse(actualDay[1]))
                {
                    buttons[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    buttons[i].GetComponent<Button>().interactable = false;
                }
            }
            else
            {
                return;
            }
        }

    }

    public void ButtonPressed(int buttonNum)
    {
        string day = System.DateTime.Now.ToString();

        string[] actualDay = day.Split('/');

        date[buttonNum] = int.Parse(actualDay[1]);
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayClip(taskSound);
        }
        CheckDay();
    }

    public void SetString(string task)
    {
        if (selectionCount == 0)
        {
            tasksToSave = new string[5];
        }
        tasksToSave[selectionCount] = task;

        if (selectionCount == 4)
        {
            doneButton.SetActive(true);
        }
    }

    public void SetTastText()
    {
        GetComponent<TaskAssign>().SetStrings(tasksToSave);
    }

    public void Save(bool check = false)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/taskDates.dat", FileMode.Create);

        TaskDates data = new TaskDates();

        if (!check)
        {
            data.dailyDate = new int[5];
            for (int i = 0; i < 5; i++)
            {
                data.dailyDate[i] = date[i];
            }
            data.curDate = curDate;

            data.tasksToSave = new string[tasksToSave.Length];
            for (int i = 0; i < tasksToSave.Length; i++)
            {
                data.tasksToSave[i] = tasksToSave[i];
            }
        }
        else
        {
            data.tasksToSave = new string[tasksToSave.Length];
            for (int i = 0; i < tasksToSave.Length; i++)
            {
                data.tasksToSave[i] = tasksToSave[i];
            }
        }

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/taskDates.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/taskDates.dat", FileMode.Open);

            TaskDates data = (TaskDates)bf.Deserialize(file);
            file.Close();

            date = new int[5];
            for (int i = 0; i < 5; i++)
            {
                if (data.dailyDate.Length > i)
                {
                    date[i] = data.dailyDate[i];
                }
                else
                {
                    date[i] = 100;
                }
            }
            tasksToSave = new string[5];
            for (int i = 0; i < tasksToSave.Length; i++)
            {
                if (!string.IsNullOrEmpty(data.tasksToSave[i]))
                {
                    tasksToSave[i] = data.tasksToSave[i];
                }
            }
            curDate = data.curDate;

        }
        else
        {
            date = new int[5];
            for (int i = 0; i < 5; i++)
            {
                date[i] = 0;
            }
            curDate = 0;
        }

    }

    public void ResetButtons()
    {
        for (int i = 0; i < 5; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
            date[i] = 100;
        }
    }
}

[Serializable]
class TaskDates
{
    public int[] dailyDate;
    public string[] tasksToSave;
    public int curDate;
}


