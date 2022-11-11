using System;
using Games;
using Mazes;
using TMPro;
using UnityEngine;

[Serializable]
public class Tile
{
    public GameObject tileSquare;
    public TMP_Text tileText;
}

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public Tile tile;
    public float distanceBtwTiles;
    private Maze _maze;
    private Tile[,] _tiles;
    
    void Start()
    {
        _maze = Maze.GenerateDefaultMaze();
        _tiles = new Tile[_maze.Height,_maze.Width];
        InstantiateField();
    }

    private void InstantiateField()
    {
        
        for (int i = 0; i < _maze.Height; i++)
        {
            for (int j = 0; j < _maze.Width; j++)
            {
                if (_maze[i,j] == 1)
                {
                    _tiles[i, j] = new Tile
                    {
                        tileSquare = Instantiate(tile.tileSquare, new Vector3(distanceBtwTiles * i, 0, distanceBtwTiles * j),
                            Quaternion.identity),
                        tileText = (TMP_Text)Instantiate(tile.tileText, new Vector3(distanceBtwTiles * i, 0, distanceBtwTiles * j),
                            Quaternion.identity).GetComponent(typeof(TMP_Text))
                    };
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
