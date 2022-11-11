using System;
using System.Collections.Generic;
using Additional;
using Extensions;
using Mazes;

namespace Games
{
    public class Game
    {
        public int EnemiesCount { get; private set; }
        public Maze Maze { get; set; }
        public Coordinates DestinationPoint { get; set; }
        public Coordinates AllyCoordinate { get; set; }
        public Coordinates[] EnemyCoordinates { get; set; }
        public bool IsFinished => AllyCoordinate.Equals(DestinationPoint);

        public Game(Maze maze, int enemiesCount)
        {
            Maze = maze;
            EnemiesCount = enemiesCount;
            EnemyCoordinates = new Coordinates[enemiesCount];
        }

        private Game(Maze maze, Coordinates destinationPoint, Coordinates allyCoordinate,
            Coordinates[] enemyCoordinates)
        {
            Maze = maze;
            DestinationPoint = destinationPoint;
            AllyCoordinate = allyCoordinate;
            EnemyCoordinates = enemyCoordinates;
        }

        public void InitNewGame()
        {
            List<Coordinates> usedCoordinates = new List<Coordinates>();
            usedCoordinates.Add(AllyCoordinate = Maze.GenerateRandomValidCoordinate(usedCoordinates));
            usedCoordinates.Add(DestinationPoint = Maze.GenerateRandomValidCoordinate(usedCoordinates));
            for (int i = 0; i < EnemiesCount; i++)
            {
                usedCoordinates.Add(EnemyCoordinates[i] = Maze.GenerateRandomValidCoordinate(usedCoordinates));
            }
        }

        public void MakeTurn()
        {
            Dictionary<ConsoleKey, Coordinates> possibleMoves = new Dictionary<ConsoleKey, Coordinates>()
            {
                { ConsoleKey.W, new Coordinates(-1, 0) },
                { ConsoleKey.A, new Coordinates(0, -1) },
                { ConsoleKey.S, new Coordinates(1, 0) },
                { ConsoleKey.D, new Coordinates(0, 1) },
            };
            ConsoleKey key;
            Coordinates newAllyCoords;
            while (!possibleMoves.ContainsKey(key = Console.ReadKey(true).Key)
                   || !Maze.CheckCoordinatesForValid(newAllyCoords = AllyCoordinate + possibleMoves[key]))
            {
            }

            AllyCoordinate = newAllyCoords;
        }

        public void MakeAiTurn()
        {
        }
    }
}