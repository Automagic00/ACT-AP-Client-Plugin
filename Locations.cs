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

namespace ACTAP
{
    
    static class LocationSwapData
    { 
        public static void LogLocation(Item itemPickup)
        {
            string path = GetPath(itemPickup.transform);
            //string json = JsonUtility.ToJson(path);
            Directory.CreateDirectory("location/" + path);
            using (StreamWriter writeText = new StreamWriter("location/" + path + ".txt"))
            {
                writeText.WriteLine(path);
            }

            //Debug.Log(GetPath(itemPickup.transform));
        }
        public static string GetPath(this Transform current)
        {
            if (current.parent == null)
            {
                return current.gameObject.scene.name + "/" + current.name;
            }
            return GetPath(current.parent) + "/" + current.name;
        }

        public static long ItemPathToAPID(string path)
        {
            long baseid = 0;
            switch (path)
            {
                case "2_A-ShallowsTidePools/Cutscenes/IntroCutscene/LoanSharkSequence/Item_HeartkelpPodsUnlock": return baseid;
                case "2_A-ShallowsTidePools/Pickups/Items/Item_00Breadclaw": return baseid;
                default: return -1;

            }
        }

    }

}
