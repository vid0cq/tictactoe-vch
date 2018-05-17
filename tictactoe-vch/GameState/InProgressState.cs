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
        public InProgressState(Lst<Move> board, Move lastMove) 
            : base(board, lastMove) { }

        public override GameState Progress(Option<Move> humanMove)
        {
            return match(humanMove,
                Some: move => {
                    var gameState = PlayerTurn(move);
                    if (gameState is WonState || gameState is FullState) return gameState;
                    return ComputerTurn(); },
                None: () => throw new ArgumentException("The move was invalid"));
        }

        protected GameState PlayerTurn(Move humanMove)
        {
            Board = humanMove.Cons(Board);
            var boardState = GetBoardState(Board, humanMove);
            return getGameState(boardState, humanMove);
        }

        private GameState ComputerTurn()
        {
            var computerMove = Move(Board);
            Board = computerMove.Cons(Board);
            var boardState = GetBoardState(Board, computerMove);
            return getGameState(boardState, computerMove);
        }

        private GameState getGameState(BoardState boardState, Move lastMove)
        {
            if (!Finished(boardState)) return new InProgressState(Board, lastMove);

            if (boardState == BoardState.Won) return new WonState(Board, lastMove);
            else return new FullState(Board, lastMove);
        }
    }
}
