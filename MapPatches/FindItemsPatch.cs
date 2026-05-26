using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;


namespace ACTAP
{
    [HarmonyPatch(typeof(Item),"Awake")]
    class FindItemsPatch
    {
        [HarmonyPostfix]
        static void Postfix(Item __instance)
        {
            if (!Plugin.items.Contains(__instance) && __instance.transform.parent != null && __instance.transform.parent.parent != null && __instance.transform.parent.parent.name != "NG+")
            {
                Plugin.items.Add(__instance);
            }
        }
    }

    [HarmonyPatch(typeof(Enemy), "Awake")]
    class FindCrystalEnemiesPatch
    {
        [HarmonyPostfix]
        static void Postfix(Enemy __instance)
        {
            if (!Plugin.crystalEnemies.Contains(__instance))
            {
                Plugin.crystalEnemies.Add(__instance);
            }
        }
    }

    [HarmonyPatch(typeof(Item),"ObtainItem")]
    class RemovePickupPatch
    {
        [HarmonyPostfix]
        static void Postfix(Item __instance)
        {
            if (Plugin.items.Contains(__instance))
            {
                Plugin.items.Remove(__instance);
            }
        }
    }

    [HarmonyPatch(typeof(Item), "Destroy")]
    class RemoveItemDestroyedPatch
    {
        [HarmonyPostfix]
        static void Postfix(Item __instance)
        {
            if (Plugin.items.Contains(__instance))
            {
                Plugin.items.Remove(__instance);
            }
        }
    }


    [HarmonyPatch(typeof(LoadingScreen), "StartLoading")]
    class ClearOnLoad
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            Plugin.items.Clear();
            Plugin.crystalEnemies.Clear();
        }
    }

    [HarmonyPatch(typeof(EventManager), "TriggerEntityDestroyedFromSave")]
    class RemoveItemsAlreadyPickedUp
    {
        [HarmonyPrefix]
        static void Prefix(Entity e)
        {
            if (e.GetComponent<Item>() != null)
            {
                Item item = e.GetComponent<Item>();
                Plugin.items.Remove(item);
            }
        }
    }

}
