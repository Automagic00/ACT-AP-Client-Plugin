using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(MoonSnailShell_Domain),"Start")]
    class MSS_DomainPatch
    {
        static void StartPatch(MoonSnailShell_Domain __instance)
        {
            __instance.gameObject.SetActive(true);
        }
    }
}
