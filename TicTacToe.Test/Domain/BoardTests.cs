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

        [Test]
        public void CanMakeAnIncompleteBoard()
        {
            Board board = new Board();

            board.IsComplete().Should().Be(false);
        }

        [Test]
        public void CanMakeACompleteBoardByPlacingXAtTheBeginningOfEachColumn()
        {
            Board board = new Board
            {
                Grid = new string[9] {"X", "X", "X", null, null, null, null, null, null}
            };

            board.IsComplete().Should().Be(true);
        }

        [Test]
        public void CanMakeACompleteBoardByPlacingXAtTheMiddleOfEachColumn()
        {
            Board board = new Board
            {
                Grid = new string[9] {null, null, null, "X", "X", "X", null, null, null}
            };

            board.IsComplete().Should().Be(true);
        }

        [Test]
        public void CanMakeACompleteBoardByPlacingXAtTheEndOfEachColumn()
        {
            Board board = new Board
            {
                Grid = new string[9] {null, null, null, null, null, null, "X", "X", "X"}
            };

            board.IsComplete().Should().Be(true);
        }

        [Test]
        public void CanMakeACompleteBoardByPlacingXAtTheEndOfEachRow()
        {
            Board board = new Board
            {
                Grid = new string[9] {null, null, "X", null, null, "X", null, null, "X"}
            };

            board.IsComplete().Should().Be(true);
        }

        [Test]
        public void CanMakeACompleteBoardByPlacingOAtTheEndOfEachRow()
        {
            Board board = new Board
            {
                Grid = new string[9] {null, null, "O", null, null, "O", null, null, "O"}
            };

            board.IsComplete().Should().Be(true);
        }

        [Test]
        public void CanMakeACompleteBoardByPlacingXInADiagonalPattern()
        {
            Board board = new Board
            {
                Grid = new string[9] {"O", null, null, null, "O", null, null, null, "O"}
            };

            board.IsComplete().Should().Be(true);
        }
    }
}