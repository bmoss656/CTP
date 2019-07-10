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


    void Start ()
    {
        pc = PlayerControl.instance;
        house = GetComponent<Image>();
        currentXP = pc.experience;

        house.fillAmount = currentXP / 10000;
    }

    void Update()
    {
        //Updates the eco-scale fill amount and sets colour depending on exp
        currentXP = pc.experience;

        house.fillAmount = currentXP / 10000;
        if (house.fillAmount < 0.2)
        {
            house.color = Color.red;
        }
        else if (house.fillAmount < 0.5)
        {
            house.color = Color.yellow;
        }
        else
        {
            house.color = Color.green;
        }

    }
}
