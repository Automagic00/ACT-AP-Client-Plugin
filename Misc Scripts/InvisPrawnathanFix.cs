using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using System.Threading.Tasks;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(NPC_Prawnathan),"Awake")]
    class InvisPrawnathanFix
    {
        [HarmonyPostfix]
        static void Postfix(NPC_Prawnathan __instance)
        {
            __instance.gameObject.SetActive(true);
            __instance.transform.parent.gameObject.SetActive(true);
        }
    }

    [HarmonyPatch(typeof(CutsceneSpot),"Awake")]
    class CutscenePrawnathanFix
    {
        [HarmonyPostfix]
        static void Postfix(CutsceneSpot __instance)
        {
            if (__instance.name == "PrawnathanSpot")
            {
                Transform child = __instance.transform.GetChild(0);
                if (child)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
