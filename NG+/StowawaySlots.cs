using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
namespace ACTAP
{
    [HarmonyPatch(typeof(StatusPanel), "SetStowawaySlots")]
    class StowawaySlots
    {
        [HarmonyPrefix]
        static bool Prefix(StatusPanel __instance)
        {
			bool NGPlusStowaways = Plugin.debugMode? true :CrabFile.current.GetBool("ngplusSlots");

			if (Plugin.debugMode || Plugin.connection.connected)
			{
				__instance.passengerSlots[3].transform.parent.gameObject.SetActive(NGPlusStowaways);
				__instance.stowawayCounter.SetCounter(PassengerManager.instance.currentWeight);
				bool flag = false;
				for (int i = 0; i <= 3; i++)
				{
					__instance.passengerSlots[i].SetIcon(PassengerManager.instance.activeStowaways[i]);
					if (PassengerManager.instance.activeStowaways[i] && !PassengerManager.instance.activeStowaways[i].worksWhileNaked)
					{
						flag = true;
					}
				}
				__instance.stowawaysInactiveWhileNaked.enabled = !Player.singlePlayer.HasEquippedShell && PassengerManager.instance.currentWeight > 0 && flag;
				return false;
			}
			return true;
		}

    }
}
