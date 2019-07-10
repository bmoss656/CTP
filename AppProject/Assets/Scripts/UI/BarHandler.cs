using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Used to help handle ui bar elements at bottom of the screen
public class BarHandler : MonoBehaviour
{
    private GameObject[] holders;

    private bool[] holdersActive;

	void Start ()
    {
        holders = new GameObject[transform.childCount];
        holdersActive = new bool[transform.childCount];
        for (int i = 0; i< transform.childCount;i++)
        {
            holders[i] = transform.GetChild(i).gameObject;
            holdersActive[i] = false;
        }
	}

    private void SetHolders()
    {
        for(int i = 0;i<transform.childCount;i++)
        {
            holders[i].GetComponent<Button>().interactable = holdersActive[i];
            holders[i].transform.GetChild(0).gameObject.GetComponent<Button>().interactable = holdersActive[i];
        }
    }

    public void SetBool(int holderNum)
    {
        if(holdersActive[holderNum])
        {
            holdersActive[holderNum] = false;
            ResetHolders();
        }
        else
        {
            holdersActive[holderNum] = true;
            SetHolders();
        }
       
       
    }
    private void ResetHolders()
    {
        for(int i = 0;i<transform.childCount;i++)
        {
            holders[i].GetComponent<Button>().interactable = true;
            holders[i].transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
            holdersActive[i] = false;
        }
    }
}
