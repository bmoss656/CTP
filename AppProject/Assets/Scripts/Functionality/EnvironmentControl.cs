﻿using System.Collections;
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
    private static EnvironmentControl m_instance;
    public static EnvironmentControl instance { get { return m_instance; } }

    public GameObject treeObjects;
    public GameObject foliageObjects;
    public GameObject deadObjects;
    public GameObject grassObjects;
    public FloorControl fc;

    public EnvironmentState curState;

    private int treeCount;
    private int foliageCount;
    private int deadCount;
    private int grassCount;

    private int activeTree;
    private int activeFoliage;
    private int activeDead;
    private int activeGrass;

    private bool badObjects = false;
    private bool firstTime = true;

    private PlayerControl pc;

    private float exp;

    private void Awake()
    {
        m_instance = this;
        pc = GameObject.FindGameObjectWithTag("SaveData").GetComponent<PlayerControl>();
        //exp = pc.experience;
        //curState = SetEnvironmentState();
    }

    // Use this for initialization
    void Start()
    {
        if (firstTime)
        {

            treeCount = treeObjects.transform.childCount;
            foliageCount = foliageObjects.transform.childCount;
            deadCount = deadObjects.transform.childCount;
            grassCount = grassObjects.transform.childCount;

            if (curState == EnvironmentState.State1 || curState == EnvironmentState.State2)
            {
                badObjects = true;
            }
            SetEnvironment();
            firstTime = false;
        }
    }
    private void OnEnable()
    {
        if (firstTime == false)
        {
            SetEnvironment();
        }
    }

    public void SetEnvironment()
    {
        exp = pc.experience;
        curState = SetEnvironmentState();
        SetEnvironmentVariables();
        SetActiveEnvironment();
        fc.SetEnviroFloor();
    }

    public EnvironmentState SetEnvironmentState()
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

    private void SetEnvironmentVariables()
    {
        switch (curState)
        {
            case EnvironmentState.State1:
                activeFoliage = 0;
                activeTree = 0;
                activeGrass = 0;
                activeDead = deadCount;
                break;
            case EnvironmentState.State2:
                activeFoliage = 0;
                activeTree = 0;
                activeGrass = 0;
                activeDead = deadCount / 2;
                break;
            case EnvironmentState.State3:
                activeFoliage = foliageCount / 5;
                activeTree = treeCount / 5;
                activeGrass = grassCount / 5;
                deadCount = 0;
                break;
            case EnvironmentState.State4:
                activeFoliage = foliageCount / 3;
                activeTree = treeCount / 3;
                activeGrass = grassCount / 3;
                deadCount = 0;
                break;
            case EnvironmentState.State5:
                activeFoliage = foliageCount / 2;
                activeTree = treeCount / 2;
                activeGrass = grassCount / 3;
                deadCount = 0;
                break;
            case EnvironmentState.State6:
                activeFoliage = foliageCount;
                activeTree = treeCount;
                activeGrass = grassCount;
                deadCount = 0;
                break;
            case EnvironmentState.State7:
                break;
            default:
                activeFoliage = 0;
                activeTree = 0;
                activeDead = 0;
                break;
        }
    }

    private void SetActiveEnvironment()
    {
        if(activeTree == 0)
        {
            treeObjects.SetActive(false);
        }
        else
        {
            treeObjects.SetActive(true);
            for (int i = 0; i < activeTree; i++)
            {
                treeObjects.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if(activeFoliage == 0)
        {
            foliageObjects.SetActive(false);
        }
        else
        {
            foliageObjects.SetActive(true);
            for (int i = 0; i < activeFoliage; i++)
            {
                foliageObjects.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        
        if(activeGrass == 0)
        {
            grassObjects.SetActive(false);
        }
        else
        {
            grassObjects.SetActive(true);
            for (int i = 0; i < activeGrass; i++)
            {
                grassObjects.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (activeDead == 0)
        {
            deadObjects.SetActive(false);
        }
        else
        {
            deadObjects.SetActive(true);
            for (int i = 0; i < activeDead; i++)
            {
                deadObjects.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}

