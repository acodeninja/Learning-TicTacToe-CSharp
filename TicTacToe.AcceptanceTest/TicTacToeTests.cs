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
}