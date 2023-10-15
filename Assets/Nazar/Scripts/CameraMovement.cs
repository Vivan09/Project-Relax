using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class CameraMovement : MonoBehaviour
{

    private Vector3 _origin;
    private Vector3 _difference;

    public int minSize = 1;
    public int maxSize = 10;
    private float _size;
    public float sizeChangeSpeed = 0.5f;
    private Touch touch;

    private Camera _mainCamera;

    private bool _isDragging;
    private bool _endedTouch;

    [SerializeField]
    private InputActionReference clickToDrag, clickPos;

    private void Awake() 
    {
        _mainCamera = Camera.main;
    }

    public void Update()
    {
        if(clickToDrag.action.triggered)
        {
            if(Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                switch (touch.phase)
            {
                case TouchPhase.Ended:
                    _endedTouch  = true;
                    break;
            }
            }
            _origin = Camera.main.ScreenToWorldPoint(clickPos.action.ReadValue<Vector2>());
            _isDragging = true;
        } else if(Input.GetMouseButtonUp(2) || _endedTouch)  {
            _isDragging = false;
            _endedTouch  = false;

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && _mainCamera.orthographicSize < maxSize)
		{
			_mainCamera.orthographicSize += sizeChangeSpeed ;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0f && _mainCamera.orthographicSize > minSize)
		{
			_mainCamera.orthographicSize -= sizeChangeSpeed ;
		}
    }

    private void LateUpdate()
    {
        if (!_isDragging) return;

        _difference = GetMousePosition - transform.position;
        transform.position = _origin - _difference;
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(clickPos.action.ReadValue<Vector2>());
}