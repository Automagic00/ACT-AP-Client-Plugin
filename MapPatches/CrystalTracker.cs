using ACTAP.Extensions;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ACTAP
{
    [HarmonyPatch(typeof(AreaMap), nameof(AreaMap.RefreshMapWithFile))]
    internal class CrystalTracker
    {
        [HarmonyPostfix]
        public static void Postfix(AreaMap __instance)
        {
            if (Plugin.RenderMapMarkers)
            { 
                GameObject icon = __instance.pagurusIcon.gameObject;
                GameObject mapOverlay = __instance.pagurusIcon.transform.parent.gameObject;
                List<Enemy> enemies = Plugin.crystalEnemies;

                PlayerLocationData location = CrabFile.current.locationData;

                LevelData dataFromLevel = WorldData.globalWorldData.GetDataFromLevel(location.currentLevel);
                Vector2 treasureMapBottomLeft = dataFromLevel.treasureMapBottomLeft;
                Vector2 treasureMapTopRight = dataFromLevel.treasureMapTopRight;
                int mapDataIndex = Traverse.Create(__instance).Field<int>("activeDataSetIndex").Value;
                AreaMapDataSet areaMapDataSet = __instance.areaMapDataSets[mapDataIndex];

                Sprite crystalSprite = ModHelper.GetSprite("crystal");

                foreach (Enemy enemy in enemies)
                {
                    SaveStateKillableEntity state = Traverse.Create(enemy).Field("saveState").GetValue() as SaveStateKillableEntity;
                    if (state == null) { continue; }
                    if (enemy.transform == null || enemy.isBoss || state.killedPreviously)
                    {
                        continue;
                    }
                    if (enemy.umamiDrops > 0)
                    {
                        GameObject crystalIcon = Object.Instantiate(icon, mapOverlay.transform);
                        crystalIcon.name = "Custom Map Icon";
                        if (crystalSprite != null)
                        {
                            crystalIcon.GetComponent<Image>().sprite = crystalSprite;
                        }
                        crystalIcon.transform.localScale = new Vector3(0.25f, 0.25f);
                        Plugin.mapMarkers.Add(crystalIcon);
                        __instance.Triangulate(crystalIcon.GetComponent<RectTransform>(), enemy.GetCenter(), treasureMapBottomLeft, treasureMapTopRight, areaMapDataSet.areaMapBottomLeft.anchoredPosition, areaMapDataSet.areaMapTopRight.anchoredPosition, false, false);
                        crystalIcon.SetActive(true);
                        crystalIcon.GetComponent<Image>().enabled = true;
                    }
                }
            }
        }
    }
}
