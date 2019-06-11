using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuildScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Placeable")
        {
            if(!other.GetComponent<BuildingFunctionality>())
            {
                other.gameObject.AddComponent<BuildingFunctionality>();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Placeable")
        {
            if (!other.GetComponent<BuildingFunctionality>())
            {
                other.gameObject.AddComponent<BuildingFunctionality>();
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Placeable")
    //    {
    //        if (other.GetComponent<BuildingFunctionality>())
    //        {
    //            Destroy(other.GetComponent<BuildingFunctionality>());
    //        }
    //    }
    //}
}
