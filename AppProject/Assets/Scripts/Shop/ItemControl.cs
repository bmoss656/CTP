using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used for swapping between items in shop
public class ItemControl : MonoBehaviour
{
    private static ItemControl m_instance;
    public static ItemControl instance { get { return m_instance; } }


    public List<GameObject> items;

    public GameObject currentObj; 


    public int objectNum = 0;
    private int maxNum = 0;
    private int shopNum = 0;

    private void OnEnable()
    {
        m_instance = this;
    }

    private void OnDisable()
    {
        this.gameObject.SetActive(false);
        Destroy(currentObj);
        currentObj = null;
        objectNum = 0;

    }

    public void ActivateItems(int i)
    {
        //Activate passed set of items
        objectNum = 0;
        maxNum = items[i].transform.childCount - 1;
        shopNum = i;
        this.gameObject.SetActive(true);
        currentObj = Instantiate(items[shopNum]);
    }

    //Move through the list to display next avaliable object
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
       
        if(objectNum > maxNum)
        {
            objectNum = maxNum;
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

        if(objectNum < 0)
        {
            objectNum = 0;
        }
    }

    public GameObject GetCurrentItem()
    {
        //return the currently selected item
        return currentObj.transform.GetChild(objectNum).gameObject;
    }
    
}
