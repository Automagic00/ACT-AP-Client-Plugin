using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(AreaMap), "OnDisable")]
    internal class ClearMapMarkers
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            foreach (GameObject marker in Plugin.mapMarkers)
            {
                Object.Destroy(marker);
            }
            Plugin.mapMarkers.Clear();

            if (Plugin.RenderMapMarkers)
            {
                Plugin.RenderWorldMarkersTemp = true;
            }
        }
    }
}
