using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    void Start()
    {
      
    }

   public void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
    }
    public void OnTriggered()
    {
        Debug.Log("triggered");
        Destroy(gameObject);
    }
}
