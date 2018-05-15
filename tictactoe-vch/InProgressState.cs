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
        public InProgressState(BoxState[,] board, bool isPlayerTurn) : base(board, isPlayerTurn) { }

        public override GameState progress(int row, int col)
        {
            var gameState = playerTurn(row, col);
            if (gameState is WonState || gameState is FullState) return gameState;
            return computerTurn();
        }

        protected override GameState playerTurn(int row, int col)
        {
            board[row, col] = BoxState.X;
            var boardState = getBoardState(board, row, col);
            return getGameState(boardState);
        }

        private GameState getGameState(BoardState boardState)
        {
            if (!finished(boardState)) return this;

            if (boardState == BoardState.Won) return new WonState(board, isPlayerTurn);
            else return new FullState(board, isPlayerTurn);
        }

        protected override GameState computerTurn()
        {
            var (row, col) = Move(board);
            board[row, col] = BoxState.O;
            var boardState = getBoardState(board, row, col);
            return getGameState(boardState);
        }
    }
}
