using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(NPC_Topoda), "GiveMantisPunch")]
    class TopodaMantisPunchPatch
    {
        [HarmonyPrefix]
        static bool GivePunchPre()
        {
            //Make sure Topoda doesnt give the adaptation when randomized
            if (Plugin.connection.session != null || Plugin.debugMode)
            {
                return false;
            }
            return true;
        }
    }
}
