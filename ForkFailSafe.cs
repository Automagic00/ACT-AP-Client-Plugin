using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ACTAP
{
    [HarmonyPatch(typeof(MoonSnailShell), "EnterShellRoutine")]
    class ForkFailsafePatch
    {
        [HarmonyPrefix]
        public static bool Prefix(MoonSnailShell __instance)
        {
            if (Plugin.connection.session == null)
            {
                return true;
            }

            __instance.StartCoroutine(ShellRoutinePatch(__instance));
            return false;

        }

        public static IEnumerator ShellRoutinePatch(MoonSnailShell inst)
        {
            GUIManager.instance.MSSMenuManager.Open();
            yield return null;
            inst.OnShellEnterEnd();
            yield break;
        }
    }
}
