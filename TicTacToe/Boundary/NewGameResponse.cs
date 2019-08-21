using System;
using TicTacToe.Domain;
using TicTacToe.Domain.BoardStatus;

namespace TicTacToe.Boundary
{
    public class NewGameResponse : IBoardResponse
    {
        public Board Board { get; set; }
        public System.Exception Error { get; set; }
        public IBoardStatus Status { get; set; }
    }
}