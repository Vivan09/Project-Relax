using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target; 
    public float followSpeed = 5f; 

    private void Update()
    {

        if (target != null)
        {
            transform.position = new Vector3(target.position.x,transform.position.y, transform.position.z);
            /*    Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);


                transform.position   = Vector3.Slerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            */
        }
    }
}
