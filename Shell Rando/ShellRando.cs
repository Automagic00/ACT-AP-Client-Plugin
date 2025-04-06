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
  /*  [HarmonyPatch(typeof(Shell), "Awake")]
    class ShellRando
    {

        [HarmonyPrefix]
        static void Prefix(Shell __instance)
        {
            if (Plugin.debugMode)
            {
                Debug.Log(__instance.prefabName);
            }
        }
    }*/

    [HarmonyPatch(typeof(Shell), "Start")]
    class ShellReplacer
    {
        [HarmonyPostfix]
        static void Postfix(Shell __instance)
        {
            //Exit if shell rando is not enabled
            if (!CrabFile.current.GetBool("shellRandoEnabled") && !Plugin.debugMode)
            {
                return;
            }

            if (!__instance.name.Contains("SWAP") && __instance.gameObject.scene.name != "Player_Main" )
            {
                if (__instance.transform.parent != null)
                {
                    if(__instance.transform.parent.name == "ShellTransform" || __instance.transform.parent.name == "Hat" || __instance.transform.parent.name == "CanSpot")
                    {
                        return;
                    }
                }
                //Debug.Log("TryReplaceShell");
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
                    //Debug.Log("Replace" + __instance.prefabName);

                    string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(__instance.prefabName)]);

                    //Debug.Log(__instance.prefabName + " : " + newShellName);

                    var newShell = GameObject.Instantiate<Shell>(AssetListCollection.GetShellPrefab(newShellName));
                    var newRB = newShell.GetComponent<Rigidbody>();
                    var oldRB = __instance.GetComponent<Rigidbody>();

                    newShell.transform.position = __instance.transform.position;
                    newShell.transform.rotation = __instance.transform.rotation;
                    newShell.name += "_SWAP";
                    if (__instance.transform.parent != null)
                    {
                        newShell.transform.parent = __instance.transform.parent;
                    }
                    newRB.velocity = oldRB.velocity;
                    newRB.isKinematic = oldRB.isKinematic;

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
                //Exit if shell rando is not enabled
                if (!CrabFile.current.GetBool("shellRandoEnabled"))
                {
                    return;
                }
                Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));

                string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(__instance.shellToSpawn.prefabName)]);

                __instance.shellToSpawn = AssetListCollection.GetShellPrefab(newShellName);
            }
        }
    }

    [HarmonyPatch(typeof(ScuttleportShellSpawner), "SpawnShell")]
    static class PlugSpawnRBFix
    {
        [HarmonyPostfix]
        static void Postfix(ScuttleportShellSpawner __instance)
        {
            __instance.currentShell.rb.isKinematic = false;
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
                //Exit if shell rando is not enabled
                if (!CrabFile.current.GetBool("shellRandoEnabled"))
                {
                    return;
                }
                Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));

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
                        //Exit if shell rando is not enabled
                        if (!CrabFile.current.GetBool("shellRandoEnabled"))
                        {
                            return;
                        }
                        Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));

                        Shell shellToReplace = GameManager.instance.assetCollection.shells.Find((Shell x) => x.stats.displayName == item.item.GetNameIndex());
                        if (shellToReplace != null)
                        {
                            //Debug.Log("Shell to replace: " + shellToReplace.prefabName);
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
                //Exit if shell rando is not enabled
                if (!CrabFile.current.GetBool("shellRandoEnabled"))
                {
                    return;
                }
                Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));

                string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(__instance.canPrefabs[0].prefabName)]);

                __instance.canPrefabs = new Shell[] { AssetListCollection.GetShellPrefab(newShellName) };
            }
        }
    }

    //Fixes the Nephro Vending Machine
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

    //Makes sure the Plug shell is insured if bought from shop
    [HarmonyPatch(typeof(ShellSelectionButtonFlag), "Initialize")]
    static class InsuredShellsPatch 
    { 
        [HarmonyPrefix]
        static void Prefix(ShellSelectionButtonFlag __instance, ShellSelectionList list, InventoryData.InventorySlot item)
        {
            //Delete after real fix
            if (item.name == "Inv_Shell_Fuse" && item.amount > 0)
            {
                Debug.Log("Shell is fuse");
                Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));

                //string plugLocationShell = shellRandoData["Plug Fuse"];
                List<string> shellsInShop = new List<string> { "Conchiglie", "Bartholomew", "Baby Shoe", "Lil' Bro", "Matryoshka Large", "Shuttlecock", "Felix Cube", "Piggy Bank", "Trophy", "Imposter"};
                bool foundInShop = false;
                
                //Check if any of the Shop Shells contain the Fuse
                foreach (var shell in shellsInShop)
                {
                    if (shellRandoData[shell] == "Plug Fuse")
                    {
                        foundInShop = true;
                        break;
                    }
                }
                if (foundInShop)
                {
                    Debug.Log("FuseFromShop");
                    item.amount = 2;
                }
                else
                {
                    Debug.Log("FuseNotFromShop");
                    item.amount = 1;
                }
            }
            __instance.shellCollectable = item.LookupItem() as ShellCollectable;
            __instance.shellCollectable.cantInsure = false;
        }
    }

    //Fix Corpse Shell
    [HarmonyPatch(typeof(PlayerCorpse),nameof(PlayerCorpse.Init),new[] { typeof(Vector3), typeof(string), typeof(int), typeof(int) })]
    static class CorpseShellPatch
    {
        [HarmonyPostfix]
        static void Postfix(ref Vector3 location, ref string shellName, int shellHealth, int breadClips)
        {
            PlayerCorpse.currentCorpse.name += "_SWAP";
        }
    }

    //Fix MSS Can Spawnner
    [HarmonyPatch(typeof(PlugSpawner), "OnEnable")]
    static class MSSCanSpawnPatch
    {
        [HarmonyPostfix]
        static void Postfix(PlugSpawner __instance)
        {
            //FieldInfo currentCan = AccessTools.Field(typeof(MSSCanSpawner), "currentCan");

            if (Plugin.debugMode)
            {
                __instance.plug = AssetListCollection.GetShellPrefab("Shell_AmongUs");
                __instance.plug.name += "_SWAP";
            }
            else if (Plugin.connection.session != null)
            {
                //Exit if shell rando is not enabled
                if (!CrabFile.current.GetBool("shellRandoEnabled"))
                {
                    return;
                }
                Dictionary<string, string> shellRandoData = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrabFile.current.GetString("shellRando"));

                string newShellName = ShellData.GetShellPrefabName(shellRandoData[ShellData.GetShellApworldName(__instance.plug.prefabName)]);

                __instance.plug = AssetListCollection.GetShellPrefab(newShellName);
                __instance.plug.name += "_SWAP";
            }
        }

    }

}
