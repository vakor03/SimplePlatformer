using System;
using Additional;

namespace PathfindingAlgorithms
{
    public class TileCheckedEventArgs : EventArgs
    {
        public TileCheckedEventArgs(Coordinates coordinate, double number)
        {
            Coordinate = coordinate;
            Number = number;
        }

        public Coordinates Coordinate { get; }
        public double Number { get; }
    }
}