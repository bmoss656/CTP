using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetButtonText : MonoBehaviour
{
    private int position;


    public void SetText(string[] text)
    {
        for(int i =0; i < text.Length;i++)
        {
            Debug.Log(text[i]);
        }
        int max = position * 5;
        int z = 0;
        Debug.Log(max);
        for(int i = max; i < max + 5; i++)
        {
            Debug.Log(i);
            if (!string.IsNullOrEmpty(text[i]))
            {
                transform.GetChild(z).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text[i];
            }
            else
            {
                transform.GetChild(z).gameObject.SetActive(false);
            }
            z++;
        }
    }


	public void SetPos(int num)
    {
        position = num;
    }
}
