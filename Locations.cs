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
using UnityEngine.SceneManagement;

namespace ACTAP
{

    static class LocationSwapData
    {
        public static void LogLocation(Item itemPickup)
        {
            FieldInfo field = AccessTools.Field(typeof(Item), "save");
            SaveStateKillableEntity save = new SaveStateKillableEntity();
            save = (SaveStateKillableEntity)field.GetValue(itemPickup);

            long test = LocationSwapData.ItemPickupUUIDToAPID(itemPickup) - 483021700;
            Debug.Log(save.UUID);

            if (test == -1)
            {
                Debug.Log("No ID currently assigned");
            }
            else
            {
                Debug.Log("AP Location ID: " + test);
            }

            Debug.Log("Item Coords: " + itemPickup.transform.position);
        }
        public static string GetPath(this Transform current)
        {
            if (current.parent == null)
            {
                return current.gameObject.scene.name + "/" + current.name;
            }
            return GetPath(current.parent) + "/" + current.name;
        }

        public static long ItemPickupUUIDToAPID(Item item)
        {
            //Get UUID
            FieldInfo field = AccessTools.Field(typeof(Item), "save");
            SaveStateKillableEntity save = new SaveStateKillableEntity();
            save = (SaveStateKillableEntity)field.GetValue(item);

            string UUID = save.UUID;
            long baseid = 483021700;
            switch (UUID)
            {
                //case "": return baseid + 1; //

                //ShallowsTidePools
                case "": return baseid - 1; //empty pickup
                case "950e628c-f657-48d4-b93b-f8717627f6b3-2_A-ShallowsTidePools": return baseid +  0; //heartkelp_inital
                case "73329d8e-7c96-4e82-9d3c-e57cc61b46b4-2_A-ShallowsTidePools": return baseid + 4; //breadclaw_caveofrespite_ledge
                case "0171a152-809a-47cf-9fcc-60ddb61511bb-2_B-ShallowsBigSand": return baseid + 5; //breadclaw_shallows_southsandal
                case "ae27274c-c3f7-4721-9ddb-7c5a880578a4-2_B-ShallowsBigSand": return baseid + 6; //breadclaw_shallows_westwall
                case "8b269a88-c6d1-4b2a-8e5f-9d60c6d6fd15-2_B-ShallowsBigSand": return baseid + 7; //breadclaw_shallows_bridge
                case "6aba5604-8269-46a5-b734-ef4dc3983265-2_B-ShallowsBigSand": return baseid + 8; //breadclaw_shallows_eastwall
                case "889c343a-b969-4295-b059-a837fc457444-2_B-ShallowsBigSand": return baseid + 9; //breadclaw_shallows_eastsandal
                case "d1ddf122-d283-4509-8bfa-bff8fa209f35-2_B-ShallowsBigSand": return baseid + 10; //breadclaw_shallows_cigarette
                case "20bede55-9eae-40a3-a80a-63ee5b52d0bf-2_B-ShallowsBigSand": return baseid + 11; //breadclaw_shallows_fortwall
                case "5732b602-c599-41b5-bc5d-93e88277a4b7-2_B-ShallowsBigSand": return baseid + 12; //breadclaw_shallows_wallpiece
                case "984eda06-d10e-4b2b-a5c4-389784ed18f6-2_B-ShallowsBigSand": return baseid + 13; //breadclaw_shallows_sandcastle
                case "51dbcbfd-4a82-4c22-b2ee-1a03deeca4cc-2_B-ShallowsBigSand": return baseid + 14; //breadclaw_shallows_eastledge
                case "b2d21ade-41ab-47fc-960b-0beb6d07359d-2_B-ShallowsBigSand": return baseid + 15; //hairclaw_shallows_turret
                case "12c6e5c5-eee1-4f9a-a3c4-68c2bd098a14-2_B-ShallowsBigSand": return baseid + 16; //chipclaw_shallows_sandcastle
                //case "": return baseid + 17; //clothesclaw_shallows_shellsplitter
                case "8c1e68dc-eb15-41f4-9a5d-c1b20dd76f52-2_B-ShallowsBigSand": return baseid + 18; //breadclaw_slacktide_crates
                case "5bb2169d-f820-41f0-9783-336c006861c6-2_D-MoonSnailShellCave": return baseid + 19; //breadclaw_snailcave_entrance
                case "436eff44-09df-451e-93c8-3a5714ec55c2-2_D-MoonSnailShellCave": return baseid + 20; //breadclaw_snailcave_shortcut
                case "07977d86-44ec-40d0-b331-8b45b1d44838-2_D-MoonSnailShellCave": return baseid + 21; //breadclaw_snailcave_jelly
                case "2af7f575-51f3-4da9-b3bf-eab9d32a3bd8-2_C-Slacktide2": return baseid + 22; //breadclaw_slacktide_crabtrio
                case "45f42ae3-41b6-4333-875f-3a9e8387e087-2_C-Slacktide2": return baseid + 23; //chipclaw_slacktide_brokenwall
                //case "": return baseid + 24; //bloodstar_shallows_help
                case "ff9d47b6-7f55-4ad2-a110-e7c4492d87c1-2_B-ShallowsBigSand": return baseid + 25; //bloodstar_shallows_parkour
                case "32fb25ee-9262-4e87-98f9-2e64d369e7ae-2_B-ShallowsBigSand": return baseid + 26; //bloodstar_shallows_bridge
                case "0474f3a7-0eb7-4d3c-b61e-cbb782ceab03-2_B-ShallowsBigSand": return baseid + 27; //bloodstar_shallows_clam
                case "15a2e4c0-4950-4de0-8b5c-e2b1400f76c1-2_D-MoonSnailShellCave": return baseid + 28; //kelpsprout_snailcave_elevator
                case "00fc94c3-320c-4d0a-a4e0-2b5eccd42d55-2_D-MoonSnailShellCave": return baseid + 29; //bloodstar_snailcave_platoon
                case "451b4cfb-7176-4103-999f-358b09374e0c-2_B-ShallowsBigSand": return baseid + 30; //bloodstar_slacktide_clam
                case "4d4ac114-06e4-400f-b888-b500a7348cc9-2_B-ShallowsBigSand": return baseid + 31; //siphonophore_shallows_westwall
                case "e9a5a6cf-2a19-4db5-bb0d-d85b260f3e2b-2_B-ShallowsBigSand": return baseid + 32; //seastar_shallows_eastledge
                case "95c0077d-518f-4ab7-85e2-ac1e0c1bb0b9-2_B-ShallowsBigSand": return baseid + 33; //sponge_shallows_puffer
                case "924670c2-839f-4e42-9cf5-8c081b17f946-2_B-ShallowsBigSand": return baseid + 34; //anothercrab_shallows_pillar
                case "804d3848-3e86-474f-8622-547d020c4cc4-2_B-ShallowsBigSand": return baseid + 35; //sanddollar_shallows_arch
                case "b5e751fb-8ff5-440e-aa9c-9d9c75977be7-2_B-ShallowsBigSand": return baseid + 36; //limpet_slacktide_stairs
                case "35603032-802e-4811-ae95-8e8eb11c1dfa-2_B-ShallowsBigSand": return baseid + 37; //seastar_slacktide_grappleroom
                case "98329dde-9889-4cbb-a656-61f83dca2eca-2_B-ShallowsBigSand": return baseid + 38; //barnacle_slacktide_bigurchin
                case "c1a31dc1-9ec5-42e9-8dcf-291f7cdc8a26-2_D-MoonSnailShellCave": return baseid + 39; //limpet_snailcave_jelly
                case "694c4c66-fefc-4539-962f-a343e13b044b-2_C-Slacktide2": return baseid + 40; //mussel_slacktide_fortentrance
                case "557546a8-096b-49cd-b63b-452fb751a8bf-2_C-Slacktide2": return baseid + 41; //anemone_slacktide_fortwall
                case "e1848736-945b-406d-853e-d213f7c80f14-2_C-Slacktide2": return baseid + 42; //whelk_slacktide_turrettop
                case "7c307763-a7b4-4e81-88d6-e1baf1b04608-2_B-ShallowsBigSand": return baseid + 43; //captain_costume_pickup
                //case "": return baseid + 44; //royal_wave_reward
                case "e13e2c76-a638-4023-aa84-f6f29fd7bf65-2_B-ShallowsBigSand": return baseid + 45; //clothesclaw_shallows_southwestfort
                    //more bosses
                case "e9bf56b8-1362-42a9-87a2-00bed3a6f5d2-2_A-NCTradeRoute": return baseid + 63; //chipclaw_reefsedge_brokenbridge
                case "d8433d6b-5400-4dec-aad0-032e27babdeb-2_A-NCTradeRoute": return baseid + 64; //stainlessrelic_reefsedge_coral
                case "c44729ce-4c25-4059-8432-212381ca6835-2_A-NCTradeRoute": return baseid + 65; //barbedhook_reefsedge_undercoral
                case "08434b00-0100-4ea1-8f2a-f1567b84bdf2-2_A-NCTradeRoute": return baseid + 66; //barbedhook_reefsedge_seahorses
                case "1f5a8d6b-10ab-4e4e-b240-284e2ec2c77a-2_A-NCTradeRoute": return baseid + 67; //breadclaw_reefsedge_thimblecrab
                case "d93f0715-7c15-4f74-b376-34468705093c-2_A-NCTradeRoute": return baseid + 68; //seastar_reefsedge_crabs
                case "2b4849b0-2798-4600-8db1-c5d6bf6952b1-2_A-NCTradeRoute": return baseid + 69; //barbedhook_reefsedge_shortcut
                case "3473ed8f-db06-4bb7-9932-862f92258542-2_A-NCTradeRoute": return baseid + 70; //seastarplus_reefsedge_pole
                case "b478df52-084c-4c14-8a55-215fdbaeaffe-2_A-NCTradeRoute": return baseid + 71; //hairclaw_reefsedge_sign
                case "76ad26b0-4305-498c-8510-4940ad569210-2_A-NCTradeRoute": return baseid + 72; //barbedhook_reefsedge_cliff
                case "5e8a3fb6-f3b1-42a0-a8b8-10b57b340138-2_A-NCTradeRoute": return baseid + 73; //whelkplus_reefsedge_sponge
                case "fb5e2e88-b498-4af4-b7a6-4f176eb4016c-2_B-NCCity": return baseid + 74; //breadclaw_newcarcinia_entrance
                case "e7b4c4fc-6208-4326-847e-ee34113f7fe0-2_B-NCCity": return baseid + 75; //limpet_newcarcinia_entrance
                case "e26a7cee-15af-4f6c-b6fb-253f8dfac6e4-2_B-NCCity": return baseid + 76; //breadclaw_newcarcinia_hammerhead
                case "f2105767-2544-4acc-9703-a47fbe97c3dc-2_B-NCCity": return baseid + 77; //rustynail_newcarcinia_hammerhead
                case "c3b90e59-5289-4ca3-8639-a5c90d656403-2_B-NCCity": return baseid + 78; //barbedhook_newcarcinia_citycenter
                case "4058fdd6-84f5-4f1e-b8f1-3dcccb288aa2-2_B-NCCity": return baseid + 79; //breadclaw_newcarcinia_bottomalley
                case "d538af5f-81e0-452c-8800-756bb635a16b-2_B-NCCity": return baseid + 80; //breadclaw_newcarcinia_prawnalley
                case "4f62e34e-df79-4e33-99bb-b65844e50304-2_B-NCCity": return baseid + 81; //chipclaw_newcarcinia_leg
                case "0e59ec85-36b5-4750-baf4-cc9899525ca6-2_B-NCCity": return baseid + 82; //breadclaw_newcarcinia_tallbuilding
                case "c91a3fa0-f774-4e5f-a43e-90dd9a1d5e3a-2_B-NCCity": return baseid + 83; //breadclaw_newcarcinia_shortbuilding
                case "d2b95a55-ec16-48cc-bce4-c7e39a16a78a-2_B-NCCity": return baseid + 84; //chipclaw_newcarcinia_prawnshop
                //case "": return baseid + 85; //Urchin Quest
                case "a1194e18-4659-4ac9-aa73-744a6df42fe3-2_B-NCCity": return baseid + 86; //sanddollar_newcarcinia_hammerhead

                default: return baseid - 1;

            }
        }

