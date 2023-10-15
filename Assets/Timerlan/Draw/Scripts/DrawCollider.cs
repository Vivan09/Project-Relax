using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCollider : MonoBehaviour
{
    public static bool isDraw;

    private void OnMouseDown()
    {
        isDraw = true;
    }

    private void OnMouseUp()
    {
        isDraw = false;
    }
}
