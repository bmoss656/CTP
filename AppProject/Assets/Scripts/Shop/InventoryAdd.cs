using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAdd : MonoBehaviour
{
    InventoryManager mainInv;
    ItemControl mainItems;
    public AudioClip purchaseSound;

    public void Start()
    {
        mainInv = InventoryManager.instance;
        mainItems = ItemControl.instance;
    }

    public void AddToInv()
    {
        //Add item to inventory if inv isn't full, 12 should be accessable variable
        if (mainInv.heldItems.Count < 12)
        {
            mainInv.AddItem(mainItems.GetCurrentItem().name);
            if(SoundManager.Instance)
            {
                SoundManager.Instance.PlayClip(purchaseSound);
            }
        }
    }
	
}
