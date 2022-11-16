using System;
using System.Collections.Generic;
using Additional;
using Mazes;
using PathfindingAlgorithms;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
    public GameObject tilePrefab;
    public float distanceBtwTiles;
    public DropdownHandler dropdownHandler;
    public double delay;
    private Maze _maze;
    private Tile[,] _tiles;
    private IPathFindingAlgorithm _pathFindingAlgorithm;
    private GameObject _tilesWrapper;

    private Coordinates _startCoordinates;
    private Coordinates _finishCoordinates;

    private bool _gameReady = false;


    private void Awake()
    {
    }

    void Start()
    {
        dropdownHandler.InitDropdown(new List<string> { AStarAlgorithm.Name, LeeAlgorithm.Name }, OnDropdownSelected);
        _maze = Maze.GenerateDefaultMaze();
        ResetCoordinates();
        _tiles = new Tile[_maze.Height, _maze.Width];
        _tilesWrapper = new GameObject("TilesWrapper");

        InitAlgorithm(AStarAlgorithm.GetInstance);
        InitAlgorithm(LeeAlgorithm.GetInstance);
        InstantiateField();

        _gameReady = true;
        SpawnEnemy();
    }

    private void InitAlgorithm(IPathFindingAlgorithm pathFindingAlgorithm)
    {
        pathFindingAlgorithm.TileChecked += (a, b) =>
        {
            _tiles[b.Coordinate.X, b.Coordinate.Y].TileText.text += b.Text;
        };
    }

    private void OnDropdownSelected(TMP_Dropdown dropdown)
    {
        //TODO: create Singleton for algorithms
        string a = dropdown.options[dropdown.value].text;
        string aStar = AStarAlgorithm.Name;
        string lee = LeeAlgorithm.Name;
        switch (dropdown.options[dropdown.value].text.TrimStart())
        {
            case AStarAlgorithm.Name:
                SetPathfindingAlgorithm(AStarAlgorithm.GetInstance);
                break;
            case LeeAlgorithm.Name:
                SetPathfindingAlgorithm(LeeAlgorithm.GetInstance);
                break;
            default:
                throw new ArgumentException();
        }
    }

    private void SetPathfindingAlgorithm(IPathFindingAlgorithm pathFindingAlgorithm)
    {
        _pathFindingAlgorithm = pathFindingAlgorithm;
        if (_gameReady)
        {
            ResetAllTiles();
            _pathFindingAlgorithm.FindPath(_maze, _startCoordinates, _finishCoordinates, out List<Coordinates> path);
            DrawPath(path);
        }
    }

    private void InstantiateField()
    {
        for (int i = 0; i < _maze.Height; i++)
        {
            for (int j = 0; j < _maze.Width; j++)
            {
                if (_maze[i, j] == 1)
                {
                    GameObject tileSquare = Instantiate(tilePrefab,
                        new Vector3(distanceBtwTiles * i, 0, distanceBtwTiles * j),
                        Quaternion.identity, _tilesWrapper.transform);
                    _tiles[i, j] = tileSquare.GetComponent<Tile>();
                    _tiles[i, j].TileText.text = "";
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        while (!_maze.CheckCoordinatesForValid(_startCoordinates))
        {
            _startCoordinates.X = Random.Range(0, _maze.Height);
            _startCoordinates.Y = Random.Range(0, _maze.Width);
        }

        while (!_maze.CheckCoordinatesForValid(_finishCoordinates) || _startCoordinates.Equals(_finishCoordinates))
        {
            _finishCoordinates.X = Random.Range(0, _maze.Height);
            _finishCoordinates.Y = Random.Range(0, _maze.Width);
        }

        _pathFindingAlgorithm.FindPath(_maze, _startCoordinates, _finishCoordinates, out List<Coordinates> path);

        DrawPath(path);
    }

    private void DrawPath(List<Coordinates> path)
    {
        _tiles[_startCoordinates.X, _startCoordinates.Y].ChangeColor(Color.green, true);
        _tiles[_finishCoordinates.X, _finishCoordinates.Y].ChangeColor(Color.magenta, true);
        foreach (var coord in path)
        {
            _tiles[coord.X, coord.Y].ChangeColor(Color.yellow, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Restart()
    {
        ResetAllTiles();
        ResetCoordinates();
        SpawnEnemy();
    }

    private void ResetAllTiles()
    {
        foreach (var tile in _tiles)
        {
            if (tile != null)
            {
                tile.Reset();
            }
        }
    }

    private void ResetCoordinates()
    {
        _startCoordinates = Coordinates.Default;
        _finishCoordinates = Coordinates.Default;
    }
}