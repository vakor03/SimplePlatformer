using System.Collections.Generic;
using Additional;
using Mazes;

namespace PathfindingAlgorithms
{
    public interface IPathFindingAlgorithm
    {
        public int FindPath(Maze maze, Coordinates startPoint, Coordinates destPoint, out List<Coordinates> path);
    }
}