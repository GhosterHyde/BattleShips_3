using System.Collections.Generic;

namespace BattleShips_Lib
{
    public interface IDrawable
    {
        void WriteMessage(string message);
        
        void DrawField(List<Player> Players, List<Field> Fields);
        
        void SetWinner(Player winner);
        
        void InformWhoseTurn(Game game);
    }
}
