using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    public GameObject toDestroy;

    public void DestroyObjects()
    {
        Destroy(toDestroy);
    }
	
}
