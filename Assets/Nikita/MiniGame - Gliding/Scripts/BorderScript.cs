using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D targetRb;
    [SerializeField] private int offsetX;

    public void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
       
        if(target.position.x - transform.position.x > offsetX)
        {
            transform.position = new Vector3(target.position.x - offsetX, target.transform.position.y, transform.position.z);
        }
        else 
        {
            transform.position = new Vector3(transform.position.x  , target.transform.position.y, transform.position.z);
        }
        
    }
}
