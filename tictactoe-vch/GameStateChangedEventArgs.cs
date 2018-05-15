using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_vch
{
    class GameStateChangedEventArgs : EventArgs
    {
        public GameState GameState { get; private set; }
        public GameStateChangedEventArgs(GameState newGameState)
        {
            this.GameState = newGameState;
        }
    }
}
