using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Domain;

namespace TicTacToe.Test.Domain
{
    public class BoardTests
    {
        private static void ExpectAnEmptyGrid(Board board)
        {
            board.Grid.Should().BeEquivalentTo(new string[9]);
        }
        
        [Test]
        public void CanMakeANewEmptyBoard()
        {
            Board board = new Board();

            ExpectAnEmptyGrid(board);
        }
    }
}