using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_MovingObject;
    [SerializeField] private Transform m_SpawnPos;
    [SerializeField] private int m_MinSpawnTime;
    [SerializeField] private int m_MaxSpawnTime;
    [SerializeField] private bool m_IsRightSide;


    void Start()
    {
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar(){
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(m_MinSpawnTime, m_MaxSpawnTime));
            GameObject go = Instantiate(m_MovingObject, m_SpawnPos.position, Quaternion.identity);
            if(m_IsRightSide)
            {
                go.transform.Rotate(new Vector3(0,180,0));
            }
        }
    }
}
