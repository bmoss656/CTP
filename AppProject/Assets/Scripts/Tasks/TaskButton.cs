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
    private PlayerControl pc;

    public bool weeklyTask = false;
    public int weeklyCount;
    public int curDate;

    private bool[] weeklyActive;

    private int dailyTasksDone = 5;
    private int weeklyTasksDone = 5;

    private void OnEnable()
    {
        date = new int[5];
        weeklyActive = new bool[5];
        for(int i = 0;i<5;i++)
        {
            weeklyActive[i] = true;
        }
        Load();
        if (!weeklyTask)
        {
            CheckDay();
        }
        else
        {
            CheckWeek();
        }
    }
    private void Start()
    {
        pc = PlayerControl.instance;
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

        if (curDate != int.Parse(actualDay[1]))
        {
            pc.LoseExp(dailyTasksDone * 50);
            curDate = int.Parse(actualDay[1]);
            dailyTasksDone = 5;
        }
        else
        {
            for(int i = 0; i< 5; i++)
            {
                if(buttons[i].activeSelf == false)
                {
                    dailyTasksDone--;
                }
            }
        }

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
            for (int i = 0; i< 5; i++)
            {
                buttons[i].SetActive(true);
                weeklyActive[i] = true;
            }
        }

        for(int i = 0;i< 5;i++)
        {
            if(weeklyActive[i])
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

    public void ButtonPressedWeek(int buttonNum)
    {
        weeklyActive[buttonNum] = false;
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/taskDates.dat", FileMode.Create);

        TaskDates data = new TaskDates();

        data.dailyDate = date;
        data.curDate = curDate;
        data.weeklyCount = weeklyCount;
        data.weeklyActive = weeklyActive;

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
            weeklyActive = data.weeklyActive;
            weeklyCount = data.weeklyCount;
            curDate = data.curDate;
        }
    }

}

[Serializable]
class TaskDates
{
    public int[] dailyDate;
    public bool[] weeklyActive;
    public int weeklyCount;
    public int curDate;
}
