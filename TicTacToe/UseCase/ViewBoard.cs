using TicTacToe.Boundary;
using TicTacToe.Gateway;

namespace TicTacToe.UseCase
{
    public class ViewBoard
    {
        private readonly IBoardReader _boardReader;

        public ViewBoard(IBoardReader boardReader)
        {
            _boardReader = boardReader;
        }

        public ViewBoardResponse Execute(ViewBoardRequest request)
        {
            return new ViewBoardResponse
            {
                Board = _boardReader.Fetch()
            };
        }
    }
}