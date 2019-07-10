using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BarButtons : MonoBehaviour
{
    public GameObject[] objects;

    public Button Holder;

    public NavMeshAgent agent;

    public void Touch()
    {
        //Sets active on first touch, deativates on second
        foreach (GameObject obj in objects)
        {
            obj.SetActive(!obj.activeSelf);
        }
        //Resets the agents path so that it doesnt move whilst touching bar
        if(agent)
        {
            agent.ResetPath();
        }

    }


    public void ResetPath()
    {
        if (agent)
        {
            agent.ResetPath();
        }
    }
}
