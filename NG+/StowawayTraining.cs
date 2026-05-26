using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
	[HarmonyPatch(typeof(StowawayUpgrade), "RefreshTrainingButton")]
    class StowawayTraining
    {
		[HarmonyPrefix]
		public static bool Prefix(StowawayUpgrade __instance)
		{
			bool NGPlusStowaways = Plugin.debugMode ? true : CrabFile.current.GetBool("ngplusSlots");

			Debug.Log("NG+ Stowaways: " + NGPlusStowaways);

			InventoryData.InventorySlot inventorySlot = CrabFile.current.inventoryData["StowawayTraining"];
			__instance.stowawayButton.SetActive(inventorySlot.amount < ((!NGPlusStowaways) ? 6 : 9));

			return false;
		}
	}
}
