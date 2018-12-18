using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskReading : MonoBehaviour
{
    private string[] easyTasks;
    private string[] hardTasks;

    private int easyMax;
    private int hardMax;

	// Use this for initialization
	void Start ()
    {

        ReadTaskLists();
        AssignTasks();


    }
	
	void ReadTaskLists()
    {
        TextAsset textEasy = (TextAsset)Resources.Load("Tasks/EasyTasks");

        easyTasks = textEasy.text.Split('\n');

        easyMax = easyTasks.Length;

        TextAsset textHard = (TextAsset)Resources.Load("Tasks/HardTasks");

        hardTasks = textHard.text.Split('\n');

        hardMax = hardTasks.Length;


    }


    void AssignTasks()
    {
        Random.seed = Random.Range(0, 1000);

        int max = 3;

        for(int i = 0; i < max; i++)
        {
            if (i < max - 1)
            {
                transform.GetChild(i).GetComponent<Text>().text = easyTasks[Random.Range(0, easyMax)];
            }
            else
            {
                transform.GetChild(i).GetComponent<Text>().text = hardTasks[Random.Range(0, hardMax)];
            }
        }
    }
}
