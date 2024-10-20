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

                long apid = LocationDataTable.BossPathToAPID(__instance.bossName);
                Debug.Log(__instance.bossName);
                Debug.Log("Assigned Location ID: " + (apid - 483021700));

                if (Plugin.debugMode == true)
                {
                    return;
                }

                if (apid != -1)
                {
                    Plugin.GetConnection().ActivateCheck(apid);
                    /*if (apid == 44)
                    {
                        Plugin.GetConnection().SendCompletion();
                    }*/
                }
            }
        }
    }
    [HarmonyPatch(typeof(Boss), "TestEnemyDiedVisually")]
    class BossKillLocationsBackup
    {
        [HarmonyPrefix]
        private static void DieVisuallyPatch(Entity e,Boss __instance)
        {
            if (e == __instance.GetEnemy())
            {
                long apid = LocationDataTable.BossPathToAPID(__instance.bossName);
                Debug.Log(__instance.bossName);
                Debug.Log("Assigned Location ID: " + (apid - 483021700));

                if (Plugin.debugMode == true)
                {
                    return;
                }

                if (apid != -1)
                {
                    Plugin.GetConnection().ActivateCheck(apid);
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
    [HarmonyPatch(typeof(Enemy), "PlaceDropsIntoData")]
    class BossKillDropLocations2
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

    [HarmonyPatch(typeof(Shell),"Interact")]
    class HomeShellCompletion
    {
        [HarmonyPrefix]
        public static void InteractPatch(Shell __instance)
        {
            Debug.Log(__instance.name);
            if (__instance.name == "Shell_HomeShell_1" && Plugin.connection.session != null)
            {
                Plugin.GetConnection().SendCompletion();
            }
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
            if (__instance.GetComponent<Item>() != null && __instance.GetComponent<Item>().name != "Item_HeartkelpPodsUnlock")
            {
                if (Plugin.itemHolder == null)
                {
                    Plugin.itemHolder = GameObject.Instantiate( __instance.gameObject);
                    Plugin.itemHolder.transform.position = new Vector3(0, -50, 0);
                    Debug.Log("Item Held " + __instance.name);
                }

                if (Plugin.debugMode && Plugin.removePickups)
                {
                    //long testid = LocationSwapData.ItemPickupUUIDToAPID(__instance.GetComponent<Item>()) - 483021700;
                    long testid = LocationDataTable.FindPickupAPID(__instance.GetComponent<Item>()) - 483021700;
                    if (testid != -1)
                    {
                        Debug.Log(__instance.gameObject.name + " Already killed: deleting. ID: " + testid);
                        Entity component = __instance.GetComponent<Entity>();
                        if (component)
                        {
                            GameManager.events.TriggerEntityDestroyedFromSave(component);
                        }
                        GameObject.Destroy(__instance.gameObject);
                        
                    }
                }
                else if (Plugin.connection.session != null)
                {
                    long testid = LocationDataTable.FindPickupAPID(__instance.GetComponent<Item>());
                    if (CrabFile.current.GetInt("LocationChecked-" + testid) == 1)
                    {
                        Debug.Log(__instance.gameObject.name + " Already killed: deleting. ID: " + testid);
                        Entity component = __instance.GetComponent<Entity>();
                        if (component)
                        {
                            GameManager.events.TriggerEntityDestroyedFromSave(component);
                        }
                        GameObject.Destroy(__instance.gameObject);
                    }
                }
            }
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
            if ( __instance.name == "Item_HeartkelpPodsUnlock" || __instance.name == "Item_HeartkelpPod(Clone)")
            {
                return true;
            }
            if (Plugin.debugMode == true)
            {
                if (__instance.name == "FishingLineUnlock")
                {
                    Debug.Log("Send Fishing Line Here");
                }
                    return true;
            }


            if (__instance.name == "ForkUnlock (1)")
            {
                CompleteCheck.CheckCoroutine(__instance, 483021701);
                return false;
            }
            else if (__instance.name == "FishingLineUnlock")
            {
                Debug.Log("Pickup Fishing Line Location");
                CompleteCheck.CheckCoroutine(__instance, 483021702);
                return false;
            }


            LocationSwapData.LogLocation(__instance);

            


            //long idToTest = LocationSwapData.ItemPickupUUIDToAPID(__instance);
            long idToTest = LocationDataTable.FindPickupAPID(__instance);


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

            //LocationInfoPacket locPack = await session.Locations.ScoutLocationsAsync(idToTest);
            
            ScoutedItemInfo locPack;
            Dictionary<long, ScoutedItemInfo> test = await session.Locations.ScoutLocationsAsync(idToTest);
            test.TryGetValue(idToTest, out locPack);

            ItemInfo testNetItem = locPack;

            //Custom Visual if item is not for crabgame
            if (testNetItem.Player != session.ConnectionInfo.Slot)
            {
                __instance.PickupVisually(0f);
                ItemSwapData.CustomItemVisual(testNetItem);
            }
            //__instance.PickupVisually(0f);
            UnityEngine.Object.Destroy(__instance.gameObject, 0.2f);

            if (CrabFile.current.GetInt("LocationChecked-" + idToTest) != 1)
            {
                Plugin.GetConnection().ActivateCheck(idToTest);
            }
        }
    }


    /*[HarmonyPatch(typeof (ShopButtonFlag), nameof (ShopButtonFlag.TryPurchase))]
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
    }*/

    /// <summary>
    /// Output save file
    /// </summary>
    [HarmonyPatch(typeof (Player), "Start")]
    class progressLog
    {
        [HarmonyPrefix]
        private static void Start()
        {
            if (Plugin.debugMode)
            {
                CrabFile crabFile = GameManager.instance.activeCrabfile;

                string json = JsonUtility.ToJson(crabFile.progressData);
                using (StreamWriter writeText = new StreamWriter("crabfile/progressdata.json"))
                {
                    writeText.WriteLine(json);
                }
                json = JsonUtility.ToJson(crabFile.locationData);
                using (StreamWriter writeText = new StreamWriter("crabfile/locationdata.json"))
                {
                    writeText.WriteLine(json);
                }
                json = JsonUtility.ToJson(crabFile.storeData);
                using (StreamWriter writeText = new StreamWriter("crabfile/storedata.json"))
                {
                    writeText.WriteLine(json);
                }
                json = JsonUtility.ToJson(crabFile.valueTable);
                using (StreamWriter writeText = new StreamWriter("crabfile/valuetable.json"))
                {
                    writeText.WriteLine(json);
                }
                json = JsonUtility.ToJson(crabFile.assistTable);
                using (StreamWriter writeText = new StreamWriter("crabfile/assisttable.json"))
                {
                    writeText.WriteLine(json);
                }
                json = JsonUtility.ToJson(crabFile.inventoryData);
                using (StreamWriter writeText = new StreamWriter("crabfile/inventorydata.json"))
                {
                    writeText.WriteLine(json);
                }
                json = JsonUtility.ToJson(crabFile.unlocks);
                using (StreamWriter writeText = new StreamWriter("crabfile/unlocks.json"))
                {
                    writeText.WriteLine(json);
                }
                json = JsonUtility.ToJson(crabFile);
                using (StreamWriter writeText = new StreamWriter("crabfile/wholefile.json"))
                {
                    writeText.WriteLine(json);
                }
            }
        }
    }

    /// <summary>
    /// Output Bools Set to Save
    /// </summary>
    [HarmonyPatch(typeof(CrabFile),"SetBool")]
    class BoolOutput
    {
        [HarmonyPrefix]
        public static bool SetBoolPatch(ref string index, bool value)
        {
            
            Debug.Log("[Bool] " + index + " : " + value);
            return true;
        }
    }
    /// <summary>
    /// Output Ints Set to Save
    /// </summary>
    [HarmonyPatch(typeof(CrabFile), "SetInt")]
    class IntOutput
    {
        [HarmonyPrefix]
        public static bool SetIntPatch(ref string index, int value)
        {
            
            Debug.Log("[Int] " + index + " : " + value);
            return true;
        }
    }
    /// <summary>
    /// Output Bools Set to Save
    /// </summary>
    [HarmonyPatch(typeof(CrabFile), "SetAssistBool")]
    class AssistBoolOutput
    {
        [HarmonyPrefix]
        public static bool SetAssistBoolPatch(ref string index, bool value)
        {

            Debug.Log("[AssistBool] " + index + " : " + value);
            return true;
        }
    }
    /// <summary>
    /// Output Ints Set to Save
    /// </summary>
    [HarmonyPatch(typeof(CrabFile), "SetAssistInt")]
    class AssistIntOutput
    {
        [HarmonyPrefix]
        public static bool SetAssistIntPatch(ref string index, int value)
        {

            Debug.Log("[AssistInt] " + index + " : " + value);
            return true;
        }
    }
    /// <summary>
    /// Output Ints Set to Save
    /// </summary>
    [HarmonyPatch(typeof(CrabFile), "SetString")]
    class StringOutput
    {
        [HarmonyPrefix]
        public static bool SetStringPatch(ref string index, ref string value)
        {

            Debug.Log("[String] " + index + " : " + value);
            return true;
        }
    }
    /// <summary>
    /// Output Ints Set to Save
    /// </summary>
    /*[HarmonyPatch(typeof(CrabFile), nameof(CrabFile.)]
    class FloatOutput
    {
        [HarmonyPrefix]
        public static bool SetFloatPatch(ref string index, float value)
        {

            Debug.Log("[Float] " + index + " : " + value);
            return true;
        }
    }*/
    /// <summary>
    /// Output Ints Set to Save
    /// </summary>
    [HarmonyPatch(typeof(CrabFile), "SetVector3")]
    class Vector3Output
    {
        [HarmonyPrefix]
        public static bool SetVector3Patch(ref string index, Vector3 value)
        {

            Debug.Log("[Int] " + index + " : " + value);
            return true;
        }
    }

    /*[HarmonyPatch(typeof(SkillTreeButtonFlag), nameof(SkillTreeButtonFlag.OnClick))]
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
    }*/
}
