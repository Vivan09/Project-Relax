using UnityEngine;

namespace SoftTetris
{
    [System.Serializable]
    public struct TetrominoData
    {
        public Tetromino tetromino;
        public GameObject tile;
        public Vector2Int[] Cells { get; private set; }
        public Vector2Int[,] WallKicks { get; private set; }

        public void Initialize()
        {
            Cells = Data.Cells[tetromino];
            WallKicks = Data.WallKicks[tetromino];
        }
    }
}