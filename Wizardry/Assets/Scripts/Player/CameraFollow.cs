using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_playerTransform;
    void Update()
    {
        transform.position = new Vector3(m_playerTransform.position.x, m_playerTransform.position.y + 0.5f, m_playerTransform.position.z); 
    }
}
