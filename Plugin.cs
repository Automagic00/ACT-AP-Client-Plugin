using BepInEx;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using System;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;


namespace ACTAP
{

    [BepInPlugin("ACTPlugins.Automagic.Archipelago", "AP Randomizer", "0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            SceneManager.sceneLoaded += DebugLogger;

            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            var harmony = new Harmony("com.example.patch");
            harmony.PatchAll();
        }

        MethodInfo method = AccessTools.Method(typeof(Item), "ObtainItem");
        public void DebugLogger(Scene s, LoadSceneMode m)
        {
            //object = GameObject.Find("")
            //Debug.Log(ToString( method.Invoke()));
            string json = JsonUtility.ToJson(CrabFile.current.inventoryData);

            List<string> namelist = new List<string>();
            foreach (var item in InventoryMasterList.staticList )
            {
                json = JsonUtility.ToJson(item);
                using (StreamWriter writeText = new StreamWriter("namelist/" + item.name + ".txt"))
                {
                    writeText.WriteLine(json);
                }
            }
            

        }

        [HarmonyPatch(typeof(Player), "Update")]
        class PlayerPatch
        {
            [HarmonyPrefix]
            public static void updatePatch()
            {
                //Debug.Log("We in it");
                if (Input.GetKeyDown(KeyCode.F3))
                {
                    Debug.Log("F3 Pressed");
                    //string json = File.ReadAllText("items/00_BreadClaw (JunkCollectable).txt");
                    //Item item = new Item();
                    //item = ItemSwapData.GetItem(ItemSwapData.ItemEnum.BloodStarLimb);
                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.SpectralTentacle);
                    //JsonUtility.FromJsonOverwrite(json, item);

                    //item.ObtainItem();
                    /*string json = File.ReadAllText("outInv.txt");
                    JObject jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as JObject;
                    JToken jsonToken = jsonObj.SelectToken("inventory[18].amount");
                    jsonToken.Replace(400);

                    string updatedJson = jsonObj.ToString();
                    File.WriteAllText("outInv.txt", updatedJson);
                    JsonUtility.FromJsonOverwrite(updatedJson, CrabFile.current.storeData);*/
                }

                if (Input.GetKeyDown(KeyCode.F4))
                {
                    Debug.Log("F4 Pressed");

                    SkillTreeData skillTree = new SkillTreeData();
                    
                    skillTree.SetSkill(SkillTreeUnlocks.Skedaddle, true, false);
                }
            }
        }

        public static class ArchipelagoConnection
        {
            private static ArchipelagoSession session = null;
            private static bool _connected = false;

            private static async Task<bool> Connect()
            {
                if (_connected) return false;

                ArchipelagoSession sesh = ArchipelagoSessionFactory.CreateSession("localhost", 38281);

                RoomInfoPacket packet = null;

                while (packet == null)
                {
                    try
                    {
                        packet = await sesh.ConnectAsync();
                    }
                    catch (Exception)
                    {
                    }
                }
                return true;
            }

            public static void ReportLocation(long id)
            {
                if (session != null)
                {
                    session.Locations.CompleteLocationChecks(id);
                }
            }

            public static void ScoutLocation(long id)
            {
                if (session != null)
                {
                    session.Locations.ScoutLocationsAsync(id);
                }
            }

            public static string GetLocationName(long id)
            {
                string locationName = session.Locations.GetLocationNameFromId(id);
                return locationName;
            }

            public static long GetLocationID(string name)
            {
                long id = session.Locations.GetLocationIdFromName("Another Crab's Treasure",name);
                return id;
            }

            public static string GetItemName(long id)
            {
                string name = session.Items.GetItemName(id) ?? $"Item: {id}";
                return name;
            }
        }


    }
}
