using TicTacToe.Domain;

namespace TicTacToe.Boundary
{
    public interface IBoardResponse
    {
        Board Board { get; set; }
    }
}