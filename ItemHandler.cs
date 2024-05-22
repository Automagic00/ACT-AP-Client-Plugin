using BepInEx;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace ACTAP
{
    //[BepInPlugin("ACTPlugins.Automagic.Archipelago", "AP Randomizer", "0.0.0")]
    //public class ItemHandler : BaseUnityPlugin
    //{

    [HarmonyPatch(typeof(Nephro), nameof(Nephro.Die))]
    class NephroKillLocation
    {
        [HarmonyPrefix]
        public static void DiePatch(Nephro __instance)
        {
            Debug.Log(__instance.transform.name);
            Plugin.ArchipelagoConnection.ReportLocation(0);
                
        }
    }

    [HarmonyPatch(typeof(Boss), "TestEnemyDied")]
    class BossKillLocations
    {
        FieldInfo enemy = AccessTools.Field(typeof(Boss), "enemy");
        [HarmonyPrefix]
        private void TestEnemyDiedPatch(HitEvent killEvent, Boss __instance)
        {
                
            if (killEvent.target == __instance.GetEnemy())
            {
                Debug.Log(__instance.transform.name);
            }
        }
    }

    [HarmonyPatch(typeof(InventoryData), "OnInventoryUpdated")]
    class InvUpdate
    {
        [HarmonyPrefix]
        private void LogItemsPatch(InventoryData __instance)
        {
            Debug.Log(__instance.GetUmamiAttackNames());
        }

    }

    //}
}
