using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(NPC),"Start")]
    static class NPCQuests
    {
        [HarmonyPostfix]
        public static void QuestPatch(NPC __instance)
        {
            if (__instance.transform.GetComponent<GiveItem>() != null)
            {
                Debug.Log("Quest NPC: " + __instance.name);
                Debug.Log(__instance.transform.GetComponent<GiveItem>().items[0].name);
            }
        }
    }

    [HarmonyPatch(typeof(GiveItem),"GiveItems")]
    static class NPCItemOverride
    {
        [HarmonyPrefix]
        public static bool ItemPatch(GiveItem __instance)
        {
            if (Plugin.debugMode)
            {
                Debug.Log("Giving Player " + __instance.items);
                return true;
            }
            else if (Plugin.connection.session != null)
            {
                int baseid = 483021700;
                switch (__instance.name)
                {
                    case "Carcinian_Snippolas": Plugin.connection.ActivateCheck(baseid + 24); break;
                    //case "Urchin": Plugin.connection.ActivateCheck(baseid + 85); break;
                    default: break;
                }

                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(StreetUrchin), "AcquireItem")]
    static class UrchinItemOverride
    {
        [HarmonyPrefix]
        public static bool ItemPatch(StreetUrchin __instance)
        {
            if (Plugin.debugMode)
            {
                Debug.Log("Giving Player Toss");
                return true;
            }
            else if (Plugin.connection.session != null)
            {
                Debug.Log("Urchin session found");
                int baseid = 483021700;
                Plugin.connection.ActivateCheck(baseid + 85);

                return false;
            }
            return true;
        }
    }

}
