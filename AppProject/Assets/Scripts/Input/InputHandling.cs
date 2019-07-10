using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandling : MonoBehaviour
{

    void Update()
    {
        //Exits out of application using android bar or escape key
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();

                return;
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();

                return;
            }
        }
    }
}
