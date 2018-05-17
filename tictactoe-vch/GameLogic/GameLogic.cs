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

        public static Move Move(Lst<Move> board)
        {
            var finalResult = Int32.MinValue;
            var (row, col) = (0, 0);
            foreach (var (urow,ucol) in GetUnusedBoxes(board))
            {
                var crtResult = ComputeMove(board, urow, ucol, false);
                if (crtResult > finalResult)
                {
                    finalResult = crtResult;
                    (row, col) = (urow, ucol);
                }
            }
            
            return new Move((row, col),MovedBy.Computer);
        }

        private static int ComputeMove(Lst<Move> board, int row, int col, bool playerTurn)
        {
            var move = new Move((row, col), playerTurn ? MovedBy.Human : MovedBy.Computer);
            board=move.Cons(board);
            if (Won(board, move)) return playerTurn ? -1 : 1;
            if (Full(board)) return 0;

            var finalResult = playerTurn ? Int32.MinValue : Int32.MaxValue;
            foreach (var (urow, ucol) in GetUnusedBoxes(board))
            {
                var result = ComputeMove(board, urow, ucol, playerTurn ^ true);
                if(playerTurn && result>finalResult) finalResult = result;
                else if(!playerTurn && result < finalResult) finalResult = result;
            }

            return finalResult;
        }

        private static IEnumerable<(int, int)> GetUnusedBoxes(Lst<Move> board)
        {
            return List((0,0),(0,1),(0,2),(1,0),(1,1),(1,2),(2,0),(2,1),(2,2))
                .Subtract(board.Map(b=>b.MovePosition));
        }

        #endregion Move

        #region End of game

        public static bool Finished(BoardState boardState)
        {
            return (boardState & BoardState.Finished) == boardState;
        }

        public static BoardState GetBoardState(Option<Lst<Move>> board, Option<Move> lastMove)
        {
            return match(from brd in board
                         from move in lastMove
                         select ( brd, move ),
                         Some: t => Won(t.brd, t.move) ? BoardState.Won : Full(t.brd) ? BoardState.Full : BoardState.InProgress,
                         None: () => throw new ArgumentException("Board or last move is not valid"));
        }

        private static bool Full(Lst<Move> board)
        {
            return board.Length() == 9;
        }

        private static bool Won(Lst<Move> board, Move lastMove)
        {
            return CheckRow(board, lastMove) || CheckCol(board, lastMove)
                || CheckDiag(board, lastMove) || CheckSecDiag(board, lastMove);
        }

        private static bool CheckRow(Lst<Move> board, Move lastMove)
        {
            return match(isMovedBy(board, lastMove),
               Some: by => board.Filter(m => m.MovePosition.col== lastMove.MovePosition.col).Count(m => m.MovedBy == by)==3,
               None: () => false);
        }

        private static bool CheckCol(Lst<Move> board, Move lastMove)
        {
            return match(isMovedBy(board, lastMove),
                Some: by => board.Filter(m => m.MovePosition.row == lastMove.MovePosition.row).Count(m => m.MovedBy == by)==3,
                None: () => false);
        }

        private static bool CheckDiag(Lst<Move> board, Move lastMove)
        {
            if (lastMove.MovePosition.row != lastMove.MovePosition.col) return false;

            return match(isMovedBy(board, lastMove),
                Some: by => board.Filter(m => m.MovePosition.row == m.MovePosition.col).Count(m => m.MovedBy == by)==3,
                None: () => false);
        }

        private static bool CheckSecDiag(Lst<Move> board, Move lastMove)
        {
            if (lastMove.MovePosition.row + lastMove.MovePosition.col != 3 - 1) return false;
            return match(isMovedBy(board, lastMove),
                Some: by => board.Filter(m=>m.MovePosition.row+m.MovePosition.col==3-1).Count(m=>m.MovedBy==by)==3,
                None: () => false);
        }

        private static Option<MovedBy> isMovedBy(Lst<Move> board, Move lastMove)
        {
            return board.Find(m => m==lastMove).Select(m => m.MovedBy);
        }

        #endregion End of game
    }
}
