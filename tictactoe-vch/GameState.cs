using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace tictactoe_vch
{
    abstract class GameState
    {
        //protected bool playerTurn;

        //protected GameState(bool playerTurn=true)
        //{
        //    //this.board = board;
        //    this.playerTurn = playerTurn;
        //}
        
        //public BoxState[,] board { get; private set; }

        public abstract GameState progress(Option<BoxState[,]> board, Option<int> row, Option<int> col);
    }
}
