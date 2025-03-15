using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    class DeathLinkPatch
    {
        public static bool isDeathLink = false;
        public static string deathMsg = "";
        public static void RecieveDeathLink(string deathMessage)
        {
            Debug.Log("DL Recieved");
            deathMsg = deathMessage;
            isDeathLink = true;

            Debug.Log("Player Die");
            Player.singlePlayer.Die();
        }
    }

    [HarmonyPatch(typeof(Player),"Update")]
    class KillPlayerPatch
    {
        [HarmonyPostfix]
        static void KillPlayerPost(Player __instance)
        {
            if (DeathLinkPatch.isDeathLink && __instance.health > 0)
            {
                //DeathLinkPatch.isDeathLink = false;
                __instance.TakeDamage(999999f);
            }
        }
    }

    [HarmonyPatch(typeof(GUIManager), nameof(GUIManager.Die))]
    class DiePatch
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if ((Plugin.connection.session == null && !Plugin.debugMode) || !DeathLinkPatch.isDeathLink)
            {
                return true;
            }

            DeathLinkPatch.isDeathLink = false;

            string deathStringUse = DeathLinkPatch.deathMsg;
            Debug.Log(deathStringUse);

            GUIManager.instance.PlayDarkSoulsText(deathStringUse, "YouDied");
            //Debug.Log(deathString);
            return false;
        }
    }

    [HarmonyPatch(typeof(GameManager),"PlayerDied")]
    class PlayerDiePatch
    {
        [HarmonyPrefix]
        static void PlayerDiedPrefix()
        {
            if (!DeathLinkPatch.isDeathLink && Plugin.connection.session!=null)
            {
                Plugin.connection.SendDeathLink();
            }
        }

    }
}
