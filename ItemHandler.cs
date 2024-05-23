using BepInEx;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.IO;
using Newtonsoft.Json.Linq;

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
        //FieldInfo enemy = AccessTools.Field(typeof(Boss), "enemy");
        [HarmonyPrefix]
        private static void TestEnemyDiedPatch(HitEvent killEvent, Boss __instance)
        {
                
            if (killEvent.target == __instance.GetEnemy())
            {
                Debug.Log(__instance.transform.name);
            }
        }
    }

    [HarmonyPatch(typeof(Item), nameof(Item.ObtainItem))]
    class InvUpdate
    {
        [HarmonyPrefix]
        private static void LogItemsPatch(Item __instance)
        {
            //Create a JSON for Item Pickups
            string json = JsonUtility.ToJson(__instance);
            using (StreamWriter writeText = new StreamWriter("items/" + __instance.item + ".txt"))
            {
                writeText.WriteLine(json);
            }

            //Read from JSON to swap items
            /*json = File.ReadAllText("items/00_BreadClaw (JunkCollectable).txt");
            JObject jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as JObject;
            
            JsonUtility.FromJsonOverwrite(json, __instance);*/

            Debug.Log(__instance.item + ", " + __instance.amount);

        }

    }

    //}
}
