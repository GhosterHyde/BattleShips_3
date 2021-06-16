using System;
using System.Collections.Generic;
using BattleShips_Lib;

namespace BattleShip_3
{
    public class TableDrawer : IDrawable
    {
        
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void SetWinner(Player winner)
        {
            Console.WriteLine("Победил " + winner.Name + " за " + winner.Count + " ходов!");
        }

        public void InformWhoseTurn(Game newGame)
        {
            newGame.Message = SetMessageWhoseTurn(newGame);
            WriteMessage(newGame.Message);
            newGame.Message = "";
        }

        string SetMessageWhoseTurn(Game newGame)
        {
            if (newGame.Player1Turn)
            {
                return "Ходит " + newGame.Players[0].Name;
            }
            else
            {
                return "Ходит " + newGame.Players[1].Name;
            }
        }

        public void DrawField(List<Player> Players, List<Field> Fields)
        {
            Console.Clear();
            WritePlayersNames(Players);
            DrawMainField(Fields);
        }

        void DrawMainField(List<Field> Fields)
        {
            const int sideBoderEmount = 1;
            int fieldSize = Fields[0].Cells.Count + sideBoderEmount;
            for (int i = 0; i < fieldSize; i++)
            {
                DrawLine(i, Fields);
            }
        }

        void DrawLine(int i, List<Field> Fields)
        {
            for (int k = 0; k < Fields.Count; k++)
            {
                if (i != 0)
                {
                    DrawMainLine(i, Fields[k]);
                }
                else
                {
                    DrawDigitLine(Fields[k]);
                }
                AddTabBetweenFields(i, k);
            }
        }

        void DrawMainLine(int i, Field field)
        {
            const string nonShootedCellMarker = "  ";
            const string shootedShipMarker = "X ";
            const string shootedEmptyCellMarker = "O ";
            const char firstRowMarker = 'A';
            Console.Write("{0}  ", (char)(Convert.ToInt32(firstRowMarker) + i - 1));
            for (int j = 0; j < field.Cells.Count; j++)
            {
                if (CheckIfCellNotShooted(field.Cells[i - 1][j]))
                {
                    Console.Write(nonShootedCellMarker);
                }
                else
                {
                    if (CheckIfCellContainShip(field.Cells[i - 1][j]))
                    {
                        Console.Write(shootedShipMarker);
                    }
                    else
                    {
                        Console.Write(shootedEmptyCellMarker);
                    }
                }
            }
        }

        bool CheckIfCellContainShip(Cell cell)
        {
            if (cell.State == CellStates.ContainsShootedShip)
            {
                return true;
            }
            return false;
        }

        bool CheckIfCellNotShooted(Cell cell)
        {
            if ((cell.State == CellStates.Empty) || (cell.State == CellStates.ContainsShip))
            {
                return true;
            }
            return false;
        }

        void WritePlayersNames(List<Player> Players)
        {
            Console.WriteLine("\t" + Players[0].Name + "\t\t\t\t\t" + Players[1].Name);
        }

        void DrawDigitLine(Field field)
        {
            Console.Write("   ");
            for (int j = 0; j < field.Cells.Count; j++)
            {
                Console.Write(j + 1 + " ");
            }
        }

        void AddTabBetweenFields(int i, int k)
        {
            if (k == 0)
            {
                Console.Write("\t\t");
                if (i != 0)
                {
                    Console.Write("\t");
                }
            }
            else
            {
                Console.WriteLine();
            }
        }
    }
}
