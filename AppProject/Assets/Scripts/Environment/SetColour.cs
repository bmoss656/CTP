using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColour : MonoBehaviour
{
    public Material set_m;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().material = set_m;
	}
	
}
