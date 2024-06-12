using BepInEx;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.IO;
using Newtonsoft.Json.Linq;
using Archipelago.MultiClient.Net.Models;

namespace ACTAP
{
    /// <summary>
    /// Sends a location check when a boss is defeated
    /// </summary>
    [HarmonyPatch(typeof(Boss), "TestEnemyDied")]
    class BossKillLocations
    {
        //FieldInfo enemy = AccessTools.Field(typeof(Boss), "enemy");
        [HarmonyPrefix]
        private static void TestEnemyDiedPatch(HitEvent killEvent, Boss __instance)
        { 
            if (killEvent.target == __instance.GetEnemy())
            {

                long apid = LocationSwapData.BossPathToAPID(__instance.bossName);
                Debug.Log(__instance.bossName);
                Debug.Log("Assigned Location ID: " + (apid - 483021700));

                if (Plugin.debugMode == true)
                {
                    return;
                }

                if (apid - 483021700 != -1)
                {
                    Plugin.GetConnection().ActivateCheck(apid);
                    if (apid - 483021700 == 44)
                    {
                        Plugin.GetConnection().SendCompletion();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Prevents bosses from placing drop items in player inventory
    /// </summary>
    [HarmonyPatch(typeof(Enemy), "AcquireItems")]
    class BossKillDropLocations
    {
        [HarmonyPrefix]
        private static bool BossDropPatch(Enemy __instance)
        {
            if (__instance.isBoss)
            {
                return false;
            }
            return true;
        }
    
    }


    /// <summary>
    /// Log Location Paths
    /// </summary>
    [HarmonyPatch(typeof(SaveStateKillableEntity), "LoadFromFile")]
    class InvUpdate
    {
        [HarmonyPostfix]
        private static void LogItemsPatch(SaveStateKillableEntity __instance)
        {
            if (__instance.GetComponent<Item>() != null && __instance.GetComponent<Item>().name != "ForkUnlock(1)" && __instance.GetComponent<Item>().name != "Item_HeartkelpPodsUnlock")
            {
                if (Plugin.debugMode && Plugin.removePickups)
                {
                    if (LocationSwapData.ItemPickupUUIDToAPID(__instance.GetComponent<Item>()) != -1)
                    {
                        //Aggro.LogError(__instance.gameObject.name + " Already killed: deleting.", null);
                        Entity component = __instance.GetComponent<Entity>();
                        if (component)
                        {
                            GameManager.events.TriggerEntityDestroyedFromSave(component);
                        }
                        GameObject.Destroy(__instance.gameObject);
                    }
                }
            }
            /*FieldInfo field = AccessTools.Field(typeof(Item), "save");
            SaveStateKillableEntity save =  new SaveStateKillableEntity();
            save = (SaveStateKillableEntity)field.GetValue(__instance);
            save.LoadFromFile();



            string json = JsonUtility.ToJson(save.UUID);
            

            int apLocationID = 0;
            Debug.Log("Field: " + json);*/
        }
    }

    /// <summary>
    /// Hook into item pickups
    /// </summary>
    [HarmonyPatch(typeof(Item), nameof(Item.Interact))]
    class PickupLocations
    {
        
        [HarmonyPrefix]
        private static bool PickupItemPatch(Item __instance)
        {
            Debug.Log(__instance.name);
            if (__instance.name == "ForkUnlock (1)" || __instance.name == "Item_HeartkelpPodsUnlock" || __instance.name == "Item_HeartkelpPod(Clone)")
            {
                return true;
            }

            LocationSwapData.LogLocation(__instance);

            if (Plugin.debugMode == true)
            {
                return true;
            }
            
            

            long idToTest = LocationSwapData.ItemPickupUUIDToAPID(__instance);

            

            if (idToTest - 483021700 == -1 || idToTest - 483021700 == 0)
            {
                return true;
            }

            CompleteCheck.CheckCoroutine(__instance, idToTest);

            return false;
        }
    }

    class CompleteCheck
    { 


        public static async void CheckCoroutine(Item __instance, long idToTest)
        {
            
            __instance.hideNotification = true;
            ArchipelagoSession session = Plugin.GetConnection().session;

            LocationInfoPacket locPack = await session.Locations.ScoutLocationsAsync(idToTest);
            NetworkItem testNetItem = locPack.Locations[0];

            //Custom Visual if item is not for crabgame
            if (testNetItem.Player != session.ConnectionInfo.Slot)
            {
                __instance.PickupVisually(0f);
                ItemSwapData.CustomItemVisual(testNetItem);
            }
            //__instance.PickupVisually(0f);
            UnityEngine.Object.Destroy(__instance.gameObject, 0.2f);
            Plugin.GetConnection().ActivateCheck(idToTest);
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
