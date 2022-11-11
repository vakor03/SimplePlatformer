namespace MinimaxAlgorithms
{
    public interface IMinimax
    {
        int Minimax(Node currentPosition, int depth, bool maximizingPlayer);
    }
}