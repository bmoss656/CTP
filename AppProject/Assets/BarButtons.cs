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
        foreach (GameObject obj in objects)
        {
            obj.SetActive(!obj.activeSelf);
        }

        if(agent)
        {
            agent.ResetPath();
        }

    }

}
