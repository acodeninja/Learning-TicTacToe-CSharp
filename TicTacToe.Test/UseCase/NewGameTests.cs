using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Gateway;
using TicTacToe.UseCase;

namespace TicTacToe.Test.UseCase
{
    public class NewGameTests : IBoardGateway
    {
        private Board _board;
        private NewGame _newGame;

        public Board Fetch()
        {
            return _board;
        }

        public string[] Read(Board board)
        {
            return board.Grid;
        }

        private NewGameResponse Execute(NewGameRequest request)
        {
            return _newGame.Execute(request);
        }

        private static void ExpectABoardResponse(NewGameResponse response)
        {
            response.Should().BeEquivalentTo(new NewGameResponse
            {
                Board = new Board()
            });
        }

        [SetUp]
        public void SetUp()
        {
            _board = new Board();
            _newGame = new NewGame(this);
        }

        [Test]
        public void CanCreateANewGame()
        {
            NewGameResponse response = Execute(new NewGameRequest());

            ExpectABoardResponse(response);
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
            return board;
        }
    }
}