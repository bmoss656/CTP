﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{

    public float speed = 20f;


    void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
