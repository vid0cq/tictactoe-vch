using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;

namespace tictactoe_vch
{
    class WonState : GameState
    {
        public WonState(BoxState[,] board, Option<(int row, int col)> computerMove, Move lastMove) 
            : base(board, computerMove, lastMove){}

        public override GameState Progress(int row, int col)
        {
            return new StartState();
        }
    }
}
