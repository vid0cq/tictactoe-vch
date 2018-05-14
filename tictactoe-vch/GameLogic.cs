using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace tictactoe_vch
{
    class GameLogic
    {

        #region Move

        public static (int, int) Move(BoxState[,] table)
        {
            var finalResult = Int32.MinValue;
            var (row, col) = (0, 0);
            foreach (var (urow,ucol) in GetUnusedBoxes(table))
            {
                var crtResult = ComputeMove((BoxState[,])table.Clone(), urow, ucol, false);
                if (crtResult > finalResult)
                {
                    finalResult = crtResult;
                    (row, col) = (urow, ucol);
                }
            }
            
            return (row, col);
        }

        private static int ComputeMove(BoxState[,] table, int row, int col, bool playerTurn)
        {
            table[row, col] = playerTurn ? BoxState.X : BoxState.O;
            if (won(table, row, col)) return playerTurn ? -1 : 1;
            if (full(table)) return 0;

            var finalResult = playerTurn ? Int32.MinValue : Int32.MaxValue;
            foreach (var (urow, ucol) in GetUnusedBoxes(table))
            {
                var result = ComputeMove((BoxState[,])table.Clone(), urow, ucol, playerTurn ^ true);
                if(playerTurn && result>finalResult) finalResult = result;
                else if(!playerTurn && result < finalResult) finalResult = result;
            }

            return finalResult;
        }

        private static IEnumerable<(int, int)> GetUnusedBoxes(BoxState[,] table)
        {
            var unusedBoxes = new List<(int, int)>();
            for (int i = 0; i < table.GetLength(0); i++)
                for (int j = 0; j < table.GetLength(0); j++)
                    if (table[i, j] == BoxState.Unused) unusedBoxes.Add((i, j));

            return unusedBoxes;
        }

        #endregion Move

        #region End of game

        public static bool finished(TableState tableState)
        {
            return (tableState & TableState.Finished) == tableState;
        }


        public static TableState getTableState(BoxState[,] table, Option<int> row, Option<int> col)
        {
            return match(from r in row
                         from c in col
                         select ( r, c ),
                         Some: t => won(table, t.r, t.c) ? TableState.Won : full(table) ? TableState.Full : TableState.InProgress,
                         None: () => full(table) ? TableState.Full : TableState.InProgress);
        }

        private static bool full(BoxState[,] table)
        {
            for (int i = 0; i < table.GetLength(0); i++)
                for (int j = 0; j < table.GetLength(0); j++)
                    if (table[i, j] == BoxState.Unused) return false;

            return true;
        }

        private static bool won(BoxState[,] table, int row, int col)
        {
            return checkRow(table, row, col) || checkCol(table, row, col)
                || checkDiag(table, row, col) || checkSecDiag(table, row, col);
        }

        private static bool checkRow(BoxState[,] table, int row, int col)
        {
            BoxState state = table[row, col];
            for (int i = 0; i < table.GetLength(0); i++)
                if (table[i, col] != state) return false;

            return true;
        }

        private static bool checkCol(BoxState[,] table, int row, int col)
        {
            BoxState state = table[row, col];
            for (int i = 0; i < table.GetLength(0); i++)
                if (table[row, i] != state) return false;

            return true;
        }

        private static bool checkDiag(BoxState[,] table, int row, int col)
        {
            if (row != col) return false;

            BoxState state = table[row, col];
            for (int i = 0; i < table.GetLength(0); i++)
                if (table[i, i] != state) return false;

            return true;
        }

        private static bool checkSecDiag(BoxState[,] table, int row, int col)
        {
            if (row + col != table.GetLength(0) - 1) return false;

            BoxState state = table[row, col];
            for (int i = 0; i < table.GetLength(0); i++)
                if (table[i, table.GetLength(0) - (i + 1)] != state) return false;

            return true;
        }

        #endregion End of game
    }
}
