using System;

namespace MinimaxAlgorithms
{
    public class MinimaxAlphaBetaPruning : IMinimax
    {
        public int Minimax(Node currentPosition, int depth, bool maximizingPlayer)
        {
            return Minimax(currentPosition, depth, Int32.MinValue, Int32.MaxValue, maximizingPlayer);
        }

        private int Minimax(Node currentPosition, int depth, int alpha, int beta, bool maximizingPlayer)
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
                    int evaluation = Minimax(child, depth - 1, alpha, beta, false);
                    maxEvaluation = Math.Max(maxEvaluation, evaluation);
                    alpha = Math.Max(alpha, evaluation);
                    if (beta >= alpha)
                    {
                        break;
                    }
                }

                return maxEvaluation;
            }
            else
            {
                int minEvaluation = Int32.MinValue;
                foreach (var child in currentPosition.GetChildren(maximizingPlayer))
                {
                    int evaluation = Minimax(child, depth - 1, alpha, beta, false);
                    minEvaluation = Math.Min(minEvaluation, evaluation);
                    beta = Math.Min(beta, evaluation);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }


                return minEvaluation;
            }
        }
    }
}