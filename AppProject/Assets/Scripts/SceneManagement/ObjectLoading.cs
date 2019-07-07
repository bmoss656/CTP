using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoading : MonoBehaviour
{
    public GameObject[] loading;

    public GameObject[] disable;

    //Easy loading and disabling of objects, used multiple times on buttons
    public void LoadObjects()
    {
        foreach (GameObject load in loading)
        {
            load.SetActive(true);
        }
    }

    public void DisableObjects()
    {
        foreach (GameObject dis in disable)
        {
            dis.SetActive(false);
        }
    }
}
