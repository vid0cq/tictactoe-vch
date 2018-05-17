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
        public FullState(Lst<Move> board, Move lastMove) 
            : base(board, lastMove) { }
        public override GameState Progress(Option<Move> humanMove)
        {
            return new StartState();
        }
    }
}
