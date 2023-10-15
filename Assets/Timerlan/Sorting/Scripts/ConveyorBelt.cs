using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().isKinematic = false;
        //collision.transform.position = new Vector3(transform.position.x, collision.transform.position.y, 0);
    }
}