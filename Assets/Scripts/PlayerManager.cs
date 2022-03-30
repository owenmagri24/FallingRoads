using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text m_ScoreText;
    private int m_Score;
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
        m_ScoreText.text = "Score: "+ m_Score;
        if(Input.GetKeyDown(KeyCode.W))
        {
            for (int i = 0; i < terrainGenerator.m_CurrentObstacles.Count; i++)
            {
                if(terrainGenerator.m_CurrentObstacles[i].transform.position.x != gameObject.transform.position.x+1)
                {
                    Debug.Log(terrainGenerator.m_CurrentObstacles[i].transform.position.x); //0
                    UpdateScore(); //updates score

                    float zDifference = 0;
                    if(transform.position.z % 1 != 0) //OnGridSpace
                    {
                        zDifference = Mathf.Round(transform.position.z) - transform.position.z;
                    }
                    MoveCharacter(new Vector3(1,0,zDifference));
                }
            }
            

        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            MoveCharacter(new Vector3(0,0,1));
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            MoveCharacter(new Vector3(0,0,-1));
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            float zDifference = 0;
            if(transform.position.z % 1 != 0) //OnGridSpace
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MoveCharacter(new Vector3(-1, 0, zDifference));
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
            m_Score++;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "Log")
        {
            gameObject.transform.parent = other.transform;
        }
        else{
            gameObject.transform.parent = null;
        }
    }
    /*
    private void OnCollisionEnter(Collision other) {
        StartCoroutine(KinematicOff(other, 1f));
    }

    IEnumerator KinematicOff(Collision collidedObject, float delayTime)
    {   
        //add falling/shaking animation

        GameObject objectToFall = collidedObject.gameObject;
        yield return new WaitForSeconds(delayTime);
        if(objectToFall != null)
        {
            objectToFall.transform.GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }
    */
}
