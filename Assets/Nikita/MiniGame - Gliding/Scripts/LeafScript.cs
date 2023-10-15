using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    [SerializeField] private float timeInterval;
    [SerializeField] private float maxFallSpeed;


    private Rigidbody2D rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(LeafMoving());
    }

    private IEnumerator LeafMoving()
    {
        while (true)
        {
            rb.AddForce(new Vector2(Random.Range(-5.0f, 5.0f), 0), ForceMode2D.Impulse);
            yield return new WaitForSeconds(timeInterval);
           
        }
    }
    private void Update()
    {
        if(transform.position.y< -22)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {

        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);


        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

  
}
