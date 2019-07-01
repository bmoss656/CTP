﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTaskOptions : MonoBehaviour
{
    public TextAsset taskList;
    public GameObject buttonPrefab;
    
    public string[] tasksRead;
    private int taskLength;

    private int tasksToSpawn;

    // Use this for initialization
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


        for(int i = 0; i < tasksToSpawn; i++)
        {
            GameObject buttons = Instantiate(buttonPrefab,new Vector3(0,0,0), Quaternion.Euler(0,0,0), transform);
            buttons.transform.localPosition = new Vector3(0, 0, 0);
            buttons.GetComponent<SetButtonText>().SetPos(i);
            buttons.GetComponent<SetButtonText>().SetText(tasksRead);
            GetComponent<SwitchPage>().AddButtons(buttons);
            if(i != 0)
            {
                buttons.SetActive(false);
            }
        }




    }
	
	// Update is called once per frame
	void Update () {
		
	}
}