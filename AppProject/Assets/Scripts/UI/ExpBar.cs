using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private float currentExp = 0;
    public float maxExp = 100;

    public Image xpBar;

    private PlayerControl playerData;

    // Use this for initialization
    void Start ()
    {
        playerData = GameObject.FindGameObjectWithTag("SaveData").GetComponent<PlayerControl>();

        currentExp = playerData.experience;
	}

    private void LateUpdate()
    {
        currentExp = playerData.experience;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if(currentExp >= maxExp)
        {
            currentExp = maxExp;
        }
        float ratio = currentExp / maxExp;
        xpBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
}
