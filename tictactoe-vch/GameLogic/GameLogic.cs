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

        public static (int, int) Move(MovedBy[,] board)
        {
            var finalResult = Int32.MinValue;
            var (row, col) = (0, 0);
            foreach (var (urow,ucol) in GetUnusedBoxes(board))
            {
                var crtResult = ComputeMove((MovedBy[,])board.Clone(), urow, ucol, false);
                if (crtResult > finalResult)
                {
                    finalResult = crtResult;
                    (row, col) = (urow, ucol);
                }
            }
            
            return (row, col);
        }

        private static int ComputeMove(MovedBy[,] board, int row, int col, bool playerTurn)
        {
            board[row, col] = playerTurn ? MovedBy.Human : MovedBy.Computer;
            if (Won(board, row, col)) return playerTurn ? -1 : 1;
            if (Full(board)) return 0;

            var finalResult = playerTurn ? Int32.MinValue : Int32.MaxValue;
            foreach (var (urow, ucol) in GetUnusedBoxes(board))
            {
                var result = ComputeMove((MovedBy[,])board.Clone(), urow, ucol, playerTurn ^ true);
                if(playerTurn && result>finalResult) finalResult = result;
                else if(!playerTurn && result < finalResult) finalResult = result;
            }

            return finalResult;
        }

        private static IEnumerable<(int, int)> GetUnusedBoxes(MovedBy[,] board)
        {
            var unusedBoxes = new List<(int, int)>();
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(0); j++)
                    if (board[i, j] == MovedBy.NA) unusedBoxes.Add((i, j));

            return unusedBoxes;
        }

        #endregion Move

        #region End of game

        public static bool Finished(BoardState boardState)
        {
            return (boardState & BoardState.Finished) == boardState;
        }


        public static BoardState GetBoardState(MovedBy[,] board, Option<int> row, Option<int> col)
        {
            return match(from r in row
                         from c in col
                         select ( r, c ),
                         Some: t => Won(board, t.r, t.c) ? BoardState.Won : Full(board) ? BoardState.Full : BoardState.InProgress,
                         None: () => Full(board) ? BoardState.Full : BoardState.InProgress);
        }

        private static bool Full(MovedBy[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(0); j++)
                    if (board[i, j] == MovedBy.NA) return false;

            return true;
        }

        private static bool Won(MovedBy[,] board, int row, int col)
        {
            return CheckRow(board, row, col) || CheckCol(board, row, col)
                || CheckDiag(board, row, col) || CheckSecDiag(board, row, col);
        }

        private static bool CheckRow(MovedBy[,] board, int row, int col)
        {
            MovedBy state = board[row, col];
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[i, col] != state) return false;

            return true;
        }

        private static bool CheckCol(MovedBy[,] board, int row, int col)
        {
            MovedBy state = board[row, col];
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[row, i] != state) return false;

            return true;
        }

        private static bool CheckDiag(MovedBy[,] board, int row, int col)
        {
            if (row != col) return false;

            MovedBy state = board[row, col];
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[i, i] != state) return false;

            return true;
        }

        private static bool CheckSecDiag(MovedBy[,] board, int row, int col)
        {
            if (row + col != board.GetLength(0) - 1) return false;

            MovedBy state = board[row, col];
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[i, board.GetLength(0) - (i + 1)] != state) return false;

            return true;
        }

        #endregion End of game
    }
}
