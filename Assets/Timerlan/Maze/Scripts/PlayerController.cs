using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController character;

    public float horizontal;
    public float speed = 1f;

    Vector3 move;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        move = new Vector2(x, y) * speed;

        transform.Translate(move * Time.deltaTime);
    }
}
