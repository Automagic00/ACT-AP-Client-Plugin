using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ACTAP
{
    [HarmonyPatch(typeof(ProgressReaderBase), "CheckProgressData")]
    class FishingLineProgressPatch
    {
        //Ensure Progress Reader doesnt remove house of cards
        [HarmonyPrefix]
        static void ProgressPatch(ProgressReaderBase __instance)
        {
            if ((Plugin.connection.session != null || Plugin.debugMode) && __instance.gameObject.name == "House of Cards")
            {
                __instance.isNotUnlocked.Invoke();
            }
            
        }
    }

    [HarmonyPatch(typeof(UnlockReaderBase), "CheckUnlockData")]
    class FishingLineUnlockPatch
    {
        //Ensure Unlock Reader doesnt remove house of cards/ item itself
        [HarmonyPrefix]
        static void UnlockPatch(ProgressReaderBase __instance)
        {
            if ((Plugin.connection.session != null || Plugin.debugMode) && (__instance.gameObject.name == "House of Cards" || __instance.gameObject.name == "FishingLineUnlock"))
            {
                __instance.isNotUnlocked.Invoke();
            }

        }
    }
}
