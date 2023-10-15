using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxScript : MonoBehaviour
{
    public float offsetSpeedx;
    public float offsetSpeedw;
    RawImage rawImage;


    void Start()
    {
        rawImage = GetComponent<RawImage>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = rawImage.uvRect.x + offsetSpeedx;
        float w = offsetSpeedw;
        rawImage.uvRect = new Rect(x, 0, w, 1);
        // float Y = rawImage.uvRect.y + offsetSpeed;
    }
}
