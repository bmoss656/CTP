using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    private bool itemSelected = false;

    private GameObject currentItem;
    private string currentItemName;


    public LayerMask touchableItems;

	void Start ()
    {
		
	}
	

	void Update ()
    {
		if(!itemSelected)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if ((Input.touchCount > 0))
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(raycast, out hitInfo, Mathf.Infinity, touchableItems))
                        {
                            
                        }
                    }
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(raycast, out hitInfo, Mathf.Infinity, touchableItems))
                    {
                       
                    }
                }
            }
        }
	}
}
