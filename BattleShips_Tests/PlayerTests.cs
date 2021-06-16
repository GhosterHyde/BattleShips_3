using BattleShips_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleShips_Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void NameOfPlayerIsNull()
        {
            Player player;
            Assert.ThrowsException<ArgumentNullException>(() => player = new Player(null));
        }
        [TestMethod]
        public void enemyFieldIsNull()
        {
            Player player = new Player("player");
            Assert.ThrowsException<ArgumentNullException>(() => player.MakeAStep(null, "A2"));
        }
        [TestMethod]
        public void cellIsNull()
        {
            Player player = new Player("player");
            Assert.ThrowsException<ArgumentNullException>(() => player.MakeAStep(new Field(), null));
        }
        [TestMethod]
        public void IncorrectRowCoordinate()
        {
            Player player = new Player("player");
            Field enemyField = new Field();
            enemyField.StartArrangement("../../../../BattleShips_3/Field2.txt");
            string dot = "L2";
            Assert.AreEqual(StepStates.CellIncorrect, player.MakeAStep(enemyField, dot));
        }
        [TestMethod]
        public void IncorrectColCoordinate()
        {
            Player player = new Player("player");
            Field enemyField = new Field();
            enemyField.StartArrangement("../../../../BattleShips_3/Field2.txt");
            string dot = "A12";
            Assert.AreEqual(StepStates.CellIncorrect, player.MakeAStep(enemyField, dot));
        }
        [TestMethod]
        public void TwiceShootTheSameDotWithShip()
        {
            Player player = new Player("player");
            Field enemyField = new Field();
            enemyField.StartArrangement("../../../../BattleShips_3/Field2.txt");
            string dot = "A2";
            player.MakeAStep(enemyField, dot);
            Assert.AreEqual(StepStates.CellShooted, player.MakeAStep(enemyField, dot));
        }
        [TestMethod]
        public void TwiceShootTheSameEmptyDot()
        {
            Player player = new Player("player");
            Field enemyField = new Field();
            enemyField.StartArrangement("../../../../BattleShips_3/Field2.txt");
            string dot = "A1";
            player.MakeAStep(enemyField, dot);
            Assert.AreEqual(StepStates.CellShooted, player.MakeAStep(enemyField, dot));
        }
        [TestMethod]
        public void ShootEmptyDot()
        {
            Player player = new Player("player");
            Field enemyField = new Field();
            enemyField.StartArrangement("../../../../BattleShips_3/Field2.txt");
            string dot = "A1";
            Assert.AreEqual(StepStates.Missed, player.MakeAStep(enemyField, dot));
        }
        [TestMethod]
        public void ShootDotWithShip()
        {
            Player player = new Player("player");
            Field enemyField = new Field();
            enemyField.StartArrangement("../../../../BattleShips_3/Field2.txt");
            string dot = "A2";
            Assert.AreEqual(StepStates.Hit, player.MakeAStep(enemyField, dot));
        }
        [TestMethod]
        public void CheckDotNearShootedShip()
        {
            Player player = new Player("player");
            Field enemyField = new Field();
            enemyField.StartArrangement("../../../../BattleShips_3/Field2.txt");
            player.MakeAStep(enemyField, "A2");
            player.MakeAStep(enemyField, "A3");
            player.MakeAStep(enemyField, "A4");
            string dot = "A1";
            Assert.AreEqual(StepStates.CellShooted, player.MakeAStep(enemyField, dot));
        }
        [TestMethod]
        public void PlayerCreationWasSucces()
        {
            Player player = new Player("Name");
        }
    }
}
