using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLink : MonoBehaviour
{
    public string[] pageName;

    public int usePage;

    public void OpenWebpage()
    {
        Application.OpenURL(pageName[usePage]);
    }
}
