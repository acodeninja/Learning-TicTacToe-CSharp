using TicTacToe.Domain;

namespace TicTacToe.Boundary
{
    public class PlaceTokenRequest
    {
        public Board Board { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public string Type { get; set; }
    }
}