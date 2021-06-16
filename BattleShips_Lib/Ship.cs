using System.Collections.Generic;

namespace BattleShips_Lib
{
    public class Ship
    {
        List<Cell> cellsOfThisShip = new List<Cell>();
        internal List<Cell> CellsOfThisShip
        {
            get => cellsOfThisShip;
            set => cellsOfThisShip = value;
        }

        public List<Cell> ReturnCellsOfThisShip()
        {
            return this.CellsOfThisShip;
        }
    }
}
