using UnityEngine.Tilemaps;
using UnityEngine;

public enum Tetromino{ I, O, T, J, L, S, Z}

namespace Tetris
{
    [System.Serializable]
    public struct TetrominoData
    {
        public Tetromino tetromino;
        public Tile tile;
        public Vector2Int[] Cells { get; private set; }
        public Vector2Int[,] WallKicks { get; private set; }

        public void Initialize()
        {
            Cells = Data.Cells[this.tetromino];
            WallKicks = Data.WallKicks[tetromino];
        }
    }
}

