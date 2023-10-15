using Nikita;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float forceMagnitude = 10f;
    [SerializeField] private TerrainController terrainGenerator;
    [SerializeField] private float maxVerticalForce = 5f;
    [SerializeField] private UIController uiController;

    private Vector2 Force;

    private int ballStartPosition;
    private int ballCurrPosition;
    private int distanceTraveled;
    private void Start()
    {
        Force = new Vector2(1f, -forceMagnitude);
        rb = GetComponent<Rigidbody2D>();
        ballStartPosition = (int)transform.position.x;
    }
    private void Update()
    {
        ballCurrPosition = (int)transform.position.x;
        distanceTraveled = (ballCurrPosition - ballStartPosition) / 10;
        uiController.DistanceTravelled = distanceTraveled;
    }
    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = Mathf.Clamp(velocity.y, -maxVerticalForce, maxVerticalForce);
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            if (Force.magnitude > maxVerticalForce)
            {
                Force = Force.normalized * maxVerticalForce;

            }
            rb.AddForce(Force);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            terrainGenerator.GenerateTerraine(terrainGenerator.shape.spline.GetPointCount() - 4);
            collision.gameObject.GetComponent<TriggerScript>().OnTriggered();
        }
        if (collision.tag == "Destroy")
        {
            terrainGenerator.ClearPointsBetweenFirstAndLast();
            collision.gameObject.GetComponent<TriggerScript>().OnTriggered();
        }
    }
}
