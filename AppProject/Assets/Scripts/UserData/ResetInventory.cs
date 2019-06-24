using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetInventory : MonoBehaviour
{
	
    public void ResetInv()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<GetItem>().SetSprite();
        }
    }

}
