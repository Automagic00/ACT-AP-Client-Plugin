using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTAP
{
    class ShellData
    {
        public static Dictionary<string, string> shellTableByPrefab = new Dictionary<string, string>();
        public static Dictionary<string, string> shellTableByApworld = new Dictionary<string, string>();

        private static List<string> prefabNames = new List<string>();
        private static List<string> apworldNames = new List<string>();
        public static void GenerateTable()
        {
            prefabNames.Add("Shell_AggroBro");
            apworldNames.Add("Lil' Bro");

            prefabNames.Add("Shell_AmongUs");
            apworldNames.Add("Imposter");

            prefabNames.Add("Shell_BabyShoe");
            apworldNames.Add("Baby Shoe");

            prefabNames.Add("Shell_BadmintonBirdie");
            apworldNames.Add("Shuttlecock");

            prefabNames.Add("Shell_BananaPeel");
            apworldNames.Add("Banana Peel");

            prefabNames.Add("Shell_BoxingGlove");
            apworldNames.Add("Boxing Glove");

            prefabNames.Add("Shell_CardboardBox");
            apworldNames.Add("Cardboard Box");

            prefabNames.Add("Shell_Coconut");
            apworldNames.Add("Coconut");

            prefabNames.Add("Shell_CoffeePod");
            apworldNames.Add("Coffee Pod");

            prefabNames.Add("Shell_ComputerMouse");
            apworldNames.Add("Mouse");

            prefabNames.Add("Shell_ConciergeBell");
            apworldNames.Add("Service Bell");

            prefabNames.Add("Shell_CrabHusk");
            apworldNames.Add("Crab Husk");

            prefabNames.Add("Shell_Dentures");
            apworldNames.Add("Dentures");

            prefabNames.Add("Shell_DetergentCap");
            apworldNames.Add("Detergent Cap");

            prefabNames.Add("Shell_DiscoBall");
            apworldNames.Add("Disco Ball");

            prefabNames.Add("Shell_DollHead");
            apworldNames.Add("Doll Head");

            prefabNames.Add("Shell_DumpTruck");
            apworldNames.Add("Dumptruck");

            prefabNames.Add("Shell_EggShell");
            apworldNames.Add("Egg Shell");

            prefabNames.Add("Shell_FelixCube");
            apworldNames.Add("Felix Cube");

            prefabNames.Add("Shell_FidgetSpinner");
            apworldNames.Add("Fidget Spinner");

            prefabNames.Add("Shell_FKeyCap");
            apworldNames.Add("F Key");

            prefabNames.Add("Shell_Fuse");
            apworldNames.Add("Plug Fuse");

            prefabNames.Add("Shell_Gacha");
            apworldNames.Add("Gacha Capsule");

            prefabNames.Add("Shell_GameCartridge");
            apworldNames.Add("Going Under 64");

            prefabNames.Add("Shell_Gun");
            apworldNames.Add("Gun");

            prefabNames.Add("Shell_HomeShell");
            apworldNames.Add("Home");

            prefabNames.Add("Shell_InkCartridge");
            apworldNames.Add("Ink Cartridge");

            prefabNames.Add("Shell_Jar");
            apworldNames.Add("Mason Jar");

            prefabNames.Add("Shell_JazzCup");
            apworldNames.Add("Bebop Cup");

            prefabNames.Add("Shell_JoblinMug");
            apworldNames.Add("Coffee Mug");

            prefabNames.Add("Shell_KnightHelmet");
            apworldNames.Add("Knight's Helmet");

            prefabNames.Add("Shell_LegoBrick");
            apworldNames.Add("LEGAL Brick");

            prefabNames.Add("Shell_LightBulb");
            apworldNames.Add("Lightbulb");

            prefabNames.Add("Shell_Matryoshka_L");
            apworldNames.Add("Matryoshka Large");

            prefabNames.Add("Shell_Matryoshka_M Variant");
            apworldNames.Add("Matryoshka Medium");

            prefabNames.Add("Shell_Matryoshka_S Varitant");
            apworldNames.Add("Matryoshka Small");

            prefabNames.Add("Shell_MoonHermit");
            apworldNames.Add("");

            prefabNames.Add("Shell_PartyHat");
            apworldNames.Add("Party Hat");

            prefabNames.Add("Shell_PartyPopper");
            apworldNames.Add("Party Popper");

            prefabNames.Add("Shell_Pawn");
            apworldNames.Add("Pawn");

            prefabNames.Add("Shell_PiggyBank");
            apworldNames.Add("Piggy Bank");

            prefabNames.Add("Shell_PillBottle");
            apworldNames.Add("Pill Bottle");

            prefabNames.Add("Shell_PlasticCap");
            apworldNames.Add("Bottle Cap");

            prefabNames.Add("Shell_RubberDucky FrederickVariant");
            apworldNames.Add("Bartholomew");

            prefabNames.Add("Shell_RubberDucky");
            apworldNames.Add("Rubber Duck");

            prefabNames.Add("Shell_SaltShaker");
            apworldNames.Add("Salt Shaker");

            prefabNames.Add("Shell_SauceNozzle");
            apworldNames.Add("Sauce Nozzle");

            prefabNames.Add("Shell_Scrubber");
            apworldNames.Add("Dish Scrubber");

            prefabNames.Add("Shell_Scrubfather");
            apworldNames.Add("Scrub Aggie");

            prefabNames.Add("Shell_ShellPasta");
            apworldNames.Add("Conchiglie");

            prefabNames.Add("Shell_ShotGlass");
            apworldNames.Add("Shot Glass");

            prefabNames.Add("Shell_ShotgunShell");
            apworldNames.Add("Shotgun Shell");

            prefabNames.Add("Shell_Skull");
            apworldNames.Add("Skull");

            prefabNames.Add("Shell_SnowGlobe");
            apworldNames.Add("Snow Globe");

            prefabNames.Add("Shell_SodaCan");
            apworldNames.Add("Soda Can");

            prefabNames.Add("Shell_SoloCup");
            apworldNames.Add("Lil' Red Cup");

            prefabNames.Add("Shell_SpamCan");
            apworldNames.Add("Ham Tin");

            prefabNames.Add("Shell_Spring");
            apworldNames.Add("Spring");

            prefabNames.Add("Shell_StiffSock");
            apworldNames.Add("Sock");

            prefabNames.Add("Shell_SugarCone");
            apworldNames.Add("Wafer Cone");

            prefabNames.Add("Shell_SushiRoll");
            apworldNames.Add("Cascadia Roll");

            prefabNames.Add("Shell_TeaCup");
            apworldNames.Add("Teacup");

            prefabNames.Add("Shell_TennisBall");
            apworldNames.Add("Tennis Ball");

            prefabNames.Add("Shell_Thimble");
            apworldNames.Add("Thimble");

            prefabNames.Add("Shell_TinCan");
            apworldNames.Add("Tin Can");

            prefabNames.Add("Shell_TissueBox");
            apworldNames.Add("Tissue Box");

            prefabNames.Add("Shell_ToiletPaperRoll");
            apworldNames.Add("UltraSoft");

            prefabNames.Add("Shell_Trophy");
            apworldNames.Add("Trophy");

            prefabNames.Add("Shell_Valve");
            apworldNames.Add("Valve");

            prefabNames.Add("Shell_WIneGlass");
            apworldNames.Add("Champagne Flute");

            prefabNames.Add("Shell_Yoccult");
            apworldNames.Add("Yoccult");

            for (int i=0; i<prefabNames.Count;i++)
            {
                shellTableByApworld.Add(apworldNames[i], prefabNames[i]);
                shellTableByPrefab.Add(prefabNames[i], apworldNames[i]);
            }
        }

        public static string GetShellPrefabName(string apworldName)
        {
            string shellFound;

            //Acount for Variants
            if(apworldName == "Shell_CrabHusk_Bleached" | apworldName == "Shell_CrabHusk_Zombie")
            {
                apworldName = "Shell_CrabHusk";
            }

            shellFound = shellTableByApworld[apworldName];

            return shellFound;
        }

        public static string GetShellApworldName(string prefabName)
        {
            string shellFound;

            shellFound = shellTableByPrefab[prefabName];

            return shellFound;
        }
    }
}
