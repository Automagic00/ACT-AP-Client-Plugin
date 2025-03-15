using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace ACTAP.Misc_Scripts
{
    [HarmonyPatch(typeof(Player),"ResetDrink")]
    class HeartKelpOverflowFix
    {
        [HarmonyPrefix]
        static bool Prefix(Player __instance)
        {
            __instance.healthyEggCount = CrabFile.current.inventoryData["Heartkelp Sprout"].amount + 3;
            return false;
        }
    }
}
