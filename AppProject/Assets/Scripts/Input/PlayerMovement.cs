using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask whatCanBeTouched;
    public GameObject closeCam;
    public GameObject taskList;
    private NavMeshAgent myAgent;
    private Rigidbody rb;

    private Animator m_animator;
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

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
                        }
                    }
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
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
                        }
                    }
                }
            }  
        }

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
