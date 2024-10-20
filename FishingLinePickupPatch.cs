using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ACTAP
{
    [HarmonyPatch(typeof(ShallowsManager),"Start")]
    class FishingLinePickupPatch
    {
        [HarmonyPostfix]
        static void PickupPostfix(Player __instance)
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            Debug.Log("Player Start");
            Debug.Log("PlayerInShallows");
            if ((Plugin.connection.session != null || Plugin.debugMode) && (CrabFile.current.progressData[ProgressData.ShallowsProgress.PearlPickedUp].unlocked == true || CrabFile.current.unlocks[SkillWorldUnlocks.String].unlocked == true))
            {
                //Plugin.connection.ActivateCheck(483021702);
                Debug.Log("Try to Create Fishing Line");
                
                CreateCustom.CreateItemWhenPossible(new Vector3(652.8f, 79.1f, 1243.8f), "FishingLineUnlock", ItemSwapData.ItemEnum.FishingLine, __instance);
            }
            
        }
    }
}
