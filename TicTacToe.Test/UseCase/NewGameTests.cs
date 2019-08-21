using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Boundary;
using TicTacToe.Domain;
using TicTacToe.Gateway;
using TicTacToe.Test.Gateway;
using TicTacToe.UseCase;

namespace TicTacToe.Test.UseCase
{
    public class NewGameTests : TestBoardGateway, IBoardGateway
    {
        private NewGame _newGame;

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
            _newGame = new NewGame(this);
        }

        [Test]
        public void CanCreateANewGame()
        {
            NewGameResponse response = Execute(new NewGameRequest());

            ExpectABoardResponse(response);
        }
    }
}