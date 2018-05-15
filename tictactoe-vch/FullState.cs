using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;

namespace tictactoe_vch
{
    class FullState : GameState
    {
        public FullState(BoxState[,] board, bool playerTurn) : base(board, playerTurn) { }
        public override GameState progress(int row, int col)
        {
            return new StartState();
        }

        protected override GameState computerTurn()
        {
            throw new NotImplementedException();
        }

        protected override GameState playerTurn(int row, int col)
        {
            throw new NotImplementedException();
        }
    }
}
