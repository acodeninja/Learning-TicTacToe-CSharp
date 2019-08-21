using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Exception;
using TicTacToe.Test.Gateway;
using TicTacToe.UseCase;

namespace TicTacToe.Test.UseCase
{
    public class PlaceTokenTests : TestBoardGateway
    {
        private PlaceToken _placeToken;

        private PlaceTokenResponse Execute(PlaceTokenRequest request)
        {
            return _placeToken.Execute(request);
        }

        private static void ExpectTheBoardToHaveAAt(PlaceTokenResponse response, string type, int index)
        {
            string[] grid = new string[9];

            grid[index] = type;

            response.Should().BeEquivalentTo(new NewGameResponse
            {
                Board = new Board
                {
                    Grid = grid
                }
            });
        }

        private static void ExpectAnEmptyBoard(PlaceTokenResponse response)
        {
            response.Should().BeEquivalentTo(new PlaceTokenResponse
            {
                Board = new Board
                {
                    Grid = new string[9],
                }
            });
        }

        private static void ExpectAnEmptyBoardWithOffBoardError(PlaceTokenResponse response)
        {
            response.Should().BeEquivalentTo(new PlaceTokenResponse
            {
                Board = new Board
                {
                    Grid = new string[9],
                },
                Error = new OffBoardException(),
            });
        }

        private static void ExpectAResponseWithAGivenGridAndError(
            PlaceTokenResponse response,
            string[] expectedGrid,
            System.Exception expectedError
        )
        {
            response.Should().BeEquivalentTo(new PlaceTokenResponse
            {
                Board = new Board
                {
                    Grid = expectedGrid,
                },
                Error = expectedError,
            });
        }

        [SetUp]
        public void SetUp()
        {
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

            ExpectTheBoardToHaveAAt(response, "X", 0);
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

            ExpectTheBoardToHaveAAt(response, "X", 8);
        }

        [Test]
        public void CannotPlaceATokenOnASquareOutsideOfTheBoard()
        {
            PlaceTokenResponse response = Execute(new PlaceTokenRequest
            {
                Board = New(),
                Type = "X",
                Column = 4,
                Row = 4,
            });

            ExpectAResponseWithAGivenGridAndError(response, new string[9], new OffBoardException());
        }

        [Test]
        public void CannotPlaceATokenOnASquareThatAlreadyHasATokenOnIt()
        {
            Board board = New();

            PlaceTokenResponse placeXResponse = Execute(new PlaceTokenRequest
            {
                Board = board,
                Type = "X",
                Column = 1,
                Row = 1,
            });

            PlaceTokenResponse placeYResponse = Execute(new PlaceTokenRequest
            {
                Board = placeXResponse.Board,
                Type = "O",
                Column = 1,
                Row = 1,
            });

            ExpectAResponseWithAGivenGridAndError(
                placeYResponse,
                new string[9] {"X", null, null, null, null, null, null, null, null},
                new AlreadyPlacedException()
            );
        }
    }
}