using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 m_playerMovementInput;
    [SerializeField] private Vector2 m_playerMouseInput;

    [SerializeField] private Rigidbody m_rigidBody;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private KeyCode m_jumpKey;

    [SerializeField] private float m_moveSpeed, m_Sensitivity, m_xRot, m_yRot, m_jumpSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_Camera.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.Rotate(Vector3.zero);    
    }

    private void Update()
    {
        m_playerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        CameraFollow();
        RotatePlayer(m_playerMouseInput);
        Jump();
    }

    private void FixedUpdate()
    {
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
        if (Input.GetKeyDown(m_jumpKey))
            m_rigidBody.AddForce(transform.up * m_jumpSpeed, ForceMode.Impulse);
    }

    private void CameraFollow()
    {
        m_Camera.transform.position = transform.position;
    }
}
