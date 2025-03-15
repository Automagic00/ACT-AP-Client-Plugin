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
            //Debug.Log("Line 0");
            if (!Plugin.debugMode && Plugin.connection.session != null && Player.singlePlayer != null && !settingsSaved)
            {
                //Debug.Log("Line 1");
                settingsSaved = true;
                //Debug.Log("Line 2");
                int player = Plugin.connection.session.ConnectionInfo.Slot;
                //Debug.Log("Line 3");
                Dictionary<string, object> slotData = Plugin.connection.slotData;
                //Debug.Log("Line 4");
                //object value;

                foreach (var key in Plugin.connection.slotData.Keys)
                {
                    //Debug.Log("Line 5");
                    Debug.Log("slotdata: " + key);
                    Debug.Log("value: " + slotData[key]);
                }
                //Debug.Log("Line 6");
                double microplaticMod = (double)Plugin.connection.slotData["microplastic_multiplier"];
                //Debug.Log("Line 7");
                microplaticMod = microplaticMod == 0 ? 1 : microplaticMod; //Make sure its not 0
                //Debug.Log("Line 8");

                CrabFile.current.SetString("setting_microplasticMod", ((float)microplaticMod).ToString());
                //Debug.Log("Line 9");
            }
        }
    }
}
