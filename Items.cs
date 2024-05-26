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

namespace ACTAP
{
    class ItemSwapData
    {
        public static void RecieveItemVisual(Item item)
        {
            item.PickupVisually();
        }
        public static void GetItem(ItemEnum itemToGet)
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
            RecieveItemVisual(item);
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
            UrchinToss
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
            StainlessRelic
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
            ZooplanktonPlus

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
            PrideGay
        }
    }
}
