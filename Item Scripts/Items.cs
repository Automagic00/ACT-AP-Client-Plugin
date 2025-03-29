using BepInEx;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.IO;
using Newtonsoft.Json.Linq;
using Archipelago.MultiClient.Net.Models;

namespace ACTAP
{
    class ItemSwapData
    {
        static Sprite apSprite;

        public static void setAPSprite(Sprite sprite)
        {
            apSprite = sprite;
        }

        public static Sprite getAPSprite() => apSprite;

        public static void RecieveItemVisual(Item item)
        {
            item.PickupVisually();
        }
        public static void CustomRecieveItemVisual(string name, Sprite sprite)
        {
            //item.PickupVisually();
            GUIManager.instance.HUD.itemNotification.Play(name, false, sprite, "test", 0);
        }
        public static void CustomItemVisual(ItemInfo networkItem)
        {
            Debug.Log("ItemVisual");
            ArchipelagoSession session = Plugin.GetConnection().session;
            var playerName = session.Players.GetPlayerName(networkItem.Player);
            var itemName = networkItem.ItemDisplayName;


            Debug.Log("CustomVis");
            GUIManager.instance.HUD.itemNotification.Play(playerName + "'s " + itemName, false, apSprite, "test", 0);
        }
        public static void GetItem(string name)
        {
            ItemEnum itemToGet = ItemEnum.NULL;
            StowawayEnum stowawayToGet = StowawayEnum.NULL;
            CostumeEnum costumeToGet = CostumeEnum.NULL;

            switch (name)
            {
                case "Fishing Line (Grapple)": itemToGet = ItemEnum.FishingLine; break;
                case "Pristine Pearl": itemToGet = ItemEnum.DuchessPearl; break;
                case "Bloodstar Limb": itemToGet = ItemEnum.BloodStarLimb; break;
                case "Heartkelp Sprout": itemToGet = ItemEnum.HeartkelpSprout; break;
                case "Siphonophore": stowawayToGet = StowawayEnum.Siphonophore; break;
                case "Seastar": stowawayToGet = StowawayEnum.SeaStar; break;
                case "Sponge": stowawayToGet = StowawayEnum.Sponge; break;
                case "Another Crab": stowawayToGet = StowawayEnum.AnotherCrab; break;
                case "Sand Dollar": stowawayToGet = StowawayEnum.SandDollar; break;
                case "Limpet": stowawayToGet = StowawayEnum.Limpet; break;
                case "Barnacle": stowawayToGet = StowawayEnum.Barnacle; break;
                case "Mussel": stowawayToGet = StowawayEnum.Mussel; break;
                case "Anemone": stowawayToGet = StowawayEnum.Anemone; break;
                case "Whelk": stowawayToGet = StowawayEnum.Whelk; break;
                case "Breadclaw": itemToGet = ItemEnum.BreadClaw; break;
                case "Chipclaw": itemToGet = ItemEnum.ChipClaw; break;
                case "Hairclaw": itemToGet = ItemEnum.HairClaw; break;
                case "Clothesclaw": itemToGet = ItemEnum.ClothesClaw; break;
                case "Captain": costumeToGet = CostumeEnum.Nephro; break;
                default: Debug.Log("could not get item " + name); break;
            }

            if (itemToGet != ItemEnum.NULL)
            {
                GetItem(itemToGet);
            }
            if (stowawayToGet != StowawayEnum.NULL)
            {
                GetItem(stowawayToGet);
            }
            if (costumeToGet != CostumeEnum.NULL)
            {
                GetItem(costumeToGet);
            }
        }
        public static void GetItem(long id)
        {
            ItemEnum itemToGet = ItemEnum.NULL;
            StowawayEnum stowawayToGet = StowawayEnum.NULL;
            CostumeEnum costumeToGet = CostumeEnum.NULL;
            AdaptationEnum adaptationToGet = AdaptationEnum.NULL;
            SkillEnum skillToGet = SkillEnum.NULL;
            TrapEnum trapToGet = TrapEnum.NULL;
            id = id - 483021700;

            switch (id)
            {
                case 0: itemToGet = ItemEnum.NULL; break;
                case 1: itemToGet = ItemEnum.Fork; break; //Fork
                case 2: itemToGet = ItemEnum.NULL; break; //Heart Kelp Pod
                case 3: itemToGet = ItemEnum.FishingLine; break;
                case 4: itemToGet = ItemEnum.DuchessPearl; break;
                case 5: itemToGet = ItemEnum.MapPiece1; break;
                case 6: itemToGet = ItemEnum.MapPiece2; break;
                case 7: itemToGet = ItemEnum.MapPiece3; break;
                case 8: itemToGet = ItemEnum.BloodStarLimb; break;
                case 9: itemToGet = ItemEnum.HeartkelpSprout; break;
                case 10: itemToGet = ItemEnum.OldWorldWhorl; break;
                case 11: itemToGet = ItemEnum.StainlessRelic; break;
                case 12: itemToGet = ItemEnum.LurePouch; break;
                case 13: stowawayToGet = StowawayEnum.Anemone; break;
                case 14: stowawayToGet = StowawayEnum.AnemonePlus; break;
                case 15: stowawayToGet = StowawayEnum.AnemonePlusPlus; break;
                case 16: stowawayToGet = StowawayEnum.AnotherCrab; break;
                case 17: stowawayToGet = StowawayEnum.Barnacle; break;
                case 18: stowawayToGet = StowawayEnum.BarnaclePlus; break;
                case 19: stowawayToGet = StowawayEnum.BarnaclePlusPlus; break;
                case 20: stowawayToGet = StowawayEnum.BleachedPhytoplankton; break;
                case 21: stowawayToGet = StowawayEnum.Bobber; break;
                case 22: stowawayToGet = StowawayEnum.Chum ; break;
                case 23: stowawayToGet = StowawayEnum.Cockle; break;
                case 113: stowawayToGet = StowawayEnum.CocklePlus; break;
                case 24: stowawayToGet = StowawayEnum.ContactLens; break;
                case 25: stowawayToGet = StowawayEnum.CottonBall; break;
                case 114: stowawayToGet = StowawayEnum.Earthworm; break;
                case 26: stowawayToGet = StowawayEnum.Fredrick; break;
                case 27: stowawayToGet = StowawayEnum.FruitSticker; break;
                case 28: stowawayToGet = StowawayEnum.FruitStickerPlus; break;
                case 29: stowawayToGet = StowawayEnum.GoogleyEye; break;
                case 30: stowawayToGet = StowawayEnum.Lamprey; break;
                case 31: stowawayToGet = StowawayEnum.LampreyPlus; break;
                case 32: stowawayToGet = StowawayEnum.Lanternfish; break;
                case 33: stowawayToGet = StowawayEnum.Limpet; break;
                case 34: stowawayToGet = StowawayEnum.LimpetPlus; break;
                case 35: stowawayToGet = StowawayEnum.LimpetPlusPlus; break;
                case 36: stowawayToGet = StowawayEnum.Lugnut; break;
                case 37: stowawayToGet = StowawayEnum.LumpSucker; break;
                case 38: stowawayToGet = StowawayEnum.LumpSuckerPlus; break;
                case 39: stowawayToGet = StowawayEnum.Mussel; break;
                case 40: stowawayToGet = StowawayEnum.MusselPlus; break;
                case 41: stowawayToGet = StowawayEnum.MusselPlusPlus; break;
                case 42: stowawayToGet = StowawayEnum.Oyster; break;
                case 43: stowawayToGet = StowawayEnum.PackingPeanut; break;
                case 44: stowawayToGet = StowawayEnum.Phytoplankton; break;
                case 45: stowawayToGet = StowawayEnum.PhytoplanktonPlus; break;
                case 46: stowawayToGet = StowawayEnum.PufferQuill; break;
                case 47: stowawayToGet = StowawayEnum.RazorBlade; break;
                case 48: stowawayToGet = StowawayEnum.RubberBand; break;
                case 49: stowawayToGet = StowawayEnum.RustyNail; break;
                case 50: stowawayToGet = StowawayEnum.RustyNailPlus; break;
                case 51: stowawayToGet = StowawayEnum.Salp; break;
                case 52: stowawayToGet = StowawayEnum.SalpPlus; break;
                case 53: stowawayToGet = StowawayEnum.SandDollar; break;
                case 54: stowawayToGet = StowawayEnum.SeaCucumber; break;
                case 55: stowawayToGet = StowawayEnum.SeaSlug; break;
                case 56: stowawayToGet = StowawayEnum.SeaStar; break;
                case 57: stowawayToGet = StowawayEnum.SeaStarPlus; break;
                case 58: stowawayToGet = StowawayEnum.SeaStarPlusPlus; break;
                case 59: stowawayToGet = StowawayEnum.SharkTooth; break;
                case 60: stowawayToGet = StowawayEnum.SharkToothPlus; break;
                case 61: stowawayToGet = StowawayEnum.Sinker; break;
                case 62: stowawayToGet = StowawayEnum.SinkerPlus; break;
                case 63: stowawayToGet = StowawayEnum.SinkerPlusPlus; break;
                case 64: stowawayToGet = StowawayEnum.Siphonophore; break;
                case 65: stowawayToGet = StowawayEnum.SiphonophorePlus; break;
                case 66: stowawayToGet = StowawayEnum.SmallBattery; break;
                case 67: stowawayToGet = StowawayEnum.SmallBatteryPlus; break;
                case 68: stowawayToGet = StowawayEnum.Sponge; break;
                case 69: stowawayToGet = StowawayEnum.SpongePlus; break;
                case 70: stowawayToGet = StowawayEnum.TurtleShellShard; break;
                case 71: stowawayToGet = StowawayEnum.UR; break;
                case 72: stowawayToGet = StowawayEnum.UsedBandage; break;
                case 73: stowawayToGet = StowawayEnum.UsedBandagePlus; break;
                case 74: stowawayToGet = StowawayEnum.WadOfGum; break;
                case 75: stowawayToGet = StowawayEnum.Whelk; break;
                case 76: stowawayToGet = StowawayEnum.WhelkPlus; break;
                case 77: stowawayToGet = StowawayEnum.WhelkPlusPlus; break;
                case 78: stowawayToGet = StowawayEnum.Zooplankton; break;
                case 79: stowawayToGet = StowawayEnum.ZooplanktonPlus; break;

                case 80: itemToGet = ItemEnum.BreadClaw; break;
                case 81: itemToGet = ItemEnum.ChipClaw; break;
                case 82: itemToGet = ItemEnum.HairClaw; break;
                case 83: itemToGet = ItemEnum.ClothesClaw; break;
                case 84: itemToGet = ItemEnum.PaperClaw; break;
                case 85: itemToGet = ItemEnum.StapleClaw; break;
                case 86: itemToGet = ItemEnum.CarClaw; break;

                case 87: itemToGet = ItemEnum.BarbedHook; break;

                case 88: costumeToGet = CostumeEnum.Default; break;
                case 89: costumeToGet = CostumeEnum.Nephro; break;
                case 90: costumeToGet = CostumeEnum.Doctor; break;
                case 91: costumeToGet = CostumeEnum.Forsburn; break;
                case 92: costumeToGet = CostumeEnum.GarbageBag_Black; break;
                case 93: costumeToGet = CostumeEnum.GarbageBag_White; break;
                case 94: costumeToGet = CostumeEnum.Jackie; break;
                case 95: costumeToGet = CostumeEnum.Knight; break;
                case 96: costumeToGet = CostumeEnum.Krillionaire; break;
                case 97: costumeToGet = CostumeEnum.Lizz; break;
                case 98: costumeToGet = CostumeEnum.Maid; break;
                case 99: costumeToGet = CostumeEnum.PrideGay; break;
                case 100: costumeToGet = CostumeEnum.Boss; break;
                case 101: costumeToGet = CostumeEnum.Cowboy; break;
                case 102: costumeToGet = CostumeEnum.CultLeader; break;
                case 103: costumeToGet = CostumeEnum.BlueCollar; break;
                case 104: costumeToGet = CostumeEnum.Clown; break;

                case 105: adaptationToGet = AdaptationEnum.RoyalWave; break;
                case 106: adaptationToGet = AdaptationEnum.BobbitTrap; break;
                case 107: adaptationToGet = AdaptationEnum.BubbleBullet; break;
                case 108: adaptationToGet = AdaptationEnum.Eelectrocute; break;
                case 109: adaptationToGet = AdaptationEnum.MantisPunch; break;
                case 110: adaptationToGet = AdaptationEnum.SnailSanctum; break;
                case 111: adaptationToGet = AdaptationEnum.SpectralTentacle; break;
                case 112: adaptationToGet = AdaptationEnum.UrchinToss; break;
                //case 113: break; //Overlapped
                //case 114: break; //Overlapped
                case 115: skillToGet = SkillEnum.NaturalDefenses; break;
                case 116: skillToGet = SkillEnum.Aggravation; break;
                case 117: skillToGet = SkillEnum.SelfRepair; break;
                case 118: skillToGet = SkillEnum.Kintsugi; break;
                case 119: skillToGet = SkillEnum.Skewer; break;
                case 120: skillToGet = SkillEnum.Plunge; break;
                case 121: skillToGet = SkillEnum.ScrapHammer; break;
                case 122: skillToGet = SkillEnum.Dispatch; break;
                case 123: skillToGet = SkillEnum.Spearfishing; break;
                case 124: skillToGet = SkillEnum.WaveBreaker; break;
                case 125: skillToGet = SkillEnum.Streamline; break;
                case 126: skillToGet = SkillEnum.Housewarming; break;
                case 127: skillToGet = SkillEnum.CircleOfLife; break;
                case 128: skillToGet = SkillEnum.ElusivePrey; break;
                case 129: skillToGet = SkillEnum.EbbAndFlow; break;
                case 130: skillToGet = SkillEnum.Umami1; break;
                case 131: skillToGet = SkillEnum.Umami2; break;
                case 132: skillToGet = SkillEnum.Umami3; break;
                case 133: skillToGet = SkillEnum.Shelleport; break;
                case 134: skillToGet = SkillEnum.Skeddadle; break;
                case 135: itemToGet = ItemEnum.LevelRespec; break;
                case 136: skillToGet = SkillEnum.Parry; break;
                case 137: skillToGet = SkillEnum.Riposte; break;
                case 138: trapToGet = TrapEnum.GunkTrap; break;
                case 139: trapToGet = TrapEnum.ScourTrap; break;
                case 140: trapToGet = TrapEnum.BleachedTrap; break;
                case 141: trapToGet = TrapEnum.FearTrap; break;
                case 142: trapToGet = TrapEnum.ClutzTrap; break;
                case 143: trapToGet = TrapEnum.TextTrap; break;
                case 144: trapToGet = TrapEnum.ShellShatterTrap; break;
                case 145: trapToGet = TrapEnum.PoisonCocktailTrap; break;
                case 146: trapToGet = TrapEnum.TaserTrap; break;
                default: Debug.Log("could not get item " + id); break;
            }

            if(itemToGet != ItemEnum.NULL)
            {
                GetItem(itemToGet);
            }
            if(stowawayToGet != StowawayEnum.NULL)
            {
                GetItem(stowawayToGet);
            }
            if(costumeToGet != CostumeEnum.NULL)
            {
                GetItem(costumeToGet);
            }
            if(adaptationToGet != AdaptationEnum.NULL)
            {
                GetItem(adaptationToGet);
            }
            if(skillToGet != SkillEnum.NULL)
            {
                GetItem(skillToGet);
            }
            if(trapToGet != TrapEnum.NULL)
            {
                GetItem(trapToGet);
            }
        }

