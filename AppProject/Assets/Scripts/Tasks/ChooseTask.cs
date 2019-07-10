using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseTask : MonoBehaviour
{
	public void SetSelected()
    {
        GetComponentInParent<SpawnTaskOptions>().SetSelected(GetComponent<Button>(), GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
