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


                //Microplastic Multiplier
                double microplaticMod = (double)Plugin.connection.slotData["microplastic_multiplier"];
                microplaticMod = microplaticMod == 0 ? 1 : microplaticMod; //Make sure its not 0

                //Shell Randomizer
                string shellRando = JsonConvert.SerializeObject(Plugin.connection.slotData["shell_rando"]);
                bool shellRandoEnabled = (bool)Plugin.connection.slotData["shell_rando_enabled"];

                Debug.Log("Shell Rando: " + shellRando);

                //Goal
                long goal = (long)Plugin.connection.slotData["goal"];

                Debug.Log("GOAL IS " + goal.GetType());

                //NG+ Options
                bool ngplusBosses = (bool)Plugin.connection.slotData["ngplus_bosses"];
                bool ngplusSlots = (bool)Plugin.connection.slotData["ngplus_slots"];

                CrabFile.current.SetString("setting_microplasticMod", ((float)microplaticMod).ToString());
                CrabFile.current.SetString("shellRando", shellRando);
                CrabFile.current.SetBool("shellRandoEnabled", shellRandoEnabled);
                CrabFile.current.SetInt("currentGoal",  (int)goal);
                CrabFile.current.SetBool("ngplusBosses", ngplusBosses);
                CrabFile.current.SetBool("ngplusSlots", ngplusSlots);

            }
        }
    }
}
