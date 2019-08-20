using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Gateway;
using TicTacToe.UseCase;

namespace TicTacToe.AcceptanceTest
{
    public class Tests
    {
        [Test]
        public void GivenANewGameIShouldHaveAnEmptyBoard()
        {
            ViewBoardRequest request = new ViewBoardRequest();

            ViewBoard viewBoard = new ViewBoard(new BoardGateway());

            ViewBoardResponse response = viewBoard.Execute(request);

            response.Should().BeEquivalentTo(new ViewBoardResponse
            {
                Board = new Board()
            });
        }
    }

    public class BoardGateway : IBoardGateway
    {
        public Board Fetch()
        {
            return new Board();
        }

        public string[] Read(Board board)
        {
            return board.Grid;
        }
    }
}