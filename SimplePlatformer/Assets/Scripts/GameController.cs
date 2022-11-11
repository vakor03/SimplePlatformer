using Games;
using Mazes;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tile;
    public float distanceBtwTiles;
    private Maze _maze;
    
    void Start()
    {
        _maze = Maze.GenerateDefaultMaze();
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
                    Instantiate(tile, new Vector3(distanceBtwTiles * i, 0, distanceBtwTiles * j),
                        Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
