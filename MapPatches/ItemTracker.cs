using ACTAP.Extensions;
using ACTAP.Utils;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ACTAP
{
    [HarmonyPatch(typeof(AreaMap), nameof(AreaMap.RefreshMapWithFile))]
    internal class ItemTracker
    {
        [HarmonyPostfix]
        public static void Postfix(AreaMap __instance)
        {
            if (Plugin.RenderMapMarkers)
            { 

            GameObject icon = __instance.pagurusIcon.gameObject;
            GameObject mapOverlay = __instance.pagurusIcon.transform.parent.gameObject;
            List<Item> items = Plugin.items;
            PlayerLocationData location = CrabFile.current.locationData;

            LevelData dataFromLevel = WorldData.globalWorldData.GetDataFromLevel(location.currentLevel);
            Vector2 treasureMapBottomLeft = dataFromLevel.treasureMapBottomLeft;
            Vector2 treasureMapTopRight = dataFromLevel.treasureMapTopRight;
            int mapDataIndex = Traverse.Create(__instance).Field<int>("activeDataSetIndex").Value;
            AreaMapDataSet areaMapDataSet = __instance.areaMapDataSets[mapDataIndex];

                foreach (Item item in items)
                {
                    string itemName = item.DisplayName.Replace("Item_", "").Replace("_Name", "").ToLower();
                    string resourceName;
                    if (ItemNameToResource.ItemToResource.ContainsKey(itemName))
                    {
                        resourceName = ItemNameToResource.ItemToResource[itemName];
                    }
                    else if (itemName.Contains("stowaway"))
                    {
                        resourceName = "stowaways";
                    }
                    else if (itemName.Contains("claw"))
                    {
                        resourceName = "junk";
                    }
                    else if (itemName.Contains("costume"))
                    {
                        resourceName = "costume";
                    }
                    else
                    {
                        resourceName = "junk";
                        //ModHelper.Msg(itemName);
                    }
                    Sprite itemSprite = ModHelper.GetSprite(resourceName);
                    SaveStateKillableEntity state = Traverse.Create(item).Field("save").GetValue() as SaveStateKillableEntity;
                    if (state == null)
                    {
                        continue;
                    }
                    if (item == null || state.killedPreviously)
                    {
                        continue;
                    }
                    GameObject itemIcon = GameObject.Instantiate(icon, mapOverlay.transform);
                    itemIcon.name = "Custom Map Icon";
                    if (itemSprite != null)
                    {
                        itemIcon.GetComponent<Image>().sprite = itemSprite;
                    }
                    itemIcon.transform.localScale = new Vector3(0.25f, 0.25f);
                    Plugin.mapMarkers.Add(itemIcon);
                    __instance.Triangulate(itemIcon.GetComponent<RectTransform>(), item.GetCenter(), treasureMapBottomLeft, treasureMapTopRight, areaMapDataSet.areaMapBottomLeft.anchoredPosition, areaMapDataSet.areaMapTopRight.anchoredPosition, false, false);
                    itemIcon.SetActive(true);
                    itemIcon.GetComponent<Image>().enabled = true;
                }
            }
        }
    }
}
