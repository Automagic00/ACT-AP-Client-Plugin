using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(CollectableItemData), "GetLocalizedDesc")]
    static class PearlDescPatch
    {
        [HarmonyPostfix]
        public static void LocalizationPatch(CollectableItemData __instance, ref string __result)
        {
            Debug.Log("InstName " + __instance.name);
            if (__instance.name == "Junk_DuchessPearl")
            {
                __result = __instance.summary;
            }
        }

    }
}
