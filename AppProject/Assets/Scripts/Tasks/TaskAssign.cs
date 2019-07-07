using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskAssign : MonoBehaviour
{
    public TextMeshProUGUI[] textObjects;

    public TextAsset taskList;

    public TaskSaving saved;

    private string[] tasksRead;

    private int taskLength;

    public bool daily;

	void Start ()
    {
        tasksRead = taskList.text.Split('\n');

        taskLength = tasksRead.Length;

        //Assign tasks based on what has been read from text file
        if (!daily)
        {
            for (int i = 0; i < 5; i++)
            {
                textObjects[i].text = tasksRead[i];
            }
        }
        //Assign tasks based on selected saved daily tasks
        else
        {
            GetComponent<DailyTaskManager>().SetTastText();
        }
    }
	

    public void SetStrings(string[] tasks)
    {
        for (int i = 0; i < 5; i++)
        {
            textObjects[i].text = tasks[i];
        }
    }
}
