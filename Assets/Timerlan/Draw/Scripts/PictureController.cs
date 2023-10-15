using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PictureController : MonoBehaviour
{
    [SerializeField] int i = 0;
    [SerializeField] private List<Sprite> currentPictures;
    [SerializeField] private List<Sprite> prefabPictures;

    public SpriteRenderer spriteRenderer;
    public GameObject[] lines;
    public bool takeScreenshot;

    private Camera myCamera;
    private const int WIDTH = 1280;
    private const int HEIGHT = 720;

    private void Awake()
    {
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void Start()
    {
        for (int i = 0; i < currentPictures.Count; i++)
        {
            SetPicture(i);
        }

        spriteRenderer.sprite = currentPictures[i];
    }

    private void OnPostRender()
    {
        if (takeScreenshot)
        {
            takeScreenshot = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            File.WriteAllBytes(Application.dataPath + "/Timerlan/Draw/SavesScrinshot/CameraScreenshot" + i + ".png", byteArray);
            Debug.Log(Application.dataPath);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    private void SetPicture(int index)
    {
        Texture2D tex = LoadPNG(Application.dataPath + "/Timerlan/Draw/SavesScrinshot/CameraScreenshot" + index + ".png");
        Sprite loadedSprite = Sprite.Create(tex, new Rect(0, 0, WIDTH, HEIGHT), new Vector2(0.5f, 0.5f));
        if (loadedSprite != null)
        {
            currentPictures[index] = loadedSprite;
        }
    }

    public void SetClearPictures()
    {
        currentPictures[i] = prefabPictures[i];
        spriteRenderer.sprite = prefabPictures[i];

        lines = GameObject.FindGameObjectsWithTag("Line");
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] != null)
            {
                Destroy(lines[i]);
            }
        }
    }

    public void NextPictures()
    {
        lines = GameObject.FindGameObjectsWithTag("Line");
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] != null)
            {
                Destroy(lines[i]);
            }
        }

        if (currentPictures.Count - 1 == i)
        {
            i = 0;
            spriteRenderer.sprite = currentPictures[0];
        }
        else
        {
            i++;
        }

        if (spriteRenderer.sprite.name == currentPictures[i].name)
        {
            if (currentPictures.Count - 1 == i)
            {
                i = 0;
                spriteRenderer.sprite = currentPictures[i];
            }
            else
            {
                spriteRenderer.sprite = currentPictures[i + 1];
            }
        }
        else
            spriteRenderer.sprite = currentPictures[i];

        spriteRenderer.sprite = currentPictures[i];

        SetPicture(i);
    }

    public void TakeScreenshot()
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(WIDTH, HEIGHT, 16);
        takeScreenshot = true;
    }

    private static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(WIDTH, HEIGHT);
            tex.LoadImage(fileData); 
        }
        return tex;
    }
}
