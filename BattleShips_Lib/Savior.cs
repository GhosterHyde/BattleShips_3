using System.IO;

namespace BattleShips_Lib
{
    public class Savior
    {
        public static void SaveGameResults(Player Winner)
        {
            StreamWriter sw = new StreamWriter("../../../output.txt", true, System.Text.Encoding.Default);
            sw.WriteLine("Победил " + Winner.Name + " за " + Winner.Count + " ходов!");
        }
    }
}
