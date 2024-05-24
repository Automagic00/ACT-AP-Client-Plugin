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

    /*[HarmonyPatch(typeof(Nephro), nameof(Nephro.Die))]
    class NephroKillLocation
    {
        [HarmonyPrefix]
        public static void DiePatch(Nephro __instance)
        {
            Debug.Log(__instance.transform.name);
            Plugin.ArchipelagoConnection.ReportLocation(0);
                
        }
    }*/

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
    [HarmonyPatch(typeof(UnlockItem), nameof(Item.ObtainItem))]
    class UnlockInvUpdate
    {
        [HarmonyPrefix]
        private static void LogUnlockItemsPatch(UnlockItem __instance)
        {
            //Create a JSON for Item Pickups
            string json = JsonUtility.ToJson(__instance);
            using (StreamWriter writeText = new StreamWriter("unlocks/" + __instance.choiceItem + ".txt"))
            {
                writeText.WriteLine(json);
            }

            //Read from JSON to swap items
            /*json = File.ReadAllText("items/00_BreadClaw (JunkCollectable).txt");
            JObject jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as JObject;
            
            JsonUtility.FromJsonOverwrite(json, __instance);*/

            //Debug.Log(__instance.item + ", " + __instance.amount);

        }

    }

    [HarmonyPatch(typeof (ShopButtonFlag), nameof (ShopButtonFlag.TryPurchase))]
    class ShopLog
    {
        [HarmonyPrefix]
        public static void LogShopItemPatch(ShopButtonFlag __instance)
        {
            string json = JsonUtility.ToJson(__instance.shopItemData.item);
            using (StreamWriter writeText = new StreamWriter("items/" + __instance.shopItemData.item + ".txt"))
            {
                writeText.WriteLine(json);
            }
        }
    }

    [HarmonyPatch(typeof(SkillTreeButtonFlag), nameof(SkillTreeButtonFlag.OnClick))]
    class SkillLog
    {
        [HarmonyPrefix]
        public static void LogShopItemPatch(SkillTreeButtonFlag __instance)
        {
            Debug.Log("SkillTreeTEST");
            string json = JsonUtility.ToJson(__instance.selectedData[0]);
            using (StreamWriter writeText = new StreamWriter("skills/" + __instance.selectedData[0].skill + ".txt"))
            {
                writeText.WriteLine(json);
            }
        }
    }

    //}
}
