using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAnimalsScript : MonoBehaviour
{
   
    [SerializeField] private float flapForce;
    [SerializeField] private float flapInterval;
    [SerializeField] private float maxFallSpeed;


    private Rigidbody2D rb;

    private void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FlapCoroutine());
    }

    private IEnumerator FlapCoroutine()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(flapInterval);
            rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(Random.Range(-1, -5), rb.velocity.y);

     
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }
}

