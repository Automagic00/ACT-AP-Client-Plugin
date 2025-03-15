using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using Newtonsoft.Json;

namespace ACTAP
{
    [HarmonyPatch(typeof(Shell),"Awake")]
    class ShellRando
    {
        [HarmonyPrefix]
        static void Prefix(Shell __instance)
        {
            if (Plugin.debugMode)
            {
                //Log Shell locations
                /*Debug.Log("Adding " + __instance.prefabName);
                Dictionary<string, int> value;
                int count;
                //Check if Scene Dict Exists
                if (Plugin.shellData.TryGetValue(__instance.gameObject.scene.name, out value))
                {
                    //Check if shell dict exits
                    if (Plugin.shellData[__instance.gameObject.scene.name].TryGetValue(__instance.prefabName,out count))
                    {
                        Plugin.shellData[__instance.gameObject.scene.name][__instance.prefabName] = count + 1;
                    }
                    else
                    {
                        Plugin.shellData[__instance.gameObject.scene.name].Add(__instance.prefabName, 1);
                    }
                    Debug.Log("Key Exists");
                    
                }
                else
                {
                    Debug.Log("Key Does Not Exist");

                    Plugin.shellData.Add(__instance.gameObject.scene.name, new Dictionary<string, int> { { __instance.prefabName, 1 } }); 
                }*/


                Debug.Log(__instance.prefabName);
                if (!__instance.prefabName.Contains("SWAP"))
                {
                    var newShell = GameObject.Instantiate<Shell>(AssetListCollection.GetShellPrefab("Shell_SoloCup"));
                    newShell.transform.position = __instance.transform.position;
                    newShell.transform.rotation = __instance.transform.rotation;
                    newShell.prefabName += "_SWAP";
                    __instance.enabled = false;
                }
            }
        }
    }

    /*[HarmonyPatch(typeof(Player),"Start")]
    class ShellOutput
    { 
        [HarmonyPrefix]
        static void Prefix()
        {

            string json = "";
            string path = "shelldata/shellData.json";

            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            Debug.Log("Adding shells");

            using (StreamWriter writeText = new StreamWriter(fileStream))
            {
                writeText.WriteLine( JsonConvert.SerializeObject(Plugin.shellData));
                writeText.Close();
                Debug.Log("Write Success");
            }
        



            string json = JsonUtility.ToJson(AssetListCollection.instance.ToCSV());
            using (StreamWriter writeText = new StreamWriter("shelldata.json"))
            {
                writeText.WriteLine(json);
            }
        }
    }*/

}
