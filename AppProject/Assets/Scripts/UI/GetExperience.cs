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
        pc = GameObject.FindGameObjectWithTag("SaveData").GetComponent<PlayerControl>();
        text = GetComponent<TextMeshProUGUI>();

        if (pc)
        {
            text.text = "Exp: " +  pc.experience.ToString();
            currentXP = pc.experience;
        }
	}

    private void Update()
    {
        if (currentXP != pc.experience)
        {
            if (pc)
            {
                text.text = "Exp: " + pc.experience.ToString();
            }
        }
    }


}
