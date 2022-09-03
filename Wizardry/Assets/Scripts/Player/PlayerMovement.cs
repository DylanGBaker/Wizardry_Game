using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 m_playerMovementInput;
    [SerializeField] private Vector2 m_playerMouseInput;

    [SerializeField] private Rigidbody m_rB;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private KeyCode m_jumpKey;

    [SerializeField] private float m_moveSpeed, m_Sensitivity, m_xRot, m_yRot, m_jumpSpeed, m_verticalVelocity;
    [SerializeField] private const float m_gravityAcceleration = 9.8f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_rB = this.GetComponent<Rigidbody>();
        m_Camera.transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        //Gravity();
        PlayerInput(m_playerMovementInput, m_playerMouseInput);
        MovePlayer(m_playerMovementInput);
        RotatePlayer(m_playerMouseInput);
        Jump();
    }

    private void PlayerInput(Vector3 playermovementinput, Vector2 playermouseinput)
    {
        m_playerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_playerMouseInput = new Vector2(Input.GetAxis("Mouse X") * m_Sensitivity * Time.deltaTime, Input.GetAxis("Mouse Y") * m_Sensitivity * Time.deltaTime);
    }
    private void MovePlayer(Vector3 playermovementinput)
    {
        Vector3 moveDirection = (transform.right * playermovementinput.x) + (transform.forward * playermovementinput.y);
        transform.position = new Vector3(transform.position.x + (moveDirection.x * m_moveSpeed), transform.position.y, transform.position.z + (moveDirection.z * m_moveSpeed));
    }

    private void RotatePlayer(Vector2 playermouseinput)
    {
        m_xRot -= playermouseinput.y;
        m_yRot += playermouseinput.x;
        m_xRot = Mathf.Clamp(m_xRot, -90f, 90f);
        m_Camera.transform.rotation = Quaternion.Euler(m_xRot, m_yRot, 0f);
        m_playerTransform.rotation = Quaternion.Euler(0, m_yRot, 0);
    }

    private void Jump()
    {

    }

    private void Gravity()
    {
        m_verticalVelocity = -m_gravityAcceleration;
        Vector3 moveDirection = transform.up * m_verticalVelocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + moveDirection.y, transform.position.z);
    }
}
