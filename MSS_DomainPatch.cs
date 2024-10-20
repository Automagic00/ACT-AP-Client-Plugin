using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(MoonSnailShell_Domain),"Start")]
    static class MSS_DomainPatch
    {
        static void StartPatch(MoonSnailShell_Domain __instance)
        {
            __instance.Activate(false);
        }
    }

    [HarmonyPatch(typeof(MoonSnailShell_Domain), "Interact")]
    static class InteractPatch
    {
        [HarmonyPrefix]
        static void InteractPrefix(MoonSnailShell_Domain __instance)
        {
            if (!CrabFile.current.inventoryData.HasItem(__instance.mssCollectable))
            {
                CrabFile.current.inventoryData.AdjustAmount(__instance.mssCollectable, 1);
                CrabFile.current.locationData.SetLastActivatedMSS(__instance.mssCollectable.dest, __instance.mssCollectable.index);
            }
        }
    }
}
