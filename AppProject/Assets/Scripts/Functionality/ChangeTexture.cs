using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    private Color[] childCol;
    private Color mainCol;

    public bool GetChildren = true;

    private void Start()
    {
        //Different functions needed depending on whether object renderer is in the children
        if (GetChildren)
        {
            childCol = new Color[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                childCol[i] = transform.GetChild(i).GetComponent<Renderer>().material.color;
            }
        }
        else
        {
            mainCol = GetComponent<Renderer>().material.color;
        }
    }

    public void SetColour(Color colour)
    {
        //Sets colour depending on passed in colour, used for building colour change
        if (GetChildren)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = colour;

            }
        }
        else
        {
            GetComponent<Renderer>().material.color = colour;
        }
    }

    //Set material back to normal
    public void ResetMaterial()
    {
        if (GetChildren)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = childCol[i];
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = mainCol;
        }
    }
}
