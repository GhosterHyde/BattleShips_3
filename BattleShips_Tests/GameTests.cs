using BattleShips_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BattleShip_3;
using System.IO;

namespace BattleShips_Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameCreationWithNullArgument()
        {
            Game game;
            Assert.ThrowsException<ArgumentNullException>(() => game = new Game(null));
        }
        [TestMethod]
        public void Player1IsNull()
        {
            TableDrawer tableDrawer = new TableDrawer();
            Game game = new Game(tableDrawer);
            Player player2 = new Player("player2");
            string path1 = "../../../Field1.txt";
            string path2 = "../../../Field2.txt";
            Assert.ThrowsException<ArgumentNullException>(() => game.StartGame(null, player2, path1, path2));
        }
        [TestMethod]
        public void Player2Null()
        {
            TableDrawer tableDrawer = new TableDrawer();
            Game game = new Game(tableDrawer);
            Player player1 = new Player("player1");
            string path1 = "../../../Field1.txt";
            string path2 = "../../../Field2.txt";
            Assert.ThrowsException<ArgumentNullException>(() => game.StartGame(player1, null, path1, path2));
        }
        [TestMethod]
        public void Path1IsNull()
        {
            TableDrawer tableDrawer = new TableDrawer();
            Game game = new Game(tableDrawer);
            Player player1 = new Player("player1");
            Player player2 = new Player("player2");
            string path2 = "../../../Field2.txt";
            Assert.ThrowsException<ArgumentNullException>(() => game.StartGame(player1, player2, null, path2));
        }
        [TestMethod]
        public void Path2IsNull()
        {
            TableDrawer tableDrawer = new TableDrawer();
            Game game = new Game(tableDrawer);
            Player player1 = new Player("player1");
            Player player2 = new Player("player2");
            string path1 = "../../../Field1.txt";
            Assert.ThrowsException<ArgumentNullException>(() => game.StartGame(player1, player2, path1, null));
        }
        [TestMethod]
        public void dotIsNull()
        {
            Game game = new Game(new TableDrawer());
            Assert.ThrowsException<ArgumentNullException>(() => game.CommitAStep(null));
        }
        [TestMethod]
        public void GameCreationWasSuccess()
        {
            Game game = new Game(new TableDrawer());
        }
        [TestMethod]
        public void GameStartedCorrect()
        {
            Game game = new Game(new TableDrawer());
            Player player1 = new Player("player1");
            Player player2 = new Player("player2");
            string path1 = "../../../Field1.txt";
            string path2 = "../../../Field2.txt";
            game.StartGame(player1, player2, path1, path2);
        }
        [TestMethod]
        public void StepEndedCorrect()
        {
            Game game = new Game(new TableDrawer());
            Player player1 = new Player("player1");
            Player player2 = new Player("player2");
            string path1 = "../../../../BattleShips_3/Field1.txt";
            string path2 = "../../../../BattleShips_3/Field2.txt";
            game.StartGame(player1, player2, path1, path2);
            //Ошибка возникла вследствие того, что отрисовывать поля не на чем
            //Оставшийся процесс пройден успешно
            Assert.ThrowsException<IOException>(() => game.CommitAStep("A2"));
        }
    }
}
