using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private float stepDelay = 1f;
        [SerializeField] private float lockDelay = 0.5f;

        [SerializeField] private Button left, right, down, hardDrop, rotateClockwise, rotateCounterclockwise; 

        private float _stepTime;
        private float _lockTime;

        private int rotationIndex { get; set; }

        private Board Board { get; set; }
        public TetrominoData Data { get; private set; }
        public Vector3Int Position { get; private set; }
        public Vector3Int[] Cells { get; private set; }

        void Awake() => InitializeInputs();
    
        public void Initialize(Board board, Vector3Int position, TetrominoData data)
        {
            Board = board;
            Position = position;
            Data = data;
            rotationIndex = 0;
            _stepTime = Time.time + stepDelay;
            _lockTime = 0f;
        
            if(Cells == null)
                Cells = new Vector3Int[data.Cells.Length];

            for (int i = 0; i < data.Cells.Length; i++) Cells[i] = (Vector3Int) data.Cells[i];
        }

        void Update()
        {
            //Board.Clear(this);

            _lockTime += Time.deltaTime;

            if(Time.time >= _stepTime)
                Step();

            //Board.Set(this);
        }

        void InitializeInputs()
        {
            left.onClick.AddListener(() =>
            {
                Board.Clear(this);
                Move(Vector2Int.left);
                Board.Set(this);
            });
            right.onClick.AddListener(() =>
            {
                Board.Clear(this);
                Move(Vector2Int.right);
                Board.Set(this);
            });
            down.onClick.AddListener(() =>
            {
                Board.Clear(this);
                Move(Vector2Int.down);
                Board.Set(this);
            });
            hardDrop.onClick.AddListener(() =>
            {
                Board.Clear(this);
                HardDrop();
                Board.Set(this);
            });
            rotateClockwise.onClick.AddListener(() =>
            {
                Board.Clear(this);
                Rotate(1);
                Board.Set(this);
            });
            rotateCounterclockwise.onClick.AddListener(() =>
            {
                Board.Clear(this);
                Rotate(-1);
                Board.Set(this);
            });
        }

        void Step()
        {
            _stepTime = Time.time + stepDelay;
        
            Board.Clear(this);
            Move(Vector2Int.down);
            Board.Set(this);

            if (_lockTime >= lockDelay)
                Lock();
        }

        void Lock()
        {
            Board.Set(this);
            Board.ClearLines();
            Board.SpawnPiece();
        }
    
        void HardDrop()
        {
            while (Move(Vector2Int.down))
                continue;
        
            Lock();
        }

        bool Move(Vector2Int translation)
        {
            var newPosition = Position;
            newPosition.x += translation.x;
            newPosition.y += translation.y;

            var valid = Board.IsValidPosition(this, newPosition);

            if (valid)
            {
                Position = newPosition;
                _lockTime = 0f;
            }

            return valid;
        }

        void Rotate(int direction)
        {
            int originalRotation = rotationIndex;
            rotationIndex = Wrap(rotationIndex + direction, 0, 4);
        
            ApplyRotationMatrix(direction);

            if (!TestWallKicks(rotationIndex, direction))
            {
                rotationIndex = originalRotation;
                ApplyRotationMatrix(-direction);
            }
        }

        void ApplyRotationMatrix(int direction)
        {
            float[] matrix = global::Data.RotationMatrix;


            for (int i = 0; i < Data.Cells.Length; i++)
            {
                Vector3 cell = Cells[i];
            
                int x, y;

                switch (Data.tetromino)
                {
                    case Tetromino.I:
                    case Tetromino.O:
                        cell.x -= 0.5f;
                        cell.y -= 0.5f;
                        x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;

                    default:
                        x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;
                }
            
                Cells[i] = new Vector3Int(x, y, 0);
            }
        }

        bool TestWallKicks(int rotationIndex, int rotationDirection)
        {
            var wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

            for (int i = 0; i < Data.WallKicks.GetLength(1); i++)
            {
                var translation = Data.WallKicks[wallKickIndex, i];

                if (Move(translation))
                    return true;
            }

            return false;
        }

        int GetWallKickIndex(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = rotationIndex * 2;

            if (rotationIndex < 0)
                wallKickIndex--;

            return Wrap(wallKickIndex, 0, Data.WallKicks.GetLength(0));
        }
    
        int Wrap(int input, int min, int max)
        {
            if (input < min) {
                return max - (min - input) % (max - min);
            } else {
                return min + (input - min) % (max - min);
            }
        }
    }
}
