﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public Material grassMat;
    public Material dirtMat;
    public Material skybox_m;

    private Renderer mainR;

    public void SetEnviroFloor()
    {
        mainR = GetComponent<Renderer>();
        if (EnvironmentControl.instance.curState == EnvironmentState.State1 ||
           EnvironmentControl.instance.curState == EnvironmentState.State2)
        {
            mainR.material = dirtMat;
            RenderSettings.skybox = dirtMat;
            RenderSettings.fog = true;
            if(EnvironmentControl.instance.curState == EnvironmentState.State1)
            {
                RenderSettings.fogEndDistance = 50;
            }
            else if (EnvironmentControl.instance.curState == EnvironmentState.State2)
            {
                RenderSettings.fogEndDistance = 90;
            }
        }
        else
        {
            mainR.material = grassMat;
            RenderSettings.fog = false;
            RenderSettings.skybox = skybox_m;
        }
    }

}