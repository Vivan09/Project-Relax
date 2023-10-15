using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    Blue, Green, Red, Yellow, White, Gray, Orange, Purple
}

public class ColorController : MonoBehaviour
{
    LineRenderer lineRenderer;
    public LineRenderer line;

    public ColorType colorType;

    public Color blueMaterial = Color.blue;
    public Color redMaterial = Color.red;
    public Color greenMaterial = Color.green;
    public Color yellowMaterial = Color.yellow;
    public Color whiteMaterial = Color.white;
    public Color orangeMaterial = new Color(255, 99, 71);
    public Color purpleMaterial = new Color(128, 0, 128);
    public Color grayMaterial = new Color(192, 192, 192);

    private void Start()
    {
        ChangeColor(Color.white);
    }

    private void OnMouseDown()
    {
        line.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        line.endColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    public void ChangeColor(Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }
}
