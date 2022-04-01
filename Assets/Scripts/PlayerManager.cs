using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    private float m_PreviousX;
    private Animator m_Animator;
    private bool m_IsHopping;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_PreviousX = transform.position.x;
    }

    void Update()
    {
        KillPlayer();
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            //checks if an obstacle is in forward tile
            if(!terrainGenerator.m_CurrentObstacles.Contains(new Vector3(Mathf.Round(transform.position.x + 1),Mathf.Round(transform.position.y), Mathf.Round(transform.position.z))))
            {
                UpdateScore(); //updates score

                float zDifference = 0;
                if(transform.position.z % 1 != 0) //OnGridSpace
                {
                    zDifference = Mathf.Round(transform.position.z) - transform.position.z;
                }
                MoveCharacter(new Vector3(1,0,zDifference));
                }
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if(!terrainGenerator.m_CurrentObstacles.Contains(new Vector3(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y), Mathf.Round(transform.position.z + 1))))
            {
                MoveCharacter(new Vector3(0,0,1));
            }
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if(!terrainGenerator.m_CurrentObstacles.Contains(new Vector3(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y), Mathf.Round(transform.position.z - 1))))
            {
                MoveCharacter(new Vector3(0,0,-1));
            }
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            if(!terrainGenerator.m_CurrentObstacles.Contains(new Vector3(Mathf.Round(transform.position.x -1),Mathf.Round(transform.position.y), Mathf.Round(transform.position.z))))
            {
                float zDifference = 0;
                if(transform.position.z % 1 != 0) //OnGridSpace
                {
                    zDifference = Mathf.Round(transform.position.z) - transform.position.z;
                }
                MoveCharacter(new Vector3(-1, 0, zDifference));
            }
        }
    }

    private void MoveCharacter(Vector3 difference)
    {
        m_Animator.SetTrigger("hop"); //animation
        transform.position = (transform.position + difference);

        terrainGenerator.SpawnTerrain(false, transform.position);
    }


    private void UpdateScore()
    {
        if(transform.position.x > m_PreviousX)
        {
            m_PreviousX = transform.position.x;
            GameManager.instance.Score++;
        }
    }

    private void KillPlayer()
    {
        if(gameObject.transform.position.y < -5f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        if(GameManager.instance.TileToggle == true)
        {
            StartCoroutine(KinematicOff(other, 2f));
        }

        if(other.transform.tag == "Log")
        {
            gameObject.transform.parent = other.transform;
        }
        else{
            gameObject.transform.parent = null;
        }
    }

    IEnumerator KinematicOff(Collision collidedObject, float delayTime)
    {   
        //add falling/shaking animation

        GameObject objectToFall = collidedObject.gameObject;
        yield return new WaitForSeconds(delayTime);
        if(objectToFall != null && objectToFall.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
    }
    
}
