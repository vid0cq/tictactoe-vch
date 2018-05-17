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
        public abstract GameState Progress(Option<Move> humanMove);

        public Option<Move> LastMove { get; protected set; }

        protected Lst<Move> Board{ get; set; }

        protected GameState(Lst<Move> board, Option<Move> lastMove)
        {
            this.Board = board;
            this.LastMove = lastMove;
        }
    }
}
