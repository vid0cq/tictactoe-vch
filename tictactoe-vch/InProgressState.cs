using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using static tictactoe_vch.GameLogic;

namespace tictactoe_vch
{
    class InProgressState : GameState
    {
        //public InProgressState(bool playerTurn) : base(playerTurn) { }

        public override GameState progress(Option<BoxState[,]> board, Option<int> row, Option<int> col)
        {
            return match(from b in board
                         from r in row
                         from c in col
                         select (b, r, c),
                        Some: all => progress(all.b, all.r, all.c),
                        None: () => new InvalidState());
        }

        private GameState progress(BoxState[,] board, int row, int col)
        {
            var tableState = getTableState(board, row, col);
            if (!finished(tableState)) return this;

            if (tableState == TableState.Won) return new WonState();
            else return new FullState();
        }
    }
}
