using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.Reflection;

namespace ACTAP
{
    [HarmonyPatch(typeof(Shell), "Awake")]
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
                /*if (!__instance.name.Contains("SWAP"))
                {
                    __instance.name += "_SWAP";
                    var newShell = GameObject.Instantiate<Shell>(AssetListCollection.GetShellPrefab("Shell_SoloCup"));
                    newShell.transform.position = __instance.transform.position;
                    newShell.transform.rotation = __instance.transform.rotation;
                    newShell.prefabName += "_SWAP";
                    __instance.enabled = false;
                }*/
            }
        }
    }

    [HarmonyPatch(typeof(Shell), "Start")]
    class ShellReplacer
    {
        [HarmonyPostfix]
        static void Postfix(Shell __instance)
        {
            if (!__instance.name.Contains("SWAP") && __instance.gameObject.scene.name != "Player_Main" && __instance.transform.parent.name != "ShellTransform" && __instance.transform.parent.name != "Hat")
            {
                Debug.Log("TryReplaceShell");
                if (Plugin.debugMode)
                {
                    __instance.name += "_SWAP";
                    var newShell = GameObject.Instantiate<Shell>(AssetListCollection.GetShellPrefab("Shell_AmongUs"));
                    var newRB = newShell.GetComponent<Rigidbody>();
                    var oldRB = __instance.GetComponent<Rigidbody>();

                    newShell.transform.position = __instance.transform.position;
                    newShell.transform.rotation = __instance.transform.rotation;
                    newShell.name += "_SWAP";
                    newShell.transform.parent = __instance.transform.parent;
                    newRB.velocity = oldRB.velocity;

                    GameObject.Destroy(__instance.gameObject);
                }
                else if (Plugin.connection.session != null)
                {
                    Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));
                    Debug.Log("Connected");

                    string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(__instance.prefabName)]);

                    Debug.Log(__instance.prefabName + " : " + newShellName);

                    var newShell = GameObject.Instantiate<Shell>(AssetListCollection.GetShellPrefab(newShellName));
                    var newRB = newShell.GetComponent<Rigidbody>();
                    var oldRB = __instance.GetComponent<Rigidbody>();

                    newShell.transform.position = __instance.transform.position;
                    newShell.transform.rotation = __instance.transform.rotation;
                    newShell.name += "_SWAP";
                    newShell.transform.parent = __instance.transform.parent;
                    newRB.velocity = oldRB.velocity;

                    GameObject.Destroy(__instance.gameObject);
                }
            }
        }
    }

    [HarmonyPatch(typeof(ScuttleportShellSpawner), "Awake")]
    static class PlugSpawnReplace
    {
        [HarmonyPostfix]
        static void Postfix(ScuttleportShellSpawner __instance)
        {
            if (Plugin.debugMode)
            {
                __instance.shellToSpawn = AssetListCollection.GetShellPrefab("Shell_AmongUs");
            }
            else if (Plugin.connection.session != null)
            {
                Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));

                string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(__instance.shellToSpawn.prefabName)]);

                __instance.shellToSpawn = AssetListCollection.GetShellPrefab(newShellName);
            }
        }
    }

    [HarmonyPatch(typeof(HermitMimic), "InitShell")]
    static class MimicShellReplace
    {
        [HarmonyPrefix]
        static void Prefix(HermitMimic __instance)
        {
            if (Plugin.debugMode)
            {
                __instance.startingShell = AssetListCollection.GetShellPrefab("Shell_AmongUs");
            }
            else if (Plugin.connection.session != null)
            {
                Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));
                //Debug.Log("Connected");

                string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(__instance.startingShell.prefabName)]);

                __instance.startingShell = AssetListCollection.GetShellPrefab(newShellName);
            }
        }
    }

    [HarmonyPatch(typeof(ShopButtonList),"InitializeItem")]
    static class ShellfishDesiresReplace
    {
        [HarmonyPrefix]
        static void Prefix(ShopItem item, ShopButtonList __instance)
        {
            //Check if its shellfish desires
            if ( __instance.shopData.shopID == StoreSaveData.ShopID.ShellfishDesires)
            {
                //ShellCollectable shellItem = (ShellCollectable)item.item;
                if (item.item.GetType() == typeof(ShellCollectable) && !item.name.Contains("SWAP"))
                {
                    item.name += "_SWAP";
                    if (Plugin.debugMode)
                    {
                        item.item.displayName = AssetListCollection.GetShellPrefab("Shell_AmongUs").stats.displayName;
                    }
                    else if (Plugin.connection.session != null)
                    {
                        Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));
                        //Debug.Log("Connected");

                        Shell shellToReplace = GameManager.instance.assetCollection.shells.Find((Shell x) => x.stats.displayName == item.item.GetNameIndex());
                        if (shellToReplace != null)
                        {
                            Debug.Log("Shell to replace: " + shellToReplace.prefabName);
                            string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(shellToReplace.prefabName)]);

                            item.item.displayName = AssetListCollection.GetShellPrefab(newShellName).stats.displayName;
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(VendingMachineButton),"Awake")]
    static class NephroCutscenePatch
    {
        [HarmonyPostfix]
        static void Postfix(VendingMachineButton __instance)
        {
            if (Plugin.debugMode)
            {
                __instance.canPrefabs = new Shell[] { AssetListCollection.GetShellPrefab("Shell_AmongUs")};
            }
            else if (Plugin.connection.session != null)
            {
                Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));
                //Debug.Log("Connected");

                string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(__instance.canPrefabs[0].prefabName)]);

                __instance.canPrefabs = new Shell[] { AssetListCollection.GetShellPrefab(newShellName) };
            }
        }
    }

    [HarmonyPatch(typeof(VendingMachineButton), "SpawnCan")]
    static class SpawnCanPatch
    {
        [HarmonyPrefix]
        static bool Prefix(VendingMachineButton __instance)
        {
            if (Plugin.debugMode || Plugin.connection.session != null)
            {
                FieldInfo nephScene = AccessTools.Field(typeof(VendingMachineButton), "nephroCutscene");

                __instance.view.PlaySound("CutsceneSFX/3_Nephro/Vending_Eject");
                Shell shell = UnityEngine.Object.Instantiate<Shell>(__instance.canPrefabs.RandomSelection((Shell c) => 1), __instance.canSpawn);
                shell.name += "_SWAP";
                shell.transform.position = __instance.canSpawn.transform.position;
                shell.rb.isKinematic = false;
                shell.rb.velocity = __instance.canSpawn.forward * __instance.canForce + __instance.canForceRandomization * UnityEngine.Random.insideUnitSphere;
                CameraController.instance.Shake(2f);
                shell.rb.angularVelocity = UnityEngine.Random.insideUnitSphere * __instance.maxCanTorque;
                if ((nephScene.GetValue(__instance)) == null)
                {
                    CutsceneSpot spot = CutsceneSpot.GetSpot("cutscene_NephroIntro");
                    if (spot)
                    {
                        nephScene.SetValue(__instance, spot.GetComponent<CutsceneContent>());
                    }
                    else
                    {
                        Debug.LogError("NEPHRO CUTSCENE IS NULL", null);
                    }
                }
                FirstShell firstShell = shell.gameObject.AddComponent<FirstShell>();
                firstShell.cutsceneTrigger = __instance.nephroCutsceneTriggerPrefab;
                firstShell.cutsceneContent = (CutsceneContent)nephScene.GetValue(__instance);
                return false;
            }
            return true;
        }
    }
}
