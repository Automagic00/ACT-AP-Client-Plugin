using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(MapBossIcon), nameof(MapBossIcon.IsKilled))]
    internal class MapIconsAlwaysColored
    {
        [HarmonyPrefix]
        public static bool Prefix(MapBossIcon __instance,ref bool __result)
        {
            if (__instance.transform.gameObject.name == "Custom Map Icon")
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
