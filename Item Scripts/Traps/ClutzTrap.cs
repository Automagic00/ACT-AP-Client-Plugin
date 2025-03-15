using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTAP.Item_Scripts.Traps
{
    class ClutzTrap
    {
        public static void ActivateTrap()
        {
            if (Player.singlePlayer.HasEquippedShell)
                Player.singlePlayer.RemoveShell();

            if (Player.singlePlayer.equippedShellHammer != null)
                Player.singlePlayer.RemoveShellHammer();


            //Player.singlePlayer.HackeysackDrop();
            if (PassengerManager.instance.activeStowaways[0] != null)
                PassengerManager.instance.SetPassengerToInventory(0);

            if (PassengerManager.instance.activeStowaways[1] != null)
                PassengerManager.instance.SetPassengerToInventory(1);

            if (PassengerManager.instance.activeStowaways[2] != null)
                PassengerManager.instance.SetPassengerToInventory(2);

            Player.singlePlayer.AssignUmamiAttackToSlot(null, 0);
            Player.singlePlayer.AssignUmamiAttackToSlot(null, 1);
        }
    }
}
