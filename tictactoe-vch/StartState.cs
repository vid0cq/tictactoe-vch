using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace tictactoe_vch
{
    class StartState : GameState
    {
        //public StartState() : base() { }

        public override GameState progress(Option<BoxState[,]> board, Option<int> row, Option<int> col)
        {
            return match(board,
                Some: b => (GameState)new InProgressState(), //why need the cast?
                None: () => new InvalidState());
        }
    }
}
