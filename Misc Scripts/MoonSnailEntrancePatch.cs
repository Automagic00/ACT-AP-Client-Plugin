using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(ProgressReaderBase),"UpdateState")]
    public class MoonSnailEntrancePatch
    {
        //Keep Ghost Moon Snail unlocked regardless of level progress
        [HarmonyPrefix]
        public static bool EntrancePrefix(ProgressReaderBase __instance)
        {
            if (__instance.name == "cutscene_enteredMoonCaves")
            {
                __instance.isNotUnlocked.Invoke();
                return false;
            }
            return true;
        }
    }
}
