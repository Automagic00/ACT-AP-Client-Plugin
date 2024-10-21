using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(Player),"Update")]
    class SaveSettingsToFile
    {
        static bool settingsSaved = false;
        [HarmonyPostfix]
        static void SaveSettingPatch()
        {
            if(!Plugin.debugMode && Plugin.connection.session != null && Player.singlePlayer != null && !settingsSaved)
            {
                int player = Plugin.connection.session.ConnectionInfo.Slot;
                Dictionary<string, object> slotData = Plugin.connection.slotData;
                //object value;

                foreach (var key in Plugin.connection.slotData.Keys)
                {
                    Debug.Log("slotdata: " + key);
                    Debug.Log("value: " + slotData[key]);
                }

                double microplaticMod = (double)Plugin.connection.slotData["microplastic_multiplier"];
                microplaticMod = microplaticMod == 0 ? 1 : microplaticMod; //Make sure its not 0

                CrabFile.current.SetString("setting_microplasticMod", ((float)microplaticMod).ToString());
            }
        }
    }
}
