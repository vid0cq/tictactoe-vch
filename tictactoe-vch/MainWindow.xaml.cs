using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static tictactoe_vch.GameLogic;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Diagnostics;

namespace tictactoe_vch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            game.GameStateChanged += Game_GameStateChanged;
            Start();
        }

        private void Start()
        {
            tictactoeGrid.Children.Cast<Button>().ToList().ForEach(btn => btn.Content = String.Empty);
        }        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            var col = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            if (button.Content.ToString()!="") return;

            var humanMove = new Move((row, col), MovedBy.Human);
            MarkBox(humanMove);
            game.Move(row, col).IfSome(move => MarkBox(move));
        }

        private void MarkBox(Move move)
        {
            move.MovePosition.IfSome(m => 
                ((Button)tictactoeGrid.FindName("Button" + m.row + m.col)).Content = move.MovedBy==MovedBy.Human ? "X":"O");
        }
        private void Game_GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (e.GameState)
            {
                case StartState start:
                    Start();
                    break;
                case WonState won:
                    won.LastMove.IfSome(move => {
                            MarkBox(move);
                            ShowMessage(move.MovedBy == MovedBy.Human ? "You won!" : "You lost!"); });
                    game.Restart();
                    break;
                case FullState full:
                    ShowMessage("It's a tie");
                    game.Restart();
                    break;
            }
        }

        private void ShowMessage(Option<String> message)
        {
            message.Match(
                Some: msg => MessageBox.Show(msg),
                None: () => Trace.WriteLine("Message was null: " + Environment.StackTrace)
                );
        }
    }
}