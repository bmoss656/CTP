using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask whatCanBeTouched;
    public GameObject closeCam;
    private NavMeshAgent myAgent;
    private Rigidbody rb;

    public GameObject doorButton;

    private Animator m_animator;

    private bool m_isGrounded;
    public bool canMove = true;


    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_isGrounded = true;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        m_animator.SetBool("Grounded", m_isGrounded);
        if (canMove)
        {
            if (Application.platform == RuntimePlatform.Android && Input.touchCount > 0)
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        Ray myRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hitInfo;

                        if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, whatCanBeTouched))
                        {
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

                        if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, whatCanBeTouched))
                        {
                            if (hitInfo.collider.tag == "Ground")
                            {
                                myAgent.SetDestination(hitInfo.point);
                            }
                            else if(hitInfo.collider.tag == "Door")
                            {
                                doorButton.SetActive(!doorButton.activeSelf);
                            }
                        }
                    }
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    if (Input.GetMouseButton(0))
                    {
                        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, whatCanBeTouched))
                        {
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
                }
            }  
        }

        bool shouldMove = myAgent.velocity.magnitude > 0.5f && myAgent.remainingDistance > myAgent.radius;

        m_animator.SetBool("Walking", shouldMove);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            m_isGrounded = true;
        }
        if(other.gameObject.tag == "Placeable")
        {
            m_animator.SetBool("Walking", false);
            myAgent.isStopped = true;
            myAgent.ResetPath();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            m_isGrounded = true;
        }
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
