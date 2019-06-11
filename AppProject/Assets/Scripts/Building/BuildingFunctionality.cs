using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFunctionality : MonoBehaviour
{
    private bool selected = false;


    public float speed = 40f;
    private bool rLeft = false;
    private bool rRight = false;

    private Vector3 lastPosition;

    public LayerMask allowTouch;

    private GameObject selectedObject;
    private GameObject curRightArrow;
    private GameObject curLeftArrow;

    public GameObject rightArrow;
    public GameObject leftArrow;   

	// Use this for initialization
	void Start ()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
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

    void SpawnArrows()
    {
        curRightArrow = Instantiate(rightArrow);

        curRightArrow.transform.position = selectedObject.transform.position;
        curRightArrow.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y + 5,
            selectedObject.transform.position.z - ((GetComponent<BoxCollider>().size.z) * 10));

        curLeftArrow = Instantiate(leftArrow);

        curLeftArrow.transform.position = selectedObject.transform.position;
        curLeftArrow.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y + 5,
            selectedObject.transform.position.z + ((GetComponent<BoxCollider>().size.z) * 10));


        curRightArrow.transform.SetParent(selectedObject.transform);
        curLeftArrow.transform.SetParent(selectedObject.transform);
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
