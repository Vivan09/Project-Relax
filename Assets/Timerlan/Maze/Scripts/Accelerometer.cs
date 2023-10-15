using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    private bool isFlat = true;
    private Rigidbody2D _rb;

    [SerializeField] float moveSpeed = 10f;
    float dirX;
    float dirY;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dirX = Input.acceleration.x * moveSpeed;
        dirY = Input.acceleration.y * moveSpeed;

        _rb.velocity = new Vector2(dirX, dirY);

        //3D Acceleroment
        /*Vector3 tilt = new Vector3(Input.acceleration.x, Input.acceleration.y, 0); 

        if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0) * tilt;

        _rb.AddForce(tilt);*/
    }
}
