using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(NewGamePlusEnabler), "SetNGPlus")]
    class NewGamePlusPatch
    {
        [HarmonyPostfix]
        static void SetNGPlusPatch(NewGamePlusEnabler __instance)
        {
            __instance.objectToEnable.gameObject.SetActive(true);

            bool bosses = Plugin.debugMode? true: CrabFile.current.GetBool("ngplusBosses");
            bool pickups = false;
            bool enemies = false;

            if (__instance.objectToEnable.name == "NG+")
            {
                for (int i = 0; i < __instance.objectToEnable.transform.childCount; i++)
                {
                    GameObject child = __instance.objectToEnable.transform.GetChild(i).gameObject;
                    switch (child.name)
                    {
                        case "Bosses": child.SetActive(bosses); break;
                        default: child.SetActive(false); break;
                    }
                }
            }
            else if (__instance.objectToEnable.name == "NG")
            {
                for (int i = 0; i < __instance.objectToEnable.transform.childCount; i++)
                {
                    GameObject child = __instance.objectToEnable.transform.GetChild(i).gameObject;
                    switch (child.name)
                    {
                        case "Bosses": child.SetActive(!bosses); break;
                        default: child.SetActive(true); break;
                    }
                }
            }


        }
    }
}
