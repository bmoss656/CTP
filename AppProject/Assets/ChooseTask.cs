using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseTask : MonoBehaviour {

    private TaskButton tasks;
    // Use this for initialization
    void Start ()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("DailyTasks").name);
        tasks = GameObject.FindGameObjectWithTag("DailyTasks").GetComponent<TaskButton>();
	}
	
	public void SetSelected()
    {
        if(tasks.selectionCount < 5)
        {
            tasks.SetString(GetComponentInChildren<TextMeshProUGUI>().text);
            tasks.selectionCount++;
            GetComponent<Button>().interactable = false;
        }
    }
}
