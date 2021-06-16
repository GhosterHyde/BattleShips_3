using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace BattleShips_Lib
{
    public class Field
    {
        ObservableCollection<ObservableCollection<Cell>> cells = new ObservableCollection<ObservableCollection<Cell>>();

        List<Ship> ships = new List<Ship>();

        List<string> stringsFromFile = new List<string>();

        public ObservableCollection<ObservableCollection<Cell>> Cells
        {
            get => cells;
            set => cells = value;
        }

        internal List<Ship> Ships
        {
            get => ships;
            set => ships = value;
        }

        public List<string> StringsFromFile
        {
            get => stringsFromFile;
            set => stringsFromFile = value;
        }

        public Field()
        {
            CreateField();
        }

        void CreateField()
        {
            if (Cells == null)
            {
                Cells = new ObservableCollection<ObservableCollection<Cell>>();
            }
            CreateCells();
        }

        void CreateCells()
        {
            for (int i = 0; i < 10; i++)
            {
                Cells.Add(new ObservableCollection<Cell>());
                for (int j = 0; j < 10; j++)
                {
                    Cells[i].Add(new Cell(i, j));
                }
            }
        }

        public void StartArrangement(string path)
        {
            try
            {
                if (CheckStringFromFile())
                {
                    int[] shipEmount = new int[4];
                    if (stringsFromFile.Count == 0)
                    {
                        ReadArrangementFromFile(path);
                    }
                    SetShips(shipEmount);
                }
            }
            catch (Exception e)
            {
                SendErrorMessage(e);
            }
        }

        bool CheckStringFromFile()
        {
            if (stringsFromFile == null)
            {
                throw new ArgumentNullException();
            }
            return true;
        }

        void ReadArrangementFromFile(string path)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    StringsFromFile.Add(line.ToUpper());
                }
            }
        }

        void CheckShipsEmount(int[] shipEmount)
        {
            for (int i = 0; i < 4; i++)
            {
                if (shipEmount[i] != 4 - i)
                {
                    throw new Exception("Количество кораблей не соответствует правилам игры!");
                }
            }
        }

        void SendErrorMessage(Exception e)
        {
            if (e.Message == "Value cannot be null.")
            {
                throw new ArgumentNullException();
            }
            if (e.Message.Contains("Could not find file"))
            {
                throw new Exception("Файл с расстановкой для игрока отутствует!");
            }
            throw new Exception(e.Message);
        }

        public void SetShips(int[] shipEmount)
        {
            CheckIfSthNull(shipEmount);
            foreach (string line in StringsFromFile)
            {
                HandleLine(line.Split(' '), shipEmount);
            }
            CheckShipsEmount(shipEmount);
        }

        void HandleLine(string[] lineArray, int[] shipEmount)
        {
            if (lineArray.Length == 3)
            {
                int shiplength = GetShipLength(lineArray[2]);
                Cell enteredCell = GetEnteredCell(lineArray[0]);
                AddNewShip(enteredCell, shiplength, lineArray[1]);
                shipEmount[shiplength - 1] = IncreaseSetShipEmount(shipEmount[shiplength - 1]);
            }
            else
            {
                throw new Exception("Установка корабля невозможна. Входная строка имеет неверный формат.");
            }
        }

        int IncreaseSetShipEmount(int emount)
        {
            return emount + 1;
        }

        Cell GetEnteredCell(string coordinates)
        {
            int enteredRow = GetEnteredRow(coordinates);
            int enteredCol = GetEnteredCol(coordinates);
            return Cells[enteredRow][enteredCol];
        }

        bool CheckIfEnvironmentCorrect(Cell enteredCell, int shiplength, string direction)
        {
            int startColIndex = enteredCell.Col + CorrectStartIndex(enteredCell.Col);
            int endColIndex = GetEndColIndex(enteredCell.Col, shiplength, direction);
            int startRowIndex = enteredCell.Row + CorrectStartIndex(enteredCell.Row);
            int endRowIndex = GetEndRowIndex(enteredCell.Row, shiplength, direction);
            CheckIfCellsNearFutureShipAreEmpty(Cells[startRowIndex][startColIndex], Cells[endRowIndex][endColIndex]);
            return true;
        }

        int GetEnteredCol(string startCoordinate)
        {
            try
            {
               int col = Convert.ToInt32(startCoordinate.Substring(1, startCoordinate.Length - 1)) - 1;
                return CheckEnteredCol(col);
            }
            catch (Exception)
            {
                throw new Exception("Установка корабля невозможна. Начальная координата имеет неверный формат.");
            }
        }

        int CheckEnteredCol(int col)
        {
            if (CheckIfCoordinateIsCorrect(col))
            {
                return col;
            }
            throw new Exception("Установка корабля невозможна. Корабль выходит за пределы поля.");
        }

        bool CheckIfCoordinateIsCorrect(int coordinate)
        {
            if ((coordinate >= 0) && (coordinate <= 9))
            {
                return true;
            }
            return false;
        }

        int GetEndColIndex(int col, int shipLength, string direction)
        {
            if (CheckIfDirectionHorizontal(direction))
            {
                if (col + shipLength <= 10)
                {
                    return col - 1 + shipLength + CorrectEndIndex(col + 1, shipLength, direction);
                }
                throw new Exception("Установка корабля невозможна. Корабль выходит за пределы поля.");
            }
            else
            {
                return col + CorrectEndIndex(col + 1, shipLength, direction);
            }
        }

        int GetEndRowIndex(int row, int shipLength, string direction)
        {
            if (CheckIfDirectionHorizontal(direction))
            {
                return row + CorrectEndIndex(row + 1, shipLength, direction);
            }
            else
            {
                if (row + shipLength <= 10)
                {
                    return row - 1 + shipLength + CorrectEndIndex(row + 1, shipLength, direction);
                }
                throw new Exception("Установка корабля невозможна. Корабль выходит за пределы поля.");
            }
        }

        int GetEnteredRow(string startCoordinate)
        {
            if (CheckIfLetterCoordinateCorrect(startCoordinate[0]))
            {
                return startCoordinate[0] - 'A';
            }
            else
            {
                throw new Exception("Установка корабля невозможна. Начальная координата имеет неверный формат.");
            }
        }

        bool CheckIfLetterCoordinateCorrect(char letter)
        {
            if ((letter >= 'A') && (letter <= 'J'))
            {
                return true;
            }
            return false;
        }

        int GetShipLength(string shipLength)
        {
            if (CheckIfShipLengthCorrect(shipLength))
            {
                return Convert.ToInt32(shipLength);
            }
            else
            {
                throw new Exception("Установка корабля невозможна. Длина корабля имеет неверный формат.");
            }

        }

        bool CheckIfShipLengthCorrect(string shipLength)
        {
            if ((shipLength.Length == 1) && (shipLength[0] >= '1') && (shipLength[0] <= '4'))
            {
                return true;
            }
            return false;
        }

        int CorrectEndIndex(int enteredArg, int shipLength, string direction)
        {
            if (CheckIfDirectionHorizontal(direction))
            {
                if (CheckIfIndexNotOutOfRange(enteredArg + shipLength))
                {
                    return 1;
                }
            }
            else
            {
                if (CheckIfIndexNotOutOfRange(enteredArg))
                {
                    return 1;
                }
            }
            return 0;
        }

        bool CheckIfIndexNotOutOfRange(int index)
        {
            if (index + 1 <= 9)
            {
                return true;
            }
            return false;
        }

        int CorrectStartIndex(int enteredArg)
        {
            if (enteredArg >= 1)
            {
                return -1;
            }
            return 0;
        }

        void CheckIfSthNull(int[] shipEmount)
        {
            if ((shipEmount == null) || (StringsFromFile == null))
            {
                throw new ArgumentNullException();
            }
        }

        void CheckIfCellsNearFutureShipAreEmpty(Cell startCell, Cell endCell)
        {
            for (int i = startCell.Row; i <= endCell.Row; i++)
            {
                for (int j = startCell.Col; j <= endCell.Col; j++)
                {
                    if (CheckIfCellContainsShip(Cells[i][j]))
                    {
                        throw new Exception("Установка корабля невозможна. Рядом находится другой корабль.");
                    }
                }
            }
        }

        bool CheckIfCellContainsShip(Cell cell)
        {
            try
            {
                if (cell.Ship != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new Exception("Клетки не существует!");
            }
        }

        void AddNewShip(Cell cell, int shiplength, string direction)
        {
            if (CheckIfEnvironmentCorrect(Cells[cell.Row][cell.Col], shiplength, direction))
            {
                Ship ship = new Ship();
                if (CheckIfDirectionHorizontal(direction))
                {
                    AddShipInAnyDirection(Cells[cell.Row][cell.Col], Cells[cell.Row][cell.Col + shiplength - 1], ship);
                }
                else
                {
                    AddShipInAnyDirection(Cells[cell.Row][cell.Col], Cells[cell.Row + shiplength - 1][cell.Col], ship);
                }
                ships.Add(ship);
            }
        }

        bool CheckIfDirectionHorizontal(string direction)
        {
            if (direction.ToUpper() == "H")
            {
                return true;
            }
            else
            {
                if (direction.ToUpper() == "V")
                {
                    return false;
                }
                else
                {
                    throw new Exception("Установка корабля невозможна. Направление корабля имеет неверный формат.");
                }
            }
        }

        void AddShipInAnyDirection(Cell startCell, Cell endCell, Ship ship)
        {
            for (int i = startCell.Row; i <= endCell.Row; i++)
            {
                for (int j = startCell.Col; j <= endCell.Col; j++)
                {
                    ship.CellsOfThisShip.Add(Cells[i][j]);
                    Cells[i][j].Ship = ship;
                    Cells[i][j].State = CellStates.ContainsShip;
                }
            }
        }

        public Cell ReturnCell(int row, int col)
        {
            return Cells[row][col];
        }

        public ObservableCollection<ObservableCollection<Cell>> ReturnCellOfThisField()
        {
            return this.Cells;
        }
    }
}
