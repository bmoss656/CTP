using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetExperience : MonoBehaviour
{
    private PlayerControl pc;
    private TextMeshProUGUI text;

    private float currentXP;
	// Use this for initialization
	void Start ()
    {
        pc = PlayerControl.instance;
        text = GetComponent<TextMeshProUGUI>();

        if (pc)
        {
            text.text = "Exp: " +  pc.experience.ToString();
            currentXP = pc.experience;
        }
	}

    private void Update()
    {
        //Updates the current exp for the eco-scale
        if (currentXP != pc.experience)
        {
            if (pc)
            {
                text.text = "Exp: " + pc.experience.ToString();
                currentXP = pc.experience;
            }
        }
    }


}
