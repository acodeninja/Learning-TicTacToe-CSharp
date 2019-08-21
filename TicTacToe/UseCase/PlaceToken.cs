using System;
using System.Linq;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Domain.BoardStatus;
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
            IBoardStatus status = new Incomplete();

            try
            {
                ValidateTokenType(request);
                ValidatePositionFree(request);
                ValidateBoardIncomplete(request);

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
            catch (BoardCompleteException)
            {
                error = new BoardCompleteException();
            }

            return new PlaceTokenResponse
            {
                Board = board,
                Error = error,
                Status = board.IsComplete() ? (IBoardStatus) new Complete() : new Incomplete(),
            };
        }

        private static void ValidateBoardIncomplete(PlaceTokenRequest request)
        {
            if (request.Board.IsComplete())
            {
                throw new BoardCompleteException();
            }
        }

        private void ValidatePositionFree(PlaceTokenRequest request)
        {
            if (_boardGateway.Read(request.Board, request.Column, request.Row) != null)
            {
                throw new AlreadyPlacedException();
            }
        }

        private static void ValidateTokenType(PlaceTokenRequest request)
        {
            if (!new string[] {"X", "O"}.Contains(request.Type))
            {
                throw new InvalidTokenException();
            }
        }
    }
}