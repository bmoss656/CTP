using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFunctionality : MonoBehaviour
{
    public float speed = 150f;
    public LayerMask allowTouch;

    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject deSelectUI;
    public GameObject bulldozer;

    private bool selected = false;
    private bool rLeft = false;
    private bool rRight = false;
    private bool inBuildMode = false;

    private Vector3 lastPosition;

    private GameObject selectedObject;

	void Start ()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        deSelectUI.SetActive(false);
        bulldozer.SetActive(false);

    }
	
	void Update ()
    {
        if (inBuildMode)
        {
            //If an item is selected
            if (selected)
            {
                //Changes colour of object depending on if it can be placed
                if (!selectedObject.GetComponent<CollisionChecker>().colliding)
                {
                    selectedObject.GetComponent<ChangeTexture>().SetColour(Color.green);
                }
                else
                {
                    selectedObject.GetComponent<ChangeTexture>().SetColour(Color.red);
                }
                FollowTouch();
                //Rotates the object left or right
                if (rLeft)
                {
                    if(selectedObject.transform.rotation.x == 0)
                    {
                        selectedObject.transform.Rotate(Vector3.up, speed * Time.deltaTime);
                    }
                    else
                    {
                        selectedObject.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
                    }
                }
                else if (rRight)
                {
                    if (selectedObject.transform.rotation.x == 0)
                    {
                        selectedObject.transform.Rotate(Vector3.down, speed * Time.deltaTime);
                    }
                    else
                    {
                        selectedObject.transform.Rotate(Vector3.back, speed * Time.deltaTime);
                    }
                }
                return;
            }
            //Raycasts to select items
            if (Application.platform == RuntimePlatform.Android)
            {
                if ((Input.touchCount > 0))
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(raycast, out hitInfo))
                        {
                            if (hitInfo.collider.tag == "Placeable")
                            {
                                if (!selected)
                                {
                                    Select(hitInfo);
                                }
                                else
                                {
                                    Deselect();
                                }
                            }
                        }
                    }
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(raycast, out hitInfo))
                    {
                        if (hitInfo.collider.tag == "Placeable")
                        {
                            if (!selected)
                            {
                                Select(hitInfo);
                            }
                            else
                            {
                                Deselect();
                            }
                        }
                    }
                }
            }
        }
        
    }

    void FollowTouch()
    {
        //Raycasts to make items follow touch position
        if (Application.platform == RuntimePlatform.Android)
        {
            if ((Input.touchCount > 0))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(raycast, out hitInfo, Mathf.Infinity, allowTouch))
                    {
                        selectedObject.transform.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
                    }
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {

            if (Input.GetMouseButton(0))
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(raycast, out hitInfo, Mathf.Infinity, allowTouch))
                {
                    selectedObject.transform.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
                }

            }
            
        }
    }
    
    public bool GetBuildMode()
    {
        return inBuildMode;
    }

    //Only allow building while in build mode
    public void SetBuildMode(bool set)
    {
        if (!set)
        {
            Deselect();
        }
        inBuildMode = set;     
    }

    public void RotateLeft(bool check)
    {
        rLeft = check;
    }

    public void RotateRight(bool check)
    {
        rRight = check;
    }

    //Select an item
    public void Select(RaycastHit hitInfo)
    {
        selected = true;
        selectedObject = hitInfo.collider.gameObject;
        lastPosition = selectedObject.transform.position;
        rightArrow.SetActive(true);
        leftArrow.SetActive(true);
        deSelectUI.SetActive(true);
        bulldozer.SetActive(true);
        selectedObject.GetComponent<ChangeTexture>().SetColour(Color.green);
    }

    public void Deselect()
    {
        if (selectedObject)
        {
            //Deselect object and save it's position in object manager
            if (selectedObject.GetComponent<CollisionChecker>().colliding)
            {
                selectedObject.transform.position = lastPosition;
            }
            selectedObject.GetComponentInParent<PlacedObjectManager>().SetPosition(selectedObject.transform.position, 
                                                                            selectedObject.transform.rotation.eulerAngles, 
                                                                            selectedObject.transform.GetSiblingIndex());
            selectedObject.GetComponent<ChangeTexture>().ResetMaterial();
        }
        selected = false;
        selectedObject = null;
        lastPosition = new Vector3(0, 0, 0);
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        deSelectUI.SetActive(false);
        bulldozer.SetActive(false);
    }

    public void DestroyItem()
    {
        selectedObject.GetComponentInParent<PlacedObjectManager>().DeleteItem(selectedObject.transform.GetSiblingIndex());
        Destroy(selectedObject);
        selected = false;
        lastPosition = new Vector3(0, 0, 0);
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        deSelectUI.SetActive(false);
        bulldozer.SetActive(false);
    }

}
