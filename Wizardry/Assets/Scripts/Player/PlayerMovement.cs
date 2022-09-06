using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 m_playerMovementInput;
    [SerializeField] private Vector2 m_playerMouseInput;
    [SerializeField] private Vector3 m_gravitationalForce;

    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private Camera m_Camera;

    [SerializeField] private KeyCode m_jumpKey;

    [SerializeField] private LayerMask m_groundLayer;

    [SerializeField] private float m_moveSpeed, m_Sensitivity, m_xRot, m_yRot, m_jumpSpeed;
    [SerializeField] private const float m_Gravity = -9.8f;

    [SerializeField] private bool isGrounded;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_Camera.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.Rotate(Vector3.zero);
        isGrounded = true;
        m_gravitationalForce = Vector3.up * m_Gravity;
    }

    private void Update()
    {
        m_playerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); 
        if (isGrounded)
        {
            m_gravitationalForce = Vector3.up * m_Gravity;
        }
        else
        {
            m_gravitationalForce += new Vector3(0f, -0.08f, 0f); //Make more neat later.
        }
        Debug.Log(m_gravitationalForce);
        CameraFollow();
        RotatePlayer(m_playerMouseInput);
        Jump();  
    }

    private void FixedUpdate()
    {
        //Apply Gravity using function later.
        m_rigidBody.AddForce(m_gravitationalForce, ForceMode.Force);
        MovePlayer(m_playerMovementInput);   
    }

    private void MovePlayer(Vector3 playermovementinput)
    {
        Vector3 moveDirection = (m_moveSpeed * playermovementinput.x * transform.right) + (m_moveSpeed * playermovementinput.y * transform.forward);
        moveDirection.y = m_rigidBody.velocity.y;
        m_rigidBody.velocity = moveDirection;
    }

    private void RotatePlayer(Vector2 playermouseinput)
    {
        m_xRot -= playermouseinput.y * m_Sensitivity * Time.deltaTime;
        m_yRot += playermouseinput.x * m_Sensitivity * Time.deltaTime;
        m_xRot = Mathf.Clamp(m_xRot, -90f, 90f);
        m_Camera.transform.rotation = Quaternion.Euler(m_xRot, m_yRot, 0f);
        transform.rotation = Quaternion.Euler(0, m_yRot, 0);
    }

    private void Jump()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, this.GetComponent<CapsuleCollider>().height/2 + 0.1f, m_groundLayer);
        
        if (Input.GetKeyDown(m_jumpKey) && isGrounded)
            m_rigidBody.AddForce(transform.up * m_jumpSpeed, ForceMode.Impulse);
    }

    private void CameraFollow()
    {
        m_Camera.transform.position = transform.position;
    }
}
