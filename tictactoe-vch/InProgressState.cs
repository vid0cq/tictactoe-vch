using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using static tictactoe_vch.GameLogic;

namespace tictactoe_vch
{
    class InProgressState : GameState
    {
        public InProgressState(BoxState[,] board, Option<(int row, int col)> computerMove, Move lastMove) 
            : base(board, computerMove, lastMove) { }

        public override GameState Progress(int row, int col)
        {
            var gameState = PlayerTurn(row, col);
            if (gameState is WonState || gameState is FullState) return gameState;
            return ComputerTurn();
        }

        protected GameState PlayerTurn(int row, int col)
        {
            Board[row, col] = BoxState.X;
            var boardState = GetBoardState(Board, row, col);
            return getGameState(boardState, None);
        }

        private GameState ComputerTurn()
        {
            var (row, col) = Move(Board);
            Board[row, col] = BoxState.O;
            var boardState = GetBoardState(Board, row, col);
            return getGameState(boardState, (row, col));
        }

        private GameState getGameState(BoardState boardState, Option<(int row, int col)> computerMove)
        {
            if (!Finished(boardState)) return new InProgressState(Board, computerMove, Move.Computer);

            var lastMove = match(computerMove, Some: m => Move.Computer, None: () => Move.Human);
            if (boardState == BoardState.Won) return new WonState(Board, computerMove, lastMove);
            else return new FullState(Board, computerMove, lastMove);
        }
    }
}
