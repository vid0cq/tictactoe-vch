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
        public abstract GameState Progress(int row, int col);

        public Option<(int row, int col)> LastComputerMove { get; protected set; }

        public Move LastMove { get; protected set; }

        protected BoxState[,] Board { get; private set; }

        protected GameState(BoxState[,] board, Option<(int row, int col)> computerMove, Move lastMove=Move.NA)
        {
            this.Board = board;
            this.LastComputerMove = computerMove;
            this.LastMove = lastMove;
        }
    }
}
