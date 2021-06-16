using BattleShips_Lib;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleShips_WPF
{
    class CellBrusher : IDrawable
    {
        public void WriteMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void SetWinner(Player winner)
        {
            TextBlock whoseTurnMessage = FindInformationTable();
            whoseTurnMessage.Text = "Победил " + winner.Name + " за " + winner.Count + " ходов!";
        }

        public void InformWhoseTurn(Game newGame)
        {
            TextBlock whoseTurnMessage = FindInformationTable();
            if (newGame.Player1Turn)
            {
                whoseTurnMessage.Text = "Ход игрока " + newGame.ReturnPlayersName(0);
                whoseTurnMessage.Background = Brushes.Pink;
            }
            else
            {
                whoseTurnMessage.Text = "Ход игрока " + newGame.ReturnPlayersName(1);
                whoseTurnMessage.Background = Brushes.Aqua;
            }
        }

        TextBlock FindInformationTable()
        {
            Window window = App.Current.MainWindow;
            return (TextBlock)window.FindName("whoseTurnMessage");
        }

        public void DrawField(List<Player> Players, List<Field> Fields)
        {
            for (int i = 0; i < Fields[0].Cells.Count; i++)
            {
                for (int j = 0; j < Fields[0].Cells[i].Count; j++)
                {
                    BrushButton(Fields[0].Cells[i][j], "firstPlayerField");
                    BrushButton(Fields[1].Cells[i][j], "secondPlayerField");
                }
            }
        }

        void BrushButton(Cell cell, string dgName)
        {
            Button btn = ReturnButtonToBrush(cell, dgName);
            if (cell.State == CellStates.ContainsShootedShip)
            {
                btn.Background = Brushes.Red;
            }
            else
            {
                if (cell.State == CellStates.EmptyShooted)
                {
                    btn.Background = Brushes.Blue;
                }
            }
        }

        Button ReturnButtonToBrush(Cell cell, string dgName)
        {
            Window window = App.Current.MainWindow;
            Grid grid = (Grid)window.FindName(dgName);
            return (Button)grid.Children[cell.Row * 10 + cell.Col];
        }
    }
}
