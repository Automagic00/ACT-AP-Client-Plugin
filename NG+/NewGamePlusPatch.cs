using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    class NGPlusHelpers
    {
        public static void DisableAllChildren(GameObject obj,bool active)
        {
            foreach(Transform child in obj.transform)
            {
                child.gameObject.SetActive(active);
            }
        }
    }

    [HarmonyPatch(typeof(NewGamePlusEnabler), "SetNGPlus")]
    class NewGamePlusPatch
    {
        [HarmonyPostfix]
        static void SetNGPlusPatch(NewGamePlusEnabler __instance)
        {
            if (Plugin.debugMode || Plugin.connection.connected)
            {
                //__instance.objectToEnable.gameObject.SetActive(true);

                bool bosses = Plugin.debugMode ? true : CrabFile.current.GetBool("ngplusBosses");
                bool pickups = false;
                bool enemies = false;

                if (__instance.objectToEnable.name == "NG+")
                {
                    __instance.objectToEnable.gameObject.SetActive(bosses || pickups || enemies);

                    for (int i = 0; i < __instance.objectToEnable.transform.childCount; i++)
                    {
                        GameObject child = __instance.objectToEnable.transform.GetChild(i).gameObject;
                        switch (child.name)
                        {
                            case "Bosses": child.SetActive(bosses); NGPlusHelpers.DisableAllChildren(child, bosses); break;
                            case "Pickups": child.SetActive(pickups); NGPlusHelpers.DisableAllChildren(child, pickups); break;
                            case "Enemies": child.SetActive(enemies || bosses); NGPlusHelpers.DisableAllChildren(child, enemies); break;
                            default: child.SetActive(false); break;
                        }

                        if (child.name == "Bosses" && bosses == true)
                        {
                            

                            Debug.Log("Child found");
                            var pag = child.GetComponentInChildren<Pagurus>();
                            if (pag != null)
                            {
                                Debug.Log("Pag Found");
                                Pagurus.instance = pag;
                            }
                        }
                    }

                }
                else if (__instance.objectToEnable.name == "NG")
                {
                    //__instance.objectToEnable.gameObject.SetActive(!bosses || !pickups || !enemies);

                    for (int i = 0; i < __instance.objectToEnable.transform.childCount; i++)
                    {
                        GameObject child = __instance.objectToEnable.transform.GetChild(i).gameObject;
                        switch (child.name)
                        {
                            case "Bosses": child.SetActive(!bosses); break;
                            default: child.SetActive(true); break;
                        }
                    }

                }
                //Very Rude Snail
                else if (__instance.objectToEnable.name == "Enemies_NewGamePlus")
                {
                    __instance.objectToEnable.gameObject.SetActive(bosses || enemies);

                    for (int i = 0; i < __instance.objectToEnable.transform.childCount; i++)
                    {
                        GameObject child = __instance.objectToEnable.transform.GetChild(i).gameObject;
                        switch (child.name)
                        {
                            case "NPC_VERYRudeSnail": child.SetActive(bosses); break;
                            default: child.SetActive(enemies); break;
                        }
                    }

                }
                //Rude Snail
                else if (__instance.objectToEnable.name == "Enemies_1")
                {
                    __instance.objectToEnable.gameObject.SetActive(!bosses || !enemies);

                    for (int i = 0; i < __instance.objectToEnable.transform.childCount; i++)
                    {
                        GameObject child = __instance.objectToEnable.transform.GetChild(i).gameObject;
                        switch (child.name)
                        {
                            case "NPC_RudeSnail": child.SetActive(!bosses); break;
                            default: child.SetActive(!enemies); break;
                        }
                    }

                }

            }
        }
    }
}
