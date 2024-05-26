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

    //Log Location Paths
    [HarmonyPatch(typeof(Item), "Awake")]
    class InvUpdate
    {
        [HarmonyPrefix]
        private static void LogItemsPatch(Item __instance)
        {
            LocationSwapData.LogLocation(__instance);
        }

    }

    //Hook into item pickups
    [HarmonyPatch(typeof(Item), nameof(Item.Interact))]
    class PickupLocations
    {
        [HarmonyPrefix]
        private static bool PickupItemPatch(Item __instance)
        {
            __instance.ObtainItem();
            __instance.PickupVisually(0f);
            UnityEngine.Object.Destroy(__instance.gameObject, 0.2f);
            return false;
        }
    }

    //Fake Pickup Visuals


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
