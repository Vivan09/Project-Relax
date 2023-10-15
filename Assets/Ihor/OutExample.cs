using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutExample : MonoBehaviour
{
    void Update()
    {
        if (Physics.Raycast(new Ray(), out RaycastHit raycastHit))
        {
            float dista = raycastHit.distance;
        }

        if (TryGetDistanceVector(out Vector3 dist))
        {
            dist += Vector3.up;
            transform.Translate(dist);
        }
        
    }

    private bool TryGetDistanceVector(out Vector3 distance)
    {
        bool found = false;

        distance = new Vector3(1, 1, 1);

        if (found)
        {
            distance = new Vector3(2, 2, 2);
        }

        return found;
    }
}
