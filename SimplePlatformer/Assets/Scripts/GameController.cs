using System.Collections.Generic;
using Additional;
using Mazes;
using PathfindingAlgorithms;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile
{
    public GameObject TileSquare;
    public TMP_Text TileText;
}

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tile;
    public float distanceBtwTiles;
    public double delay;
    private Maze _maze;
    private Tile[,] _tiles;
    private IPathFindingAlgorithm _pathFindingAlgorithm;
    private GameObject _tilesWrapper;

    void Start()
    {
        _maze = Maze.GenerateDefaultMaze();
        _tiles = new Tile[_maze.Height, _maze.Width];
        _pathFindingAlgorithm = new AStarAlgorithm();
        _tilesWrapper = new GameObject("TilesWrapper");
        _pathFindingAlgorithm.TileChecked += (a, b) =>
        {
            _tiles[b.Coordinate.X, b.Coordinate.Y].TileText.text += b.Text;
        };
        InstantiateField();
        SpawnEnemy();
        //TODO: Try use events in pathfinding algorithms
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
                        Quaternion.identity, _tilesWrapper.transform);
                    _tiles[i, j] = new Tile
                    {
                        TileSquare = tileSquare,
                        TileText = tileSquare.GetComponentInChildren<TMP_Text>()
                    };
                    _tiles[i, j].TileText.text = "";
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

        _tiles[enemyCoordinates.X, enemyCoordinates.Y].TileText.text = "S";

        Coordinates enemyCoordinates1 = new Coordinates(-1, -1);
        while (!_maze.CheckCoordinatesForValid(enemyCoordinates1))
        {
            enemyCoordinates1 = new Coordinates(Random.Range(0, _maze.Height), Random.Range(0, _maze.Width));
        }

        _tiles[enemyCoordinates1.X, enemyCoordinates1.Y].TileText.text = "F";

        _pathFindingAlgorithm.FindPath(_maze, enemyCoordinates, enemyCoordinates1, out List<Coordinates> path);
        foreach (var coordinate in path)
        {
            _tiles[coordinate.X, coordinate.Y].TileText.text += "X";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}