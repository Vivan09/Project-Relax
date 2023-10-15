using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDraw : MonoBehaviour 
{
    public GameObject[] lines;

    public void Undo()
    {
        lines = GameObject.FindGameObjectsWithTag("Line");
        if (lines[lines.Length - 2] != null)
        {
            Destroy(lines[lines.Length - 2]);
        }
    }
}
