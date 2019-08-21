using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Domain;
using TicTacToe.Gateway;

namespace TicTacToe.Test.Gateway
{
    public class TestBoardGatewayTests
    {
        private TestBoardGateway _gateway;

        private void ExpectANewBoard(Board board)
        {
            board.Should().BeEquivalentTo(new Board());
        }

        private void ExpectAnEmptyGrid(string[] grid)
        {
            grid.Should().BeEquivalentTo(new string[9]);
        }
        
        private void ExpectAGivenGrid(string[] grid, string[] expected)
        {
            grid.Should().BeEquivalentTo(expected);
        }
        
        [SetUp]
        public void SetUp()
        {
            _gateway = new TestBoardGateway();
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
        
        [Test]
        public void CanWriteToASquareInTheGrid()
        {
            Board board = _gateway.New();

            board = _gateway.Write(board, "X", 1, 1);

            string[] grid = _gateway.Read(board);

            ExpectAGivenGrid(grid, new string[9] {"X", null, null, null, null, null, null, null, null});
        }

        [Test]
        public void CanReadFromASquareInTheGrid()
        {
            Board board = _gateway.New();

            board = _gateway.Write(board, "X", 1, 1);

            string square = _gateway.Read(board, 1, 1);

            square.Should().BeEquivalentTo("X");
        }
    }
}