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
    }

    private void Update()
    {
        

        m_animator.SetBool("Grounded", m_isGrounded);
        if (canMove)
        {
            OnPlayerTouch();
            if (Application.platform == RuntimePlatform.Android && canMove)
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
            else if(canMove)
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
        //float v = Input.GetAxis("Vertical");
        //float h = Input.GetAxis("Horizontal");

        //bool walk = Input.GetKey(KeyCode.LeftShift);

        //if (v < 0)
        //{
        //    if (walk) { v *= m_backwardsWalkScale; }
        //    else { v *= m_backwardRunScale; }
        //}
        //else if (walk)
        //{
        //    v *= m_walkScale;
        //}

        //m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        //m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        //transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        //transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        //m_animator.SetFloat("MoveSpeed", m_currentV);

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

    private void OnPlayerTouch()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100))
                {
                    if(hitInfo.collider.tag == "Player")
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
                        closeCam.SetActive(true);
                        canMove = false;
                        taskList.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100))
                {
                    if (hitInfo.collider.tag == "Player")
                    {

                        GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
                        closeCam.SetActive(true);
                        canMove = false;
                        taskList.SetActive(true);
                    }
                }
            }
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
