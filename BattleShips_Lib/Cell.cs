using System;

namespace BattleShips_Lib
{
    public class Cell
    {
        int row;
        int col;

        Ship ship;

        CellStates state;

        public int Row
        {
            get => row;
            set => row = value;
        }
        public int Col
        {
            get => col;
            set => col = value;
        }

        internal Ship Ship
        {
            get => ship;
            set => ship = value;
        }

        public CellStates State
        {
            get => state;
            set => state = value;
        }

        public Cell(int row, int col)
        {
            if (CheckCoordinatesCorrectnes(row, col))
            {
                this.Row = row;
                this.Col = col;
            }
        }

        bool CheckCoordinatesCorrectnes(int row, int col)
        {
            if (CheckPositionCorrectness(row) && CheckPositionCorrectness(col))
            {
                return true;
            }
            throw new Exception("Ячейку с заданными координатами задать невозможно!");
        }

        bool CheckPositionCorrectness(int position)
        {
            if ((position >= 0) && (position <= 9))
            {
                return true;
            }
            return false;
        }

        public CellStates ReturnCellState()
        {
            return this.State;
        }

        public void SetCellState(CellStates cellstate)
        {
            this.State = cellstate;
        }

        public Ship ReturnShip()
        {
            return this.Ship;
        }
    }
}
