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
using static tictactoe_vch.Logic;

namespace tictactoe_vch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BoxState[,] table;
        private Logic logic;

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            table = new BoxState[3, 3];
            logic = new Logic();

            tictactoeGrid.Children.Cast<Button>().ToList().ForEach(btn => btn.Content = String.Empty);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            var col = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            if (table[row, col] != BoxState.Unused) return;

            table[row, col] = BoxState.X;
            button.Content = "X";

            if (won(table, row, col)) ShowMessage("Human Player Won!");
            else if (full(table)) ShowMessage("It's a tie");
            else
            {
                var (mrow,mcol) = logic.Move(table);
                table[mrow, mcol] = BoxState.O;
                ((Button)tictactoeGrid.FindName("Button" + mrow + mcol)).Content="O";
                if (won(table, mrow, mcol)) ShowMessage("Computer Won!");
                else if (full(table)) ShowMessage("It's a tie");
            }
        }

        private void ShowMessage(String message)
        {
            MessageBox.Show(message);
            Start();
        }

       
    }
}
