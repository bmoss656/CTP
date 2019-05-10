using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetScore : MonoBehaviour
{
    private int currency = 0;

    private InventoryManager inv;

    private TextMeshProUGUI text;

	// Use this for initialization
	void Start ()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        currency = inv.GetCurrency();

        text = GetComponent<TextMeshProUGUI>();

        text.text = ":" + currency.ToString();
        
	}

    private void Update()
    {
        if(inv.GetCurrency() != currency)
        {
            currency = inv.GetCurrency();

            text.text = ":" + currency.ToString();
        }
    }


}
