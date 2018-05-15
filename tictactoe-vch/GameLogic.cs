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

        public static (int, int) Move(BoxState[,] board)
        {
            var finalResult = Int32.MinValue;
            var (row, col) = (0, 0);
            foreach (var (urow,ucol) in GetUnusedBoxes(board))
            {
                var crtResult = ComputeMove((BoxState[,])board.Clone(), urow, ucol, false);
                if (crtResult > finalResult)
                {
                    finalResult = crtResult;
                    (row, col) = (urow, ucol);
                }
            }
            
            return (row, col);
        }

        private static int ComputeMove(BoxState[,] board, int row, int col, bool playerTurn)
        {
            board[row, col] = playerTurn ? BoxState.X : BoxState.O;
            if (won(board, row, col)) return playerTurn ? -1 : 1;
            if (full(board)) return 0;

            var finalResult = playerTurn ? Int32.MinValue : Int32.MaxValue;
            foreach (var (urow, ucol) in GetUnusedBoxes(board))
            {
                var result = ComputeMove((BoxState[,])board.Clone(), urow, ucol, playerTurn ^ true);
                if(playerTurn && result>finalResult) finalResult = result;
                else if(!playerTurn && result < finalResult) finalResult = result;
            }

            return finalResult;
        }

        private static IEnumerable<(int, int)> GetUnusedBoxes(BoxState[,] board)
        {
            var unusedBoxes = new List<(int, int)>();
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(0); j++)
                    if (board[i, j] == BoxState.Unused) unusedBoxes.Add((i, j));

            return unusedBoxes;
        }

        #endregion Move

        #region End of game

        public static bool finished(BoardState boardState)
        {
            return (boardState & BoardState.Finished) == boardState;
        }


        public static BoardState getBoardState(BoxState[,] board, Option<int> row, Option<int> col)
        {
            return match(from r in row
                         from c in col
                         select ( r, c ),
                         Some: t => won(board, t.r, t.c) ? BoardState.Won : full(board) ? BoardState.Full : BoardState.InProgress,
                         None: () => full(board) ? BoardState.Full : BoardState.InProgress);
        }

        private static bool full(BoxState[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(0); j++)
                    if (board[i, j] == BoxState.Unused) return false;

            return true;
        }

        private static bool won(BoxState[,] board, int row, int col)
        {
            return checkRow(board, row, col) || checkCol(board, row, col)
                || checkDiag(board, row, col) || checkSecDiag(board, row, col);
        }

        private static bool checkRow(BoxState[,] board, int row, int col)
        {
            BoxState state = board[row, col];
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[i, col] != state) return false;

            return true;
        }

        private static bool checkCol(BoxState[,] board, int row, int col)
        {
            BoxState state = board[row, col];
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[row, i] != state) return false;

            return true;
        }

        private static bool checkDiag(BoxState[,] board, int row, int col)
        {
            if (row != col) return false;

            BoxState state = board[row, col];
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[i, i] != state) return false;

            return true;
        }

        private static bool checkSecDiag(BoxState[,] board, int row, int col)
        {
            if (row + col != board.GetLength(0) - 1) return false;

            BoxState state = board[row, col];
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[i, board.GetLength(0) - (i + 1)] != state) return false;

            return true;
        }

        #endregion End of game
    }
}
