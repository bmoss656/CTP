using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColour : MonoBehaviour
{
    public Material set_m;

	void Start ()
    {
        GetComponent<Renderer>().material = set_m;
	}
	
}
