using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Domain;
using TicTacToe.Gateway;

namespace TicTacToe.Test.Gateway
{
    public class JsonBoardGatewayTests
    {
        private JsonBoardGateway _gateway;

        private void ExpectANewBoard(Board board)
        {
            board.Should().BeEquivalentTo(new Board());
        }

        private void ExpectAnEmptyGrid(string[] grid)
        {
            grid.Should().BeEquivalentTo(new string[9]);
        }

        [SetUp]
        public void SetUp()
        {
            _gateway = new JsonBoardGateway();
        }

        [Test]
        public void CanCreateANewGame()
        {
            ExpectANewBoard(_gateway.New());
        }

        [Test]
        public void CanReadAFetchedEmptyBoard()
        {
            Board board = _gateway.Fetch();

            string[] grid = _gateway.Read(board);

            ExpectAnEmptyGrid(grid);
        }
    }
}