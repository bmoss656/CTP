using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private bool rLeft = false;
    private bool rRight = false;

    public GameObject orbit;

    public float rotateSpeed = 200;


    void Update ()
    {
        //Used in combination with buttons to rotate the camera
        if (rLeft)
        {
            transform.RotateAround(orbit.transform.position, orbit.transform.up, rotateSpeed * Time.deltaTime);
        }
        else if (rRight)
        {
            transform.RotateAround(orbit.transform.position, orbit.transform.up, -rotateSpeed * Time.deltaTime);
        }
    }

    public void OnPressedLeft(bool check)
    {
        rLeft = check;
    }

    public void OnPressedRight(bool check)
    {
        rRight = check; 
    }
}
