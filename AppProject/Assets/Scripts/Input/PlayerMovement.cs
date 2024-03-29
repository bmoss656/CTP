﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask whatCanBeTouched;
    public GameObject doorButton;

    private NavMeshAgent myAgent;
    private Animator m_animator;

    private bool m_isGrounded;

    public bool canMove = true;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_isGrounded = true;
    }

    private void FixedUpdate()
    {
        
        m_animator.SetBool("Grounded", m_isGrounded);
        if (canMove)
        {
            //Raycasts for movement and selection of house door
            if (Application.platform == RuntimePlatform.Android && Input.touchCount > 0)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        Ray myRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hitInfo;
                        
                            if (Physics.Raycast(myRay, out hitInfo, 30f, whatCanBeTouched))
                            {
                                //Move navmesh agent if ground is touched
                                if (hitInfo.collider.tag == "Ground")
                                {
                                    myAgent.SetDestination(hitInfo.point);
                                }
                                else if (hitInfo.collider.tag == "Door")
                                {
                                    doorButton.SetActive(!doorButton.activeSelf);
                                }
                            }
                        
                    }
                    else if(Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        Ray myRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hitInfo;
                        
                            if (Physics.Raycast(myRay, out hitInfo, 20f, whatCanBeTouched))
                            {
                                if (hitInfo.collider.tag == "Ground")
                                {
                                    myAgent.SetDestination(hitInfo.point);
                                }
                            }
                        
                    }
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor || 
                Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if(Input.GetMouseButtonDown(0))
                    {
                        if (Physics.Raycast(myRay, out hitInfo, 40f, whatCanBeTouched))
                        {                         
                            if (hitInfo.collider.tag == "Door")
                            {
                                doorButton.SetActive(!doorButton.activeSelf);
                            }
                        }
                    }
                    else if (Input.GetMouseButton(0))
                    {                      
                        if (Physics.Raycast(myRay, out hitInfo, 20f, whatCanBeTouched))
                        {
                            if (hitInfo.collider.tag == "Ground")
                            {
                                myAgent.SetDestination(hitInfo.point);
                            }                          
                        }
                    }
                }
            }  
        }

        //Dealing with player animation
        bool shouldMove = myAgent.velocity.magnitude > 0.5f && myAgent.remainingDistance > myAgent.radius;

        m_animator.SetBool("Walking", shouldMove);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            m_isGrounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            m_isGrounded = true;
        }
    }

    private bool CheckOver()
    {
        bool check = false;
        //Check to see if touching UI, if so dont move player
        for(int i = 0; i < Input.touchCount; i++)
        {
            check = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId);
            if(check)
            {
                break;
            }
        }
        return check;
    }

    public void SetMove(bool set)
    {
        canMove = set;
    }

    public void Waving()
    {
        if (!m_animator.GetBool("Wave"))
        {
            m_animator.SetBool("Wave", true);
        }
    }
}
