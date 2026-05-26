using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;


namespace ACTAP
{
    [HarmonyPatch(typeof(Pagurus), "Awake")]
    class PagSpawnPatch
    {
        static Pagurus realInst = null;

        [HarmonyPostfix]
        static void Postfix(Pagurus __instance)
        {

            bool bosses = Plugin.debugMode ? true : CrabFile.current.GetBool("ngplusBosses");
            if (__instance.transform.parent.parent.name == "NG+" && bosses)
            {
                Debug.Log("NG+ Pag");
                Pagurus.instance = __instance;
                realInst = __instance;
            }
            else if (__instance.transform.parent.parent.name == "NG" && !bosses)
            {
                Debug.Log("NG Pag");
                Pagurus.instance = __instance;
                realInst = __instance;
            }
            else if (realInst != null)
            {
                Pagurus.instance = realInst;
            }


        }
    }
}
