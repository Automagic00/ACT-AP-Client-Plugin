using BepInEx;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public static void DebugLogger(Scene s, LoadSceneMode m)
        {
            Debug.Log("");
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
