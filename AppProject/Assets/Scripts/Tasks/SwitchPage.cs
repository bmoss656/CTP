using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Amount of selectable tasks is dynamic so a list is used to switch between pages
public class SwitchPage : MonoBehaviour
{
    private List<GameObject> buttons;
    private int maxObjects;
    private int currentObj = 0;

	void OnEnable ()
    {
        buttons = new List<GameObject>();
	}
	
	public void AddButtons(GameObject obj)
    {
        buttons.Add(obj);
        maxObjects = buttons.Count;
    }

    public void NextObject()
    {
        //Used to switch page between the different 5 tasks you can select in intro
        if (maxObjects == 1 || buttons.Count == 0)
        {
            return;
        }
        else
        {
            buttons[currentObj].SetActive(false);
            if((currentObj + 1) >= maxObjects)
            {
                currentObj = 0;
            }
            else
            {
                currentObj++;
            }
            buttons[currentObj].SetActive(true);
        }
    }

    public void PreviousObject()
    {
        if (maxObjects == 1 || buttons.Count == 0)
        {
            return;
        }
        else
        {
            buttons[currentObj].SetActive(false);
            if ((currentObj - 1) < 0)
            {
                currentObj = maxObjects - 1;
            }
            else
            {
                currentObj--;
            }
            buttons[currentObj].SetActive(true);
        }
    }
}
