using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;

namespace tictactoe_vch
{
    class InvalidState : GameState
    {
        //public InvalidState() : base(null) {}

        public override GameState progress(Option<BoxState[,]> board, Option<int> row, Option<int> col)
        {
            throw new NotImplementedException();
        }
    }
}
