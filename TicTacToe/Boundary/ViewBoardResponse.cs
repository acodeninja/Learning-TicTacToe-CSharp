using System;
using TicTacToe.Domain;

namespace TicTacToe.Boundary
{
    public class ViewBoardResponse : IBoardResponse
    {
        public Board Board { get; set; }
        public System.Exception Error { get; set; }
    }
}