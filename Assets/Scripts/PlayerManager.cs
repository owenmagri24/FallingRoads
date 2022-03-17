using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    private Animator m_Animator;
    private bool m_IsHopping;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            //animations
            m_Animator.SetTrigger("hop");

            float zDifference = 0;
            if(transform.position.z % 1 != 0) //OnGridSpace
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MoveCharacter(new Vector3(1,0,zDifference));
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            MoveCharacter(new Vector3(0,0,1));
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            MoveCharacter(new Vector3(0,0,-1));
        }
    }

    private void MoveCharacter(Vector3 difference)
    {
        m_Animator.SetTrigger("hop"); //animation
        transform.position = (transform.position + difference);

        terrainGenerator.SpawnTerrain(false, transform.position);
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
