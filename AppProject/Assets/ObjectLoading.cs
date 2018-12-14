using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoading : MonoBehaviour
{
    public GameObject[] loading;

	public void LoadObjects()
    {
        foreach (GameObject load in loading)
        {
            load.SetActive(true);
        }
    }
}
