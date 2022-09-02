using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public Transform Player;
    public bool canMove;
    public float MoveSpeed;
    [SerializeField] private Vector3 MoveDirection;

    private void Start()
    {
        canMove = false;
    }

    private void Update()
    {
        Vector3 Direction = (Player.position - transform.position).normalized;
        MoveDirection = Direction;
    }

    void FixedUpdate()
    {
        //transform.LookAt(Player);
        LookAtPlayer(MoveDirection);

        if (canMove)
        {
            MoveTowardsPlayer(MoveDirection);
        }
    }

    private void MoveTowardsPlayer(Vector3 movedirection)
    {
        //transform.position = (transform.position) + (movedirection * MoveSpeed *Time.deltaTime);
        rb.velocity = new Vector3(movedirection.x, movedirection.y, movedirection.z) * MoveSpeed;
    }

    private void LookAtPlayer(Vector3 movedirection)
    {
        float Theta = Mathf.Atan2(movedirection.z, movedirection.x) * Mathf.Rad2Deg;
        Quaternion DeltaAngle = Quaternion.Euler(rb.rotation.x, -Theta, rb.rotation.z);
        rb.MoveRotation(DeltaAngle); 
    }
}
