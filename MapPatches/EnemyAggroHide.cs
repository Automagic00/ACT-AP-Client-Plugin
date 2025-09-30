using HarmonyLib;

namespace ACTAP
{
    [HarmonyPatch(typeof(Enemy), nameof(Enemy.Aggro))]
    internal class EnemyAggroHide
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            if (Plugin.RenderWorldMarkers)
            {
                Plugin.RenderWorldMarkersTemp = !Plugin.EnemiesAggro();
            }
        }
    }

    [HarmonyPatch(typeof(Enemy), nameof(Enemy.DropAggro))]
    internal class EnemyDropAggroHide
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            if (Plugin.RenderWorldMarkers)
            {
                Plugin.RenderWorldMarkersTemp = !Plugin.EnemiesAggro();
            }
        }
    }
}
