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

    [SerializeField]
    private bool isSelected = false;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public GameObject InventoryObject;
    public GameObject buildCanvas;

    public Transform lastParent;

    public GameObject objectHolder;

    public LayerMask whatCanBeTouched;

    private bool isEmpty = true;

    // Use this for initialization
    void Start ()
    {
        SetSprite();

        //Gets the Raycaster from the gameobject
        m_Raycaster = GetComponentInParent<GraphicRaycaster>();

        //Get the scene's event system
        m_EventSystem = GetComponent<EventSystem>();

        startingPos = transform.GetChild(0).position;
    }

    public void SetSprite()
    {
        /*Sets the sprites in the inventory to the correct image, loaded in from the resources
        if there is an object in that position of inventory*/
        itemName = mainInv.GetItem(invPosition);
        if (itemName == "NULL")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            
            isEmpty = true;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if(Resources.Load("Sprites/" + itemName))
            {
                transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Sprites/" + itemName, typeof(Sprite)) as Sprite;
            }
            isEmpty = false;
        }
    }



    void Update()
    {
        if (buildCanvas)
        {
            if (!isEmpty)
            {
                if (Application.platform == RuntimePlatform.WindowsEditor ||
                    Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        /*Raycasts different for UI, check against anything touched
                         on the ui to see if it is an object*/
                        m_PointerEventData = new PointerEventData(m_EventSystem);
                        m_PointerEventData.position = Input.mousePosition;
                        List<RaycastResult> results = new List<RaycastResult>();
                        m_Raycaster.Raycast(m_PointerEventData, results);

                        foreach (RaycastResult result in results)
                        {
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
                        //If released then spawn object at ray's position
                        RayToSpawnPos();
                        isSelected = false;
                        transform.GetChild(0).position = startingPos;
                        InventoryObject.SetActive(true);
                        GetComponent<Image>().enabled = true;
                        transform.parent = lastParent;
                        transform.GetComponentInParent<ResetInventory>().ResetInv();
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
                        transform.parent = lastParent;
                        transform.GetComponentInParent<ResetInventory>().ResetInv();
                    }
                }
                if (isSelected)
                {
                    transform.parent = buildCanvas.transform;
                    GetComponent<Image>().enabled = false;
                    InventoryObject.SetActive(false);
                    transform.GetChild(0).position = Input.mousePosition;
                }
            }
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
        else if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, whatCanBeTouched))
        {
            //If ray is within the allowed building area, spawn object
            if (hitInfo.collider.CompareTag("BuildingArea"))
            {
                SpawnItem(hitInfo.point);
                mainInv.TakeItem(invPosition);
                itemName = "NULL";
                SetSprite();
            }
        }

    }

    private void SpawnItem(Vector3 position)
    {
        //Loads in object from resources folder according to name and instantiates it
        GameObject spawn = Resources.Load("Items/" + itemName) as GameObject;
        Instantiate(spawn, position,spawn.transform.rotation , objectHolder.transform);
        objectHolder.GetComponent<PlacedObjectManager>().AddItem(itemName, position, spawn.transform.rotation.eulerAngles);
    }


    public string GetItemName()
    {
        return itemName;
    }

}
