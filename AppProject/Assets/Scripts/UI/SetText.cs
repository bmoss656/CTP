using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    private TextMeshProUGUI text;

    public ItemControl items;


	void Start ()
    {
        text = GetComponent<TextMeshProUGUI>();
        
	}


    void LateUpdate()
    {
        if (items.currentObj)
        {
            if (items.currentObj.transform.GetChild(items.objectNum))
            {
                //Set price text ui in shop
                text.text = items.currentObj.transform.GetChild(items.objectNum).GetComponent<RotateItem>().price.ToString();
            }
        }
    }
}
