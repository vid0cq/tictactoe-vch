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
        public InProgressState(MovedBy[,] board, Move lastMove) 
            : base(board, lastMove) { }

        public override GameState Progress(Option<(int row, int col)> humanMove)
        {
                return match(humanMove,
                        Some: move => {
                        var gameState = PlayerTurn(move.row, move.col);
                        if (gameState is WonState || gameState is FullState) return gameState;
                        return ComputerTurn(); },
                    None: () => throw new ArgumentException("The move was invalid"));
        }

        protected GameState PlayerTurn(int row, int col)
        {
            Board[row, col] = MovedBy.Human;
            var boardState = GetBoardState(Board, row, col);
            return getGameState(boardState, new Move((row,col), MovedBy.Human));
        }

        private GameState ComputerTurn()
        {
            var (row, col) = Move(Board);
            Board[row, col] = MovedBy.Computer;
            var boardState = GetBoardState(Board, row, col);
            return getGameState(boardState, new Move((row, col), MovedBy.Computer));
        }

        private GameState getGameState(BoardState boardState, Move lastMove)
        {
            if (!Finished(boardState)) return new InProgressState(Board, lastMove);

            if (boardState == BoardState.Won) return new WonState(Board, lastMove);
            else return new FullState(Board, lastMove);
        }
    }
}
