using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to subtract currency on purchase
public class ShopManager : MonoBehaviour
{
    private InventoryManager inv;
    private ItemControl items;

	void Start ()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        items = GetComponent<ItemControl>();
	}
	
	public void CheckBuy()
    {
        //if you have enough currency, buy item
        if(items.currentObj.transform.GetChild(items.objectNum).GetComponent<RotateItem>().price < inv.GetCurrency() &&
            inv.heldItems.Count < 12)
        {
            BuyItem();
        }
        else
        {
            //Display Something
        }
    }

    private void BuyItem()
    {
        inv.MinusCurrency(items.currentObj.transform.GetChild(items.objectNum).GetComponent<RotateItem>().price);
    }
}
