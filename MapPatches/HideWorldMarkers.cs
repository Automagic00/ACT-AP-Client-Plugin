using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.OnGamePaused))]
    internal class HideWorldMarkers
    {
        [HarmonyPostfix]
        public static void Postfix(bool paused)
        {
            if (Plugin.RenderWorldMarkers)
            {
                Plugin.RenderWorldMarkersTemp = !paused;
            }
            else
            {
                Plugin.RenderWorldMarkersTemp = false;
            }
        }
    }
}
