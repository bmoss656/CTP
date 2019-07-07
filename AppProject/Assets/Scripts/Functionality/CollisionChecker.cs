using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script used for building functionality, to check whether an object can be 
placed or not. */
public class CollisionChecker : MonoBehaviour
{
    public bool colliding = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            colliding = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            colliding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }
}
