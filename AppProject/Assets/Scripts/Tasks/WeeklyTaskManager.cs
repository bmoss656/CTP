using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class WeeklyTaskManager : MonoBehaviour
{
    public GameObject[] buttons;

    private AudioClip taskSound;
    private PlayerControl pc;

    public int weeklyCount = 0;
    public int curDate;

    private int weeklyTasksDone = 5;
    private bool[] weeklyActive;

    void Start ()
    {
        taskSound = Resources.Load("Sound/Success", typeof(AudioClip)) as AudioClip;
    }

    private void OnEnable()
    {
        pc = PlayerControl.instance;
        Load();
        CheckWeek();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }

    private void CheckWeek()
    {
        string day = System.DateTime.Now.ToString();

        string[] actualDay = day.Split('/');


        if (curDate != int.Parse(actualDay[1]))
        {
            weeklyCount++;
            curDate = int.Parse(actualDay[1]);
        }
        for (int i = 0; i < 5; i++)
        {
            if (buttons[i].activeSelf == false)
            {
                weeklyTasksDone--;
            }
        }
        if (weeklyCount >= 7)
        {
            weeklyCount = 0;
            pc.LoseExp(weeklyTasksDone * 200);
            for (int i = 0; i < 5; i++)
            {
                buttons[i].GetComponent<Button>().interactable = true;
                weeklyActive[i] = true;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (weeklyActive[i])
            {
                buttons[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void ButtonPressedWeek(int buttonNum)
    {
        weeklyActive[buttonNum] = false;
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayClip(taskSound);
        }
        CheckWeek();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/weeklyTaskDates.dat", FileMode.Create);

        WeeklyTaskDates data = new WeeklyTaskDates();

        data.curDate = curDate;

        data.weeklyCount = weeklyCount;
        data.weeklyActive = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            data.weeklyActive[i] = weeklyActive[i];
        }
        bf.Serialize(file, data);
        file.Close();
    }


    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/weeklyTaskDates.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/weeklyTaskDates.dat", FileMode.Open);

            WeeklyTaskDates data = (WeeklyTaskDates)bf.Deserialize(file);
            file.Close();

            curDate = data.curDate;

            weeklyActive = new bool[5];
            for (int i = 0; i < 5; i++)
            {
                weeklyActive[i] = true;
                if (data.weeklyActive.Length > i)
                {
                    weeklyActive[i] = data.weeklyActive[i];
                }
            }

        }
        else
        {
            weeklyCount = 0;
            curDate = 0;
            weeklyActive = new bool[5];
            for (int i = 0; i < 5; i++)
            {
                weeklyActive[i] = true;
            }
        }
    }

    public void ResetButtons()
    {
        for (int i = 0; i < 5; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
            weeklyCount = 100;
        }
    }

}

[Serializable]
class WeeklyTaskDates
{
    public bool[] weeklyActive;
    public int weeklyCount;
    public int curDate;
}