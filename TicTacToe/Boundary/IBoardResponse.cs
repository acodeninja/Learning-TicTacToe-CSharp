using TicTacToe.Domain;
using TicTacToe.Domain.BoardStatus;

namespace TicTacToe.Boundary
{
    public interface IBoardResponse
    {
        Board Board { get; set; }
        System.Exception Error { get; set; }
        IBoardStatus Status { get; set; }
    }
}