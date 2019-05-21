using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcoScaleFill : MonoBehaviour
{
    private Image house;
    private PlayerControl pc;

    private float currentXP;

    public bool initalFill = true;
    public float waitTime = 5.0f;

    // Use this for initialization
    void Start ()
    {
        pc = GameObject.FindGameObjectWithTag("SaveData").GetComponent<PlayerControl>();
        house = GetComponent<Image>();
        currentXP = pc.experience;


       


	}
	
	// Update is called once per frame
	void Update ()
    {
        currentXP = pc.experience;

        if (initalFill == true)
        {
            //Reduce fill amount over 30 seconds
            if (house.fillAmount < currentXP /10000)
            {
                house.fillAmount += 1.0f / (currentXP / 100)  * Time.deltaTime;
            }
            else
            {
                initalFill = false;
            }
        }
        else
        {
            house.fillAmount = currentXP / 10000;
        }
    }
}
