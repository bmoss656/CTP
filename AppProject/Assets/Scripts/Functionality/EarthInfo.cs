using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EarthInfo : MonoBehaviour
{
    public GameObject infoCan;
    private SetInformation setInfo;
    public int num;


    void Update()
    {
        //Detect if a raycast hits a info holder, display info if yes
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
        else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
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
        //Enable/disable info ui and rotate buttons
        GetComponent<ObjectLoading>().LoadObjects();
        GetComponent<ObjectLoading>().DisableObjects();
        setInfo = infoCan.GetComponent<SetInformation>();
        setInfo.SetCurrentInfo(num);
        setInfo.SetText();
    }

}
