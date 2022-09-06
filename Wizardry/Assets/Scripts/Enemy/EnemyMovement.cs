using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 m_moveDirection;

    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private Transform m_Player;

    [SerializeField] private bool m_canMove;
    [SerializeField] private float m_moveSpeed;

    private void Start()
    {
        m_canMove = false;
    }

    private void Update()
    {
        Vector3 Direction = (m_Player.position - transform.position).normalized;
        m_moveDirection = Direction;
        LookAtPlayer();
    }

    void FixedUpdate()
    {
        if (m_canMove)
            MoveTowardsPlayer(m_moveDirection);       
    }

    private void MoveTowardsPlayer(Vector3 movedirection)
    {
        m_rigidBody.velocity = new Vector3(movedirection.x, movedirection.y, movedirection.z) * m_moveSpeed;    
    }

    private void LookAtPlayer()
    {
        transform.LookAt(m_Player);
    }
}
