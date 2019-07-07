using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Text mesh pro is a better UI text built in Unity asset
using UnityEngine.UI;


public class SpawnTaskOptions : MonoBehaviour
{
    public TextAsset taskList;
    public GameObject buttonPrefab;
    public DailyTaskManager tasks;
    
    public string[] tasksRead;

    private int taskLength;
    private int tasksToSpawn;
  

    void Start ()
    {
        tasksRead = taskList.text.Split('\n');

        taskLength = tasksRead.Length;

        if (taskLength > 4)
        {
            for (int i = 0; i < taskLength; i++)
            {
                if ((i + 1) % 5 == 0)
                {
                    tasksToSpawn++;
                }
            }
        }
        else
        {
            tasksToSpawn = 1;
        }


        for (int i = 0; i < tasksToSpawn; i++)
        {
            //How many tasks need to be spawned depending on text file amount
            GameObject buttons = Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), transform);
            buttons.transform.localPosition = new Vector3(0, 0, 0);
            buttons.GetComponent<SetButtonText>().SetPos(i);
            buttons.GetComponent<SetButtonText>().SetText(tasksRead);
            GetComponent<SwitchPage>().AddButtons(buttons);
            if (i != 0)
            {
                buttons.SetActive(false);
            }
        }
    }

    public void SetSelected(Button but, string text)
    {
        //Set the selected tasks in intro page
        if (tasks.selectionCount < 5)
        {
            tasks.SetString(text);
            tasks.selectionCount++;
            but.interactable = false;
        }
       
    }
}
