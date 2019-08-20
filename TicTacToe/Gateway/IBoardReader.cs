using TicTacToe.Domain;

namespace TicTacToe.Gateway
{
    public interface IBoardReader
    {
        Board Fetch();
        string[] Read(Board board);
    }
}