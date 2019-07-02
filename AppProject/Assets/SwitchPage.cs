using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPage : MonoBehaviour
{
    private List<GameObject> buttons;
    private int maxObjects;
    private int currentObj = 0;
	// Use this for initialization
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
