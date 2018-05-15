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
    class StartState : GameState
    {
        public StartState() : base(new BoxState[3, 3], None) { }

        public override GameState Progress(int row, int col)
        {
            Board[row, col] = BoxState.X;
            return ComputerTurn();
        }

        private GameState ComputerTurn()
        {
            var (row, col) = Move(Board);
            Board[row, col] = BoxState.O;
            return new InProgressState(Board, (row,col), Move.Computer);
        }
    }
}
