using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Domain.BoardStatus;
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
                },
                Status = new Incomplete(),
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
                Status = new Incomplete(),
            });
        }
        
        private static void ExpectACompleteBoard(PlaceTokenResponse response)
        {
            response.Status.Should().BeOfType<Complete>();
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

            PlaceTokenResponse placeOResponse = Execute(new PlaceTokenRequest
            {
                Board = placeXResponse.Board,
                Type = "O",
                Column = 1,
                Row = 1,
            });

            ExpectAResponseWithAGivenGridAndError(
                placeOResponse,
                new string[9] {"X", null, null, null, null, null, null, null, null},
                new AlreadyPlacedException()
            );
        }

        [Test]
        public void CannotPlaceAnInvalidToken()
        {
            Board board = New();

            PlaceTokenResponse response = Execute(new PlaceTokenRequest
            {
                Board = board,
                Type = "Y",
                Column = 1,
                Row = 1,
            });

            ExpectAResponseWithAGivenGridAndError(
                response,
                new string[9],
                new InvalidTokenException()
            );
        }

        [Test]
        public void CanCompleteAGameByPlacingMultipleTokens()
        {
            Board board = New();

            PlaceTokenResponse response = Execute(new PlaceTokenRequest
            {
                Board = board,
                Type = "X",
                Column = 1,
                Row = 1,
            });

            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "X",
                Column = 1,
                Row = 2,
            });

            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "X",
                Column = 1,
                Row = 3,
            });

            ExpectACompleteBoard(response);
        }
        
        [Test]
        public void CanCompleteAGameByPlacingMultipleTokensDiagonally()
        {
            Board board = New();

            PlaceTokenResponse response = Execute(new PlaceTokenRequest
            {
                Board = board,
                Type = "X",
                Column = 1,
                Row = 1,
            });

            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "X",
                Column = 2,
                Row = 2,
            });

            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "X",
                Column = 3,
                Row = 3,
            });

            ExpectACompleteBoard(response);
        }

        [Test]
        public void CannotPlaceATokenOnACompleteBoard()
        {
            Board board = New();

            PlaceTokenResponse response = Execute(new PlaceTokenRequest
            {
                Board = board,
                Type = "X",
                Column = 1,
                Row = 1,
            });

            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "X",
                Column = 1,
                Row = 2,
            });

            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "X",
                Column = 1,
                Row = 3,
            });
            
            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "X",
                Column = 2,
                Row = 2,
            });

            response.Error.Should().BeOfType<BoardCompleteException>();
        }
        
        [Test]
        public void CanCompleteAGameByPlacingMultipleOTokens()
        {
            Board board = New();

            PlaceTokenResponse response = Execute(new PlaceTokenRequest
            {
                Board = board,
                Type = "O",
                Column = 1,
                Row = 1,
            });

            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "O",
                Column = 1,
                Row = 2,
            });

            response = Execute(new PlaceTokenRequest
            {
                Board = response.Board,
                Type = "O",
                Column = 1,
                Row = 3,
            });

            ExpectACompleteBoard(response);
        }
    }
}