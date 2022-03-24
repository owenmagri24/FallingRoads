using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private float m_Smoothness;

    void Start()
    {
        
    }
    
    private void Update(){
        if(m_Player != null)
        {
            transform.position = Vector3.Lerp(transform.position, m_Player.transform.position + m_Offset, m_Smoothness);
        }
    }
}
