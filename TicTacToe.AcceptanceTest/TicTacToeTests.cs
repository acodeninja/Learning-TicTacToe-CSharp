using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Domain.BoardStatus;
using TicTacToe.Gateway;
using TicTacToe.UseCase;

namespace TicTacToe.AcceptanceTest
{
    public class Tests
    {
        private NewGame _newGame;
        private PlaceToken _placeToken;
        private ViewBoard _viewBoard;

        private Board _board;

        private void GivenANewGame()
        {
            NewGameResponse response = _newGame.Execute(new NewGameRequest());
            _board = response.Board;
        }

        private PlaceTokenResponse GivenACompleteGame()
        {
            NewGameResponse response = _newGame.Execute(new NewGameRequest());
            _board = response.Board;

            WhenIPlaceAToken("X", 1, 1);
            WhenIPlaceAToken("X", 1, 2);
            return WhenIPlaceAToken("X", 1, 3);
        }

        private PlaceTokenResponse WhenIPlaceAToken(string type, int column, int row)
        {
            PlaceTokenRequest request = new PlaceTokenRequest
            {
                Board = _board,
                Row = row,
                Column = column,
                Type = type,
            };

            PlaceTokenResponse response = _placeToken.Execute(request);

            _board = response.Board;

            return response;
        }

        private void ExpectAnEmptyBoard()
        {
            ViewBoardRequest request = new ViewBoardRequest();
            ViewBoardResponse response = _viewBoard.Execute(request);

            response.Board.Should().BeEquivalentTo(new Board());
        }

        private void ExpectAGridWithXTokenAtFirstPosition()
        {
            ViewBoardRequest request = new ViewBoardRequest();
            ViewBoardResponse response = _viewBoard.Execute(request);

            response.Board.Grid.Should()
                .BeEquivalentTo(new string[] {"X", null, null, null, null, null, null, null, null});
        }

        private void ExpectAGridWithXTokenAtLastPosition()
        {
            ViewBoardRequest request = new ViewBoardRequest();
            ViewBoardResponse response = _viewBoard.Execute(request);

            response.Board.Grid.Should()
                .BeEquivalentTo(new string[] {null, null, null, null, null, null, null, null, "X"});
        }

        private static void ExpectACompleteBoard(PlaceTokenResponse response)
        {
            response.Status.Should().BeOfType<Complete>();
        }

        [SetUp]
        public void SetUp()
        {
            _placeToken = new PlaceToken(new JsonBoardGateway());
            _viewBoard = new ViewBoard(new JsonBoardGateway());
            _newGame = new NewGame(new JsonBoardGateway());
        }

        [Test]
        public void GivenANewGameThenIShouldHaveAnEmptyBoard()
        {
            GivenANewGame();
            ExpectAnEmptyBoard();
        }

        [Test]
        public void GivenANewGameWhenIPlaceAnXTokenInTheFirstSquareThenICanViewThatTokenOnTheBoard()
        {
            GivenANewGame();
            WhenIPlaceAToken("X", 1, 1);
            ExpectAGridWithXTokenAtFirstPosition();
        }

        [Test]
        public void GivenANewGameWhenIPlaceAnXTokenInTheLastSquareThenICanViewThatTokenOnTheBoard()
        {
            GivenANewGame();
            WhenIPlaceAToken("X", 3, 3);
            ExpectAGridWithXTokenAtLastPosition();
        }

        [Test]
        public void GivenANewGameWhenIPlaceATokenOutsideOfTheBoardThenIShouldHaveAnEmptyBoard()
        {
            GivenANewGame();
            WhenIPlaceAToken("X", 4, 4);
            ExpectAnEmptyBoard();
        }

        [Test]
        public void GivenANewGameWhenIPlaceATokenOnTheFirstSquareTwiceThenIShouldHaveABoardWithoutTheSecondToken()
        {
            GivenANewGame();
            WhenIPlaceAToken("X", 1, 1);
            WhenIPlaceAToken("O", 1, 1);
            ExpectAGridWithXTokenAtFirstPosition();
        }

        [Test]
        public void GivenANewGameWhenIPlaceAyTokenOnTheFirstSquareThenIShouldHaveAnEmptyBoard()
        {
            GivenANewGame();
            WhenIPlaceAToken("Y", 1, 1);
            ExpectAnEmptyBoard();
        }

        [Test]
        public void GivenANewGameWhenIPlaceAWinningSetOfTokensOnTheBoardThenIShouldHaveACompleteBoard()
        {
            GivenANewGame();
            WhenIPlaceAToken("X", 1, 1);
            WhenIPlaceAToken("X", 1, 2);
            PlaceTokenResponse response = WhenIPlaceAToken("X", 1, 3);

            ExpectACompleteBoard(response);
        }

        [Test]
        public void GivenACompleteGameWhenIPlaceATokenOnTheBoardThenTheStateOfTheBoardShouldNotChange()
        {
            GivenACompleteGame();
            PlaceTokenResponse response = WhenIPlaceAToken("X", 2, 2);

            response.Board.IsComplete().Should().BeTrue("The game is finished");
            response.Board.Grid[3 * 1 + 1].Should().BeNullOrEmpty("New tokens are not allowed");
        }
    }
}