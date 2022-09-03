using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 m_PlayerMovementInput;
    [SerializeField] private Vector2 m_PlayerMouseInput;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private KeyCode m_JumpKey;

    [SerializeField] private float m_MoveSpeed, m_Sensitivity, m_xRot, m_yRot, m_JumpSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = this.GetComponent<Rigidbody>();
        m_Camera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.Rotate(Vector3.zero);    
    }

    private void Update()
    {
        PlayerInput();
        RotatePlayer(m_PlayerMouseInput);
        Jump();
    }

    private void FixedUpdate()
    {
        MovePlayer(m_PlayerMovementInput);
    }

    private void PlayerInput()
    {
        m_PlayerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X") * m_Sensitivity * Time.deltaTime, Input.GetAxis("Mouse Y") * m_Sensitivity * Time.deltaTime);
    }
    private void MovePlayer(Vector3 playermovementinput)
    {
        Vector3 moveDirection = (transform.right * playermovementinput.x * m_MoveSpeed) + (transform.forward * playermovementinput.y * m_MoveSpeed);
        moveDirection.y = rb.velocity.y;
        rb.velocity = moveDirection;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(m_JumpKey))
            rb.AddForce(transform.up * m_JumpSpeed, ForceMode.Impulse);
    }

    private void RotatePlayer(Vector2 playermouseinput)
    {
        m_xRot -= playermouseinput.y;
        m_yRot += playermouseinput.x;
        m_xRot = Mathf.Clamp(m_xRot, -90f, 90f);
        m_Camera.transform.rotation = Quaternion.Euler(m_xRot, m_yRot, 0f);
        m_PlayerTransform.rotation = Quaternion.Euler(0, m_yRot, 0);
    }
}
