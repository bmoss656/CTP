using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GetItem : MonoBehaviour
{
    public InventoryManager mainInv;
    private string itemName;
    public int invPosition;

    private Vector3 startingPos;

    private Button thisButton;
    [SerializeField]
    private bool isSelected = false;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;


    // Use this for initialization
    void Start ()
    {
        itemName = mainInv.GetItem(invPosition);
        SetSprite();
        thisButton = GetComponent<Button>();

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponentInParent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        startingPos = transform.GetChild(0).position;
    }

    void SetSprite()
    {
        if (itemName == "NULL")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            //Set image to something
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }



    void Update()
    {

        //Check if the left Mouse button is clicked
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                m_Raycaster.Raycast(m_PointerEventData, results);

                foreach (RaycastResult result in results)
                {
                    Debug.Log(result.gameObject.name);
                    if (result.gameObject.name == gameObject.name)
                    {
                        if (isSelected)
                        {
                            isSelected = false;
                        }
                        else
                        {
                            isSelected = true;
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isSelected = false;
                transform.GetChild(0).position = startingPos;
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = Input.GetTouch(0).position;
                List<RaycastResult> results = new List<RaycastResult>();
                m_Raycaster.Raycast(m_PointerEventData, results);

                foreach (RaycastResult result in results)
                {
                    Debug.Log(result.gameObject.name);
                    if (result.gameObject.name == gameObject.name)
                    {
                        if (isSelected)
                        {
                            isSelected = false;
                        }
                        else
                        {
                            isSelected = true;
                        }
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isSelected = false;
                transform.GetChild(0).position = startingPos;
            }
        }
        if(isSelected)
        {
            //transform.position = Input.GetTouch(0).position;
            transform.GetChild(0).position = Input.mousePosition;
        }









        //// Check if there is a touch
        //if (Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    if(Input.GetMouseButtonDown(0))
        //    {
        //        Debug.Log(EventSystem.current.currentSelectedGameObject);
        //        if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null)
        //        {
        //            Debug.Log("DoingSomething");
        //                if (EventSystem.current.currentSelectedGameObject.name == gameObject.name)
        //                {

        //                    isSelected = true;
        //               }

        //        }
        //    }
        //}
        //else
        //{
        //    if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        //    {
        //        // Check if finger is over a UI element 
        //        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //        {
        //            isSelected = true;
        //        }
        //        else
        //        {
        //            Debug.Log("UI is not touched");
        //            //so here call the methods you call when your other in-game objects are touched 
        //        }
        //    }
        //    else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //    {

        //    }
        //    else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.touchCount == 0)
        //    {

        //    }
        //}
    }

    public string GetItemName()
    {
        return itemName;
    }

}
