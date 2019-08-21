using System;
using Newtonsoft.Json;
using TicTacToe.Domain;

namespace TicTacToe.Gateway
{
    public class JsonBoardGateway : IBoardGateway
    {
        public Board Fetch()
        {
            string json = System.IO.File.ReadAllText(@"/tmp/tic-tac-toe.json");

            return new Board
            {
                Grid = JsonConvert.DeserializeObject<string[]>(json),
            };
        }

        public string[] Read(Board board)
        {
            return board.Grid;
        }
        
        public string Read(Board board, int column, int row)
        {
            return board.Grid[3 * (column - 1) + (row - 1)];
        }

        public Board Flush(Board board)
        {
            string json = JsonConvert.SerializeObject(board.Grid);

            System.IO.File.WriteAllText(@"/tmp/tic-tac-toe.json", json);

            return board;
        }

        public Board New()
        {
            Board board = new Board();

            return Flush(board);
        }

        public Board Write(Board board, string type, int column, int row)
        {
            board.Grid[3 * (column - 1) + (row - 1)] = type;

            return board;
        }
    }
}