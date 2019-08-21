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
            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i] == null)
                {
                    continue;
                }

                string tokenUnderEvaluation = Grid[i];

                // evaluate full row from square
                if (EvaluateSquareIsPartOfCompleteRow(
                    new[] {new int[3] {0, 1, 2}, new int[3] {3, 4, 5}, new int[3] {6, 7, 8}}, i,
                    tokenUnderEvaluation)) return true;

                // evaluate full column from square
                if (EvaluateSquareIsPartOfCompleteRow(
                    new[] {new int[3] {0, 3, 6}, new int[3] {1, 4, 7}, new int[3] {2, 5, 8}}, i,
                    tokenUnderEvaluation)) return true;

                // conditionally evaluate diagonal from square
                if (EvaluateSquareIsPartOfCompleteRow(new[] {new int[3] {0, 4, 8}, new int[3] {2, 4, 6}}, i,
                    tokenUnderEvaluation)) return true;
            }

            return false;
        }

        private bool EvaluateSquareIsPartOfCompleteRow(int[][] rows, int i, string tokenUnderEvaluation)
        {
            int[] sudoku = new int[9] {8, 1, 6, 3, 5, 7, 4, 9, 2};

            foreach (int[] row in rows)
            {
                if (row.Contains(i))
                {
                    int value = 0;
                    foreach (int rowPosition in row)
                    {
                        if (Grid[rowPosition] == tokenUnderEvaluation)
                        {
                            value += sudoku[rowPosition];
                        }
                    }

                    if (value == 15)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}