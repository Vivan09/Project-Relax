using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour {

    private static ScreenshotHandler instance;

    private Camera myCamera;
    public bool takeScreenshotOnNextFrame;

    private void Awake() 
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender() 
    {
        if (takeScreenshotOnNextFrame) 
        {
            Debug.Log(2);
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(1024, 720, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, 1024, 720);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath  + "/Timerlan/Draw/SavesScrinshot/CameraScreenshot" + ".png" , byteArray);
            Debug.Log(Application.dataPath);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    public void TakeScreenshot(int width, int height) 
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height) 
    {
        instance.TakeScreenshot(width, height);
    }
}
