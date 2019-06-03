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
        m_isGrounded = false;
    }

    private void Update()
    {
        
        m_animator.SetBool("Grounded", m_isGrounded);
        if (canMove)
        {
            if (Application.platform == RuntimePlatform.Android && Input.touchCount > 0)
            {

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray myRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeTouched))
                    {
                        myAgent.SetDestination(hitInfo.point);
                    }
                }

            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeTouched))
                    {
                        myAgent.SetDestination(hitInfo.point);
                    }
                }
            }  
        }

        if (myAgent.velocity.x > 0.1f || myAgent.velocity.z > 0.1f)
        {
            m_animator.SetBool("Walking", true);
        }
        else
        {
            m_animator.SetBool("Walking", false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_isGrounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            m_isGrounded = true;
        }

    }

    //private void OnPlayerTouch()
    //{
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        if (Input.GetTouch(0).phase == TouchPhase.Began)
    //        {
    //            Ray myRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
    //            RaycastHit hitInfo;

    //            if (Physics.Raycast(myRay, out hitInfo, 100))
    //            {
    //                if(hitInfo.collider.tag == "Player")
    //                {
    //                    GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
    //                    closeCam.SetActive(true);
    //                    canMove = false;
    //                    taskList.SetActive(true);
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if(Input.GetMouseButtonDown(0))
    //        {
    //            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hitInfo;

    //            if (Physics.Raycast(myRay, out hitInfo, 100))
    //            {
    //                if (hitInfo.collider.tag == "Player")
    //                {

    //                    GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
    //                    closeCam.SetActive(true);
    //                    canMove = false;
    //                    taskList.SetActive(true);
    //                }
    //            }
    //        }
    //    }
    //}

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
