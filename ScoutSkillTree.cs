using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;


namespace ACTAP
{
    [HarmonyPatch(typeof(Player),"Start")]
    class ScoutOnAwake
    {
        [HarmonyPrefix]
        public static void AwakeVoid()
        {
            if (Plugin.connection.session != null && Plugin.debugMode == false && Player.singlePlayer != null)
            {
                LocationDataTable.GenerateTable();
                ScoutSkillTree.ScoutAsync();
            }
        }
    }
    class ScoutSkillTree
    {
        public static async void ScoutAsync()
        {
            Debug.Log("In Scout Async");
            var connection = Plugin.connection;
            if (connection.session != null)
            {
                Debug.Log("Found Session");
                //LocationInfoPacket locPack;

                long[] ids = new long[] { LocationDataTable.FindSkillAPID("Skill_Shelleport"),
                    (LocationDataTable.FindSkillAPID("Skill_Skedaddle")),
                    (LocationDataTable.FindSkillAPID("Skill_Parries")),
                    (LocationDataTable.FindSkillAPID("Skill_Riposte")),
                    (LocationDataTable.FindSkillAPID("Skill_NakedParry")),
                    (LocationDataTable.FindSkillAPID("Skill_Aggravation")),
                    (LocationDataTable.FindSkillAPID("Skill_SelfRepair")),
                    (LocationDataTable.FindSkillAPID("Skill_Kintsugi")),
                    (LocationDataTable.FindSkillAPID("Skill_Skewer")),
                    (LocationDataTable.FindSkillAPID("Skill_Plunge")),
                    (LocationDataTable.FindSkillAPID("Skill_ScrapHammer")),
                    (LocationDataTable.FindSkillAPID("Skill_Dispatch")),
                    (LocationDataTable.FindSkillAPID("Skill_Fishing")),
                    (LocationDataTable.FindSkillAPID("Skill_WaveBreaker")),
                    (LocationDataTable.FindSkillAPID("Skill_AirDodge")),
                    (LocationDataTable.FindSkillAPID("Skill_Housewarming")),
                    (LocationDataTable.FindSkillAPID("Skill_ChumInTheWater")),
                    (LocationDataTable.FindSkillAPID("Skill_ElusivePrey")),
                    (LocationDataTable.FindSkillAPID("Skill_EbbAndFlow")),
                    (LocationDataTable.FindSkillAPID("Skill_UmamiTrainingA")),
                    (LocationDataTable.FindSkillAPID("Skill_UmamiTrainingB")),
                    (LocationDataTable.FindSkillAPID("Skill_UmamiTrainingC"))
                };
                
                

                //ScoutedItemInfo locPack;
                //ictionary<long, ScoutedItemInfo> test = await connection.session.Locations.ScoutLocationsAsync(ids);
                //test.TryGetValue(idToTest, out locPack);
                Debug.Log("Begin Scout");
                try
                {
                    var locPack = await connection.session.Locations.ScoutLocationsAsync(ids);

                    foreach (ItemInfo itemInfo in locPack.Values)
                    {
                        Debug.Log("ItemInfo Found " + ids.Length);
                        CrabFile.current.SetString(itemInfo.LocationDisplayName + "ItemName", itemInfo.ItemName);
                        CrabFile.current.SetString(itemInfo.LocationDisplayName + "PlayerName", itemInfo.Player.Name);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
                


                //locPack = await connection.session.Locations.ScoutLocationsAsync(ids);

                /*foreach (var loc in locPack.Locations)
                {
                    CrabFile.current.SetString( connection.GetLocationName(loc.Location) + "ItemName", connection.GetItemName(loc.Item));
                    CrabFile.current.SetString(connection.GetLocationName(loc.Location) + "PlayerName", connection.session.Players.GetPlayerName(loc.Player));
                }*/
                
            }
        }
    }
}
