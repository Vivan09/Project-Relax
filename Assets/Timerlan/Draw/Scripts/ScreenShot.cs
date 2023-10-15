using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public ScreenshotHandler screenshotHandler;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            screenshotHandler.TakeScreenshot(1024, 720);
        }
    }
}
