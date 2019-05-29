using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    InventoryManager mainInv;
    private string itemName;
    public int invPosition;
	// Use this for initialization
	void Start ()
    {
        mainInv = InventoryManager.instance;
        itemName = mainInv.GetItem(invPosition);
        SetSprite();
	}
	
    void SetSprite()
    {
        if(itemName == "NULL")
        {
            transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            //Set image to something
            transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }
}
