using TicTacToe.Domain;

namespace TicTacToe.Boundary
{
    public interface IBoardResponse
    {
        Board Board { get; set; }
        System.Exception Error { get; set; }
    }
}