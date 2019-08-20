using TicTacToe.Domain;

namespace TicTacToe.Gateway
{
    public interface IBoardWriter
    {
        Board Flush(Board board);
        Board New();
        Board Write(Board requestBoard, string requestType, int requestColumn, int requestRow);
    }
}