using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Gateway;

namespace TicTacToe.UseCase
{
    public class NewGame
    {
        private IBoardWriter _boardWriter;

        public NewGame(IBoardWriter boardWriter)
        {
            _boardWriter = boardWriter;
        }

        public NewGameResponse Execute(NewGameRequest request)
        {
            Board board = _boardWriter.New();
            
            return new NewGameResponse
            {
                Board = board
            };
        }
    }
}