        public static long BossPathToAPID(string path)
        {
            long baseid = 483021700;
            switch (path)
            {
                case "": return baseid -1;
                case "Boss_NephroCaptainoftheGuard": return baseid + 3;
                case "Boss_Duchess": return baseid + 44;
                case "Boss_Bruiser": return baseid + 46; //
                case "Boss_RoyalShellsplitter": return baseid + 47; //
                case "Boss_Pagurus": return baseid + 48; //Pagurus
                /*case "": return baseid + 49; //lycanthrope
                case "": return baseid + 50; //carbonara_connessuer
                case "": return baseid + 51; //heikea
                case "Boss_Topoda": return baseid + 52; //topoda
                case "Boss_Consortium": return baseid + 53; //consortium
                case "": return baseid + 54; //sludge_steamroller
                case "": return baseid + 55; //ceviche_sisters
                case "": return baseid + 56; //voltai
                case "Boss_Roland": return baseid + 57; //roland
                case "Boss_MoonHermit": return baseid + 58; //petroch
                case "Boss_Inkerton": return baseid + 59; //inkerton
                case "Boss_MoltedKing1": return baseid + 60; //camtscha
                case "Boss_PrayaDubia_Phase2 Variant": return baseid + 61; //praya_dubia
                case "Boss_Firth": return baseid + 62; //firth*/
                default: return -1;
            }
        }
    }
}
