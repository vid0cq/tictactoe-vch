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

        private BoxState[,] table;

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            table = new BoxState[3, 3];
            tictactoeGrid.Children.Cast<Button>().ToList().ForEach(btn => btn.Content = String.Empty);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            var col = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            if (table[row, col] != BoxState.Unused) return;

            var tableState = PlayerTurn(row, col);
            if (finished(tableState))
            {
                ResetTable(tableState, true);
                return;
            }

            tableState = ComputerTurn();
            if (finished(tableState)) ResetTable(tableState, false);
        }

        private BoardState PlayerTurn(int row, int col)
        {
            table[row, col] = BoxState.X;
            ((Button)tictactoeGrid.FindName("Button" + row + col)).Content = "X";
            var tableState = getBoardState(table, row, col);
            return tableState;
        }

        private BoardState ComputerTurn()
        {
            var (row, col) = Move(table);
            table[row, col] = BoxState.O;
            ((Button)tictactoeGrid.FindName("Button" + row + col)).Content = "O";
            return getBoardState(table, row, col);
        }

        private void ResetTable(BoardState tableState, bool playerTurn)
        {
            string msg = null;
            switch (tableState)
            {
                case BoardState.Full:
                    msg = "It's a tie";
                    break;
                case BoardState.Won:
                    msg = playerTurn ? "You Won!" : "You Lost!";
                    break;
            }

            ShowMessage(msg);
            Start();
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
