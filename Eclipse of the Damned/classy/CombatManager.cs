using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse_of_the_Damned.classy
{
    public class CombatManager
    {
       public Entity Enemy { get; set; }
       public int Turn { get; set; }
       public bool PlayerTurn { get; set; }
        public CombatManager(Entity enemy, int turn, bool playerTurn)
        {
            Enemy = enemy;
            Turn = turn;
            PlayerTurn = playerTurn;
        }

    }
}
