using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Gateway;
using TicTacToe.UseCase;

namespace TicTacToe.Test.UseCase
{
    public class PlaceTokenTests : IBoardGateway
    {
        private Board _board;
        private PlaceToken _placeToken;

        public Board Fetch()
        {
            return new Board();
        }

        public string[] Read(Board board)
        {
            return board.Grid;
        }
        
        public Board Flush(Board board)
        {
            _board = board;
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

        private PlaceTokenResponse Execute(PlaceTokenRequest request)
        {
            return _placeToken.Execute(request);
        }

        private static void ExpectABoardResponseWithTheFirstSquareContainingX(PlaceTokenResponse response)
        {
            response.Should().BeEquivalentTo(new NewGameResponse
            {
                Board = new Board
                {
                    Grid = new string[] {"X", null, null, null, null, null, null, null, null}
                }
            });
        }
        
        private static void ExpectABoardResponseWithTheLastSquareContainingX(PlaceTokenResponse response)
        {
            response.Should().BeEquivalentTo(new NewGameResponse
            {
                Board = new Board
                {
                    Grid = new string[] {null, null, null, null, null, null, null, null, "X"}
                }
            });
        }

        [SetUp]
        public void SetUp()
        {
            _board = new Board();
            _placeToken = new PlaceToken(this);
        }

        [Test]
        public void CanPlaceATokenOnTheFirstSquareOfABoard()
        {
            PlaceTokenResponse response = Execute(new PlaceTokenRequest
            {
                Board = New(),
                Type = "X",
                Column = 1,
                Row = 1,
            });

            ExpectABoardResponseWithTheFirstSquareContainingX(response);
        }
        
        [Test]
        public void CanPlaceATokenOnTheLastSquareOfABoard()
        {
            PlaceTokenResponse response = Execute(new PlaceTokenRequest
            {
                Board = New(),
                Type = "X",
                Column = 3,
                Row = 3,
            });

            ExpectABoardResponseWithTheLastSquareContainingX(response);
        }
    }
}