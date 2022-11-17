using System;
using System.Collections.Generic;
using Additional;
using Mazes;
using PathfindingAlgorithms;
using TMPro;
using UnityEngine;


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

    private bool _gameReady;

    void Start()
    {
        _maze = Maze.GenerateDefaultMaze();

        _tiles = new Tile[_maze.Height, _maze.Width];
        _tilesWrapper = new GameObject("TilesWrapper");


        InstantiateField();
        ResetCoordinates();
        dropdownHandler.InitDropdown(new List<string> {AStarAlgorithm.Name, LeeAlgorithm.Name}, OnDropdownSelected);

        InitAlgorithm(AStarAlgorithm.GetInstance);
        InitAlgorithm(LeeAlgorithm.GetInstance);
    }

    private void InitAlgorithm(IPathFindingAlgorithm pathFindingAlgorithm)
    {
        pathFindingAlgorithm.TileChecked += (_, b) =>
        {
            _tiles[b.Coordinate.X, b.Coordinate.Y].TileText.text += b.Text;
        };
    }

    private void OnDropdownSelected(TMP_Dropdown dropdown)
    {
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
        RedrawPath();
    }

    private void RedrawPath()
    {
        ResetAllTiles();
        if (_gameReady)
        {
            int length = _pathFindingAlgorithm.FindPath(_maze, _startCoordinates, _finishCoordinates,
                out List<Coordinates> path);
            if (length == -1)
            {
                _tiles[_startCoordinates.X, _startCoordinates.Y].ChangeColor(Color.red, true);
                _tiles[_finishCoordinates.X, _finishCoordinates.Y].ChangeColor(Color.red, true);
            }
            else
            {
                _tiles[_startCoordinates.X, _startCoordinates.Y].ChangeColor(Color.green, true);
                _tiles[_finishCoordinates.X, _finishCoordinates.Y].ChangeColor(Color.magenta, true);
                foreach (var coord in path)
                {
                    _tiles[coord.X, coord.Y].ChangeColor(Color.yellow, true);
                }
            }
        }
        else
        {
            if (!_startCoordinates.Equals(Coordinates.Default))
            {
                _tiles[_startCoordinates.X, _startCoordinates.Y].ChangeColor(Color.green, true);
            }
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
                    _tiles[i, j].OnMouseDownEvent += OnTileMouseDown;
                    _tiles[i, j].Coordinates = new Coordinates(i, j);
                }
            }
        }
    }

    private void OnTileMouseDown(Tile tile)
    {
        if (_gameReady)
        {
            if (tile.Coordinates.Equals(_startCoordinates))
            {
                _startCoordinates = _finishCoordinates;
                _finishCoordinates = Coordinates.Default;
                _gameReady = false;
                RedrawPath();
            }
            else if (tile.Coordinates.Equals(_finishCoordinates))
            {
                _finishCoordinates = Coordinates.Default;
                _gameReady = false;
                RedrawPath();
            }

            return;
        }

        if (_startCoordinates.Equals(Coordinates.Default))
        {
            _startCoordinates = tile.Coordinates;
        }
        else if (!_startCoordinates.Equals(tile.Coordinates))
        {
            _finishCoordinates = tile.Coordinates;
            _gameReady = true;
        }
        else
        {
            _startCoordinates = Coordinates.Default;
        }

        RedrawPath();
    }

    public void Restart()
    {
        ResetAllTiles();
        ResetCoordinates();
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