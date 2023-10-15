using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    Vector3 mousePos;

    float posX;
    float posY;

    bool isPressed = false;

    void FixedUpdate()
    {
        if (isPressed)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector2(mousePos.x, mousePos.y);
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
    }
}
