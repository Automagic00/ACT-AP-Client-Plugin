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
        public static string deathMsgSent = "";
        public static void RecieveDeathLink(string deathMessage)
        {
            Debug.Log("DL Recieved");
            deathMsg = deathMessage;
            isDeathLink = true;

            Debug.Log("Player Die");
            Player.singlePlayer.Die();
        }
        public static void GenerateDeathMessage(HitEvent killEvent)
        {
            var session = Plugin.connection.session;

            if (Plugin.connection.session != null)
            {
                string msgBody = "";
                if (killEvent.source.GetComponent<Boss>() != null)
                {
                    string killer = killEvent.source.GetComponent<Boss>().bossName;
                    killer = killer.Replace("Enemy_", "");
                    killer = killer.Replace("Boss_", "");
                    killer = killer.Replace("_", " ");

                    string[] possibleMsgBodies;

                    switch (killer)
                    {
                        case "NephroCaptainoftheGuard": possibleMsgBodies = new string[] { "'s soul is destined for the drain" , "was impaled by an overzealous guard", "did not take their claws off", "was a lawbreaker", "was not above the law"}; break;
                        case "Duchess": possibleMsgBodies = new string[] { "is as sand beneath the tide", "was worthless", "could not preserve their beauty", "was destined to wash away" }; break;
                        case "Bruiser": possibleMsgBodies = new string[] { "had a bottle broken over their head", "died to the demo boss", "couldn't follow her majesty's order" }; break;
                        case "RoyalShellsplitter": possibleMsgBodies = new string[] { "was promptly executed", "found a secret fight a bit too early", "probably shouldn't have gone that way" }; break;
                        case "Pagurus": possibleMsgBodies = new string[] { "became a snack", "wasn't very filling", "strayed too far from the path", "got a little bit eated" }; break;
                        case "DiseasedLichenthrope": possibleMsgBodies = new string[] { "expired in the grove" }; break;
                        case "BruiserGrove": possibleMsgBodies = new string[] { "became a noodle topping", "couldn't handle a connoisseur" }; break;
                        case "HeikeaIntimidationCrab": possibleMsgBodies = new string[] { "was easily intimidated", "isn't a fan of sushi", "thought the shopsticks were shorter" }; break;
                        case "GrovekeeperTopoda": possibleMsgBodies = new string[] { "got punched real hard" }; break;
                        case "TheConsortium": possibleMsgBodies = new string[] { "got jumped", "was crushed by a cage full of crabs"  }; break;
                        case "ScuttleportBruiser": possibleMsgBodies = new string[] { "was turned into a canvas", "couldn't escape the sludge" }; break;
                        case "TwinPistolShrimp": possibleMsgBodies = new string[] { "died to the easiest boss in the game", "got shot from behind", "lost track of the other one" }; break;
                        case "VoltaiTheAccumulator": possibleMsgBodies = new string[] { "played too hard", "made a shocking discovery", "probably got cheesed" }; break;
                        case "Roland": possibleMsgBodies = new string[] { "trespassed onto private property", "was sent down the drain", "'s corpse became property of Shellfish Corp", "hates pinball now" }; break;
                        case "MOONHERMIT": possibleMsgBodies = new string[] { "learned what getting parried feels like", "got baited", "thought it was a Moon Snail Shell" }; break;
                        case "INKERTON": possibleMsgBodies = new string[] { "died at the bottom of the drain", "is a worthless sinner", "was on the business end of a party popper", "was riddled with confetti", "got shot" }; break;
                        case "BLEACHEDKING": possibleMsgBodies = new string[] { "was scrubbed clean", "ingested too much bleach" }; break;
                        case "MOLTEDKING": possibleMsgBodies = new string[] { "was murdered by a naked king", "was not reborn" }; break;
                        case "PRAYADUBIA1": possibleMsgBodies = new string[] { "couldn't bring themselves to killing a friend", "listed to the voices" }; break;
                        case "PRAYADUBIA2": possibleMsgBodies = new string[] { "couldn't hold out", "wasn't able to run away" }; break;
                        case "FIRTH_1": possibleMsgBodies = new string[] { "didn't get what they wanted", "lost to an honest businesscrab", "learned the power of the Perfect Whorl" }; break;
                        case "FIRTH_2": possibleMsgBodies = new string[] { "wasn't ready for phase 2", "couldn't adapt", "died atop an island of trash" }; break;
                        default: possibleMsgBodies = new string[] { "died to " + killer }; break;
                    }
                    System.Random rand = new System.Random();
                    msgBody = possibleMsgBodies[rand.Next(possibleMsgBodies.Length)];
                }
                else
                {


                    string killer = killEvent.source.name;
                    killer = killer.Replace("Enemy_", "");
                    killer = killer.Replace("Boss_", "");
                    killer = killer.Replace("_", " ");

                    msgBody = "died to " + killer;
                }

                deathMsgSent =  $"{session.Players.GetPlayerName(session.ConnectionInfo.Slot)} {msgBody}.";
                Debug.Log("Death Msg sent: " + deathMsgSent);
            }
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
                Plugin.connection.SendDeathLink(DeathLinkPatch.deathMsgSent);
            }
        }

    }

    [HarmonyPatch(typeof(Player),nameof(Player.Die), new[] {typeof(HitEvent),typeof(bool)})]
    class KillEventPatch
    {
        [HarmonyPrefix]
        static void Prefix(ref HitEvent killEvent, Player __instance)
        {
            if (!DeathLinkPatch.isDeathLink && Plugin.connection.session != null)
            {
                DeathLinkPatch.GenerateDeathMessage(killEvent);
                
            }
        }
    }
}
