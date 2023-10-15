using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrawingManager : MonoBehaviour
{
    [SerializeField] public static List<DrawLine> lines = new List<DrawLine>();
    [SerializeField] private DrawLine _linePrefab;

    private Camera _cam;
    private DrawLine _currentLine;

    public const float RESOLUTION = .1f;

    void Start()
    {
        _cam = Camera.main;
    }

    public void Undo()
    {
        Destroy(lines[lines.Count]);
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);

        if (Input.GetMouseButton(0)) _currentLine.SetPosition(mousePos);
    }
}