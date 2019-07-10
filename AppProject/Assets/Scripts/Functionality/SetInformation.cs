using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetInformation : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public TextAsset[] textInfo;

    private string[] readInfo;
    private int currentInfo;


    public void SetText()
    {
        //Set the infopoint information depending on current state
        //Loading in from text files that are easily customised
        readInfo = textInfo[currentInfo].text.Split('\n');


        switch (EnvironmentControl.instance.curState)
        {
            case EnvironmentState.State1:
                mainText.text = readInfo[0];
                break;
            case EnvironmentState.State2:
                mainText.text = readInfo[1];
                break;
            case EnvironmentState.State3:
                mainText.text = readInfo[2];
                break;
            case EnvironmentState.State4:
                mainText.text = readInfo[3];
                break;
            case EnvironmentState.State5:
                mainText.text = readInfo[4];
                break;
            case EnvironmentState.State6:
                mainText.text = readInfo[5];
                break;
            case EnvironmentState.State7:
                mainText.text = readInfo[6];
                break;
            default:
                break;
        }
    }

    public void SetCurrentInfo(int i)
    {
        currentInfo = i;
    }
}
