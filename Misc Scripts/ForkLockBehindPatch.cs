using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(OptionsMenuOption),"Init")]
    class ForkLockBehindPatch
    {
        [HarmonyPrefix]
        static void lockPre(OptionsMenuOption __instance)
        {
            __instance.lockBehindFork = false;
        }
    }
}
