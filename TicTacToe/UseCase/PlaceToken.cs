using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Gateway;

namespace TicTacToe.UseCase
{
    public class PlaceToken
    {
        private IBoardGateway _boardGateway;

        public PlaceToken(IBoardGateway boardGateway)
        {
            _boardGateway = boardGateway;
        }

        public PlaceTokenResponse Execute(PlaceTokenRequest request)
        {
            Board board = _boardGateway.Write(request.Board, request.Type, request.Column, request.Row);
            _boardGateway.Flush(board);
            
            return new PlaceTokenResponse
            {
                Board = board
            };
        }
    }
}