using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public List<GameObject> items;
    private GameObject currentObj;


    private int objectNum = 0;
    private int maxNum = 0;
    private int shopNum = 0;

    
    private void OnDisable()
    {
        this.gameObject.SetActive(false);
        Destroy(currentObj);
    }


    public void ActivateItems(int i)
    {
        maxNum = items[i].transform.childCount - 1;
        shopNum = i;
        this.gameObject.SetActive(true);
        currentObj = Instantiate(items[shopNum]);
    }

    public void NextObject()
    {
        if(objectNum == maxNum)
        {
            currentObj.transform.GetChild(objectNum).gameObject.SetActive(false);
            objectNum = 0;
            currentObj.transform.GetChild(objectNum).gameObject.SetActive(true);
        }
        else
        {
            currentObj.transform.GetChild(objectNum).gameObject.SetActive(false);
            objectNum += 1;
            currentObj.transform.GetChild(objectNum).gameObject.SetActive(true);
        }
       
    }

    public void PreviousObject()
    {
        if (objectNum == 0)
        {
            currentObj.transform.GetChild(objectNum).gameObject.SetActive(false);
            objectNum = maxNum;
            currentObj.transform.GetChild(objectNum).gameObject.SetActive(true);
        }
        else
        {
            currentObj.transform.GetChild(objectNum).gameObject.SetActive(false);
            objectNum -= 1;
            currentObj.transform.GetChild(objectNum).gameObject.SetActive(true);
        }
    }

}
