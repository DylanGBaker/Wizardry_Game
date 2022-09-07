using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int m_Health;

    public void TakeDamage(int damage)
    {
        m_Health -= damage;

        if (m_Health <= 0)
            Destroy(gameObject);
    }
}
