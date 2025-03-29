using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using System.Threading.Tasks;

namespace ACTAP.Misc_Scripts
{
    [HarmonyPatch(typeof(NPC_Prawnathan),"Awake")]
    class InvisPrawnathanFix
    {
        [HarmonyPostfix]
        static void Postfix(NPC_Prawnathan __instance)
        {
            __instance.gameObject.SetActive(true);
        }
    }
}
