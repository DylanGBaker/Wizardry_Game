using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform Player;
    [SerializeField] private bool canMove;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Vector3 MoveDirection;

    private void Start()
    {
        canMove = false;
    }

    private void Update()
    {
        Vector3 Direction = (Player.position - transform.position).normalized;
        MoveDirection = Direction;
        LookAtPlayer();
    }

    void FixedUpdate()
    { 
        if (canMove)
            MoveTowardsPlayer(MoveDirection);
    }

    private void MoveTowardsPlayer(Vector3 movedirection)
    {
        rb.velocity = new Vector3(movedirection.x, movedirection.y, movedirection.z) * MoveSpeed;
    }

    private void LookAtPlayer()
    {
        transform.LookAt(Player);
    }
}
