﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EarthInfo : MonoBehaviour
{
    public GameObject infoCan;
    private SetInformation setInfo;
    public int num;
	// Use this for initialization
	void Start ()
    {
		
	}

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if ((Input.touchCount > 0))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(raycast, out hitInfo))
                    {
                        if (hitInfo.collider.name == this.name)
                        {
                            OpenUI();
                        }
                    }
                }
            }
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(myRay, out hitInfo))
                {
                    if (hitInfo.collider.name == this.name)
                    {
                        OpenUI();
                    }
                }
            }
        }
    }

    void OpenUI()
    {
        infoCan.SetActive(true);
        setInfo = infoCan.GetComponent<SetInformation>();
        setInfo.SetCurrentInfo(num);
        setInfo.SetText();
    }

}
