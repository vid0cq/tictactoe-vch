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
        public StartState() : base(new MovedBy[3, 3], new Move(None, MovedBy.NA)) { }

        public override GameState Progress(Option<(int row, int col)> humanMove)
        {
            return match(humanMove,
                    Some: pos =>
                    {
                        Board[pos.row, pos.col] = MovedBy.Human;
                        return ComputerTurn();
                    },
                    None: () => throw new ArgumentException("The move was invalid"));
        }

        private GameState ComputerTurn()
        {
            var (row, col) = Move(Board);
            Board[row, col] = MovedBy.Computer;
            var move = new Move((row, col), MovedBy.Computer);
            return new InProgressState(Board, move);
        }
    }
}
