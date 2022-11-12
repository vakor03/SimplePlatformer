using System;
using System.Collections.Generic;
using Additional;
using Mazes;
using Unity.VisualScripting;

namespace PathfindingAlgorithms
{
    public interface IPathFindingAlgorithm
    {
        event EventHandler<TileCheckedEventArgs> TileChecked;
        public int FindPath(Maze maze, Coordinates startPoint, Coordinates destPoint, out List<Coordinates> path);
    }
}