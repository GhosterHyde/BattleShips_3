using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BattleShips_Lib
{
    public class Player
    {
        string name;

        int count;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Count
        {
            get => count;
            set => count = value;
        }

        public Player(string nameOfPlayer)
        {
            try
            {
                Name = nameOfPlayer;
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }

        public StepStates MakeAStep(Field enemyField, string dot)
        {
            CheckIfArgumentsAreNull(enemyField, dot);
            try
            {
                return ShootCell(enemyField, dot);
            }
            catch (Exception)
            {
                return StepStates.CellIncorrect;
            }
        }

        StepStates ShootCell(Field enemyField, string dot)
        {
            Cell cell = enemyField.ReturnCell(GetRow(dot), GetColumn(dot));
            if (CheckIfCellWasNotShooted(cell))
            {
                Count++;
                return ShootNonShootedCell(enemyField, cell);
            }
            else
            {
                return StepStates.CellShooted;
            }
        }

        StepStates ShootNonShootedCell(Field enemyField, Cell cell)
        {
            if (CheckIfCellNotContainsShip(cell))
            {
                GetCellAndChangeItState(cell, CellStates.EmptyShooted);
                return StepStates.Missed;
            }
            else
            {
                HitNonEmptyCell(enemyField, cell);
                return StepStates.Hit;
            }
        }

        void HitNonEmptyCell(Field enemyField, Cell cell)
        {
            GetCellAndChangeItState(cell, CellStates.ContainsShootedShip);
            TryToSankShip(enemyField, cell);
        }

        void TryToSankShip(Field enemyField, Cell cell)
        {
            if (CheckIfShipSank(cell))
            {
                HitAllCellsNearShootedShip(enemyField, cell);
            }
        }

        void GetCellAndChangeItState(Cell cell, CellStates cellState)
        {
            cell.SetCellState(cellState);
        }

        int GetRow(string dot)
        {
            return dot[0] - 'A';
        }

        int GetColumn(string dot)
        {
            return Convert.ToInt32(dot.Substring(1, dot.Length - 1)) - 1;
        }

        bool CheckIfCellNotContainsShip(Cell cell)
        {
            if (cell.Ship == null)
            {
                return true;
            }
            return false;
        }

        bool CheckIfCellWasNotShooted(Cell cell)
        {
            if ((cell.State != CellStates.EmptyShooted) && (cell.State != CellStates.ContainsShootedShip))
            {
                return true;
            }
            return false;
        }

        void CheckIfArgumentsAreNull(Field enemyField, string dot)
        {
            if ((enemyField == null) || (dot == null))
            {
                throw new ArgumentNullException();
            }
        }

        bool CheckIfShipSank(Cell shootedCell)
        {
            foreach (Cell cell in ReturnCellsOfShip(shootedCell.ReturnShip()))
            {
                if (CheckIfCellContainsShip(cell))
                {
                    return false;
                }
            }
            return true;
        }

        bool CheckIfCellContainsShip(Cell cell)
        {
            if (cell.State == CellStates.ContainsShip)
            {
                return true;
            }
            return false;
        }

        bool CheckIfCellWasEmpty(Cell cell)
        {
            if (cell.ReturnCellState() == CellStates.Empty)
            {
                return true;
            }
            return false;
        }

        void HitAllCellsNearShootedShip(Field enemyField, Cell shootedCell)
        {
            ObservableCollection<ObservableCollection<Cell>> cells = enemyField.ReturnCellOfThisField();
            foreach (Cell cell in ReturnCellsOfShip(shootedCell.ReturnShip()))
            {
                for (int i = cell.Row - 1; i <= cell.Row + 1; i++)
                {
                    for (int j = cell.Col - 1; j <= cell.Col + 1; j++)
                    {
                        if (CheckIfCellCoordinatesAreCorrect(i, j))
                        {
                            if (CheckIfCellWasEmpty(cells[i][j]))
                            {
                                cells[i][j].SetCellState(CellStates.EmptyShooted);
                            }
                        }
                    }
                }
            }
        }

        List<Cell> ReturnCellsOfShip(Ship ship)
        {
            return ship.ReturnCellsOfThisShip();
        }

        bool CheckIfCellCoordinatesAreCorrect(int row, int col)
        {
            if (CheckIfCoordinateIsCorrect(row) && CheckIfCoordinateIsCorrect(col))
            {
                return true;
            }
            return false;
        }

        bool CheckIfCoordinateIsCorrect(int coordinate)
        {
            if ((coordinate >= 0) && (coordinate <= 9))
            {
                return true;
            }
            return false;
        }
    }
}
