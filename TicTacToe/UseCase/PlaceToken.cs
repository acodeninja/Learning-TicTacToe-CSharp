using System;
using System.Linq;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Exception;
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
            Board board = request.Board;
            System.Exception error = null;

            try
            {
                if (!new string[] {"X", "O"}.Contains(request.Type))
                {
                    throw new InvalidTokenException();
                }

                if (_boardGateway.Read(board, request.Column, request.Row) != null)
                {
                    throw new AlreadyPlacedException();
                }

                board = _boardGateway.Write(request.Board, request.Type, request.Column, request.Row);
                _boardGateway.Flush(board);
            }
            catch (IndexOutOfRangeException)
            {
                error = new OffBoardException();
            }
            catch (AlreadyPlacedException)
            {
                error = new AlreadyPlacedException();
            }
            catch (InvalidTokenException)
            {
                error = new InvalidTokenException();
            }
            
            return new PlaceTokenResponse
            {
                Board = board,
                Error = error,
            };
        }
    }
}