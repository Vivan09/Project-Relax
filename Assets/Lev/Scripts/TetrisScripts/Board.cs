using Tetris;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Tetris
{
    public class Board : MonoBehaviour
{
    private Score _score;
    
    public TetrominoData[] tetrominoes = new TetrominoData[7];
    
    public Vector2Int boardSize = new Vector2Int(8, 20);
    public Vector3Int spawnPosition;

    private Piece activePiece { get; set; }
    private Tilemap tilemap { get; set; }

    private RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = GetComponentInChildren<Piece>();
        _score = FindObjectOfType<Score>();

        for (int i = 0; i < tetrominoes.Length; i++) tetrominoes[i].Initialize();
    }

    void Start()
    {
        SpawnPiece();
    }
    
    public void SpawnPiece()
    {
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = tetrominoes[random];
        
        activePiece.Initialize(this, spawnPosition, data);
        
        if(IsValidPosition(activePiece, spawnPosition))
            Set(activePiece);
        else
            for (int row = Bounds.yMin; row < Bounds.yMin + 6; row++)
                ClearLine(row);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            var tilePosition = piece.Cells[i] + piece.Position;
            tilemap.SetTile(tilePosition, piece.Data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            var tilePosition = piece.Cells[i] + piece.Position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;
        
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            var tilePosition = piece.Cells[i] + position;

            if (!bounds.Contains((Vector2Int) tilePosition))
                return false;

            if (tilemap.HasTile(tilePosition))
                return false;
        }

        return true;
    }

    public void ClearLines()
    {
        var bounds = Bounds;
        int row = bounds.yMin;
        int clearedLines = 0;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                ClearLine(row);
                clearedLines++;
            }
            else row++;
        }
        
        _score.UpdateScore(clearedLines);
    }

    void ClearLine(int row)
    {
        var bounds = Bounds;

        for (int i = bounds.xMin; i < bounds.xMax; i++)
        {
            var position = new Vector3Int(i, row, 0);
            tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int i = bounds.xMin; i < bounds.xMax; i++)
            {
                var position = new Vector3Int(i, row + 1, 0);
                TileBase above = tilemap.GetTile(position);
                
                position = new Vector3Int(i, row, 0);
                tilemap.SetTile(position, above);
            }

            row++;
        }
    }

    bool IsLineFull(int row)
    {
        var bounds = Bounds;
        for (int i = bounds.xMin; i < bounds.xMax; i++)
        {
            var position = new Vector3Int(i, row, 0);

            if (!tilemap.HasTile(position))
                return false;
        }

        return true;
    }
}

}
