using System.Collections.Generic;
using System.Linq;
using Additional;
using Mazes;
using PathfindingAlgorithms;
using Vakor.MiniMaxAlgorithm.PathfindingAlgorithms;

namespace MinimaxAlgorithms
{
    public class Node
    {
        private static readonly int[] RowNum = { -1, 0, 0, 1 };
        private static readonly int[] ColNum = { 0, -1, 1, 0 };
        private const int K = 10;
        private const int P = 10;

        private Maze _maze;
        private Coordinates[] _enemyCoordinates;
        private Coordinates _allyCoordinates;
        private Coordinates _destinationPoint;
        private IPathFindingAlgorithm _pathFindingAlgorithm;

        public Node(Maze maze, Coordinates[] enemyCoordinates, Coordinates allyCoordinates,
            Coordinates destinationPoint, IPathFindingAlgorithm pathFindingAlgorithm)
        {
            _maze = maze;
            _enemyCoordinates = enemyCoordinates;
            _allyCoordinates = allyCoordinates;
            _destinationPoint = destinationPoint;
            _pathFindingAlgorithm = pathFindingAlgorithm;
        }

        public bool GameIsOver =>
            _allyCoordinates.Equals(_destinationPoint) ||
            _enemyCoordinates.Any(coord => coord.Equals(_allyCoordinates));

        public int GetEvaluation() =>
            _enemyCoordinates.Min(enemyCoord =>
                _pathFindingAlgorithm.FindPath(_maze, enemyCoord, _allyCoordinates, out _)) * K
            + _pathFindingAlgorithm.FindPath(_maze, _allyCoordinates, _destinationPoint, out _) * P;


        public IEnumerable<Node> GetChildren(bool allyTurn)
        {
            List<Coordinates> childCoordinates = new List<Coordinates>();
            Coordinates current = allyTurn ? _allyCoordinates : _enemyCoordinates[0];
            for (int i = 0; i < 4; i++)
            {
                Coordinates adjCoordinates =
                    new Coordinates(current.X + RowNum[i], current.Y + ColNum[i]);
                if (_maze.CheckCoordinatesForValid(adjCoordinates) && _maze[adjCoordinates] == 1)
                {
                    childCoordinates.Add(adjCoordinates);
                }
            }

            return childCoordinates.Select(coord => new Node(_maze, allyTurn ? _enemyCoordinates : new[] { coord },
                allyTurn ? coord : _allyCoordinates, _destinationPoint, _pathFindingAlgorithm));
        }
    }
}