using TicTacToe.Domain;
using TicTacToe.Gateway;

namespace TicTacToe.Test.Gateway
{
    public class TestBoardGateway : IBoardGateway
    {
        private Board _board;

        public TestBoardGateway()
        {
            _board = new Board();
        }

        public Board Fetch()
        {
            return _board;
        }

        public string[] Read(Board board)
        {
            return board.Grid;
        }

        public string Read(Board board, int column, int row)
        {
            return board.Grid[3 * (column - 1) + (row - 1)];
        }

        public Board Flush(Board board)
        {
            return board;
        }

        public Board New()
        {
            _board = new Board();

            return _board;
        }

        public Board Write(Board board, string type, int column, int row)
        {
            board.Grid[3 * (column - 1) + (row - 1)] = type;

            return board;
        }
    }
}