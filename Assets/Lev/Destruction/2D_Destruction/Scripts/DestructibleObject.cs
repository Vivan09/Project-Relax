using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;

namespace Destruction
{
    public class DestructibleObject : MonoBehaviour
    {
        public Sprite uiImage;
        
        [SerializeField] private float moveForce;
        [SerializeField] private float destructionForce;

        private Rigidbody2D _rigidbody;

        private PlayerActions _playerActions;
        private Explodable _explodable;

        private Vector3 _touchPosition;

        private bool _canMove;

        void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _explodable = GetComponent<Explodable>();

            _playerActions = new PlayerActions();

            _playerActions.Enable();
        }

        void Update()
        {
            HandleMouseInputs();
            HandleTouchInput();
        }

        void HandleMouseInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsGameObjectOnPosition())
                    _canMove = true;
            }
            else if (Input.GetMouseButtonUp(0))
                _canMove = false;
        }

        void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if(touch.phase == TouchPhase.Began)
                {
                    if (IsGameObjectOnPosition())
                        _canMove = true;
                }
                else if (touch.phase == TouchPhase.Ended)
                        _canMove = false;
            }
        }

        private void FixedUpdate()
        {
            if(_canMove)
                MoveObject();
        }

        bool IsGameObjectOnPosition()
        {
            _touchPosition = GetTouchPosition();
            Collider2D hit = Physics2D.OverlapCircle(_touchPosition, 0.1f);
            
            if (hit != null && hit.gameObject.CompareTag("Destroy"))
                return true;
            
            return false;
        }

        void MoveObject()
        {
            _touchPosition = GetTouchPosition();

            _rigidbody.velocity = (_touchPosition - transform.position) * moveForce;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (Mathf.Abs(_rigidbody.velocity.magnitude) > destructionForce)
            {
                _explodable.fragmentInEditor();
                _explodable.explode();
                ExplosionForce ef = FindObjectOfType<ExplosionForce>();
                ef.doExplosion(transform.position);
                Spawner.Instance.StartCoroutine(nameof(Spawner.SpawnPiece));
                Destroy(gameObject);
            }
        }

        private void OnDisable()
        {
            _playerActions.Disable();
        }

        Vector2 GetTouchPosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
