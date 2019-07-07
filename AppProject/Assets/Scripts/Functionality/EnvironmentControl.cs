using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnvironmentState
{
    //States for how the environment will be displayed
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
    public GameObject deadObjects;

    public GameObject foliageObjects;
    public GameObject deadFoliage;

    public GameObject grassObjects;
    public GameObject deadGrass;
    public FloorControl fc;

    public EnvironmentState curState;

    #region EnvironmentCounts
    private int deadTreeCount;
    private int deadGrassCount;
    private int deadFoliageCount;

    private int treeCount;
    private int activeTree;
    private int activeDeadTrees;

    private int foliageCount;
    private int activeFoliage;
    private int activeDeadFoliage;


    private int grassCount;
    private int activeGrass;
    private int activeDeadGrass;
    #endregion

    private bool firstTime = true;

    private PlayerControl pc;

    private float exp;

    private void Awake()
    {
        m_instance = this;
        
    }

    void Start()
    {
        pc = PlayerControl.instance;
        if (firstTime)
        {
            //Set ints to how many objects are avaliable
            treeCount = treeObjects.transform.childCount;
            foliageCount = foliageObjects.transform.childCount;
            deadTreeCount = deadObjects.transform.childCount;
            grassCount = grassObjects.transform.childCount;
            deadGrassCount = deadGrass.transform.childCount;
            deadFoliageCount = deadFoliage.transform.childCount;

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
        //Check exp and set state accordingly
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
        //Setting variables of the environment to decide what objects to make active
        switch (curState)
        {
            case EnvironmentState.State1:
                activeFoliage = 0;
                activeTree = 0;
                activeGrass = 0;
                activeDeadTrees = deadTreeCount;
                activeDeadGrass = deadGrassCount / 2;
                activeDeadFoliage = deadFoliageCount / 2;
                break;
            case EnvironmentState.State2:
                activeFoliage = 0;
                activeTree = 0;
                activeGrass = 0;
                activeDeadTrees = deadTreeCount;
                activeDeadGrass = deadGrassCount;
                activeDeadFoliage = deadFoliageCount;
                break;
            case EnvironmentState.State3:
                activeFoliage = foliageCount / 5;
                activeTree = treeCount / 5;
                activeGrass = grassCount / 5;
                activeDeadTrees = 3;
                activeDeadGrass = 0;
                activeDeadFoliage = 0;
                break;
            case EnvironmentState.State4:
                activeFoliage = foliageCount / 3;
                activeTree = treeCount / 3;
                activeGrass = grassCount / 3;
                activeDeadTrees = 0;
                activeDeadGrass = 0;
                activeDeadFoliage = 0;
                break;
            case EnvironmentState.State5:
                activeFoliage = foliageCount / 2;
                activeTree = treeCount / 2;
                activeGrass = grassCount / 3;
                activeDeadTrees = 0;
                activeDeadGrass = 0;
                activeDeadFoliage = 0;
                break;
            case EnvironmentState.State6:
                activeFoliage = foliageCount;
                activeTree = treeCount;
                activeGrass = grassCount;
                activeDeadTrees = 0;
                activeDeadGrass = 0;
                activeDeadFoliage = 0;
                break;
            case EnvironmentState.State7:
                break;
            default:
                activeFoliage = 0;
                activeTree = 0;
                activeDeadTrees = 0;
                activeGrass = 0;
                activeDeadGrass = 0;
                activeDeadFoliage = 0;
                break;
        }
    }

    private void SetActiveEnvironment()
    {
        //Enabling the different environmental objects depending on count
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

        if (activeDeadTrees == 0)
        {
            deadObjects.SetActive(false);
        }
        else
        {
            deadObjects.SetActive(true);
            for (int i = 0; i < activeDeadTrees; i++)
            {
                deadObjects.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (activeDeadGrass == 0)
        {
            deadGrass.SetActive(false);
        }
        else
        {
            deadGrass.SetActive(true);
            for (int i = 0; i < activeDeadGrass; i++)
            {
                deadGrass.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (activeDeadFoliage == 0)
        {
            deadFoliage.SetActive(false);
        }
        else
        {
            deadFoliage.SetActive(true);
            for (int i = 0; i < activeDeadFoliage; i++)
            {
                deadFoliage.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}

