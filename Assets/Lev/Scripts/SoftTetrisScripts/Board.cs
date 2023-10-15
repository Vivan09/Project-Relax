using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Score = Tetris.Score;

namespace SoftTetris
{
    public class Board : MonoBehaviour
    {
        public float moveForce, rotationAngle;
        public Vector2 velocity;

        private Score _score;

        [Header("Swipe restrictions"), SerializeField] 
        
        private float minDistance, maxTime;

        [Range(0, 1), SerializeField] private float directionThreshold = .9f;

        [SerializeField] private TetrominoData[] tetrominoes = new TetrominoData[7];

        [SerializeField] private Vector3Int spawnPosition;

        public delegate void StartTouch(Vector2 position, float time);

        public event StartTouch OnStartTouch;

        public delegate void EndTouch(Vector2 position, float time);

        public event EndTouch OnEndTouch;


        private Piece ActivePiece { get; set; }
        private InputManager InputManager { get; set; }
        private DetectSwipe DetectSwipe { get; set; }
        private UIManager UiManager { get; set; }

        private float _width;
        private int _height;
        
        private float _bottom;

        private Camera _mainCamera;

        private bool _canCreateBlock = true;


        void OnEnable()
        {
            _score = FindObjectOfType<Score>();
            _width = ViewportHandler.Instance.Width;
            _height = Mathf.RoundToInt(ViewportHandler.Instance.Height);
            _mainCamera = Camera.main;
            UiManager = FindObjectOfType<UIManager>();

            InputManager = new InputManager();
            InputManager.EnablePlayersControls();

            InputManager.PlayerActions.SoftTetrisTouch.PrimaryContact.started += StartPrimaryTouch;
            InputManager.PlayerActions.SoftTetrisTouch.PrimaryContact.canceled += EndPrimaryTouch;
            
            DetectSwipe = new DetectSwipe(this, minDistance, maxTime);
            DetectSwipe.SubscribeEvents();

            _bottom = -Utils.CalculateBottom(_mainCamera.transform.position, 15);

            for (int i = 0; i < tetrominoes.Length; i++) tetrominoes[i].Initialize();
        }

        void OnDisable() 
        {
            InputManager?.DisablePlayerControls();
            DetectSwipe?.UnsubscribeEvents();
        }

        void FixedUpdate() => UpdateActivePiece();

        void UpdateActivePiece()
        {
            if (ActivePiece == null || ActivePiece.CellsRigidbodies[0].velocity.magnitude <= 0.05f && _canCreateBlock)
            {
                if (ActivePiece != null)
                {
                    if (Vector2.Distance(ActivePiece.transform.position, new Vector2(spawnPosition.x, spawnPosition.y)) < 1.5f) // here to open the game over menu
                    {
                        UiManager.OnGameOver.Invoke();
                        _canCreateBlock = false;
                        return;
                    }
                            
                    foreach (var cell in ActivePiece.Cells)
                        for (int i = 0; i < cell.transform.childCount; i++)
                            cell.transform.GetChild(i).gameObject.tag = "Tetromino";

                    ActivePiece.SetRigidbodiesVelocity(Vector2.zero);

                    ClearLines();
                }
                
                ActivePiece = GeneratePiece();
            }
        }

        Piece GeneratePiece()
        {
            var pieceObject = new GameObject("Tetromino");
            var piece = pieceObject.AddComponent<Piece>();
            var pieceRigidbody = pieceObject.AddComponent<Rigidbody2D>();
            pieceRigidbody.drag = 0.8f;
            pieceRigidbody.gravityScale = 0.5f;
            pieceRigidbody.velocity = velocity;
            piece.transform.position = spawnPosition;
            piece.Initialize(this, spawnPosition, tetrominoes[Random.Range(0, tetrominoes.Length)]);
            
            return piece;
        }

        public bool IsValidPosition(Vector2 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.CompareTag("Border") || collider.gameObject.CompareTag("Tetromino"))
                    return false;
            }

            return true;
        }

        void ClearLines()
        {
            List<GameObject> listToDestroy = new List<GameObject>();
            Vector2 position = Vector2.zero;
            int clearedLines = 0;



            for (float y = _bottom + 0.5f; y <= _height / 2; y++)
            {
                for (float x = -_width / 2 + 0.5f; x <= _width / 2; x++)
                {
                    position = new Vector2(x, y);
                    var cube = GetGameObjectOnPosition(position);
                    if (cube != null) listToDestroy.Add(cube);
                }

                if (listToDestroy.Count == Mathf.RoundToInt(_width))
                {
                    clearedLines++;
                    StartCoroutine(ClearLine(listToDestroy));
                }

                listToDestroy = new List<GameObject>();
            }

            _score.UpdateScore(clearedLines);
        }

        IEnumerator ClearLine(List<GameObject> lineToClear)
        {
            Piece piece = null;

            foreach (var cube in lineToClear)
            {
                if (cube != null)
                {
                    piece = cube.GetComponentInParent<Piece>();
                    
                    for (int i = 0; i < piece.Cells.Count; i++)
                    {
                        if (piece.Cells[i] == cube)
                        {
                            if (i > 0 && piece.Cells[i - 1].GetComponentInChildren<FixedJoint2D>() != null)
                                piece.Cells[i - 1].GetComponentInChildren<FixedJoint2D>().enabled = false;
                            else if (i == 0 && piece.Cells[0].GetComponentInChildren<FixedJoint2D>() != null)
                                piece.Cells[0].GetComponentInChildren<FixedJoint2D>().enabled = false;

                            piece.Cells.Remove(cube);
                            Destroy(cube);
                            if (piece.Cells.Count == 0)
                                Destroy(piece.gameObject);
                            yield return new WaitForSeconds(.05f);
                            break;
                        }
                    }
                }
            }
        }

        GameObject GetGameObjectOnPosition(Vector2 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.15f);

            foreach (var collider in colliders)
                if (collider.gameObject.CompareTag("Tetromino"))
                    return collider.gameObject.transform.parent.gameObject;

            return null;
        }

        void StartPrimaryTouch(InputAction.CallbackContext context) =>
            OnStartTouch?.Invoke(
                Utils.ScreenToWorldPoint(_mainCamera,
                    Input.mousePosition),
                (float) context.startTime);

        void EndPrimaryTouch(InputAction.CallbackContext context) =>
            OnEndTouch?.Invoke(
                Utils.ScreenToWorldPoint(_mainCamera,
                    Input.mousePosition),
                (float) context.time);

        public void SwipeDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
                ActivePiece.Rotate(1);
            else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
                ActivePiece.MovePiece(Vector2.right);
            else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
                ActivePiece.Rotate(-1);
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold) 
                ActivePiece.MovePiece(Vector2.left);
        }
    }
}