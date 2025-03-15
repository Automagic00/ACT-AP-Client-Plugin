using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTAP.Item_Scripts.Traps
{
    class ShellShatterTrap
    {
        public static void Shatter()
        {
            if (Player.singlePlayer.HasEquippedShell)
            {
                Player.singlePlayer.BreakShell();
                Player.singlePlayer.equippedShell.Die();
            }
        }
    }
}
