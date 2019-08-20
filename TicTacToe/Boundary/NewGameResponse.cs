using TicTacToe.Domain;

namespace TicTacToe.Boundary
{
    public class NewGameResponse : IBoardResponse
    {
        public Board Board { get; set; }
    }
}