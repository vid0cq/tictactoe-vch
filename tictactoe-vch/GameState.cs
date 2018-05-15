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
        protected bool isPlayerTurn;

        protected GameState(BoxState[,] board, bool isPlayerTurn = true)
        {
            this.board = board;
            this.isPlayerTurn = isPlayerTurn;
        }

        public BoxState[,] board { get; private set; }

        public abstract GameState progress(int row, int col);

        protected abstract GameState playerTurn(int row, int col);
        protected abstract GameState computerTurn();
    }
}
