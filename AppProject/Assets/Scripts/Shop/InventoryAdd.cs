using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAdd : MonoBehaviour
{
    InventoryManager mainInv;
    ItemControl mainItems;


    public void Start()
    {
        mainInv = InventoryManager.instance;
        mainItems = ItemControl.instance;
    }

    public void AddToInv()
    {
        mainInv.AddItem(mainItems.GetCurrentItem().name);
    }
	
}
