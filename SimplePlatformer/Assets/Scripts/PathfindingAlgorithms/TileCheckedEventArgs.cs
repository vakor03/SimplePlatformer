using System;
using Additional;

namespace PathfindingAlgorithms
{
    public class TileCheckedEventArgs : EventArgs
    {
        public TileCheckedEventArgs(Coordinates coordinate, string text)
        {
            Coordinate = coordinate;
            Text = text;
        }

        public Coordinates Coordinate { get; }
        public string Text { get; }
    }
}