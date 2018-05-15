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
        public StartState() : base(new BoxState[3, 3]) { }

        public override GameState progress(int row, int col)
        {
            playerTurn(row, col);
            return computerTurn();
        }

        protected override GameState playerTurn(int row, int col)
        {
            board[row, col] = BoxState.X;
            return new InProgressState(board, isPlayerTurn ^ true);
        }

        protected override GameState computerTurn()
        {
            var (row, col) = Move(board);
            board[row, col] = BoxState.O;
            return new InProgressState(board, isPlayerTurn ^ true);
        }
    }
}
