using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;
using Newtonsoft.Json;
using UnityEngine;

namespace ACTAP
{
    
    [HarmonyPatch(typeof(Player),"Update")]
    class SaveSettingsToFile
    {
        //static bool settingsSaved = false;
        [HarmonyPostfix]
        static void SaveSettingPatch()
        {
            
            if (!Plugin.debugMode && Plugin.connection.session != null && Player.singlePlayer != null && !Plugin.settingsSaved)
            {
                Debug.Log("Saving Settings");

                Plugin.settingsSaved = true;
                int player = Plugin.connection.session.ConnectionInfo.Slot;
                Dictionary<string, object> slotData = Plugin.connection.slotData;


                /*foreach (var key in Plugin.connection.slotData.Keys)
                {
                    //Debug.Log("Line 5");
                    Debug.Log("slotdata: " + key);
                    Debug.Log("value: " + slotData[key]);
                }*/

                //Microplastic Multiplier
                double microplaticMod = (double)Plugin.connection.slotData["microplastic_multiplier"];
                microplaticMod = microplaticMod == 0 ? 1 : microplaticMod; //Make sure its not 0

                //Shell Randomizer
                //Dictionary<string, string> shellSlotData = new Dictionary<string, string>;
                //PropertyInfo[] properties = Plugin.connection.slotData["shell_rando"].GetType().GetProperties();

                /*foreach (PropertyInfo property in properties)
                {
                    shellSlotData[property.Name] = property.GetValue(Plugin.connection.slotData["shell_rando"]);
                }*/

                //Debug.Log(shellSlotData);

                string shellRando = JsonConvert.SerializeObject(Plugin.connection.slotData["shell_rando"]);
                bool shellRandoEnabled = (bool)Plugin.connection.slotData["shell_rando_enabled"];

                Debug.Log("Shell Rando: " + shellRando);

                //Goal
                long goal = (long)Plugin.connection.slotData["goal"];

                Debug.Log("GOAL IS " + goal.GetType());

                CrabFile.current.SetString("setting_microplasticMod", ((float)microplaticMod).ToString());
                CrabFile.current.SetString("shellRando", shellRando);
                CrabFile.current.SetBool("shellRandoEnabled", shellRandoEnabled);
                CrabFile.current.SetInt("currentGoal",  (int)goal);
                //Debug.Log("Line 9");
            }
        }
    }
}
