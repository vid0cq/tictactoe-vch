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
        public StartState() : base(Lst<Move>.Empty, None) { }

        public override GameState Progress(Option<Move> humanMove)
        {
            return match(humanMove,
                    Some: move =>
                    {
                        Board = move.Cons(Board);
                        return ComputerTurn();
                    },
                    None: () => throw new ArgumentException("The move was invalid"));
        }

        private GameState ComputerTurn()
        {
            var computerMove = Move(Board);
            Board = computerMove.Cons(Board);
            return new InProgressState(Board, computerMove);
        }
    }
}
