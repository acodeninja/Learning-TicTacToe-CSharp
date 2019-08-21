using System;
using TicTacToe.Domain;

namespace TicTacToe.Boundary
{
    public class PlaceTokenResponse : IBoardResponse
    {
        public Board Board { get; set; }
        public System.Exception Error { get; set; }
    }
}