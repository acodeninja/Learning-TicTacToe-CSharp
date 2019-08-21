using System;
using System.Linq;
using Newtonsoft.Json;

namespace TicTacToe.Domain
{
    public class Board
    {
        public string[] Grid;

        public Board()
        {
            Grid = new string[9];
        }

        public bool IsComplete()
        {
            var rowSlicedGrid = new string[][]
            {
                Grid.Take(3).ToArray(), 
                Grid.Skip(3).Take(3).ToArray(),
                Grid.Skip(6).Take(3).ToArray(),
            };

            foreach (string[] row in rowSlicedGrid)
            {
                if (row.SequenceEqual(new string[3] {"X", "X", "X"}))
                {
                    return true;
                }
            }
            
            string[][] colSlicedGrid = new string[][]
            {
                new string[] { Grid[0], Grid[3], Grid[6] },
                new string[] { Grid[1], Grid[4], Grid[7] },
                new string[] { Grid[2], Grid[5], Grid[8] },
            };
            
            foreach (string[] col in colSlicedGrid)
            {
                if (col.SequenceEqual(new string[3] {"X", "X", "X"}))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}