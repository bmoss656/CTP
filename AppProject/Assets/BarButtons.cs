using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarButtons : MonoBehaviour
{
    public GameObject[] objects;

    public Button Holder;

    public void Touch()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

}
