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
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ACTAP
{

    static class LocationSwapData
    {
        
        public static void LogLocation(Item itemPickup)
        {
            FieldInfo field = AccessTools.Field(typeof(Item), "save");
            SaveStateKillableEntity save = new SaveStateKillableEntity();
            save = (SaveStateKillableEntity)field.GetValue(itemPickup);

            //long test = LocationSwapData.ItemPickupUUIDToAPID(itemPickup) - 483021700;
            long test = LocationDataTable.FindPickupAPID(itemPickup) - 483021700;
            Debug.Log(save.UUID);

            if (test == -1)
            {
                Debug.Log("No ID currently assigned");
            }
            else
            {
                Debug.Log("AP Location ID: " + test);
            }

            Debug.Log("Item Coords: " + itemPickup.transform.position);
        }
        public static string GetPath(this Transform current)
        {
            if (current.parent == null)
            {
                return current.gameObject.scene.name + "/" + current.name;
            }
            return GetPath(current.parent) + "/" + current.name;
        }
        public static long BossPathToAPID(string path)
        {
            long baseid = 483021700;
            switch (path)
            {
                case "": return baseid -1;
                case "Boss_NephroCaptainoftheGuard": return baseid + 3;
                case "Boss_Duchess": return baseid + 44;
                case "Boss_Bruiser": return baseid + 46; //
                case "Boss_RoyalShellsplitter": return baseid + 47; //
                case "Boss_Pagurus": return baseid + 48; //Pagurus
                /*case "": return baseid + 49; //lycanthrope
                case "": return baseid + 50; //carbonara_connessuer
                case "": return baseid + 51; //heikea
                case "Boss_Topoda": return baseid + 52; //topoda
                case "Boss_Consortium": return baseid + 53; //consortium
                case "": return baseid + 54; //sludge_steamroller
                case "": return baseid + 55; //ceviche_sisters
                case "": return baseid + 56; //voltai
                case "Boss_Roland": return baseid + 57; //roland
                case "Boss_MoonHermit": return baseid + 58; //petroch
                case "Boss_Inkerton": return baseid + 59; //inkerton
                case "Boss_MoltedKing1": return baseid + 60; //camtscha
                case "Boss_PrayaDubia_Phase2 Variant": return baseid + 61; //praya_dubia
                case "Boss_Firth": return baseid + 62; //firth*/
                default: return baseid -1;
            }
        }
    }
}
