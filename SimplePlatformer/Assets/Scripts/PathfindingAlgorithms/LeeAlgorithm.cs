﻿using System;
using System.Collections.Generic;
using Additional;
using Mazes;

namespace PathfindingAlgorithms
{
    public class LeeAlgorithm : IPathFindingAlgorithm
    {
        public static LeeAlgorithm GetInstance { get; } = new();
        public const string Name = "Lee algorithm";
        private bool[,] _visitedNodes;

        private LeeAlgorithm()
        {
        }

        static readonly int[] RowNum = { -1, 0, 0, 1 };
        static readonly int[] ColNum = { 0, -1, 1, 0 };

        public event EventHandler<TileCheckedEventArgs> TileChecked;

        protected virtual void OnTileChecked(TileCheckedEventArgs e)
        {
            TileChecked?.Invoke(this,e);
        }

        public int FindPath(Maze maze, Coordinates startPoint, Coordinates destPoint, out List<Coordinates> path)
        {
            path = null!;
            if (maze[startPoint] != 1 || maze[destPoint] != 1)
            {
                return -1;
            }

            _visitedNodes = new bool[maze.Height, maze.Width];
            _visitedNodes[startPoint.X, startPoint.Y] = true;

            Queue<QueueNode> nodeQueue = new();
            nodeQueue.Enqueue(new QueueNode(startPoint, 0, null!));

            while (nodeQueue.Count != 0)
            {
                QueueNode current = nodeQueue.Dequeue();
                Coordinates coordinates = current.Coordinates;

                if (coordinates.X == destPoint.X && coordinates.Y == destPoint.Y)
                {
                    path = RestorePath(current);
                    return current.Distance;
                }

                AddAdjNodesToQueue(nodeQueue,current,maze);
            }

            return -1;
        }
    
        private void AddAdjNodesToQueue(Queue<QueueNode> nodeQueue, QueueNode current, Maze maze)
        {
            for (int i = 0; i < 4; i++)
            {
                Coordinates adjCoordinates =
                    new Coordinates(current.Coordinates.X + RowNum[i], current.Coordinates.Y + ColNum[i]);
                if (maze.CheckCoordinatesForValid(adjCoordinates) && maze[adjCoordinates] == 1 &&
                    !_visitedNodes[adjCoordinates.X, adjCoordinates.Y])
                {
                    _visitedNodes[adjCoordinates.X, adjCoordinates.Y] = true;
                    QueueNode node = new QueueNode(adjCoordinates, current.Distance + 1, current);
                    nodeQueue.Enqueue(node);
                    OnTileChecked(new TileCheckedEventArgs(node.Coordinates,node.Distance.ToString()));
                }
            }
        }

        private List<Coordinates> RestorePath(QueueNode currentNode)
        {
            List<Coordinates> path = new List<Coordinates>();

            var curNode = currentNode;
            path.Add(curNode.Coordinates);

            while (curNode.Distance != 0)
            {
                curNode = curNode.PreviousNode;
                path.Add(curNode.Coordinates);
            }

            path.Reverse();
            return path;
        }
    }

    public class QueueNode
    {
        public QueueNode PreviousNode { get; }
        public Coordinates Coordinates { get; }
        public int Distance { get; }

        public QueueNode(Coordinates coordinates, int distance, QueueNode previousNode)
        {
            Coordinates = coordinates;
            Distance = distance;
            PreviousNode = previousNode;
        }
    }
}