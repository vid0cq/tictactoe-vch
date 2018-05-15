using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace tictactoe_vch
{
    class InvalidState : GameState
    {
        public InvalidState() : base(null,None) {}

        public override GameState Progress(int row, int col)
        {
            throw new NotImplementedException();
        }
    }
}