        static void GetItem(TrapEnum trapToGet)
        {
            string name = "";
            Sprite sprite = apSprite;
            switch (trapToGet)
            {
                case TrapEnum.TextTrap:
                    Item_Scripts.Traps.DarkSoulsTrap trap = new Item_Scripts.Traps.DarkSoulsTrap();
                    trap.ActivateTrap();
                    name = "Text Trap";
                    sprite = apSprite;
                    break;

                case TrapEnum.GunkTrap:
                    Item_Scripts.Traps.StatusEffectTraps.Gunked();
                    name = "Gunked Trap";
                    sprite = apSprite;
                    break;

                case TrapEnum.ScourTrap:
                    Item_Scripts.Traps.StatusEffectTraps.Scoured();
                    name = "Scoured Trap";
                    sprite = apSprite;
                    break;

                case TrapEnum.BleachedTrap:
                    Item_Scripts.Traps.StatusEffectTraps.Bleached();
                    name = "Bleached Trap";
                    sprite = apSprite;
                    break;

                case TrapEnum.FearTrap:
                    Item_Scripts.Traps.StatusEffectTraps.Afraid();
                    name = "Fear Trap";
                    sprite = apSprite;
                    break;

                case TrapEnum.TaserTrap:
                    Item_Scripts.Traps.StatusEffectTraps.Electrocute();
                    name = "Taser Trap";
                    sprite = apSprite;
                    break;

                case TrapEnum.PoisonCocktailTrap:
                    Item_Scripts.Traps.StatusEffectTraps.PoisonCocktail();
                    name = "Poison Cocktail Trap";
                    sprite = apSprite;
                    break;

                case TrapEnum.ShellShatterTrap:
                    Item_Scripts.Traps.ShellShatterTrap.Shatter();
                    name = "Shell Shatter Trap";
                    sprite = apSprite;
                    break;

                case TrapEnum.ClutzTrap:
                    Item_Scripts.Traps.ClutzTrap.ActivateTrap();
                    name = "Clutz Trap";
                    sprite = apSprite;
                    break;
            }
            CustomRecieveItemVisual(name, sprite);
        }


