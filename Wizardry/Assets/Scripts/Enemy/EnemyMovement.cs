using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    public bool canMove;
    public float MoveSpeed;

    private void Start()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, MoveSpeed * Time.deltaTime);
        }
    }
}
