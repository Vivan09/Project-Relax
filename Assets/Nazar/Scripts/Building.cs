using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;
    public List<Tile> buildingTiles;
    public Tile availableTile;
    public Tile _selectedTile;
    public bool _canPlaceTile = false;
    public List<Sprite> sprites;

    private bool _isBuilding = false;
    private bool _isDeleting = false;
    private bool wasInteractionPressed = false;
    [SerializeField]
    private InputActionReference click, clickPos;

    private readonly Vector3Int[] neighbourPositions = 
    {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left,
    };

    private readonly Vector3Int[] diagonalPositions = 
    {
        Vector3Int.up + Vector3Int.right,
        Vector3Int.up + Vector3Int.up,
        Vector3Int.down + Vector3Int.down,
        Vector3Int.right + Vector3Int.right,
        Vector3Int.up + Vector3Int.left,
        Vector3Int.down + Vector3Int.right,
        Vector3Int.down + Vector3Int.left,
        Vector3Int.left + Vector3Int.left,
    };

    void Update()
    {

        if(click.action.triggered && _canPlaceTile && !_isDeleting)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(clickPos.action.ReadValue<Vector2>());
            var worldPos = tilemap.WorldToCell(mousePos);
            var tileSprite = tilemap.GetSprite(worldPos);
            if(tileSprite == availableTile.sprite && _canPlaceTile)
            {
                if(_isBuilding)
                {
                    var _tileTransform = Matrix4x4.Translate(new Vector3(0.02f, 0.12f, 0));
                    var tileChangeData = new TileChangeData
                    {
                        position = worldPos,
                        tile = _selectedTile,
                        color = Color.white,
                        transform = _tileTransform
                    };
                    tilemap.SetTile(tileChangeData, false);
                }else
                {
                    tilemap.SetTile(worldPos, _selectedTile);
                }
                PaintNeighbourTiles(mousePos); 
            }
        } else if(click.action.triggered && _isDeleting)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(clickPos.action.ReadValue<Vector2>());
            var worldPos = tilemap.WorldToCell(mousePos);
            var clickedTile = tilemap.GetSprite(worldPos);
            if(clickedTile != null)
            {
                var _tileTransform = Matrix4x4.Translate(new Vector3(0, 0, 0));

                var tileChangeData = new TileChangeData
                {
                   position = worldPos,
                   tile = availableTile,
                   color = Color.white,
                   transform = _tileTransform
                };
                    tilemap.SetTile(tileChangeData, false);
                DeleteNeighbourTiles(mousePos);
            }
        }
    }

    void PaintNeighbourTiles(Vector2 tilePosition)
    {
        var gridPosition = tilemap.WorldToCell(tilePosition);
        foreach(var neighbourPosition in neighbourPositions)
        {
            var position = gridPosition + neighbourPosition;

            if(!tilemap.HasTile(position))
            {
                tilemap.SetTile(position, availableTile);
            }
        }
    }

    void DeleteNeighbourTiles(Vector2 tilePosition)
    {
        var gridPosition = tilemap.WorldToCell(tilePosition);
        foreach(var neighbourPosition in neighbourPositions)
        {
            var position = gridPosition + neighbourPosition;

            Sprite sprite = tilemap.GetSprite(position);

            if(sprite == availableTile.sprite)
            {
                tilemap.SetTile(position, null);
            }
        }

        foreach(var diagonalPosition in diagonalPositions)
        {
            var position = gridPosition + diagonalPosition;

            Sprite sprite = tilemap.GetSprite(position);

            if(sprite != availableTile.sprite && sprite != null)
            {
                foreach(var neighbourPosition in neighbourPositions)
                {
                    var pos = position + neighbourPosition;
                    if(!tilemap.HasTile(pos))
                    {
                        tilemap.SetTile(pos, availableTile);
                    }
                }
            }
        }
    }

    public void SelectTile()
    {
        _canPlaceTile = true;
        var _pressedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        foreach(var tile in tiles)
        {
            if(tile.sprite == _pressedButton.GetComponent<Image>().sprite)
            {
                _selectedTile = tile;
                _isBuilding = false;
            }
        }

        foreach(var tile in buildingTiles)
        {
            if(tile.sprite == _pressedButton.GetComponent<Image>().sprite)
            {
                _selectedTile = tile;
                _isBuilding = true;
            }
        }

    }

    public void DeleteTile()
    {
         var _pressedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        _isDeleting = !_isDeleting;
        foreach(var sprite in sprites)
        {
            if(sprite != _pressedButton.GetComponent<Image>().sprite)
            {
                _pressedButton.GetComponent<Image>().sprite = sprite;
                return;
            }
        }
    }
}
