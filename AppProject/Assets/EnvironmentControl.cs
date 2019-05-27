using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnvironmentState
{
    State1,
    State2,
    State3,
    State4,
    State5,
    State6,
    State7,
    UndeterminedState
}

public class EnvironmentControl : MonoBehaviour
{
    public GameObject treeObjects;
    public GameObject foliageObjects;

    public EnvironmentState curState;

    private int treeCount;
    private int foliageCount;

    private PlayerControl pc;

    private float exp;

	// Use this for initialization
	void Start ()
    {
        pc = GameObject.FindGameObjectWithTag("SaveData").GetComponent<PlayerControl>();
        exp = pc.experience;
        curState = SetEnvironmentState();

        treeCount = treeObjects.transform.childCount;
        foliageCount = foliageObjects.transform.childCount;
	}
	

    private EnvironmentState SetEnvironmentState()
    {
        if (exp < 500)
        {
            return EnvironmentState.State1;
        }
        else if (exp < 1000)
        {
            return EnvironmentState.State2;
        }
        else if (exp < 2500)
        {
            return EnvironmentState.State3;
        }
        else if (exp <= 5000)
        {
            return EnvironmentState.State4;
        }
        else if (exp <= 7500)
        {
            return EnvironmentState.State5;
        }
        else if (exp < 10000)
        {
            return EnvironmentState.State6;
        }
        else if (exp == 10000)
        {
            return EnvironmentState.State7;
        }
        else
        {
            return EnvironmentState.UndeterminedState;
        }
    }

	private void LoadTrees()
    {
       
    }

    private void LoadFoliage()
    {

    }
}

