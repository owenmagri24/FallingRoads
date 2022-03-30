using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private float m_Speed;
    [SerializeField] private float m_MinSpeed;
    [SerializeField] private float m_MaxSpeed;

    void Start()
    {
        m_Speed = Random.Range(m_MinSpeed, m_MaxSpeed);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);
        DestroyObject();
    }

    void DestroyObject()
    {
        if(gameObject.transform.position.z > 15 || gameObject.transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }
}
