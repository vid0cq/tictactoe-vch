using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace tictactoe_vch
{
    abstract class GameState
    {
        public abstract GameState Progress(Option<(int row, int col)> humanMove);

        public Option<Move> LastMove { get; protected set; }

        protected MovedBy[,] Board { get; private set; }

        protected GameState(MovedBy[,] board, Option<Move> lastMove)
        {
            this.Board = board;
            this.LastMove = lastMove;
        }
    }
}
