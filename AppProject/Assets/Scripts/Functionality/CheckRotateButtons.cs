using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRotateButtons : MonoBehaviour
{
    public GameObject inv;
    public GameObject ecoScale;


	void Update ()
    {
		if(inv.activeSelf || ecoScale.activeSelf)
        {
            for(int i = 0; i <transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
	}
}
