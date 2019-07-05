using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class TaskButton : MonoBehaviour
{
    public int[] date;

    public GameObject[] buttons;
    public GameObject doneButton;
    private AudioClip taskSound;
    private PlayerControl pc;

    public bool weeklyTask = false;
    public int weeklyCount = 0;
    public int curDate;

    public int selectionCount = 0;
    public string[] tasksToSave;

    private bool[] weeklyActive;

    private int dailyTasksDone = 5;
    private int weeklyTasksDone = 5;

    private void Start()
    {
        taskSound = Resources.Load("Sound/Success", typeof(AudioClip)) as AudioClip;
    }

    private void OnEnable()
    {
        pc = PlayerControl.instance;

        Load();

        if (!weeklyTask)
        {
            GetComponent<TaskAssign>().SetStrings(tasksToSave);
            CheckDay();
        }
        else
        {
            //GetComponent<TaskAssign>().SetStrings(tasksToSave);
            CheckWeek();
        }
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

    public void ButtonPressed(int buttonNum)
    {
        string day = System.DateTime.Now.ToString();

        string[] actualDay = day.Split('/');

        date[buttonNum] = int.Parse(actualDay[1]);
        Debug.Log(date[buttonNum]);
        SoundManager.Instance.PlayClip(taskSound);
        CheckDay();
    }

    public void ButtonPressedWeek(int buttonNum)
    {
        weeklyActive[buttonNum] = false;
        SoundManager.Instance.PlayClip(taskSound);
        CheckWeek();
    }

    public void SetString(string task)
    {
        Debug.Log("Shouldnt be here");
        if (selectionCount == 0)
        {
            tasksToSave = new string[5];
        }
        tasksToSave[selectionCount] = task;
        Debug.Log(tasksToSave[selectionCount]);
        if (selectionCount == 4)
        {
            doneButton.SetActive(true);
            //Save(true);
        }
    }

    public void SetTastText()
    {
        GetComponent<TaskAssign>().SetStrings(tasksToSave);
    }

    public void Save(bool check = false)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!weeklyTask)
        {
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
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/weeklyTaskDates.dat", FileMode.Create);

            WeeklyTaskDates data = new WeeklyTaskDates();

            data.weeklyCount = weeklyCount;
            data.weeklyActive = new bool[5];
            for (int i = 0; i < 5; i++)
            {
                data.weeklyActive[i] = weeklyActive[i];
            }
            bf.Serialize(file, data);
            file.Close();
        }

       
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!weeklyTask)
        {
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
        else
        {
            if (File.Exists(Application.persistentDataPath + "/weeklyTaskDates.dat"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/weeklyTaskDates.dat", FileMode.Open);

                WeeklyTaskDates data = (WeeklyTaskDates)bf.Deserialize(file);
                file.Close();
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
                weeklyActive = new bool[5];
                for (int i = 0; i < 5; i++)
                {
                    weeklyActive[i] = true;
                }
            }
        }



    }

    public void ResetButtons()
    {
        for (int i = 0; i < 5; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
            date[i] = 100;
            weeklyCount = 100;
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


[Serializable]
class WeeklyTaskDates
{
    public bool[] weeklyActive;
    public int weeklyCount;
}
