using TicTacToe.Domain;

namespace TicTacToe.Gateway
{
    public class BoardGateway : IBoardGateway
    {
        public Board Fetch()
        {
            return new Board();
        }

        public string[] Read(Board board)
        {
            return board.Grid;
        }
    }
}