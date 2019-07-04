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
        if (!daily)
        {
            if (!saved.assignedTasks)
            {
                for (int i = 0; i < 5; i++)
                {
                    textObjects[i].text = tasksRead[i];
                }
                saved.assignedTasks = true;
            }
        }
        else
        {
            GetComponent<TaskButton>().SetTastText();
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
