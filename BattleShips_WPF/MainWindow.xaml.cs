using BattleShips_Lib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleShips_WPF
{
    public partial class MainWindow : Window
    {
        Game newGame;

        Double width;
        Double height;

        public MainWindow()
        {
            InitializeComponent();
            AlignLabels();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    SetCell(firstPlayerField, "player1Field_" + i + "_" + j);
                    SetCell(secondPlayerField, "player2Field_" + i + "_" + j);
                }
                SetLabels(i);
            }
        }

        void SetLabels(int index)
        {
            SetColLabels(firstPlayerColDigits, index);
            SetColLabels(secondPlayerColDigits, index);
            SetRowLabels(firstPlayerRowDigits, index);
            SetRowLabels(secondPlayerRowDigits, index);
        }

        void AlignLabels()
        {
            firstPlayerRowDigits.HorizontalAlignment = HorizontalAlignment.Right;
            secondPlayerRowDigits.HorizontalAlignment = HorizontalAlignment.Right;
            firstPlayerColDigits.VerticalAlignment = VerticalAlignment.Bottom;
            secondPlayerColDigits.VerticalAlignment = VerticalAlignment.Bottom;
        }

        void SetColLabels(Grid grid, int index)
        {
            Label colDigit = new Label();
            colDigit.Content = index + 1;
            colDigit.HorizontalContentAlignment = HorizontalAlignment.Center;
            grid.Children.Add(colDigit);
        }

        void SetRowLabels(Grid grid, int index)
        {
            Label rowDigit = new Label();
            rowDigit.Content = (char)(index + 'A');
            rowDigit.VerticalContentAlignment = VerticalAlignment.Center;
            grid.Children.Add(rowDigit);
        }

        void SetCell(Grid grid, string name)
        {
            Button cell = new Button();
            grid.Children.Add(cell);
            cell.VerticalAlignment = VerticalAlignment.Top;
            cell.HorizontalAlignment = HorizontalAlignment.Left;
            cell.Background = Brushes.White;
            cell.Name = name;
            cell.Click += ReadStep;
        }

        void ChangeAppSize(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                BattleShip.Width = SystemParameters.PrimaryScreenWidth;
                BattleShip.Height = SystemParameters.PrimaryScreenHeight;
            }
            for (int i = 0; i < 10; i++)
            {
                width = (BattleShip.Width - 20) / 25 + 0.15;
                height = (BattleShip.Height - 20) / 18;
                for (int j = 0; j < 10; j++)
                {
                    ChangeButtonsSize(firstPlayerField, i, j);
                    ChangeButtonsSize(secondPlayerField, i, j);
                }
                ChangeLabels(i);
            }
        }

        void ChangeLabels(int i)
        {
            ChangeRowLabels(firstPlayerRowDigits, i);
            ChangeRowLabels(secondPlayerRowDigits, i);
            ChangeColLabels(firstPlayerColDigits, i);
            ChangeColLabels(secondPlayerColDigits, i);
        }

        void ChangeButtonsSize(Grid grid, int i, int j)
        {
            Button cell = (Button)grid.Children[i * 10 + j];
            cell.Width = width;
            cell.Height = height;
            cell.Margin = new Thickness((cell.Width) * j, cell.Height * i, 0, 0);
            cell.BorderBrush = Brushes.Black;
        }

        void ChangeRowLabels(Grid grid, int index)
        {
            Label rowDigit = (Label)grid.Children[index];
            rowDigit.Height = height;
            rowDigit.FontSize = height / 2;
            rowDigit.Margin = new Thickness(0, rowDigit.Height * index, 5, 0);
            rowDigit.VerticalAlignment = VerticalAlignment.Top;
        }

        void ChangeColLabels(Grid grid, int index)
        {
            Label colDigit = (Label)grid.Children[index];
            colDigit.Width = width;
            colDigit.FontSize = height / 2;
            colDigit.Margin = new Thickness(colDigit.Width * index, 0, 0, 5);
            colDigit.HorizontalAlignment = HorizontalAlignment.Left;
        }

        void CheckNames(object sender, TextChangedEventArgs e)
        {
            if (CheckIfNamesAreNotEmptyAndNotEquel(player1Name.Text, player2Name.Text))
            {
                startGameButton.IsEnabled = true;
            }
            else
            {
                startGameButton.IsEnabled = false;
            }
            if (player1Name.Text == player2Name.Text)
            {
                MessageBox.Show("Имена игроков не должны совпадать!");
            }
        }

        bool CheckIfNamesAreNotEmptyAndNotEquel(string name1, string name2)
        {
            if ((name1 != "") && (name2 != "") && (name1 != name2))
            {
                return true;
            }
            return false;
        }

        void StartNewGame(object sender, RoutedEventArgs e)
        {
            BlockNames();
            Player player1 = new Player(player1Name.Text);
            Player player2 = new Player(player2Name.Text);
            whoseTurnMessage.Text = "Ход игрока " + player1.Name;
            SetTextBox(whoseTurnMessage, Brushes.Pink);
            field1Label.Text = "Поле игрока " + player1.Name;
            SetTextBox(field1Label, Brushes.Pink);
            field2Label.Text = "Поле игрока " + player2.Name;
            SetTextBox(field2Label, Brushes.Aqua);
            newGame = new Game(new CellBrusher());
            newGame.StartGame(player1, player2, "../../../BattleShips_3/Field1.txt", "../../../BattleShips_3/Field2.txt");
            startGameButton.IsEnabled = false;
            if (newGame.Message != "")
            {
                EndGameWithErrors();
            }
        }

        void SetTextBox(TextBlock textBlock, Brush brush)
        {
            textBlock.FontSize = BattleShip.Height / 22;
            textBlock.Background = brush;
            textBlock.TextAlignment = TextAlignment.Center;
        }

        void EndGameWithErrors()
        {
            newGame.Drawer.WriteMessage("Игра не может быть начата.\n" + newGame.Message);
            BlockButtons();
        }

        void BlockButtons()
        {
            for (int i = 0; i < 100; i++)
            {
                firstPlayerField.Children[i].IsEnabled = false;
                secondPlayerField.Children[i].IsEnabled = false;
            }
        }

        void BlockNames()
        {
            player1Name.IsReadOnly = true;
            player2Name.IsReadOnly = true;
        }

        void ReadStep(object sender, RoutedEventArgs e)
        {
            if (newGame != null)
            {
                if (CheckIfArgumentsOfGameNotNull())
                {
                    Button cell = (Button)sender;
                    if (CheckIfButtonPressedCorrect(cell))
                    {
                        string[] coordinates = cell.Name.Split('_');
                        string dot = Convert.ToString((char)(Convert.ToInt32(coordinates[1]) + 'A'));
                        newGame.CommitAStep(AddColCoordinate(dot, coordinates[2]));
                        TryToSaveGameResults();
                    }
                }
            }
        }

        void TryToSaveGameResults()
        {
            if (newGame.Winner != null)
            {
                Savior.SaveGameResults(newGame.Winner);
            }
        }

        bool CheckIfArgumentsOfGameNotNull()
        {
            if ((newGame.Fields.Count != 0) && (newGame.Winner == null))
            {
                return true;
            }
            return false;
        }

        string AddColCoordinate(string dot, string col)
        {
            return dot + (Convert.ToInt32(col) + 1).ToString();
        }

        bool CheckIfButtonPressedCorrect(Button cell)
        {
            if (((cell.Name.Contains("player1Field")) && (!newGame.Player1Turn)) || ((cell.Name.Contains("player2Field")) && (newGame.Player1Turn)))
            {
                return true;
            }
            return false;
        }
    }
}
