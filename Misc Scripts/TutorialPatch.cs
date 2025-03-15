using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(Player), "Start")]
    class TutorialPatch
    {
        [HarmonyPostfix]
        static void TutPostfix()
        {
            Debug.Log("Patching Tutorials");
            UserSettings.SetBool("tutorialPopupsActive", false);
            return;
        }
    }

    /*class TutStore
    {
        public static TutorialData data;
    }*/

    [HarmonyPatch(typeof(TutorialPopup), nameof(TutorialPopup.ActivatePopUp), new Type[] { typeof(TutorialData), typeof(float), typeof(bool), typeof(bool), typeof(CollectableItemData), typeof(bool) })]
    class TutorialForcePatch
    {
        [HarmonyPostfix]
        static void forcePost()
        {
            UserSettings.SetBool("tutorialPopupsActive", false);
        }
    }
}
