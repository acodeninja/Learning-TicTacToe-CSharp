using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Domain.BoardStatus;
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
            Board board = _boardReader.Fetch();

            return new ViewBoardResponse
            {
                Board = board,
                Status = board.IsComplete() ? (IBoardStatus) new Complete() : new Incomplete()
            };
        }
    }
}