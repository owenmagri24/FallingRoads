using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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
            transform.position = (transform.position + new Vector3(1,0,zDifference));
        }
    }


    private void OnCollisionEnter(Collision other) {
        StartCoroutine(KinematicOff(other, 1f));
    }

    IEnumerator KinematicOff(Collision collidedObject, float delayTime)
    {   
        //add falling animation

        GameObject objectToFall = collidedObject.gameObject;
        yield return new WaitForSeconds(delayTime);
        if(objectToFall != null)
        {
            objectToFall.transform.GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }
}
