using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFunctionality : MonoBehaviour
{
    private bool selected = false;


    public float speed = 150f;
    private bool rLeft = false;
    private bool rRight = false;

    private Vector3 lastPosition;

    public LayerMask allowTouch;

    private GameObject selectedObject;
    private GameObject curRightArrow;
    private GameObject curLeftArrow;

    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject deSelectUI;
    public GameObject bulldozer; 

    private bool inBuildMode = false;

	// Use this for initialization
	void Start ()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        deSelectUI.SetActive(false);
        bulldozer.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (inBuildMode)
        {
            if (selected)
            {
                if (!selectedObject.GetComponent<CollisionChecker>().colliding)
                {
                    selectedObject.GetComponent<ChangeTexture>().SetColour(Color.green);
                }
                else
                {
                    selectedObject.GetComponent<ChangeTexture>().SetColour(Color.red);
                }
                FollowTouch();
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

    public void Select(RaycastHit hitInfo)
    {
        selected = true;
        selectedObject = hitInfo.collider.gameObject;
        lastPosition = selectedObject.transform.position;
        //SpawnArrows();
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
            if (selectedObject.GetComponent<CollisionChecker>().colliding)
            {
                selectedObject.transform.position = lastPosition;
            }
            Debug.Log(selectedObject.transform.GetSiblingIndex());
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
