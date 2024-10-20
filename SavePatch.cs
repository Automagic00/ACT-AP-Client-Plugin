using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(DisableButtonIfNoAutoSave),"OnEnable")]
    public static class SavePatch
    {
        [HarmonyPostfix]
        public static void savePost(DisableButtonIfNoAutoSave __instance)
        {
            __instance.selectable.interactable = true;
        }
    }

    [HarmonyPatch(typeof(GameManager), "AutoSave")]
    public static class AutoSavePatch
    {
        [HarmonyPrefix]
        public static bool savePre(GameManager __instance, ref bool collectPlace)
        {
            Debug.Log("In Save Pre");
            __instance.ForceSave(collectPlace);
            return false;
        }
    }
}
