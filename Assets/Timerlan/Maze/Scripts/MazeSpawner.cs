using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] Cell CellPrefab;
    [SerializeField] GameObject finishPrefab;
    [SerializeField] HintRenderer hintRenderer;
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Vector3 startPos = new Vector3(0.5f, 0.5f, 0);

    public Vector3 CellSize = new Vector3(1, 1, 0);
    public List<GameObject> cells = new List<GameObject>();
    public GameObject mazeContainer;
    public Maze maze;

    private int _score;
    private Vector3 finishPos;
    private FinishTriger finishTriger;

    private void Start()
    {
        SpawnMaze();

        _score = PlayerPrefs.GetInt("score");
        PlayerPrefs.SetInt("score", _score);
        scoreText.text = _score.ToString();
    }

    public void SpawnMaze()
    {
        player.transform.position = startPos;

        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        CreateCells();

        CreateFinishTrigger();
    }

    private void CreateCells()
    {
        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity, mazeContainer.transform);
                cells.Add(c.gameObject);

                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }
    }

    private void CreateFinishTrigger()
    {
        finishPos = MazeGenerator.pos;
        SmoothingFinishPosition();

        GameObject cloneFinishPrefab = Instantiate(finishPrefab, finishPos, Quaternion.identity, mazeContainer.transform);
        finishTriger = cloneFinishPrefab.GetComponent<FinishTriger>();
        finishTriger.OnFinished += GenerateNewLevel;

        cells.Add(cloneFinishPrefab);
    }

    private void SmoothingFinishPosition()
    {
        if (finishPos.y == 8) //Top offset
        {
            if (finishPos.x == 0)
            {
                finishPos.x -= 0.5f;
                finishPos.y += 0.5f;
            }
            else
            {
                finishPos.x += 0.5f;
                finishPos.y += 1.5f;
            }
        }
        else if (finishPos.y == 0) //Bottom offset
        {
            if (finishPos.x == 8)
            {
                finishPos.x += 0.5f;
                finishPos.y -= 0.5f;
            }
            else if (finishPos.x == 1)
            {
                finishPos.x += 0.5f;
                finishPos.y -= 0.5f;
            }
            else
            {
                finishPos.x += 0.5f;
                finishPos.y -= 0.5f;
            }
        }
        else if (finishPos.x == 8) //Right offset
        {
            if (finishPos.y == 8)
            {
                finishPos.x += 1.5f;
                finishPos.y += 0.5f;
            }
            else
            {
                finishPos.x += 1.5f;
                finishPos.y += 0.5f;
            }
        }
        else if (finishPos.x == 0) //Left offset
        {
            if (finishPos.y == 0)
            {
                finishPos.x -= 1.5f;
                finishPos.y -= 0.5f;
            }
            else
            {
                finishPos.x -= 0.5f;
                finishPos.y += 0.5f;
            }
        }
    }

    private void GenerateNewLevel()
    {
        finishTriger.OnFinished -= GenerateNewLevel;

        DestroyMaze();

        PlayerPrefs.SetInt("score", _score += 1);
        _score = PlayerPrefs.GetInt("score");
        scoreText.text = _score.ToString();

        hintRenderer.gameObject.SetActive(false);
        SpawnMaze();
    }

    private void DestroyMaze()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i] != null)
                Destroy(cells[i]);
        }
    }

    public void DrawPath()
    {
        hintRenderer.DrawPath();
    }
}