using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    private TextMeshProUGUI text;

    public ItemControl items;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<TextMeshProUGUI>();
        
	}

    // Update is called once per frame
    void LateUpdate()
    {

        text.text = items.currentObj.transform.GetChild(items.objectNum).GetComponent<RotateItem>().price.ToString();

    }
}
