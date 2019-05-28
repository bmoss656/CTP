using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public Material grassMat;
    public Material dirtMat;

    private Renderer mainR;
	// Use this for initialization
	void Start ()
    {
        mainR = GetComponent<Renderer>();
        if (EnvironmentControl.instance.curState == EnvironmentState.State1 ||
            EnvironmentControl.instance.curState == EnvironmentState.State2)
        {
            mainR.material = dirtMat;
            RenderSettings.skybox = dirtMat;
        }
        else
        {
            mainR.material = grassMat;
           
        }


    }

}