        public static void GetItem(ItemEnum itemToGet)
        {
            Item item = new Item();
            if (itemToGet == ItemEnum.Fork)
            {
                //MethodInfo method = AccessTools.Method(typeof(ForkItem), "Unlocked");
                CrabFile.current.unlocks[SkillWorldUnlocks.BasicFork].unlocked = true;
                GameManager.instance.StartCoroutine(Player.singlePlayer.view.ObtainForkRoutine(true, Player.singlePlayer.transform.position));
                
            }
            else
            {
                
                item.item = ScriptableObject.CreateInstance<CollectableItemData>();
                item.item.name = GetItemJson(itemToGet);

                //Replace all item data with masterlist item that matches name
                foreach (var listItem in InventoryMasterList.staticList)
                {
                    if (listItem.name == item.item.name)
                    {
                        item.item = listItem;
                    }
                }

                if (itemToGet == ItemEnum.FishingLine)
                {
                    CrabFile.current.unlocks[SkillWorldUnlocks.String].unlocked = true;
                }
                else if (itemToGet == ItemEnum.MapPiece1)
                {
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotAnyMap].unlocked = true;
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotExpiredGroveMap].unlocked = true;
                }
                else if (itemToGet == ItemEnum.MapPiece2)
                {
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotAnyMap].unlocked = true;
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotFlotsamValeMap].unlocked = true;
                }
                else if (itemToGet == ItemEnum.MapPiece3)
                {
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotAnyMap].unlocked = true;
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotPagurusMap].unlocked = true;
                }
                else if (itemToGet == ItemEnum.DuchessPearl)
                {
                    UserSettings.SetBool("tutorialPopupsActive", true);

                    CrabFile.current.progressData[ProgressData.ShallowsProgress.PearlPickedUp].unlocked = true;
                    //Debug.Log(item.item.summary);
                    item.item.summary = "NOTICE: If playing the Archipelago players must save, quit to menu, then continue the file to enter Fallen Slacktide";
                    //CrabFile.current.progressData[ProgressData.ShallowsProgress.EnteredFallenSlacktide].unlocked = true;
                    GameManager.events.CheckProgress();
                }

                CrabFile.current.inventoryData.AdjustAmount(item.item, 1);
                
                RecieveItemVisual(item);
            }


            
        }

        public static void GetItem(StowawayEnum itemToGet)
        {
            Item item = new Item();
            item.item = ScriptableObject.CreateInstance<CollectableItemData>();
            item.item.name = GetItemJson(itemToGet);

            //Replace all item data with masterlist item that matches name
            foreach (var listItem in InventoryMasterList.staticList)
            {
                if (listItem.name == item.item.name)
                {
                    item.item = listItem;
                }
            }

            CrabFile.current.inventoryData.AdjustAmount(item.item, 1);
            RecieveItemVisual(item);
        }

        public static void GetItem(AdaptationEnum itemToGet)
        {
            Item item = new Item();
            item.item = ScriptableObject.CreateInstance<CollectableItemData>();
            item.item.name = GetItemJson(itemToGet);

            //Replace all item data with masterlist item that matches name
            foreach (var listItem in InventoryMasterList.staticList)
            {
                if (listItem.name == item.item.name)
                {
                    item.item = listItem;
                }
            }

            CrabFile.current.inventoryData.AdjustAmount(item.item, 1);
            CustomRecieveItemVisual(item.DisplayName,item.item.GetIcon());
        }

        public static void GetItem(CostumeEnum itemToGet)
        {
            Item item = new Item();
            item.item = ScriptableObject.CreateInstance<CollectableItemData>();
            item.item.name = GetItemJson(itemToGet);

            //Replace all item data with masterlist item that matches name
            foreach (var listItem in InventoryMasterList.staticList)
            {
                if (listItem.name == item.item.name)
                {
                    item.item = listItem;
                }
            }

            CrabFile.current.inventoryData.AdjustAmount(item.item, 1);
            RecieveItemVisual(item);
        }

        public static void GetItem(SkillEnum skillToGet)
        {
            SkillTreeData skillTree = new SkillTreeData();
            
            SkillTreeUnlocks skill = GetSkillTreeUnlock(skillToGet);
            skillTree.SetSkill(skill, true, false);
            Debug.Log("Skill Recieved" + skillTree.displayTitle);
            CustomRecieveItemVisual("Recieved Skill: "+skillToGet.ToString(),apSprite);
        }

        public static SkillTreeUnlocks GetSkillTreeUnlock(SkillEnum skillEnum)
        {
            switch (skillEnum)
            {
                case SkillEnum.Shelleport: return SkillTreeUnlocks.Sheleport;
                case SkillEnum.Parry: return SkillTreeUnlocks.Parries;
                case SkillEnum.Riposte: return SkillTreeUnlocks.Riposte;
                case SkillEnum.NaturalDefenses: return SkillTreeUnlocks.NakedParries;
                case SkillEnum.Aggravation: return SkillTreeUnlocks.Aggravation;
                case SkillEnum.SelfRepair: return SkillTreeUnlocks.SelfRepair;
                case SkillEnum.Kintsugi: return SkillTreeUnlocks.Kintsugi;
                case SkillEnum.Skewer: return SkillTreeUnlocks.Skewer;
                case SkillEnum.Plunge: return SkillTreeUnlocks.Plunge;
                case SkillEnum.ScrapHammer: return SkillTreeUnlocks.ScrapHammer;
                case SkillEnum.Dispatch: return SkillTreeUnlocks.Dispatch;
                case SkillEnum.Spearfishing: return SkillTreeUnlocks.Fishing;
                case SkillEnum.WaveBreaker: return SkillTreeUnlocks.WaveBreaker;
                case SkillEnum.Streamline: return SkillTreeUnlocks.AirDodge;
                case SkillEnum.Housewarming: return SkillTreeUnlocks.Housewarming;
                case SkillEnum.CircleOfLife: return SkillTreeUnlocks.ChumInTheWater;
                case SkillEnum.ElusivePrey: return SkillTreeUnlocks.ElusivePrey;
                case SkillEnum.Skeddadle: return SkillTreeUnlocks.Skedaddle;
                case SkillEnum.EbbAndFlow: return SkillTreeUnlocks.EbbAndFlow;
                case SkillEnum.Umami1: return SkillTreeUnlocks.UmamiTrainingA;
                case SkillEnum.Umami2: return SkillTreeUnlocks.UmamiTrainingB;
                case SkillEnum.Umami3: return SkillTreeUnlocks.UmamiTrainingC;

                default: return SkillTreeUnlocks.Sheleport;
            }
        }

        public static string GetItemJson(ItemEnum itemName)
        {
            switch (itemName)
            {
                case ItemEnum.BreadClaw: return "00_BreadClaw";
                case ItemEnum.ChipClaw: return "01_ChipClaw";
                case ItemEnum.ClothesClaw: return "02_ClothesClaw";
                case ItemEnum.HairClaw: return "03_HairClaw";
                case ItemEnum.PaperClaw: return "04_PaperClaw";
                case ItemEnum.StapleClaw:return "05_StapleClaw";
                case ItemEnum.CarClaw: return "06_CarClaw";
                case ItemEnum.BarbedHook: return "BarbedHook";
                case ItemEnum.BarbedHood_Bundle10: return "BarbedHook_Bundle10";
                case ItemEnum.BloodStarLimb:return "BloodstarLimb";
                case ItemEnum.BloodStarWhole: return "BloodstarWhole";
                case ItemEnum.HeartkelpSprout: return "Heartkelp Sprout"; 
                case ItemEnum.DuchessPearl:return "Junk_DuchessPearl";
                case ItemEnum.FishingLine: return "Junk_FishingLine";
                case ItemEnum.KidsDoll: return "Junk_KidsDoll";
                case ItemEnum.LevelRespec: return "Level_Respec";
                case ItemEnum.LurePouch: return "LurePouch";
                case ItemEnum.MapPiece1: return "MapPiece1";
                case ItemEnum.MapPiece2: return "MapPiece2";
                case ItemEnum.MapPiece3:return "MapPiece3";
                case ItemEnum.OldWorldWhorl: return "OldWorldWhorl";
                case ItemEnum.StainlessRelic:return "StainlessRelic";
                case ItemEnum.Fork: return "ForkUnlock (1)";


                default: return "00_BreadClaw";
                    
            }    
        }
        public static string GetItemJson(AdaptationEnum itemName)
        {
            switch (itemName)
            {
                case AdaptationEnum.BobbitTrap: return "Item_Tech_BobbitTrap";
                case AdaptationEnum.BubbleBullet: return "Item_Tech_BubbleBullet";
                case AdaptationEnum.Eelectrocute: return "Item_Tech_Eelectrocute";
                case AdaptationEnum.MantisPunch: return "Item_Tech_MantisPunch";
                case AdaptationEnum.RoyalWave: return "Item_Tech_RoyalWave";
                case AdaptationEnum.SnailSanctum: return "Item_Tech_SnailSanctum";
                case AdaptationEnum.SpectralTentacle: return "Item_Tech_SpectralTentacle";
                case AdaptationEnum.UrchinToss: return "Item_Tech_UrchinToss";
                default: return "";

            }
        }

        public static string GetItemJson(StowawayEnum itemName)
        {
            switch (itemName)
            {
                case StowawayEnum.Anemone: return "Stowaway_Anemone";
                case StowawayEnum.AnemonePlus: return "Stowaway_Anemone+";
                case StowawayEnum.AnemonePlusPlus: return "Stowaway_Anemone++";
                case StowawayEnum.AnotherCrab: return "Stowaway_Another Crab";
                case StowawayEnum.Barnacle: return "Stowaway_Barnacle";
                case StowawayEnum.BarnaclePlus: return "Stowaway_Barnacle+";
                case StowawayEnum.BarnaclePlusPlus: return "Stowaway_Barnacle++";
                case StowawayEnum.BleachedPhytoplankton : return "Stowaway_Bleached Phytoplankton";
                case StowawayEnum.Bobber: return "Stowaway_Bobber";
                case StowawayEnum.Chum: return "Stowaway_Chum";
                case StowawayEnum.Cockle: return "Stowaway_Cockle";
                case StowawayEnum.CocklePlus: return "Stowaway_Cockle+";
                case StowawayEnum.ContactLens: return "Stowaway_Contact Lens";
                case StowawayEnum.CottonBall: return "Stowaway_Cotton Ball";
                case StowawayEnum.Earthworm: return "Stowaway_Earthworm";
                case StowawayEnum.EmptySpace: return "Stowaway_EmptySpace";
                case StowawayEnum.Fredrick: return "Stowaway_Fredrick";
                case StowawayEnum.FruitSticker: return "Stowaway_Fruit Sticker";
                case StowawayEnum.FruitStickerPlus: return "Stowaway_Fruit Sticker+";
                case StowawayEnum.GoogleyEye: return "Stowaway_Googly Eye";
                case StowawayEnum.Isopod: return "Stowaway_Isopod";
                case StowawayEnum.Lamprey: return "Stowaway_Lamprey";
                case StowawayEnum.LampreyPlus: return "Stowaway_Lamprey+";
                case StowawayEnum.Lanternfish: return "Stowaway_Lanternfish";
                case StowawayEnum.Limpet: return "Stowaway_Limpet";
                case StowawayEnum.LimpetPlus: return "Stowaway_Limpet+";
                case StowawayEnum.LimpetPlusPlus: return "Stowaway_Limpet++";
                case StowawayEnum.Lugnut: return "Stowaway_Lugnut";
                case StowawayEnum.LumpSucker: return "Stowaway_Lump Sucker";
                case StowawayEnum.LumpSuckerPlus: return "Stowaway_Lump Sucker+";
                case StowawayEnum.Mussel: return "Stowaway_Mussel";
                case StowawayEnum.MusselPlus: return "Stowaway_Mussel+";
                case StowawayEnum.MusselPlusPlus: return "Stowaway_Mussel++";
                case StowawayEnum.Oyster: return "Stowaway_Oyster";
                case StowawayEnum.PackingPeanut: return "Stowaway_Packing Peanut";
                case StowawayEnum.Phytoplankton: return "Stowaway_Phytoplankton";
                case StowawayEnum.PhytoplanktonPlus: return "Stowaway_Phytoplankton+";
                case StowawayEnum.PufferQuill: return "Stowaway_Puffer Quill";
                case StowawayEnum.RazorBlade: return "Stowaway_Razor Blade";
                case StowawayEnum.RubberBand: return "Stowaway_Rubber Band";
                case StowawayEnum.RustyNail: return "Stowaway_Rusty Nail";
                case StowawayEnum.RustyNailPlus: return "Stowaway_Rusty Nail+";
                case StowawayEnum.Salp: return "Stowaway_Salp";
                case StowawayEnum.SalpPlus: return "Stowaway_Salp+";
                case StowawayEnum.SandDollar: return "Stowaway_Sand Dollar";
                case StowawayEnum.SeaCucumber: return "Stowaway_Sea Cucumber";
                case StowawayEnum.SeaSlug: return "Stowaway_Sea Slug";
                case StowawayEnum.SeaStar: return "Stowaway_Sea Star";
                case StowawayEnum.SeaStarPlus: return "Stowaway_Sea Star+";
                case StowawayEnum.SeaStarPlusPlus: return "Stowaway_Sea Star++";
                case StowawayEnum.SharkTooth: return "Stowaway_SharkTooth";
                case StowawayEnum.SharkToothPlus: return "Stowaway_SharkTooth+";
                case StowawayEnum.Sinker: return "Stowaway_Sinker";
                case StowawayEnum.SinkerPlus: return "Stowaway_Sinker+";
                case StowawayEnum.SinkerPlusPlus: return "Stowaway_Sinker++";
                case StowawayEnum.Siphonophore: return "Stowaway_Siphonophore";
                case StowawayEnum.SiphonophorePlus: return "Stowaway_Siphonophore+";
                case StowawayEnum.SmallBattery: return "Stowaway_Small Battery";
                case StowawayEnum.SmallBatteryPlus: return "Stowaway_Small Battery+";
                case StowawayEnum.Sponge: return "Stowaway_Sponge";
                case StowawayEnum.SpongePlus: return "Stowaway_Sponge+";
                case StowawayEnum.TurtleShellShard: return "Stowaway_Turtle Shell Shard";
                case StowawayEnum.UR: return "Stowaway_UR";
                case StowawayEnum.UsedBandage: return "Stowaway_Used Bandage";
                case StowawayEnum.UsedBandagePlus: return "Stowaway_Used Bandage+";
                case StowawayEnum.WadOfGum: return "Stowaway_Wad of Gum";
                case StowawayEnum.Whelk: return "Stowaway_Whelk";
                case StowawayEnum.WhelkPlus: return "Stowaway_Whelk+";
                case StowawayEnum.WhelkPlusPlus: return "Stowaway_Whelk++";
                case StowawayEnum.Zooplankton: return "Stowaway_Zooplankton";
                case StowawayEnum.ZooplanktonPlus: return "Stowaway_Zooplankton+";
                default: return "";

            }
        }
        public static string GetItemJson(CostumeEnum itemName)
        {
            switch (itemName)
            {
                case CostumeEnum.BlueCollar: return "Inv_Costume_BlueCollar";
                case CostumeEnum.Boss: return "Inv_Costume_Boss";
                case CostumeEnum.Clown: return "Inv_Costume_Clown";
                case CostumeEnum.Cowboy: return "Inv_Costume_Cowboy";
                case CostumeEnum.CultLeader: return "Inv_Costume_CultLeader";
                case CostumeEnum.Default: return "Inv_Costume_Default";
                case CostumeEnum.Doctor: return "Inv_Costume_Doctor";
                case CostumeEnum.Forsburn: return "Inv_Costume_Forsburn";
                case CostumeEnum.GarbageBag_Black: return "Inv_Costume_GarbageBag-Black";
                case CostumeEnum.GarbageBag_White: return "Inv_Costume_GarbageBag-White";
                case CostumeEnum.Jackie: return "Inv_Costume_Jackie";
                case CostumeEnum.Knight: return "Inv_Costume_Knight";
                case CostumeEnum.Krillionaire: return "Inv_Costume_Krillionaire";
                case CostumeEnum.Lizz: return "Inv_Costume_Lizz";
                case CostumeEnum.Maid: return "Inv_Costume_Maid";
                case CostumeEnum.Nephro: return "Inv_Costume_Nephro";
                case CostumeEnum.PrideGay: return "Inv_Costume_PrideGay";
                default: return "Inv_Costume_Default";
            }
        }


        public enum AdaptationEnum
        {          
            BobbitTrap,
            BubbleBullet,
            Eelectrocute,
            MantisPunch,
            RoyalWave,
            SnailSanctum,
            SpectralTentacle,
            UrchinToss,
            NULL
        }

        public enum ItemEnum
        {
            BreadClaw,
            ChipClaw,
            ClothesClaw,
            HairClaw,
            PaperClaw,
            StapleClaw,
            CarClaw,
            BarbedHook,
            BarbedHood_Bundle10,
            BloodStarLimb,
            BloodStarWhole,
            HeartkelpSprout,
            DuchessPearl,
            FishingLine,
            KidsDoll,
            LevelRespec,
            LurePouch,
            MapPiece1,
            MapPiece2,
            MapPiece3,
            OldWorldWhorl,
            StainlessRelic,
            Fork,
            NULL
        }

        public enum StowawayEnum
        {
            Anemone,
            AnemonePlus,
            AnemonePlusPlus,
            AnotherCrab,
            Barnacle,
            BarnaclePlus,
            BarnaclePlusPlus,
            BleachedPhytoplankton,
            Bobber,
            Chum,
            Cockle,
            CocklePlus,
            ContactLens,
            CottonBall,
            Earthworm,
            EmptySpace,
            Fredrick,
            FruitSticker,
            FruitStickerPlus,
            GoogleyEye,
            Isopod,
            Lamprey,
            LampreyPlus,
            Lanternfish,
            Limpet,
            LimpetPlus,
            LimpetPlusPlus,
            Lugnut,
            LumpSucker,
            LumpSuckerPlus,
            Mussel,
            MusselPlus,
            MusselPlusPlus,
            Oyster,
            PackingPeanut,
            Phytoplankton,
            PhytoplanktonPlus,
            PufferQuill,
            RazorBlade,
            RubberBand,
            RustyNail,
            RustyNailPlus,
            Salp,
            SalpPlus,
            SandDollar,
            SeaCucumber,
            SeaSlug,
            SeaStar,
            SeaStarPlus,
            SeaStarPlusPlus,
            SharkTooth,
            SharkToothPlus,
            Sinker,
            SinkerPlus,
            SinkerPlusPlus,
            Siphonophore,
            SiphonophorePlus,
            SmallBattery,
            SmallBatteryPlus,
            Sponge,
            SpongePlus,
            TurtleShellShard,
            UR,
            UsedBandage,
            UsedBandagePlus,
            WadOfGum,
            Whelk,
            WhelkPlus,
            WhelkPlusPlus,
            Zooplankton,
            ZooplanktonPlus,
            NULL

        }

        public enum CostumeEnum
        { 
            BlueCollar,
            Boss,
            Clown,
            Cowboy,
            CultLeader,
            Default,
            Doctor,
            Forsburn,
            GarbageBag_Black,
            GarbageBag_White,
            Jackie,
            Knight,
            Krillionaire,
            Lizz,
            Maid,
            Nephro,
            PrideGay,
            NULL
        }

        public enum SkillEnum
        {
            Shelleport,
            Parry,
            Riposte,
            NaturalDefenses,
            Aggravation,
            SelfRepair,
            Kintsugi,
            Skewer,
            Plunge,
            ScrapHammer,
            Dispatch,
            Spearfishing,
            WaveBreaker,
            Streamline,
            Housewarming,
            CircleOfLife,
            ElusivePrey,
            Skeddadle,
            EbbAndFlow,
            Umami1,
            Umami2,
            Umami3,
            NULL
        }

        public enum TrapEnum
        {
            GunkTrap,
            ScourTrap,
            BleachedTrap,
            FearTrap,
            ClutzTrap,
            TextTrap,
            ShellShatterTrap,
            PoisonCocktailTrap,
            TaserTrap,
            NULL
        }
    }
}
