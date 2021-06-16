using BattleShips_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleShips_Tests
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void PathToFileWithStartArrangementIsNull()
        {
            Field field = new Field();
            Exception result = Assert.ThrowsException<Exception>(() => field.StartArrangement(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'path')", result.Message);
        }
        [TestMethod]
        public void StringsFromFileIsNullInStartArrangement()
        {
            Field field = new Field();
            field.StringsFromFile = null;
            Assert.ThrowsException<ArgumentNullException>(() => field.StartArrangement("Field1.txt"));
        }
        [TestMethod]
        public void FileWithStartArrangementIsMissing()
        {
            Field field = new Field();
            string path = "../../../MissingField.txt";
            Exception result = Assert.ThrowsException<Exception>(() => field.StartArrangement(path));
            Assert.AreEqual("Файл с расстановкой для игрока отутствует!", result.Message);
        }
        [TestMethod]
        public void EmountOfShipsListIsNull()
        {
            Field field = new Field();
            Assert.ThrowsException<ArgumentNullException>(() => field.SetShips(null));
        }
        [TestMethod]
        public void StringsFromFileIsNull()
        {
            Field field = new Field();
            field.StringsFromFile = null;
            int[] shipEmount = new int[4];
            Assert.ThrowsException<ArgumentNullException>(() => field.SetShips(shipEmount));
        }
        [TestMethod]
        public void ListOfStringsFromFileContainsIncorrectDataLength()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            //Отсутствует длина корабля
            field.StringsFromFile.Add("G5 v");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Входная строка имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void StartCoordinateRowIncorrectData()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            //Клетки L1 не существует
            field.StringsFromFile.Add("L1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Начальная координата имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void StartCoordinateColumnIncorrectData()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            //Клетки ES не существует
            field.StringsFromFile.Add("ES h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Начальная координата имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void ShipLengthMoreThenFour()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            //Длина корабля должна быть от 1 до 4
            field.StringsFromFile.Add("I1 v 5");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Длина корабля имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void ShipLengthLessThenOne()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            //Длина корабля должна быть от 1 до 4
            field.StringsFromFile.Add("I1 v 0");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Длина корабля имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void ShipLengthLengthMoreThenOne()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            //Длина строки данных о длине корабля должна быть равна 1
            field.StringsFromFile.Add("I1 v 11");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Длина корабля имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void ShipLengthLengthLessThenOne()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            //Длина строки данных о длине корабля должна быть равна 1
            field.StringsFromFile.Add("I1 v ");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Длина корабля имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void IncorrectShipDirection_Empty()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            //Отствует направление
            field.StringsFromFile.Add("B6  4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Направление корабля имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void IncorrectShipDirection_LengthMoreThenOne()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            //Направления vv не существует
            field.StringsFromFile.Add("B6 vv 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Направление корабля имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void IncorrectShipDirection_SymbolIsNotVNeitherH()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            //Направления l не существует
            field.StringsFromFile.Add("B6 l 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Направление корабля имеет неверный формат.", result.Message);
        }
        [TestMethod]
        public void StartCoordinateOutOfField()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            //Стартовая координата уже находится за пределами поля
            field.StringsFromFile.Add("J12 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Корабль выходит за пределы поля.", result.Message);
        }
        [TestMethod]
        public void VerticalShipIsOutOfField()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            //Корабль заходит за пределы поля по вертикали
            field.StringsFromFile.Add("J4 v 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Корабль выходит за пределы поля.", result.Message);
        }
        [TestMethod]
        public void HorizontalShipIsOutOfField()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            //Корабль заходит за пределы поля по горизонтали
            field.StringsFromFile.Add("J10 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Корабль выходит за пределы поля.", result.Message);
        }
        [TestMethod]
        public void ShipStandsOnAnotherShip()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            //Корабль становится на тот, что уже поставили ранее
            field.StringsFromFile.Add("A4 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Рядом находится другой корабль.", result.Message);
        }
        [TestMethod]
        public void ShipStandsCloseToAnotherShip()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            //Корабль стоит слишком близко к другому, поставленному ранее
            field.StringsFromFile.Add("A5 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Установка корабля невозможна. Рядом находится другой корабль.", result.Message);
        }
        [TestMethod]
        public void IrregularShipEmount_MoreThen10()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            //Лишний корабль
            field.StringsFromFile.Add("C1 h 1");
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Количество кораблей не соответствует правилам игры!", result.Message);
        }
        [TestMethod]
        public void IrregularShipEmount_LessThen10()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            //Всего 9 кораблей
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Количество кораблей не соответствует правилам игры!", result.Message);
        }
        [TestMethod]
        public void IrregularShipEmount_IncorrectEmountOf1and2Stages()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 1");
            field.StringsFromFile.Add("J8 h 1");
            //В итоге получается 5 однопалубных кораблей и 2 двупалубных, вместо 4 и 3 соответственно
            Exception result = Assert.ThrowsException<Exception>(() => field.SetShips(shipEmount));
            Assert.AreEqual("Количество кораблей не соответствует правилам игры!", result.Message);
        }
        [TestMethod]
        public void AllShipsSetCorrect()
        {
            Field field = new Field();
            int[] shipEmount = new int[4];
            field.StringsFromFile.Add("A2 h 3");
            field.StringsFromFile.Add("A10 v 2");
            field.StringsFromFile.Add("B6 v 4");
            field.StringsFromFile.Add("D9 v 2");
            field.StringsFromFile.Add("E1 h 3");
            field.StringsFromFile.Add("G5 v 1");
            field.StringsFromFile.Add("H7 v 1");
            field.StringsFromFile.Add("I1 v 1");
            field.StringsFromFile.Add("J4 h 2");
            field.StringsFromFile.Add("J8 h 1");
            field.SetShips(shipEmount);
        }
        [TestMethod]
        public void FieldCreationWasSuccess()
        {
            Field field = new Field();
        }
        [TestMethod]
        public void StartArrangementWasSuccess()
        {
            Field field = new Field();
            field.StartArrangement("../../../../BattleShips_3/Field1.txt");
        }
    }
}
