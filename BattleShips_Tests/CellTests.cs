using BattleShips_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleShips_Tests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void CellCreationWithRowCoordinateBelowZero()
        {
            Exception result = Assert.ThrowsException<Exception>(() => new Cell(-1, 0));
            Assert.AreEqual("Ячейку с заданными координатами задать невозможно!", result.Message);
        }
        [TestMethod]
        public void CellCreationWithRowCoordinateUpperNine()
        {
            Exception result = Assert.ThrowsException<Exception>(() => new Cell(10, 0));
            Assert.AreEqual("Ячейку с заданными координатами задать невозможно!", result.Message);
        }
        [TestMethod]
        public void CellCreationWithColumnCoordinateBelowZero()
        {
            Exception result = Assert.ThrowsException<Exception>(() => new Cell(0, -1));
            Assert.AreEqual("Ячейку с заданными координатами задать невозможно!", result.Message);
        }
        [TestMethod]
        public void CellCreationWithRowCoordinateUpper9()
        {
            Exception result = Assert.ThrowsException<Exception>(() => new Cell(0, 10));
            Assert.AreEqual("Ячейку с заданными координатами задать невозможно!", result.Message);
        }
        [TestMethod]
        public void CellCreationWasCorrect()
        {
            Cell cell = new Cell(0, 1);
        }
    }
}
