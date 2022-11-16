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
        string Name { get; }
        public int FindPath(Maze maze, Coordinates startPoint, Coordinates destPoint, out List<Coordinates> path);
    }
}