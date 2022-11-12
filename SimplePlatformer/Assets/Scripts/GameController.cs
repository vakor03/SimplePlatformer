using System;
using System.Collections.Generic;
using Additional;
using Games;
using Mazes;
using PathfindingAlgorithms;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile
{
    public GameObject tileSquare;
    public TMP_Text tileText;
}

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tile;
    public float distanceBtwTiles;
    private Maze _maze;
    private Tile[,] _tiles;
    private IPathFindingAlgorithm _pathFindingAlgorithm;

    void Start()
    {
        _maze = Maze.GenerateDefaultMaze();
        _tiles = new Tile[_maze.Height, _maze.Width];
        _pathFindingAlgorithm = new LeeAlgorithm();
        InstantiateField();
        SpawnEnemy();
    }

    private void InstantiateField()
    {
        for (int i = 0; i < _maze.Height; i++)
        {
            for (int j = 0; j < _maze.Width; j++)
            {
                if (_maze[i, j] == 1)
                {
                    GameObject tileSquare = Instantiate(tile,
                        new Vector3(distanceBtwTiles * i, 0, distanceBtwTiles * j),
                        Quaternion.identity);
                    _tiles[i, j] = new Tile
                    {
                        tileSquare = tileSquare,
                        tileText = tileSquare.GetComponentInChildren<TMP_Text>()
                    };
                    _tiles[i, j].tileText.text = "";
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        Coordinates enemyCoordinates = new Coordinates(-1, -1);
        while (!_maze.CheckCoordinatesForValid(enemyCoordinates))
        {
            enemyCoordinates = new Coordinates(Random.Range(0, _maze.Height), Random.Range(0, _maze.Width));
        }

        _tiles[enemyCoordinates.X, enemyCoordinates.Y].tileText.text = "S";

        Coordinates enemyCoordinates1 = new Coordinates(-1, -1);
        while (!_maze.CheckCoordinatesForValid(enemyCoordinates1))
        {
            enemyCoordinates1 = new Coordinates(Random.Range(0, _maze.Height), Random.Range(0, _maze.Width));
        }

        _tiles[enemyCoordinates1.X, enemyCoordinates1.Y].tileText.text = "F";

        _pathFindingAlgorithm.FindPath(_maze, enemyCoordinates, enemyCoordinates1, out List<Coordinates> path);
        foreach (var coordinate in path)
        {
            _tiles[coordinate.X, coordinate.Y].tileText.text += "X";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}