using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace tictactoe_vch
{
    class Move : Record<Move>
    {
        public readonly (int row, int col) MovePosition;
        public readonly MovedBy MovedBy;

        public Move((int row, int col) movePosition, MovedBy movedBy)
        {
            MovePosition = movePosition;
            MovedBy = movedBy;
        }
    }
}
