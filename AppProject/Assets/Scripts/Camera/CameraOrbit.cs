using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public GameObject orbit;
    public float rotateSpeed = 200;

    void Update ()
    {
        //Allows camera to orbit around a set position
        transform.RotateAround(orbit.transform.position, orbit.transform.up, rotateSpeed * Time.deltaTime);
    }
}
