using TicTacToe.Domain;

namespace TicTacToe.Boundary
{
    public class PlaceTokenResponse : IBoardResponse
    {
        public Board Board { get; set; }
    }
}