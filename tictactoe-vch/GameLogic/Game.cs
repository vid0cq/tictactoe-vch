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
        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        private GameState state;

        public Game()
        {
            state = new StartState();
        }

        public Option<Move> Move(int row, int col)
        {
            var newState = state.Progress((row, col));
            var stateChanged = state != newState;
            state = newState;
            if (stateChanged && GameStateChanged != null)
                GameStateChanged(this, new GameStateChangedEventArgs(newState));

            return state.LastMove;
        }

        public void Restart()
        {
            Move(-1, -1);
        }
    }
}
