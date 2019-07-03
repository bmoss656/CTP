using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseTask : MonoBehaviour
{

	public void SetSelected()
    {
        //if(tasks.selectionCount < 5)
        //{
        //    tasks.SetString(GetComponentInChildren<TextMeshProUGUI>().text);
        //    tasks.selectionCount++;
        //    GetComponent<Button>().interactable = false;
        //}
        GetComponentInParent<SpawnTaskOptions>().SetSelected(GetComponent<Button>(), GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
