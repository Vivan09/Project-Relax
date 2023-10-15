using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchDetection : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    
    private TouchControls _controls;
    private Coroutine zoomCoroutine;
    private Camera Camera;

    private void Awake()
    {
        _controls = new TouchControls();
        Camera = Camera.main;
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        _controls.Touch.SecondaryTouchContact.started += _ => ZoomStart();
        _controls.Touch.SecondaryTouchContact.canceled += _ => ZoomEnd();
    }

    private void ZoomEnd()
    {
        zoomCoroutine = StartCoroutine("ZoomDetection");
    }

    private void ZoomStart()
    {
        StopCoroutine(zoomCoroutine);
    }

    IEnumerator ZoomDetection()
    {
        float previousDistance = 0f, distance = 0f;
        while (true)
        {
            distance = Vector2.Distance(_controls.Touch.PrimaryFinderPosition.ReadValue<Vector2>(),
                _controls.Touch.SecondaryFinderPosition.ReadValue<Vector2>());
            // zoom out
            if (distance > previousDistance)
            { 
                Camera.orthographicSize++;
            }
            else if (distance < previousDistance) // zoom in
            {
                Camera.orthographicSize--;
            }

            previousDistance = distance;

            yield return null;
        }
    }
}
