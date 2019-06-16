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

    public GameObject InventoryObject;
    public GameObject buildCanvas;

    public LayerMask whatCanBeTouched;

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
                RayToSpawnPos();
                isSelected = false;
                transform.GetChild(0).position = startingPos;
                InventoryObject.SetActive(true);
                GetComponent<Image>().enabled = true;
                transform.parent = InventoryObject.transform;               
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
                RayToSpawnPos();
                isSelected = false;
                transform.GetChild(0).position = startingPos;
                InventoryObject.SetActive(true);
                GetComponent<Image>().enabled = true;
                transform.parent = InventoryObject.transform;
                RayToSpawnPos();
            }
        }
        if(isSelected)
        {
            //transform.position = Input.GetTouch(0).position;
            transform.parent = buildCanvas.transform;
            GetComponent<Image>().enabled = false;
            InventoryObject.SetActive(false);
            transform.GetChild(0).position = Input.mousePosition;
        }
    }

    private void RayToSpawnPos()
    {
        Ray myRay;
        RaycastHit hitInfo;
        if (Application.platform == RuntimePlatform.Android)
        {
            myRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, whatCanBeTouched))
        {
            if (hitInfo.collider.CompareTag("BuildingArea"))
            {
                SpawnItem(hitInfo.point);
            }
        }

    }

    private void SpawnItem(Vector3 position)
    {
        Debug.Log("Trying to spawn");
        Instantiate(Resources.Load("Items/" + itemName) as GameObject, position, Quaternion.Euler(-90, 0, 90));
    }


    public string GetItemName()
    {
        return itemName;
    }

}
