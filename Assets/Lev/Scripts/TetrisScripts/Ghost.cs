using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private Tile tile;
    
        private Board _mainBoard;
        private Piece _trackingPiece;

        public Tilemap tilemap { get; private set; }
        public Vector3Int[] Cells { get; private set; }
        public Vector3Int Position { get; private set; }

        private void Awake()
        {
            tilemap = GetComponentInChildren<Tilemap>();
            Cells = new Vector3Int[4];
            _mainBoard = FindObjectOfType<Board>();
            _trackingPiece = FindObjectOfType<Piece>();
        }

        private void LateUpdate()
        {
            Clear();
            Copy();
            Drop();
            Set();
        }

        private void Clear()
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                Vector3Int tilePosition = Cells[i] + Position;
                tilemap.SetTile(tilePosition, null);
            }
        }

        private void Copy()
        {
            for (int i = 0; i < Cells.Length; i++) {
                Cells[i] = _trackingPiece.Cells[i];
            }
        }

        private void Drop()
        {
            Vector3Int position = _trackingPiece.Position;

            int current = position.y;
            int bottom = -_mainBoard.boardSize.y / 2 - 1;

            _mainBoard.Clear(_trackingPiece);

            for (int row = current; row >= bottom; row--)
            {
                position.y = row;

                if (_mainBoard.IsValidPosition(_trackingPiece, position)) {
                    Position = position;
                } else {
                    break;
                }
            }

            _mainBoard.Set(_trackingPiece);
        }

        private void Set()
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                Vector3Int tilePosition = Cells[i] + Position;
                tilemap.SetTile(tilePosition, tile);
            }
        }

    }
}

