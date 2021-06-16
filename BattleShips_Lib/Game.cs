using System;
using System.Collections.Generic;
using System.IO;

namespace BattleShips_Lib
{
    public class Game
    {
        List<Player> players = new List<Player>();

        List<Field> fields = new List<Field>();

        bool player1Turn = true;

        string message = "";

        Player winner;

        IDrawable drawer;

        public List<Player> Players
        {
            get => players;
            set => players = value;
        }

        public List<Field> Fields
        {
            get => fields;
            set => fields = value;
        }

        public bool Player1Turn
        {
            get => player1Turn;
            set => player1Turn = value;
        }

        public string Message
        {
            get => message;
            set => message = value;
        }

        public Player Winner
        {
            get => winner;
            set => winner = value;
        }

        public IDrawable Drawer
        {
            get => drawer;
            set => drawer = value;
        }

        public Game(IDrawable tableDrawer)
        {
            CheckIfArgumentsAreNull(tableDrawer);
            Drawer = tableDrawer;
        }

        void CheckIfArgumentsAreNull(IDrawable tableDrawer)
        {
            if (tableDrawer == null)
            {
                throw new ArgumentNullException();
            }
        }

        public void StartGame(Player player1, Player player2, string path1, string path2)
        {
            SetDefaultValues();
            CheckIfArgumentsAreNull(player1, player2, path1, path2);
            try
            {
                SetGameProperties(player1, player2, path1, path2);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }

        void SetGameProperties(Player player1, Player player2, string path1, string path2)
        {
            AddPlayers(player1, player2);
            AddFields(path1, path2);
            Drawer.DrawField(Players, Fields);
            Drawer.InformWhoseTurn(this);
        }

        void AddPlayers(Player player1, Player player2)
        {
            Players.Add(player1);
            Players.Add(player2);
        }

        void AddFields(string path1, string path2)
        {
            CreateField(path1);
            CreateField(path2);
        }

        void CreateField(string path)
        {
            Field field = new Field();
            Fields.Add(field);
            field.StartArrangement(path);
        }

        void SetDefaultValues()
        {
            Players.Clear();
            Fields.Clear();
            Player1Turn = true;
            Message = "";
            Winner = null;
        }

        void CheckIfArgumentsAreNull(Player player1, Player player2, string path1, string path2)
        {
            if (CheckIfPlayersNull(player1, player2) || CheckIfPathesNull(path1, path2))
            {
                throw new ArgumentNullException();
            }
        }

        bool CheckIfPathesNull(string path1, string path2)
        {
            if((path1 == null) || (path2 == null))
            {
                return true;
            }
            return false;
        }

        bool CheckIfPlayersNull(Player player1, Player player2)
        {
            if ((player1 == null) || (player2 == null))
            {
                return true;
            }
            return false;
        }

        public void CommitAStep(string dot)
        {
            Message = "";
            CheckIfDotIsNull(dot);
            StepStates howStepEnded = Step(dot);
            if (howStepEnded == StepStates.Missed)
            {
                Player1Turn = !Player1Turn;
            }
            else
            {
                if (howStepEnded == StepStates.CellShooted)
                {
                    Message = "Вы уже стреляли в эту клетку!";
                }
                else
                {
                    if (howStepEnded == StepStates.CellIncorrect)
                    {
                        Message = "Выбранной клетки не существует!";
                    }
                }
            }
            Winner = CheckWinner();
            Drawer.DrawField(Players, Fields);
            CheckMessage();
            EndStep();
        }

        void CheckMessage()
        {
            if (Message != "")
            {
                Drawer.WriteMessage(Message);
            }
        }

        void EndStep()
        {
            if (Winner == null)
            {
                Drawer.InformWhoseTurn(this);
            }
            else
            {
                EndGame();
            }
        }

        void EndGame()
        {
            Drawer.DrawField(Players, Fields);
            Drawer.SetWinner(Winner);
        }

        StepStates Step(string dot)
        {
            if (Player1Turn)
            {
                return Players[0].MakeAStep(Fields[1], dot);
            }
            return Players[1].MakeAStep(Fields[0], dot);
        }

        void CheckIfDotIsNull(string dot)
        {
            if (dot == null)
            {
                throw new ArgumentNullException();
            }
        }

        Player CheckWinner()
        {
            int i = 0;
            foreach (Field field in Fields)
            {
                if (CheckIfAllShipsWereDestroyed(field))
                {
                    return Players[1 - i];
                }
                i++;
            }
            return null;
        }

        bool CheckIfAllShipsWereDestroyed(Field field)
        {
            foreach (Ship ship in field.Ships)
            {
                foreach (Cell cell in ship.ReturnCellsOfThisShip())
                {
                    if (cell.ReturnCellState() == CellStates.ContainsShip)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public string ReturnPlayersName(int index)
        {
            return Players[index].Name;
        }
    }
}
