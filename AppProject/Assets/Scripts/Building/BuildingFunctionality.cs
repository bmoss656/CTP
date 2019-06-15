using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFunctionality : MonoBehaviour
{
    private bool selected = false;


    public float speed = 80f;
    private bool rLeft = false;
    private bool rRight = false;

    private Vector3 lastPosition;

    public LayerMask allowTouch;

    private GameObject selectedObject;
    private GameObject curRightArrow;
    private GameObject curLeftArrow;

    public GameObject rightArrow;
    public GameObject leftArrow;


    private bool inBuildMode = false;

	// Use this for initialization
	void Start ()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (inBuildMode)
        {
            if (selected)
            {
                FollowTouch();
                if (rLeft)
                {
                    selectedObject.transform.Rotate(Vector3.up, speed * Time.deltaTime);
                }
                else if (rRight)
                {
                    selectedObject.transform.Rotate(Vector3.down, speed * Time.deltaTime);
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
                                    selected = true;
                                    selectedObject = hitInfo.collider.gameObject;
                                    lastPosition = selectedObject.transform.position;
                                    //SpawnArrows();
                                    rightArrow.SetActive(true);
                                    leftArrow.SetActive(true);
                                }
                                else
                                {
                                    selected = false;
                                    selectedObject = null;
                                    lastPosition = new Vector3(0, 0, 0);
                                    rightArrow.SetActive(false);
                                    leftArrow.SetActive(false);
                                }
                            }
                        }
                    }
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
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
                                selected = true;
                                selectedObject = hitInfo.collider.gameObject;
                                lastPosition = selectedObject.transform.position;
                                //SpawnArrows();
                                rightArrow.SetActive(true);
                                leftArrow.SetActive(true);
                            }
                            else
                            {
                                selected = false;
                                selectedObject = null;
                                lastPosition = new Vector3(0, 0, 0);
                                rightArrow.SetActive(false);
                                leftArrow.SetActive(false);
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
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(raycast, out hitInfo, Mathf.Infinity, allowTouch))
                    {
                        selectedObject.transform.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
                    }
                }
            }
            else if (Input.touchCount == 0)
            {
                if (!selectedObject.GetComponent<CollisionChecker>().colliding)
                {
                    lastPosition = selectedObject.transform.position;
                    
                }
                else
                {
                    selectedObject.transform.position = lastPosition;
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(raycast, out hitInfo, Mathf.Infinity, allowTouch))
                {
                    selectedObject.transform.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
                }

            }
            else
            {
                if (!selectedObject.GetComponent<CollisionChecker>().colliding)
                {
                    lastPosition = selectedObject.transform.position;
                }
                else
                {
                    selectedObject.transform.position = lastPosition;
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
}
