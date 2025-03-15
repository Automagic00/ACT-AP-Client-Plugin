using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(LocalizedText), nameof(LocalizedText.GetText), new Type[] { typeof(string), typeof(bool) })]
    //[HarmonyPatch(new Type[] {typeof(string), typeof(bool)})]
    class LocalizeTextPatch
    {
        [HarmonyPostfix]
        public static string Postfix(string __result)
        {
            string temp = __result;
            if (__result.Contains("LOC: "))
            {
                //Debug.Log(temp);
                temp = __result.Replace("LOC: ", "");
                //Debug.Log(temp);
            }

            return temp;
        }
    }
}
