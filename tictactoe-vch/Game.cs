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
    class Game
    {
        private GameState state;
        //private BoxState[,] board;

        public Game()
        {
            Start();
        }

        public void Move(int row, int col)
        {
            state = state.progress(row, col);
            //PlayerTurn(row, col);
            //ComputerTurn();
        }

        private void Start()
        {
            state = new StartState();
            //board = new BoxState[3, 3];
        }

        //public void PlayerTurn(Option<int> row, Option<int> col)
        //{
        //    match(from r in row
        //          from c in col
        //          select (r, c),
        //          Some: t => { board[t.r, t.c] = BoxState.X; state = state.progress(board, t.r, t.c); },
        //          None: () => Console.Write("as"));
        //}

        //private void ComputerTurn()
        //{
        //    var (row, col) = Move(board);
        //    board[row, col] = BoxState.O;
        //    state = state.progress(board, row, col);
        //}

    }
}
