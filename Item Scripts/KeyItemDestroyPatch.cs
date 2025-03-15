using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ACTAP.Item_Scripts
{
    [HarmonyPatch(typeof(KeyItem), "CheckProgressData")]
    class KeyItemDestroyPatch
    {
        [HarmonyPrefix]
        static bool Prefix(KeyItem __instance)
        {
            if (Plugin.connection.session != null || Plugin.debugMode)
            {
                /*long testid = LocationDataTable.FindPickupAPID(__instance.GetComponent<Item>());
                if (CrabFile.current.GetInt("LocationChecked-" + testid) == 1)
                {
                    Debug.Log(__instance.gameObject.name + " Already killed: deleting. ID: " + testid);
                    Entity component = __instance.GetComponent<Entity>();
                    if (component)
                    {
                        GameManager.events.TriggerEntityDestroyedFromSave(component);
                    }
                    GameObject.Destroy(__instance.gameObject);
                }*/

                return false;
            }
            return true;
        }
    }
}
