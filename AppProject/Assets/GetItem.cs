using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetItem : MonoBehaviour
{
    public InventoryManager mainInv;
    private string itemName;
    public int invPosition;
   
	// Use this for initialization
	void Start ()
    {
        mainInv = InventoryManager.instance;
        mainInv.AddCurrency(10);
        itemName = mainInv.GetItem(invPosition);
        SetSprite();
	}

    void SetSprite()
    {
        if (itemName == "NULL")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Set false: " + invPosition);
        }
        else
        {
            //Set image to something
            transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Set True: " + invPosition);
        }
    }
}
