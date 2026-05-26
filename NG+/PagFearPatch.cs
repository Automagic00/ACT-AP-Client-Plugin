using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(Pagurus), "UpdateEntity")]
    class PagFearPatch
    {
        static void Postfix(Pagurus __instance)
        {
            if (Plugin.debugMode || (Plugin.connection.connected && CrabFile.current.GetBool("ngplusBosses")))
            {
                if (__instance.aggro && Vector3.Distance(Player.singlePlayer.GetCenter(), __instance.GetCenter()) < __instance.persistentFearRadius && Player.singlePlayer.canBeDamaged && !Player.singlePlayer.blocking)
                {
                    Player.singlePlayer.TakeAffliction(HitEvent.AFFLICTIONTYPE.FEAR, Time.deltaTime * __instance.persistentFearRate, __instance);
                }
            }
        }
    }
}
