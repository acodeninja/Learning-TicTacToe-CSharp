using TicTacToe.Domain;

namespace TicTacToe.Gateway
{
    public interface IBoardWriter
    {
        Board Flush(Board board);
        Board New();
        Board Write(Board board, string type, int column, int row);
    }
}