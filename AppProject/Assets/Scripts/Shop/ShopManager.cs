using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private InventoryManager inv;
    private ItemControl items;
	// Use this for initialization
	void Start ()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        items = GetComponent<ItemControl>();
	}
	
	public void CheckBuy()
    {
        if(items.currentObj.transform.GetChild(items.objectNum).GetComponent<RotateItem>().price < inv.GetCurrency())
        {
            BuyItem();
        }
        else
        {
            //DisplaySomething
        }
    }

    private void BuyItem()
    {
        inv.MinusCurrency(items.currentObj.transform.GetChild(items.objectNum).GetComponent<RotateItem>().price);
    }
}
