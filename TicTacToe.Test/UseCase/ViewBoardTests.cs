using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Gateway;
using TicTacToe.UseCase;

namespace TicTacToe.Test.UseCase
{
    public class ViewBoardTests : IBoardReader
    {
        private ViewBoard _viewBoard;

        public Board Fetch()
        {
            return new Board();
        }

        public string[] Read(Board board)
        {
            return board.Grid;
        }

        private ViewBoardResponse Execute(ViewBoardRequest request)
        {
            return _viewBoard.Execute(request);
        }

        private static void ExpectABoardResponse(ViewBoardResponse response)
        {
            response.Should().BeEquivalentTo(new ViewBoardResponse
            {
                Board = new Board()
            });
        }

        [SetUp]
        public void SetUp()
        {
            _viewBoard = new ViewBoard(this);
        }

        [Test]
        public void CanViewABoard()
        {
            ViewBoardResponse response = Execute(new ViewBoardRequest());

            ExpectABoardResponse(response);
        }
    }
}