using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EarthInfo : MonoBehaviour
{
    public Canvas infoCan;
	// Use this for initialization
	void Start ()
    {
		
	}

    void Update()
    {
        if ((Input.touchCount > 0))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.name == this.name)
                    {
                        OpenUI();
                    }
                }
            }
        }
    }

    void OpenUI()
    {
        infoCan.enabled = true;
    }

}
