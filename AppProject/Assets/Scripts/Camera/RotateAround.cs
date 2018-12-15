using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private bool rLeft = false;
    private bool rRight = false;

    public GameObject orbit;

    public float rotateSpeed = 200;

	
	// Update is called once per frame
	void Update ()
    {
        if (rLeft)
        {
            transform.RotateAround(orbit.transform.position, orbit.transform.up, 90);
        }
        else if (rRight)
        {
            transform.RotateAround(orbit.transform.position, orbit.transform.up, -90);
        }

        rRight = false;
        rLeft = false;
    }

    public void OnPressedLeft()
    {
        rLeft = true;
    }

    public void OnPressedRight()
    {
        rRight = true; 
    }
}
