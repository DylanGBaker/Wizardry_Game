using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 m_PlayerMovementInput;
    [SerializeField] private Vector2 m_PlayerMouseInput;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Transform m_PlayerTransform;

    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_Sensitivity;
    [SerializeField] private float m_xRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = this.GetComponent<Rigidbody>();
        m_Camera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.Rotate(Vector3.zero);    
    }

    private void FixedUpdate()
    {
        m_PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        m_PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X") * m_Sensitivity * Time.deltaTime, Input.GetAxis("Mouse Y") * m_Sensitivity * Time.deltaTime);

        MovePlayer(m_PlayerMovementInput);
        RotatePlayer(m_PlayerMouseInput);
    }

    private void MovePlayer(Vector3 playermovementinput)
    {
        rb.velocity = ((transform.right * playermovementinput.x) + (transform.forward * playermovementinput.z)) * m_MoveSpeed * Time.deltaTime;
    }

    private void RotatePlayer(Vector2 playermouseinput)
    {
        m_xRot -= playermouseinput.y;
        m_xRot = Mathf.Clamp(m_xRot, -60f, 70f);
        m_Camera.transform.localRotation = Quaternion.Euler(m_xRot, 0, 0);
        transform.Rotate(Vector3.up * playermouseinput.x);
    }
}
