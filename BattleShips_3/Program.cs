using System;
using BattleShips_Lib;

namespace BattleShip_3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                Console.WriteLine("Игрок 1, введите свое имя!");
                Player player1 = new Player(Console.ReadLine());
                Console.WriteLine("Игрок 2, введите свое имя!");
                Player player2 = new Player(CheckPlayer2Name(Console.ReadLine(), player1));
                Game newGame = new Game(new TableDrawer());
                newGame.StartGame(player1, player2, args[0], args[1]);
                if (newGame.Message == "")
                {
                    PlayTheGame(newGame);
                    Savior.SaveGameResults(newGame.Winner);
                }
                else
                {
                    ReportError(newGame);
                }
            }
            else
            {
                Console.WriteLine("Игра не может быть начата! Не указаны пути для 1 или нескольких входных файлов.");
            }
            Console.ReadKey();
        }

        static void ReportError(Game newGame)
        {
            newGame.Drawer.DrawField(newGame.Players, newGame.Fields);
            newGame.Drawer.WriteMessage("Игра не может быть начата.\n" + newGame.Message);
        }

        static void PlayTheGame(Game newGame)
        {
            while (newGame.Winner == null)
            {
                newGame.CommitAStep(Console.ReadLine().ToUpper());
            }
        }

        static string CheckPlayer2Name(string name, Player player1)
        {
            while (name == player1.Name)
            {
                Console.WriteLine("Имена игроков должны отличаться!");
                Console.WriteLine("Игрок 2, введите свое имя!");
                name = Console.ReadLine();
            }
            return name;
        }
    }
}