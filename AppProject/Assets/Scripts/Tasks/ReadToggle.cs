using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadToggle : MonoBehaviour
{

    public Toggle[] toggles;

    public int hours;
    public string[] electronics;
    public int electronicCount;

    private string[] actualE = { "Games Console", "TV", "Laptop", "Computer", "Mobile Phone/Tablet" };

    public void ReadElectronics()
    {
        foreach(Toggle t in toggles)
        {
            if(t.isOn)
            {
                electronicCount++;
            }
        }

        electronics = new string[electronicCount];

        int z = 0;

        for (int i = 0; i < 5; i++)
        {
            if (toggles[i].isOn)
            {
                electronics[z] = actualE[i];
                z++;
            }

        }

    }

    public void ReadHours()
    {
        for (int i = 0; i < 5; i++)
        {
            if(toggles[i].isOn)
            {
                hours = i;
            }
        }
    }

}
