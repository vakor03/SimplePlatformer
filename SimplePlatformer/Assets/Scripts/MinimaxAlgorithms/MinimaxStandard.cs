using System;

namespace MinimaxAlgorithms
{
    public class MinimaxStandard : IMinimax
    {
        private int _maxDepth = 4;

        public MinimaxStandard(int maxDepth)
        {
            _maxDepth = maxDepth;
        }

        public MinimaxStandard()
        {
        }

        public int Minimax(Node currentPosition, int depth, bool maximizingPlayer)
        {
            if (depth == 0 || currentPosition.GameIsOver)
            {
                return currentPosition.GetEvaluation();
            }

            if (maximizingPlayer)
            {
                int maxEvaluation = Int32.MinValue;
                foreach (var child in currentPosition.GetChildren(maximizingPlayer))
                {
                    int evaluation = Minimax(child, depth - 1, false);
                    maxEvaluation = Math.Max(maxEvaluation, evaluation);
                }

                return maxEvaluation;
            }
            else
            {
                int minEvaluation = Int32.MinValue;
                foreach (var child in currentPosition.GetChildren(maximizingPlayer))
                {
                    int evaluation = Minimax(child, depth - 1, false);
                    minEvaluation = Math.Min(minEvaluation, evaluation);
                }

                return minEvaluation;
            }
        }
    }
}