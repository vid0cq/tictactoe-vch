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
        public FullState(MovedBy[,] board, Move lastMove) 
            : base(board, lastMove) { }
        public override GameState Progress(Option<(int row, int col)> humanMove)
        {
            return new StartState();
        }
    }
}
