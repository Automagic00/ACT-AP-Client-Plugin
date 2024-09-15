using BepInEx;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


namespace ACTAP
{

    public class LocationData
    {
        public string uuid = "";
        public int apid { get; set; }
        public string bossname { get; set; }
        public string itemAtLocationName { get; set; }
        public string skillName = "";
        //public bool shopIsPurchased { get; set; }
        //public bool shopIsObtained { get; set; }
    }
    public class BossData
    {
        public string bossname { get; set; }
        public int apid { get; set; }
    }

    public static class LocationDataTable
    {
        public static List<LocationData> pickupTable = new List<LocationData>();

        public static void GenerateTable()
        {
            //pickupTable.Add(new LocationData() { uuid =  "", apid =  baseid + }); //
            int baseid = 483021700;

            //ShallowsTidePools
            pickupTable.Add(new LocationData() { uuid = "950e628c-f657-48d4-b93b-f8717627f6b3-2_A-ShallowsTidePools", apid = baseid + 0 }); //heartkelp_inital
            pickupTable.Add(new LocationData() { uuid =  "73329d8e-7c96-4e82-9d3c-e57cc61b46b4-2_A-ShallowsTidePools", apid =  baseid + 4}); //breadclaw_caveofrespite_ledge
            pickupTable.Add(new LocationData() { uuid =  "0171a152-809a-47cf-9fcc-60ddb61511bb-2_B-ShallowsBigSand", apid =  baseid + 5}); //breadclaw_shallows_southsandal
            pickupTable.Add(new LocationData() { uuid =  "ae27274c-c3f7-4721-9ddb-7c5a880578a4-2_B-ShallowsBigSand", apid =  baseid + 6}); //breadclaw_shallows_westwall
            pickupTable.Add(new LocationData() { uuid =  "8b269a88-c6d1-4b2a-8e5f-9d60c6d6fd15-2_B-ShallowsBigSand", apid =  baseid + 7}); //breadclaw_shallows_bridge
            pickupTable.Add(new LocationData() { uuid =  "6aba5604-8269-46a5-b734-ef4dc3983265-2_B-ShallowsBigSand", apid =  baseid + 8}); //breadclaw_shallows_eastwall
            pickupTable.Add(new LocationData() { uuid =  "889c343a-b969-4295-b059-a837fc457444-2_B-ShallowsBigSand", apid =  baseid + 9}); //breadclaw_shallows_eastsandal
            pickupTable.Add(new LocationData() { uuid =  "d1ddf122-d283-4509-8bfa-bff8fa209f35-2_B-ShallowsBigSand", apid =  baseid + 10}); //breadclaw_shallows_cigarette
            pickupTable.Add(new LocationData() { uuid =  "20bede55-9eae-40a3-a80a-63ee5b52d0bf-2_B-ShallowsBigSand", apid =  baseid + 11}); //breadclaw_shallows_fortwall
            pickupTable.Add(new LocationData() { uuid =  "5732b602-c599-41b5-bc5d-93e88277a4b7-2_B-ShallowsBigSand", apid =  baseid + 12}); //breadclaw_shallows_wallpiece
            pickupTable.Add(new LocationData() { uuid =  "984eda06-d10e-4b2b-a5c4-389784ed18f6-2_B-ShallowsBigSand", apid =  baseid + 13}); //breadclaw_shallows_sandcastle
            pickupTable.Add(new LocationData() { uuid =  "51dbcbfd-4a82-4c22-b2ee-1a03deeca4cc-2_B-ShallowsBigSand", apid =  baseid + 14}); //breadclaw_shallows_eastledge
            pickupTable.Add(new LocationData() { uuid =  "b2d21ade-41ab-47fc-960b-0beb6d07359d-2_B-ShallowsBigSand", apid =  baseid + 15}); //hairclaw_shallows_turret
            pickupTable.Add(new LocationData() { uuid =  "12c6e5c5-eee1-4f9a-a3c4-68c2bd098a14-2_B-ShallowsBigSand", apid =  baseid + 16}); //chipclaw_shallows_sandcastle
            //data.Add(new LocationData() { uuid =  "", apid =  baseid + 17}); //clothesclaw_shallows_shellsplitter
            pickupTable.Add(new LocationData() { uuid =  "8c1e68dc-eb15-41f4-9a5d-c1b20dd76f52-2_B-ShallowsBigSand", apid =  baseid + 18}); //breadclaw_slacktide_crates
            pickupTable.Add(new LocationData() { uuid =  "5bb2169d-f820-41f0-9783-336c006861c6-2_D-MoonSnailShellCave", apid =  baseid + 19}); //breadclaw_snailcave_entrance
            pickupTable.Add(new LocationData() { uuid =  "436eff44-09df-451e-93c8-3a5714ec55c2-2_D-MoonSnailShellCave", apid =  baseid + 20}); //breadclaw_snailcave_shortcut
            pickupTable.Add(new LocationData() { uuid =  "07977d86-44ec-40d0-b331-8b45b1d44838-2_D-MoonSnailShellCave", apid =  baseid + 21}); //breadclaw_snailcave_jelly
            pickupTable.Add(new LocationData() { uuid =  "2af7f575-51f3-4da9-b3bf-eab9d32a3bd8-2_C-Slacktide2", apid =  baseid + 22}); //breadclaw_slacktide_crabtrio
            pickupTable.Add(new LocationData() { uuid =  "45f42ae3-41b6-4333-875f-3a9e8387e087-2_C-Slacktide2", apid =  baseid + 23}); //chipclaw_slacktide_brokenwall
            //data.Add(new LocationData() { uuid =  "", apid =  baseid + 24}); //bloodstar_shallows_help
            pickupTable.Add(new LocationData() { uuid =  "ff9d47b6-7f55-4ad2-a110-e7c4492d87c1-2_B-ShallowsBigSand", apid =  baseid + 25}); //bloodstar_shallows_parkour
            pickupTable.Add(new LocationData() { uuid =  "32fb25ee-9262-4e87-98f9-2e64d369e7ae-2_B-ShallowsBigSand", apid =  baseid + 26}); //bloodstar_shallows_bridge
            pickupTable.Add(new LocationData() { uuid =  "0474f3a7-0eb7-4d3c-b61e-cbb782ceab03-2_B-ShallowsBigSand", apid =  baseid + 27}); //bloodstar_shallows_clam
            pickupTable.Add(new LocationData() { uuid =  "15a2e4c0-4950-4de0-8b5c-e2b1400f76c1-2_D-MoonSnailShellCave", apid =  baseid + 28}); //kelpsprout_snailcave_elevator
            pickupTable.Add(new LocationData() { uuid =  "00fc94c3-320c-4d0a-a4e0-2b5eccd42d55-2_D-MoonSnailShellCave", apid =  baseid + 29}); //bloodstar_snailcave_platoon
            pickupTable.Add(new LocationData() { uuid =  "451b4cfb-7176-4103-999f-358b09374e0c-2_B-ShallowsBigSand", apid =  baseid + 30}); //bloodstar_slacktide_clam
            pickupTable.Add(new LocationData() { uuid =  "4d4ac114-06e4-400f-b888-b500a7348cc9-2_B-ShallowsBigSand", apid =  baseid + 31}); //siphonophore_shallows_westwall
            pickupTable.Add(new LocationData() { uuid =  "e9a5a6cf-2a19-4db5-bb0d-d85b260f3e2b-2_B-ShallowsBigSand", apid =  baseid + 32}); //seastar_shallows_eastledge
            pickupTable.Add(new LocationData() { uuid =  "95c0077d-518f-4ab7-85e2-ac1e0c1bb0b9-2_B-ShallowsBigSand", apid =  baseid + 33}); //sponge_shallows_puffer
            pickupTable.Add(new LocationData() { uuid =  "924670c2-839f-4e42-9cf5-8c081b17f946-2_B-ShallowsBigSand", apid =  baseid + 34}); //anothercrab_shallows_pillar
            pickupTable.Add(new LocationData() { uuid =  "804d3848-3e86-474f-8622-547d020c4cc4-2_B-ShallowsBigSand", apid =  baseid + 35}); //sanddollar_shallows_arch
            pickupTable.Add(new LocationData() { uuid =  "b5e751fb-8ff5-440e-aa9c-9d9c75977be7-2_B-ShallowsBigSand", apid =  baseid + 36}); //limpet_slacktide_stairs
            pickupTable.Add(new LocationData() { uuid =  "35603032-802e-4811-ae95-8e8eb11c1dfa-2_B-ShallowsBigSand", apid =  baseid + 37}); //seastar_slacktide_grappleroom
            pickupTable.Add(new LocationData() { uuid =  "98329dde-9889-4cbb-a656-61f83dca2eca-2_B-ShallowsBigSand", apid =  baseid + 38}); //barnacle_slacktide_bigurchin
            pickupTable.Add(new LocationData() { uuid =  "c1a31dc1-9ec5-42e9-8dcf-291f7cdc8a26-2_D-MoonSnailShellCave", apid =  baseid + 39}); //limpet_snailcave_jelly
            pickupTable.Add(new LocationData() { uuid =  "694c4c66-fefc-4539-962f-a343e13b044b-2_C-Slacktide2", apid =  baseid + 40}); //mussel_slacktide_fortentrance
            pickupTable.Add(new LocationData() { uuid =  "557546a8-096b-49cd-b63b-452fb751a8bf-2_C-Slacktide2", apid =  baseid + 41}); //anemone_slacktide_fortwall
            pickupTable.Add(new LocationData() { uuid =  "e1848736-945b-406d-853e-d213f7c80f14-2_C-Slacktide2", apid =  baseid + 42}); //whelk_slacktide_turrettop
            pickupTable.Add(new LocationData() { uuid =  "7c307763-a7b4-4e81-88d6-e1baf1b04608-2_B-ShallowsBigSand", apid =  baseid + 43}); //captain_costume_pickup
            //data.Add(new LocationData() { uuid =  "", apid =  baseid + 44}); //royal_wave_reward
            pickupTable.Add(new LocationData() { uuid =  "e13e2c76-a638-4023-aa84-f6f29fd7bf65-2_B-ShallowsBigSand", apid =  baseid + 45}); //clothesclaw_shallows_southwestfort
                //more bosses
            pickupTable.Add(new LocationData() { uuid =  "e9bf56b8-1362-42a9-87a2-00bed3a6f5d2-2_A-NCTradeRoute", apid =  baseid + 63}); //chipclaw_reefsedge_brokenbridge
            pickupTable.Add(new LocationData() { uuid =  "d8433d6b-5400-4dec-aad0-032e27babdeb-2_A-NCTradeRoute", apid =  baseid + 64}); //stainlessrelic_reefsedge_coral
            pickupTable.Add(new LocationData() { uuid =  "c44729ce-4c25-4059-8432-212381ca6835-2_A-NCTradeRoute", apid =  baseid + 65}); //barbedhook_reefsedge_undercoral
            pickupTable.Add(new LocationData() { uuid =  "08434b00-0100-4ea1-8f2a-f1567b84bdf2-2_A-NCTradeRoute", apid =  baseid + 66}); //barbedhook_reefsedge_seahorses
            pickupTable.Add(new LocationData() { uuid =  "1f5a8d6b-10ab-4e4e-b240-284e2ec2c77a-2_A-NCTradeRoute", apid =  baseid + 67}); //breadclaw_reefsedge_thimblecrab
            pickupTable.Add(new LocationData() { uuid =  "d93f0715-7c15-4f74-b376-34468705093c-2_A-NCTradeRoute", apid =  baseid + 68}); //seastar_reefsedge_crabs
            pickupTable.Add(new LocationData() { uuid =  "2b4849b0-2798-4600-8db1-c5d6bf6952b1-2_A-NCTradeRoute", apid =  baseid + 69}); //barbedhook_reefsedge_shortcut
            pickupTable.Add(new LocationData() { uuid =  "3473ed8f-db06-4bb7-9932-862f92258542-2_A-NCTradeRoute", apid =  baseid + 70}); //seastarplus_reefsedge_pole
            pickupTable.Add(new LocationData() { uuid =  "b478df52-084c-4c14-8a55-215fdbaeaffe-2_A-NCTradeRoute", apid =  baseid + 71}); //hairclaw_reefsedge_sign
            pickupTable.Add(new LocationData() { uuid =  "76ad26b0-4305-498c-8510-4940ad569210-2_A-NCTradeRoute", apid =  baseid + 72}); //barbedhook_reefsedge_cliff
            pickupTable.Add(new LocationData() { uuid =  "5e8a3fb6-f3b1-42a0-a8b8-10b57b340138-2_A-NCTradeRoute", apid =  baseid + 73}); //whelkplus_reefsedge_sponge
            pickupTable.Add(new LocationData() { uuid =  "fb5e2e88-b498-4af4-b7a6-4f176eb4016c-2_B-NCCity", apid =  baseid + 74}); //breadclaw_newcarcinia_entrance
            pickupTable.Add(new LocationData() { uuid =  "e7b4c4fc-6208-4326-847e-ee34113f7fe0-2_B-NCCity", apid =  baseid + 75}); //limpet_newcarcinia_entrance
            pickupTable.Add(new LocationData() { uuid =  "e26a7cee-15af-4f6c-b6fb-253f8dfac6e4-2_B-NCCity", apid =  baseid + 76}); //breadclaw_newcarcinia_hammerhead
            pickupTable.Add(new LocationData() { uuid =  "f2105767-2544-4acc-9703-a47fbe97c3dc-2_B-NCCity", apid =  baseid + 77}); //rustynail_newcarcinia_hammerhead
            pickupTable.Add(new LocationData() { uuid =  "c3b90e59-5289-4ca3-8639-a5c90d656403-2_B-NCCity", apid =  baseid + 78}); //barbedhook_newcarcinia_citycenter
            pickupTable.Add(new LocationData() { uuid =  "4058fdd6-84f5-4f1e-b8f1-3dcccb288aa2-2_B-NCCity", apid =  baseid + 79}); //breadclaw_newcarcinia_bottomalley
            pickupTable.Add(new LocationData() { uuid =  "d538af5f-81e0-452c-8800-756bb635a16b-2_B-NCCity", apid =  baseid + 80}); //breadclaw_newcarcinia_prawnalley
            pickupTable.Add(new LocationData() { uuid =  "4f62e34e-df79-4e33-99bb-b65844e50304-2_B-NCCity", apid =  baseid + 81}); //chipclaw_newcarcinia_leg
            pickupTable.Add(new LocationData() { uuid =  "0e59ec85-36b5-4750-baf4-cc9899525ca6-2_B-NCCity", apid =  baseid + 82}); //breadclaw_newcarcinia_tallbuilding
            pickupTable.Add(new LocationData() { uuid =  "c91a3fa0-f774-4e5f-a43e-90dd9a1d5e3a-2_B-NCCity", apid =  baseid + 83}); //breadclaw_newcarcinia_shortbuilding
            pickupTable.Add(new LocationData() { uuid =  "d2b95a55-ec16-48cc-bce4-c7e39a16a78a-2_B-NCCity", apid =  baseid + 84}); //chipclaw_newcarcinia_prawnshop
            //data.Add(new LocationData() { uuid =  "", apid =  baseid + 85}); //Urchin Quest
            pickupTable.Add(new LocationData() { uuid =  "a1194e18-4659-4ac9-aa73-744a6df42fe3-2_B-NCCity", apid =  baseid + 86}); //sanddollar_newcarcinia_hammerhead
            pickupTable.Add(new LocationData() { uuid =  "4a22c058-bbb2-467a-b204-9c1a82dc0d9c-2_A-OOGroveRadius", apid =  baseid + 87}); //breadclaw_sandsbetween_centralvista
            pickupTable.Add(new LocationData() { uuid =  "6c5631a3-d941-4990-818f-117140982384-2_A-OOGroveRadius", apid =  baseid + 88}); //barbedhook_sandsbetween_centralvista
            pickupTable.Add(new LocationData() { uuid =  "3197c269-c43d-4bff-b388-a6c3d3e4b5f3-2_A-OOGroveRadius", apid =  baseid + 89}); //phytoplankton_sandsbetween_centralvista
            pickupTable.Add(new LocationData() { uuid =  "73aec841-ee9a-411b-8919-53eeeb33ec59-2_A-OOGroveRadius", apid =  baseid + 90}); //anemone_sandsbetween_centralvista
            pickupTable.Add(new LocationData() { uuid =  "6801f8fb-6dd9-4129-a182-791a7e66d1c2-2_A-OOGroveRadius", apid =  baseid + 91}); //bloodstar_sandsbetween_playground
            pickupTable.Add(new LocationData() { uuid =  "bd3a89be-89f5-4029-ac76-c1966c70299d-2_A-OOGroveRadius", apid =  baseid + 92}); //anemone_sandsbetween_playground
            pickupTable.Add(new LocationData() { uuid =  "8778ffd7-a95d-4021-bb90-4a116de9894a-2_A-OOGroveRadius", apid =  baseid + 93}); //rustynail_sandsbetween_fence
            pickupTable.Add(new LocationData() { uuid =  "cc1a7d82-3803-474e-8f66-c149055c4cb9-2_A-OOGroveRadius", apid =  baseid + 94}); //hairclaw_sandsbetween_southernreef
            pickupTable.Add(new LocationData() { uuid =  "20b38e81-1198-4905-809a-e42ac4e4759d-2_A-OOGroveRadius", apid =  baseid + 95}); //barbedhook_sandsbetween_southernreef
            pickupTable.Add(new LocationData() { uuid =  "52821431-a6c7-458a-b54f-d42196919993-2_A-OOGroveRadius", apid =  baseid + 96}); //googlyeye_sandsbetween_pizza
            pickupTable.Add(new LocationData() { uuid =  "550401bf-30db-4deb-8267-ac73d522236d-2_A-OOGroveRadius", apid =  baseid + 97}); //tacklepouch_sandsbetween_cooler
            pickupTable.Add(new LocationData() { uuid =  "74dac1c4-2458-4cca-a280-11e643e610ce-2_A-OOGroveRadius", apid =  baseid + 98}); //barbedhook3_sandsbetween_cooler
            pickupTable.Add(new LocationData() { uuid =  "21b40d22-140c-493c-98e0-218a5ff29c02-2_A-OOGroveRadius", apid =  baseid + 99}); //barbedhook_sandsbetween_fence
            pickupTable.Add(new LocationData() { uuid =  "8d4c2499-b344-4e0f-b4e4-dc2a3e7ccf74-2_A-OOGroveRadius", apid =  baseid + 100}); //siphonophore_sandsbetween_cooler
            pickupTable.Add(new LocationData() { uuid =  "1f5bc2e0-a613-4afd-bdf6-5ed4805edf29-2_A-OOGroveRadius", apid =  baseid + 101}); //limpet_sandsbetween_anchor
            pickupTable.Add(new LocationData() { uuid =  "0bae76f4-e90b-435b-9089-356ef62ba0fa-2_A-OOGroveRadius", apid =  baseid + 102}); //chipclaw_sandsbetween_grove
            pickupTable.Add(new LocationData() { uuid =  "57559d1c-80ce-4396-afdf-7d7e6af71ff6-2_A-OOGroveRadius", apid =  baseid + 103}); //anemone_sandsbetween_chains
            pickupTable.Add(new LocationData() { uuid =  "6f1f751a-b27c-4eb0-b95c-a89d66dd242a-2_A-OOGroveRadius", apid =  baseid + 104}); //fredrick_sandsbetween_fredrick
            pickupTable.Add(new LocationData() { uuid =  "03ba104a-efe3-4294-b378-b2aeff7aa9e3-2_A-OOGroveRadius", apid =  baseid + 105}); //anothercrab_sandsbetween_chains
            pickupTable.Add(new LocationData() { uuid =  "d78747bd-2da0-4421-add8-3c5842fa0f61-2_A-OOGroveRadius", apid =  baseid + 106}); //breadclaw_sandsbetween_chains
            pickupTable.Add(new LocationData() { uuid =  "e128b8e9-0c20-426f-8f47-783043f829dc-2_A-OOGroveRadius", apid =  baseid + 107}); //clothesclaw_sandsbetween_bobbit
            pickupTable.Add(new LocationData() { uuid =  "8d9edda5-d554-4e75-8842-8239075a334a-2_A-OOGroveRadius", apid =  baseid + 108}); //hairclaw_sandsbetween_bobbit
            pickupTable.Add(new LocationData() { uuid =  "3bb06349-c151-4333-8305-ba0eb7512bc3-2_A-OOGroveRadius", apid =  baseid + 109}); //bobbit_trap_pickup
            pickupTable.Add(new LocationData() { uuid =  "a08ac116-549b-4691-8e88-79daf965dff8-2_A-OOGroveRadius", apid =  baseid + 110}); //mussel_sandsbetween_anchor
            pickupTable.Add(new LocationData() { uuid =  "7e93197a-47f1-4bdd-b7f0-5d0ecffb39f4-2_A-OOGroveRadius", apid =  baseid + 111}); //clothesclaw_sandsbetween_northwest
            pickupTable.Add(new LocationData() { uuid =  "cd2f3731-b9f4-4f88-abd2-44d142eca493-2_A-OOGroveRadius", apid =  baseid + 112}); //clothesclaw_sandsbetween_grove
            pickupTable.Add(new LocationData() { uuid =  "234f3d49-7fc5-4039-949f-8d95f38d7c79-2_A-OOGroveRadius", apid =  baseid + 113}); //barnacle_sandsbetween_northeast
            pickupTable.Add(new LocationData() { uuid =  "2dc13e33-724d-41a7-983b-5f3bf010ce89-2_A-OOGroveRadius", apid =  baseid + 114}); //salp_sandsbetween_northeast
            pickupTable.Add(new LocationData() { uuid =  "c192eaf1-dd16-459b-a489-376899aabb9b-2_A-OOGroveRadius", apid =  baseid + 115}); //barbedhook_sandsbetween_spire
            pickupTable.Add(new LocationData() { uuid =  "fc6e8558-9515-40a7-9857-9584a196a1ec-2_A-OOGroveRadius", apid =  baseid + 116}); //rustynailplus_sandsbetween_centralvista
            pickupTable.Add(new LocationData() { uuid =  "d19ac393-ea1d-4237-9d31-b620c3a2617d-2_A-OOGroveRadius", apid =  baseid + 117}); //seastarplus_sandsbetween_flotsam
            pickupTable.Add(new LocationData() { uuid =  "bceed85e-5560-4a77-a498-749af9f76780-2_A-OOGroveRadius", apid =  baseid + 118}); //seaslug_sandsbetween_crabrave
            pickupTable.Add(new LocationData() { uuid =  "63b93466-f406-492e-ac12-f243fe6a8165-2_A-OOGroveRadius", apid =  baseid + 119}); //whelk_sandsbetween_crabrave
            pickupTable.Add(new LocationData() { uuid =  "d985ffba-0ece-414d-8161-fa65241dd2b1-2_B-ShallowsBigSand", apid =  baseid + 120}); //chipclaw_caveofrespite_forkroomfishing
            pickupTable.Add(new LocationData() { uuid =  "d1af097b-c6c9-44a4-9ced-4a09a973c0d5-2_B-ShallowsBigSand", apid =  baseid + 121}); //mussel_caveofrespite_crabfightfishing
            pickupTable.Add(new LocationData() { uuid =  "0ac3fa03-5aab-4f12-86e4-8e9dfde8c04f-2_B-ShallowsBigSand", apid =  baseid + 122}); //clothesclaw_caveofrespite_entrancefishing
            pickupTable.Add(new LocationData() { uuid =  "9ef4b1f3-5a80-408c-962a-b3fed8c68770-2_B-ShallowsBigSand", apid =  baseid + 123}); //sanddollar_caveofrespite_pathfishing
            pickupTable.Add(new LocationData() { uuid =  "1e33dfd0-6445-47f9-b1bf-3e8be7897a79-2_B-ShallowsBigSand", apid =  baseid + 124}); //breadclaw_shallows_sunkentower
            pickupTable.Add(new LocationData() { uuid =  "41a6780e-55a2-4a0a-96a9-bb279a3b1ec3-2_B-ShallowsBigSand", apid =  baseid + 125}); //mussel_shallows_southwestcastlefish
            pickupTable.Add(new LocationData() { uuid =  "0f64d031-4163-4979-9497-116cac06e234-2_B-ShallowsBigSand", apid =  baseid + 126}); //breadclaw_shallows_slacktidesouthfish
            pickupTable.Add(new LocationData() { uuid =  "daaa6ab6-8aac-4713-942f-f322e918dd55-2_B-ShallowsBigSand", apid =  baseid + 127}); //anemone_shallows_southwestcastlehiddenfishing
            pickupTable.Add(new LocationData() { uuid =  "728b3da1-5b4b-4a7f-885c-b5ed9abfadbc-2_B-ShallowsBigSand", apid =  baseid + 128}); //razorblade_shallows_slacktidebottlefishing
            pickupTable.Add(new LocationData() { uuid =  "b29c140a-08c2-44c6-81c3-5f91e5621e47-2_B-ShallowsBigSand", apid =  baseid + 129}); //anemone_shallows_umbrellafishing
            pickupTable.Add(new LocationData() { uuid =  "0ad90b46-ca8b-46ed-9cc6-f72ffcdb9d33-2_B-ShallowsBigSand", apid =  baseid + 130}); //breadclaw_slacktide_training
            pickupTable.Add(new LocationData() { uuid =  "59e2ec5a-b650-43fa-879d-3af1569fb640-2_B-ShallowsBigSand", apid =  baseid + 131}); //breadclaw_slacktide_roofhiddenfish
            pickupTable.Add(new LocationData() { uuid =  "4ebb3072-53e8-4cff-9672-5f82f926035e-2_A-OOGroveRadius", apid =  baseid + 132}); //barbedhook_sandsbetween_southcentral
            pickupTable.Add(new LocationData() { uuid =  "956ad9a9-5b43-4f18-a7d9-f67f21c50137-2_A-OOGroveRadius", apid =  baseid + 133}); //bloodstar_sandsbetween_southwestcentral
            pickupTable.Add(new LocationData() { uuid =  "90171fe5-d483-4905-a440-a9ab5a7bc074-2_A-OOGroveRadius", apid =  baseid + 134}); //barbedhook_sandsbetween_bobbitbase
            pickupTable.Add(new LocationData() { uuid =  "f553b94c-14ea-4852-8ae7-564b8d30b24d-2_A-OOGroveRadius", apid =  baseid + 135}); //breadclaw_sandsbetween_propanecliff
            pickupTable.Add(new LocationData() { uuid =  "f8b96a25-70ba-492b-bb87-7b51c81d6102-2_A-OOGroveRadius", apid =  baseid + 136}); //barbedhook_sandsbetween_northofridge
            pickupTable.Add(new LocationData() { uuid =  "7e4ef7d0-214a-4358-a0c3-866a869d71e0-2_A-OOGroveRadius", apid =  baseid + 137}); //hairclaw_sandsbetween_northchains
            pickupTable.Add(new LocationData() { uuid =  "9faef117-fd92-47cb-b4d8-cc2a07404a69-2_A-OOGroveRadius", apid =  baseid + 138}); //clothesclaw_sandsbetween_litterpitfall
            pickupTable.Add(new LocationData() { uuid =  "305be10e-d5a7-42e6-88f1-2cebdd524bbb-2_A-OOGroveRadius", apid =  baseid + 139}); //hairclaw_sandsbetween_southgrovepit
            pickupTable.Add(new LocationData() { uuid =  "cd2d0535-6c06-4530-bf60-afbffc2d5086-2_A-OOGroveRadius", apid =  baseid + 140}); //clown_costume_pickup
            pickupTable.Add(new LocationData() { uuid =  "a8e01dfb-5dd9-4da0-92b1-39a7b18e8d83-2_A-OOGroveRadius", apid =  baseid + 141}); //sinkerplus_sandsbetween_northridgemantis
            pickupTable.Add(new LocationData() { uuid =  "56e45097-4ffa-41b0-ac56-cdd6c93b5e88-2_A-OOGroveRadius", apid =  baseid + 142}); //limpetplus_sandsbetween_grovemantis
            pickupTable.Add(new LocationData() { uuid =  "8c7fdc4d-423b-4456-8869-56387a4d7665-2_A-OOGroveRadius", apid =  baseid + 143}); //oldworldwhorl_sandsbetween_mantispitfall
            pickupTable.Add(new LocationData() { uuid =  "859c746b-c52f-4887-aa96-bb19bd7e25dd-2_A-OOGroveRadius", apid =  baseid + 144}); //lumpsuckerplus_sandsbetween_anchor
            pickupTable.Add(new LocationData() { uuid =  "83daf909-77d3-4302-858b-6e92fe54fe74-2_A-OOGroveRadius", apid =  baseid + 145}); //stainlessrelic_sandsbetween_eelectrocute
            pickupTable.Add(new LocationData() { uuid =  "699b7323-fe71-4bfe-864f-cdd0c9186bbd-2_A-OOGroveRadius", apid =  baseid + 146}); //tacklepouch_sandsbetween_eelnorthchain
            pickupTable.Add(new LocationData() { uuid =  "0e2d558b-26a7-4213-be83-77355fe06128-2_A-OOGroveRadius", apid =  baseid + 147}); //paperclaw_sandsbetween_southeelshortcut
            pickupTable.Add(new LocationData() { uuid =  "ae4a7ac6-5eb0-4590-a3c7-5d5f236a978d-2_A-OOGroveRadius", apid =  baseid + 148}); //sinkerplus_sandsbetween_southeel
            pickupTable.Add(new LocationData() { uuid =  "7111735c-772e-4c1f-b255-183e62760ae2-2_A-OOGroveRadius", apid =  baseid + 149}); //musselplus_sandsbetween_southeel
            pickupTable.Add(new LocationData() { uuid =  "5c8133eb-ddb3-4b24-9849-f4d3f03491e0-2_A-OOGroveRadius", apid =  baseid + 150}); //hairclaw_sandsbetween_southeel
            pickupTable.Add(new LocationData() { uuid =  "a7c60b04-8f1b-4833-a6f4-d472e79748c5-2_A-OOGroveRadius", apid =  baseid + 151}); //kelpsprout_sandsbetween_southeelside
            pickupTable.Add(new LocationData() { uuid =  "771e0942-ebf4-4938-a7e7-c03ce67f9c16-2_A-OOGroveRadius", apid =  baseid + 152}); //barbedhook_sandsbetween_southeel
            pickupTable.Add(new LocationData() { uuid =  "2ccd3299-d207-4d4d-b5a5-d777c4e8aa29-2_A-OOGroveRadius", apid =  baseid + 153}); //oyster_sandsbetween_southeelashtray
            pickupTable.Add(new LocationData() { uuid =  "a53aefd5-fa6f-4675-a1b5-2f9e43f3fed0-2_A-OOGroveRadius", apid =  baseid + 154}); //barbedhook_sandsbetween_southeelshellsplitter
            pickupTable.Add(new LocationData() { uuid =  "98e4d994-92c4-4b3b-82b7-f460a49d604b-2_A-OOGroveRadius", apid =  baseid + 155}); //paperclaw_sandsbetween_trashanchor
            pickupTable.Add(new LocationData() { uuid =  "cab91803-4006-456d-95c7-6ec17d90c26a-2_A-OOGroveRadius", apid =  baseid + 156}); //hairclaw_sandsbetween_anchorfish
            pickupTable.Add(new LocationData() { uuid =  "b0bf9c58-ff96-404c-bd1b-361fb1711b64-2_A-OOGroveRadius", apid =  baseid + 157}); //chipclaw_sandsbetween_northeastchainfish
            pickupTable.Add(new LocationData() { uuid =  "17b29697-5b33-4568-a311-2dbfac60317a-2_A-OOGroveRadius", apid =  baseid + 158}); //clothesclaw_sandsbetween_propanefish
            pickupTable.Add(new LocationData() { uuid =  "da5f126f-e470-432c-b5b0-8eca9918ed77-2_A-OOGroveRadius", apid =  baseid + 159}); //mussel_sandsbetween_bobbitfish
            pickupTable.Add(new LocationData() { uuid =  "628bf319-c5f3-448b-8ec0-71a173246a09-2_A-OOGroveRadius", apid =  baseid + 160}); //clothesclaw_sandsbetween_anchorcentralfish
            pickupTable.Add(new LocationData() { uuid =  "15cf7503-de68-4475-b39c-fe687f85c448-2_A-OOGroveRadius", apid =  baseid + 161}); //barnacle_sandsbetween_bobbitfish
            pickupTable.Add(new LocationData() { uuid =  "98c3bf98-c4bd-4c67-8678-0464e7095970-2_A-OOGroveRadius", apid =  baseid + 162}); //breadclaw_sandsbetween_nailfish
            pickupTable.Add(new LocationData() { uuid =  "69ac316d-8d42-48a4-8d55-4ec4d6429c7c-2_A-OOGroveRadius", apid =  baseid + 163}); //breadclaw_sandsbetween_palletfish
            pickupTable.Add(new LocationData() { uuid =  "38edc1a4-dea8-4000-8683-6cda7f6ba3bc-2_A-OOGroveRadius", apid =  baseid + 164}); //hairclaw_sandsbetween_southeelfish
            pickupTable.Add(new LocationData() { uuid =  "01999bed-4813-4fe3-ab9b-5ed19cce90f1-2_A-OOGroveRadius", apid =  baseid + 165}); //chipclaw_sandsbetween_anchoreelhiddenfish
            pickupTable.Add(new LocationData() { uuid =  "d61a21a2-c27d-45d7-8e11-75fcd11e8b78-2_A-OOGroveRadius", apid =  baseid + 166}); //whelkplusplus_sandsbetween_southeelpeak
            pickupTable.Add(new LocationData() { uuid =  "7faef89f-c78a-4002-9454-c20bf5098229-2_A-OOGroveRadius", apid =  baseid + 167}); //salpplus_sandsbetween_groveeel
            pickupTable.Add(new LocationData() { uuid =  "3123f8ec-3548-41f5-a777-c10268e06b8b-2_A-OOGroveRadius", apid =  baseid + 168}); //usedbandage_sandsbetween_groveeel
            pickupTable.Add(new LocationData() { uuid =  "29e54e45-4248-4a3d-96bf-ddf5cf6046c7-2_A-OOGroveRadius", apid =  baseid + 169}); //bloodstar_postpag_anchorswarm
            pickupTable.Add(new LocationData() { uuid =  "e872d437-5bdb-4e70-820a-030bc21d66d5-2_A-OOGroveRadius", apid =  baseid + 170}); //stapleclaw_postpag_litter
            pickupTable.Add(new LocationData() { uuid =  "4e9d02aa-75f5-4b87-b029-6ee1e7d62f7a-2_A-OOGroveRadius", apid =  baseid + 171}); //hairclaw_postpag_chains
            pickupTable.Add(new LocationData() { uuid =  "2fa3fdd0-b3da-4c69-b579-a5d316075635-2_A-OOGroveRadius", apid =  baseid + 172}); //clothesclaw_postpag_chains
            pickupTable.Add(new LocationData() { uuid =  "a5c14863-82c3-47d0-b0e1-a67c93da96b8-2_A-OOGroveRadius", apid =  baseid + 173}); //barbedhook_postpag_nechain
            pickupTable.Add(new LocationData() { uuid =  "d2cd8afa-4bf2-490e-8542-cb92331800d6-2_A-OOGroveRadius", apid =  baseid + 174}); //barbedhook_postpag_echain
            pickupTable.Add(new LocationData() { uuid =  "b03e7050-778b-4029-ab54-f82207d724fa-2_A-OOGroveRadius", apid =  baseid + 175}); //barbedhook_postpag_echainpreserver
            pickupTable.Add(new LocationData() { uuid =  "2738d986-20cb-4a09-b6c5-7076a3918283-2_A-OOGroveRadius", apid =  baseid + 176}); //barbedhook_postpag_necentral
            pickupTable.Add(new LocationData() { uuid =  "a8513df4-5533-4a1e-8fa0-b0423757f162-2_A-OOGroveRadius", apid =  baseid + 177}); //barbedhook_postpag_nwridge
            pickupTable.Add(new LocationData() { uuid =  "f14d696d-88a4-471e-a512-b5770c3cfbad-2_A-OOGroveRadius", apid =  baseid + 178}); //barbedhook_postpag_neeelpath
            pickupTable.Add(new LocationData() { uuid =  "55fafe55-5d22-49a8-8b2b-aea149468a61-2_A-OOGroveRadius", apid =  baseid + 179}); //hairclaw_postpag_nridge
            pickupTable.Add(new LocationData() { uuid =  "c2d06d3f-65f4-46cf-b069-0b4098b425d0-2_A-OOGroveRadius", apid =  baseid + 180}); //hairclaw_postpag_ecentral
            pickupTable.Add(new LocationData() { uuid =  "9ecea6b6-f3db-48f8-a753-6644bbbf1aaf-2_A-OOGroveRadius", apid =  baseid + 181}); //barbedhook_postpag_ridgeentrance
            pickupTable.Add(new LocationData() { uuid =  "45af0c47-a8e0-498f-8345-9c9ddabbd6b3-2_A-OOGroveRadius", apid =  baseid + 182}); //chipclaw_postpag_nwpropane
            pickupTable.Add(new LocationData() { uuid =  "ecea05b3-9908-4b63-b422-d999b78c87e1-2_A-OOGroveRadius", apid =  baseid + 183}); //barbedhook_postpag_wplayground
            pickupTable.Add(new LocationData() { uuid =  "7563f2eb-e052-4107-b787-de2f67a40f2f-2_A-OOGroveRadius", apid =  baseid + 184}); //barbedhook_postpag_swcentral
            pickupTable.Add(new LocationData() { uuid =  "16dfae13-103f-45eb-9661-3e6463a2b805-2_A-OOGroveRadius", apid =  baseid + 185}); //paperclaw_postpag_schain
            pickupTable.Add(new LocationData() { uuid =  "c56be461-0e5c-406e-8af0-83021f2f2fb6-2_A-OOGroveRadius", apid =  baseid + 186}); //stainlessrelic_ridge_overlook
            pickupTable.Add(new LocationData() { uuid =  "97a24ab6-20a3-4f21-b0af-55bf243f33f0-2_A-OOGroveRadius", apid =  baseid + 187}); //barbedhook_ridge_near
            pickupTable.Add(new LocationData() { uuid =  "d56339c0-fc26-4f53-a289-2c00b6f7ab38-2_A-OOGroveRadius", apid =  baseid + 188}); //kelpsprout_ridge_eelend
            pickupTable.Add(new LocationData() { uuid =  "e2803d8a-feea-45a7-addf-3330748d9e4b-2_A-OOGroveRadius", apid =  baseid + 189}); //clothesclaw_ridge_neridge
            pickupTable.Add(new LocationData() { uuid =  "8e381b9e-ab7a-44ad-81c3-19157c97e0c6-2_A-OOGroveRadius", apid =  baseid + 190}); //oldworldwhorl_ridge_ncliff
            pickupTable.Add(new LocationData() { uuid =  "fcb1f0bb-2ebf-449c-b5fd-ebb7d515c7af-2_A-OOGroveRadius", apid =  baseid + 191}); //limpet_ridge_ncliffkelp
            pickupTable.Add(new LocationData() { uuid =  "d9ee8d70-3493-4e7b-91b3-b42278ff0ad4-2_A-OOGroveRadius", apid =  baseid + 192}); //barbedhook_ridge_overlookpath
            pickupTable.Add(new LocationData() { uuid =  "f6db9d48-5df1-4a7f-9f17-b760901954c9-2_A-OOGroveRadius", apid =  baseid + 193}); //paperclaw_ridge_nearoverlook
            pickupTable.Add(new LocationData() { uuid =  "42686eee-4b9f-4d44-a52d-569d2c814f13-2_A-OOGroveRadius", apid =  baseid + 194}); //sanddollar_ridge_broom
            pickupTable.Add(new LocationData() { uuid =  "3f8d4c85-202f-42dc-9105-434d601d1d75-2_A-OOGroveRadius", apid =  baseid + 195}); //clothesclaw_ridge_broom
            pickupTable.Add(new LocationData() { uuid =  "a59b9d02-9711-48e3-8296-e2164c79f1f2-2_A-OOGroveRadius", apid =  baseid + 196}); //musselplus_ridge_togo
            pickupTable.Add(new LocationData() { uuid =  "d1883b57-b339-4c31-826a-e76a3de0ee4b-2_A-OOGroveRadius", apid =  baseid + 197}); //breadclaw_ridge_south
            pickupTable.Add(new LocationData() { uuid =  "35aa72f2-fa50-4efc-b600-b597382ed877-2_A-OOGroveRadius", apid =  baseid + 198}); //sunlight_costume_pickup
            pickupTable.Add(new LocationData() { uuid =  "ca4c297f-e9fd-4a78-b89a-811d2394406b-2_A-OOGroveRadius", apid =  baseid + 199}); //cockle_trashbin_peak
            pickupTable.Add(new LocationData() { uuid =  "36611427-18cc-45f3-ad50-a51cc02ccb1d-2_A-OOGroveRadius", apid =  baseid + 200}); //sinker_trashbin_peak
            pickupTable.Add(new LocationData() { uuid =  "0d86fe0e-ae41-4fb2-beb2-9bb5e558429a-2_A-OOGroveRadius", apid =  baseid + 201}); //smallbattery_trashbin_mantis
            pickupTable.Add(new LocationData() { uuid =  "7935fe1e-318c-4f90-901b-fa001de6e4dc-2_A-OOGroveRadius", apid =  baseid + 202}); //googlyeye_trashbin_pineapple
            pickupTable.Add(new LocationData() { uuid =  "e2a1e3e1-8ee4-4539-ab81-98c4d6d99630-2_A-OOGroveRadius", apid =  baseid + 203}); //siphonophoreplus_ridge_pitfall
            pickupTable.Add(new LocationData() { uuid =  "1b6f2070-3ff2-4f48-b061-0a41990013e1-2_A-OOGroveRadius", apid =  baseid + 204}); //anemoneplus_ridge_eel
            pickupTable.Add(new LocationData() { uuid =  "80e66e11-8e9a-4896-9db1-f8cd4a25fefc-2_A-OOGroveRadius", apid =  baseid + 205}); //oldworldwhorl_ridge_ncliff
            pickupTable.Add(new LocationData() { uuid =  "4f737f9e-a952-490c-b54d-1f74735af9cc-2_A-OOGroveRadius", apid =  baseid + 206}); //clothesclaw_ridge_overlookfish
            pickupTable.Add(new LocationData() { uuid =  "82043889-30eb-45f4-8f87-45dd2f5d69ed-2_A-OOGroveRadius", apid =  baseid + 207}); //chipclaw_ridge_southfish
            pickupTable.Add(new LocationData() { uuid =  "d32ade52-c34e-4cb7-957c-df605339ee4c-2_A-OOGroveRadius", apid =  baseid + 208}); //cockle_ridge_eelfish
            pickupTable.Add(new LocationData() { uuid =  "190034bb-c13e-42ec-bac4-037cc828fde3-2_A-OOGroveRadius", apid =  baseid + 209}); //stapleclaw_trashbin_eelfish
            pickupTable.Add(new LocationData() { uuid =  "2bccca10-1f27-4762-9a9b-8e628586e0a4-2_A-OOGroveRadius", apid =  baseid + 210}); //barbedhook_trashbin_eelgrapple
            pickupTable.Add(new LocationData() { uuid =  "fc2cf7ed-6ff6-40bb-987a-936da0afa93b-2_A-OOGroveRadius", apid =  baseid + 211}); //bobber_ridge_broomspire
            pickupTable.Add(new LocationData() { uuid =  "dcc6b99e-1d47-481f-86ca-e14a5743f460-2_A-OOGroveRadius", apid =  baseid + 212}); //sharkegg_ridge_broomspire
            pickupTable.Add(new LocationData() { uuid =  "74aa4107-6a5a-4868-ae54-942e4421be3f-2_A-GroveForestLow", apid =  baseid + 213}); //barnacle_lowergrove_colander
            pickupTable.Add(new LocationData() { uuid =  "8fd70034-2415-4934-975b-2303d159f567-2_A-GroveForestLow", apid =  baseid + 214}); //barbedhook_lowergrove_sniper
            pickupTable.Add(new LocationData() { uuid =  "0860891c-3db6-4e51-8c0b-58b09ac8ece0-2_A-GroveForestLow", apid =  baseid + 215}); //barbedhook_lowergrove_riverledge
            pickupTable.Add(new LocationData() { uuid =  "2465aea2-9800-49e5-9e7e-d751ca2b3612-2_A-GroveForestLow", apid =  baseid + 216}); //barbedhook_lowergrove_riversand
            pickupTable.Add(new LocationData() { uuid =  "09b60c18-2172-41bb-9588-a3e764559b15-2_B-GroveForestHigh", apid =  baseid + 217}); //oldworldwhorl_lowergrove_southclam
            pickupTable.Add(new LocationData() { uuid =  "17cb1eed-8d88-4464-b33b-215c837035f6-2_A-GroveForestLow", apid =  baseid + 218}); //seastar_lowergrove_eastrock
            pickupTable.Add(new LocationData() { uuid =  "4933d4b0-a56a-4472-b9a6-4469fd335fa4-2_A-GroveForestLow", apid =  baseid + 219}); //breadclaw_lowergrove_takeout
            pickupTable.Add(new LocationData() { uuid =  "c6a56f3d-c134-41cd-83b7-4a755fa32284-2_A-GroveForestLow", apid =  baseid + 220}); //pufferquill_lowergrove_sponges
            pickupTable.Add(new LocationData() { uuid =  "35455018-8cf6-48db-ba76-4ffbca84b0a8-2_A-GroveForestLow", apid =  baseid + 221}); //barbedhook_lowergrove_lichenthrope
            pickupTable.Add(new LocationData() { uuid =  "5a106f4d-8139-44da-9cdf-8d0bd70c6bf3-2_A-GroveForestLow", apid =  baseid + 222}); //barbedhook_lowergrove_lichenthropeeast
            pickupTable.Add(new LocationData() { uuid =  "10a99891-923e-4bab-8057-b6d5781cb35d-2_A-GroveForestLow", apid =  baseid + 223}); //barbedhook_lowergrove_sodacans
            pickupTable.Add(new LocationData() { uuid =  "da04ed8f-2d99-48f9-8412-67748d983d2b-2_A-GroveForestLow", apid =  baseid + 224}); //breadclaw_lowergrove_tiretop
            pickupTable.Add(new LocationData() { uuid =  "902c2deb-1b51-4226-9fd0-1c76bea668c3-2_A-GroveForestLow", apid =  baseid + 225}); //chipclaw_lowergrove_plastic
            pickupTable.Add(new LocationData() { uuid =  "e38ef2da-daaf-4312-bf7f-2e0a97d3d67b-2_A-GroveForestLow", apid =  baseid + 226}); //clothesclaw_lowergrove_choccymilk
            pickupTable.Add(new LocationData() { uuid =  "def84515-cfc0-4124-a1e1-81f6953b0c06-2_A-GroveForestLow", apid =  baseid + 227}); //breadclaw_lowergrove_insidetire
            pickupTable.Add(new LocationData() { uuid =  "50f9f7d1-0e70-4333-b8b3-fbc46f04f3ea-2_A-GroveForestLow", apid =  baseid + 228}); //barbedhook_lowergrove_acrossriver
            pickupTable.Add(new LocationData() { uuid =  "f1d3eafe-c252-4768-abcd-7f459e734d35-2_A-GroveForestLow", apid =  baseid + 229}); //bloodstar_lowergrove_waterfall
            pickupTable.Add(new LocationData() { uuid =  "e7382e22-3232-4380-a734-bef8eb9a2a76-2_A-GroveForestLow", apid =  baseid + 230}); //barbedhook_lowergrove_afternets
            pickupTable.Add(new LocationData() { uuid =  "be378f83-9ec2-4a3b-85b2-9a7499e6d693-2_A-GroveForestLow", apid =  baseid + 231}); //breadclaw_lowergrove_smallhall
            pickupTable.Add(new LocationData() { uuid =  "faa3c9ce-87c7-4f6d-b07e-be334c3bc447-2_A-GroveForestLow", apid =  baseid + 232}); //chipclaw_lowergrove_apples
            pickupTable.Add(new LocationData() { uuid =  "25737275-a222-4a6e-af6b-0c66c197cab7-2_A-GroveForestLow", apid =  baseid + 233}); //chipclaw_lowergrove_sodacans
            pickupTable.Add(new LocationData() { uuid =  "fd557b69-ddba-4b14-bd80-f0cb0d786203-2_A-GroveForestLow", apid =  baseid + 234}); //hairclaw_lowergrove_moonshine
            pickupTable.Add(new LocationData() { uuid =  "dc13eb69-ac98-4de9-9fe0-7f16a03d4ebb-2_A-GroveForestLow", apid =  baseid + 235}); //barbedhook_lowergrove_cartledge
            pickupTable.Add(new LocationData() { uuid =  "093c4972-ff5e-4ba6-b9a8-136411e1017f-2_A-GroveForestLow", apid =  baseid + 236}); //limpet_lowergrove_sodacan
            pickupTable.Add(new LocationData() { uuid =  "a5b6f3f6-6e1c-4934-9006-26da6cee47f4-2_A-GroveForestLow", apid =  baseid + 237}); //barbedhook_lowergrove_cartledgebottle
            pickupTable.Add(new LocationData() { uuid =  "1b74ef4f-528c-484b-9a5f-abbb3434f133-2_A-GroveForestLow", apid =  baseid + 238}); //chipclaw_lowergrove_circlerock
            pickupTable.Add(new LocationData() { uuid =  "f99a120b-43c2-4e65-91fd-71cbfbf3e8f7-2_A-GroveForestLow", apid =  baseid + 239}); //clothesclaw_lowergrove_alcove
            pickupTable.Add(new LocationData() { uuid =  "8f4a1f33-e567-4c84-b34b-739353b3ba08-2_A-GroveForestLow", apid =  baseid + 240}); //hairclaw_lowergrove_sniper
            pickupTable.Add(new LocationData() { uuid =  "776881ba-f61b-4400-8cf5-f11fb11b6f94-2_B-GroveForestHigh", apid =  baseid + 241}); //barbedhook_lowergrove_paperplate
            pickupTable.Add(new LocationData() { uuid =  "12c73a88-beef-4bfb-a2d1-c159c91d8304-2_B-GroveForestHigh", apid =  baseid + 242}); //barbedhook_lowergrove_oildrum
            pickupTable.Add(new LocationData() { uuid =  "c859129f-cc29-4624-a161-fdeffa00d0ca-2_B-GroveForestHigh", apid =  baseid + 243}); //breadclaw_lowergrove_amongus
            pickupTable.Add(new LocationData() { uuid =  "b85cb09c-f1d7-4396-85cb-fde4dc845acd-2_B-GroveForestHigh", apid =  baseid + 244}); //breadclaw_lowergrove_oildrum
            pickupTable.Add(new LocationData() { uuid =  "070b84a6-7a8b-4d88-8216-f55882856713-2_A-GroveForestLow", apid =  baseid + 245}); //chipclaw_lowergrove_sniper
            pickupTable.Add(new LocationData() { uuid =  "e78f03e8-2827-45d7-8cfa-cbaa0f17cd3d-2_A-GroveForestLow", apid =  baseid + 246}); //hairclaw_lowergrove_milkurchins
            pickupTable.Add(new LocationData() { uuid =  "3789e56d-69b5-4d6c-adb1-6454b101840d-2_A-GroveForestLow", apid =  baseid + 247}); //lumpsucker_lowergrove_canopy
            pickupTable.Add(new LocationData() { uuid =  "101eff6d-e09a-471d-86fd-cdc587012c32-2_A-GroveForestLow", apid =  baseid + 248}); //barbedhook_lowergrove_canopy
            pickupTable.Add(new LocationData() { uuid =  "bc18b92b-a6c6-420c-9095-e15c34c2f921-2_B-GroveForestHigh", apid =  baseid + 249}); //breadclaw_lowergrove_oilgrapple
            pickupTable.Add(new LocationData() { uuid =  "3a1e7a2c-5111-4360-b168-cc5e84088b6a-2_B-GroveForestHigh", apid =  baseid + 250}); //barbedhook_lowergrove_drumtop
            pickupTable.Add(new LocationData() { uuid =  "8e34f6a5-5844-4767-ab15-4fbaf6c147dc-2_B-GroveForestHigh", apid =  baseid + 251}); //sharktooth_lowergrove_pizza
            pickupTable.Add(new LocationData() { uuid =  "7f311dc9-3acc-4c14-8866-e8dfd4a3abfb-2_C-Village", apid =  baseid + 252}); //Map Piece (Heikea Arena)
            pickupTable.Add(new LocationData() { uuid = "df0386e0-99ee-478a-873d-b40e973fc16b-2_D-Caves", apid = baseid + 253});//
            pickupTable.Add(new LocationData() { skillName = "Skill_Shelleport", apid = baseid + 254 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Skedaddle", apid = baseid + 255 });//
            pickupTable.Add(new LocationData() { uuid = "048736b5-59c1-4dd2-b36a-e11b6bd9304f-2_A-GroveForestLow", apid = baseid + 256 });//
            pickupTable.Add(new LocationData() { uuid = "23907bde-1820-4c4d-9c59-aab72c64139a-2_A-GroveForestLow", apid = baseid + 257 });//
            pickupTable.Add(new LocationData() { uuid = "ebad553d-fbff-441e-8c1d-0ccc65fe81ae-2_A-GroveForestLow", apid = baseid + 258 });//
            pickupTable.Add(new LocationData() { uuid = "d1b73602-aca6-4d33-991d-39110a5e6780-2_A-GroveForestLow", apid = baseid + 259 });//
            pickupTable.Add(new LocationData() { uuid = "f734eb5e-d674-4c84-9493-61d20f70fde3-2_A-GroveForestLow", apid = baseid + 260 });//
            pickupTable.Add(new LocationData() { uuid = "7069eee2-4c77-4a80-a276-1131e6cfe4b8-2_A-GroveForestLow", apid = baseid + 261 });//
            pickupTable.Add(new LocationData() { uuid = "0e278f86-0f9d-468f-95b2-586df2a94689-2_A-GroveForestLow", apid = baseid + 262 });//
            pickupTable.Add(new LocationData() { uuid = "fa3cfc64-4f87-452c-bf59-4f599cb3f6fa-2_A-GroveForestLow", apid = baseid + 263 });//
            pickupTable.Add(new LocationData() { uuid = "a1c14883-bc09-441c-a188-20dabf047532-2_A-GroveForestLow", apid = baseid + 264 });//
            pickupTable.Add(new LocationData() { uuid = "7aa1847-5a38-4f15-a6fc-90c15cd9baf7-2_A-GroveForestLow", apid = baseid + 265 });//
            pickupTable.Add(new LocationData() { uuid = "6ce4acf2-09a4-43ca-9be7-5d79a9d5c781-2_A-GroveForestLow", apid = baseid + 266 });//
            pickupTable.Add(new LocationData() { uuid = "027511ce-7f48-480e-91af-18b4c7aaa57d-2_A-GroveForestLow", apid = baseid + 267 });//
            pickupTable.Add(new LocationData() { uuid = "66a4a627-ad3a-45d6-aaf1-1e505dac0495-2_B-GroveForestHigh", apid = baseid + 268 });//
            pickupTable.Add(new LocationData() { uuid = "a6e719fc-65b0-4c3a-830e-43065a819c7d-2_B-GroveForestHigh", apid = baseid + 269 });//
            pickupTable.Add(new LocationData() { uuid = "507a5ca4-3de6-4d85-ba51-04c1e42c891e-2_B-GroveForestHigh", apid = baseid + 270 });//
            pickupTable.Add(new LocationData() { uuid = "7a38fbf2-47a9-4bf6-85d1-f8d4f58b8d66-2_B-GroveForestHigh", apid = baseid + 271 });//
            pickupTable.Add(new LocationData() { uuid = "ec08fee3-e62f-45ee-9fbe-9101522d4f30-2_B-GroveForestHigh", apid = baseid + 272 });//
            pickupTable.Add(new LocationData() { uuid = "9c983d04-ac83-40f0-8220-d92f76ea7099-2_B-GroveForestHigh", apid = baseid + 273 });//
            pickupTable.Add(new LocationData() { uuid = "5e81380f-3e8f-4d44-a0ef-ebbad0f23fdb-2_B-GroveForestHigh", apid = baseid + 274 });//
            pickupTable.Add(new LocationData() { uuid = "5c973136-cfe9-4ea5-bc93-60ffa4b5cc13-2_C-Village", apid = baseid + 275 });//
            pickupTable.Add(new LocationData() { uuid = "84c9cc27-0876-4e4d-a9e2-c28f06abdfc3-2_C-Village", apid = baseid + 276 });//
            pickupTable.Add(new LocationData() { uuid = "4721ab20-67be-4d5b-b0bb-3f2c2ecb510f-2_C-Village", apid = baseid + 277 });//
            pickupTable.Add(new LocationData() { uuid = "bae2eb9a-56a0-4548-8c6d-798d234ba479-2_C-Village", apid = baseid + 278 });//
            pickupTable.Add(new LocationData() { uuid = "20238db7-8d1e-4045-8e0a-de2aaea1cb49-2_C-Village", apid = baseid + 279 });//
            pickupTable.Add(new LocationData() { uuid = "87e729ae-0659-4b6e-9ac1-c1d059719ba8-2_C-Village", apid = baseid + 280 });//
            pickupTable.Add(new LocationData() { uuid = "c83c8f42-474a-4dd4-bbae-99df2ad1681a-2_C-Village", apid = baseid + 281 });//
            pickupTable.Add(new LocationData() { uuid = "b29402c2-62e8-4fa3-8c27-4e419750b16f-2_C-Village", apid = baseid + 282 });//
            pickupTable.Add(new LocationData() { uuid = "1cd62e82-14b4-4a8a-8dc1-7e8f0ff9b11b-2_C-Village", apid = baseid + 283 });//
            pickupTable.Add(new LocationData() { uuid = "256daaaa-675c-47e5-bd4e-367dc4abd9e2-2_C-Village", apid = baseid + 284 });//
            pickupTable.Add(new LocationData() { uuid = "451f2fe5-9c29-4710-9493-0878963547e4-2_C-Village", apid = baseid + 285 });//
            pickupTable.Add(new LocationData() { uuid = "99c33625-2da4-4f69-a0af-8a8d2da3b399-2_D-Caves", apid = baseid + 286 });//
            pickupTable.Add(new LocationData() { uuid = "bcfabbd4-8575-4a6a-8325-7259996a7d68-2_D-Caves", apid = baseid + 287 });//
            pickupTable.Add(new LocationData() { uuid = "", apid = baseid + 288 });//Unused
            pickupTable.Add(new LocationData() { uuid = "ff8f29f4-2859-4ff7-857f-54e0a9c185fa-2_D-Caves", apid = baseid + 289 });//
            pickupTable.Add(new LocationData() { uuid = "10eae42f-9cca-447a-8cb8-9cffcba11ecd-2_D-Caves", apid = baseid + 290 });//
            pickupTable.Add(new LocationData() { uuid = "6dbab2ee-2440-4b3d-944e-688f11a88199-2_D-Caves", apid = baseid + 291 });//
            pickupTable.Add(new LocationData() { uuid = "7434f4f5-8e47-40f0-b3a3-507ada236333-2_D-Caves", apid = baseid + 292 });//
            pickupTable.Add(new LocationData() { uuid = "b1b6be9e-1973-4562-8e96-43adf0d8fcd5-2_C-Village", apid = baseid + 293 });//
            pickupTable.Add(new LocationData() { uuid = "dc39a6c7-45be-4e9a-9ac2-4da3094395dd-2_C-Village", apid = baseid + 294 });//
            pickupTable.Add(new LocationData() { uuid = "e9f1284d-a5cf-441e-a358-8cabf5e3a7b1-2_C-Village", apid = baseid + 295 });//
            pickupTable.Add(new LocationData() { uuid = "4ba7f2ff-ff35-4d74-b5ac-d9c59fe1c94d-2_C-Village", apid = baseid + 296 });//
            pickupTable.Add(new LocationData() { uuid = "3eee4944-4b1e-4680-92de-0ee564c91330-2_C-Village", apid = baseid + 297 });//
            pickupTable.Add(new LocationData() { uuid = "c69ffc64-04af-44d1-b470-a867c5f06199-2_D-Caves", apid = baseid + 298 });//
            pickupTable.Add(new LocationData() { uuid = "8e368d78-572e-4fed-8607-2fb4c35b23e8-2_C-Village", apid = baseid + 299 });//
            pickupTable.Add(new LocationData() { uuid = "03bc01f8-4691-40b7-93c6-4d45563b5b46-2_C-Village", apid = baseid + 300 });//
            pickupTable.Add(new LocationData() { uuid = "28362ae0-defe-4a04-b449-93d7e5e60676-2_C-Village", apid = baseid + 301 });//
            pickupTable.Add(new LocationData() { uuid = "0177d8ef-8849-4ea4-b1b3-08a1c3134a99-2_E-Cliffs", apid = baseid + 302 });//
            pickupTable.Add(new LocationData() { uuid = "b3a776e1-52ab-4dc8-adc1-55c95eca9972-2_E-Cliffs", apid = baseid + 303 });//
            pickupTable.Add(new LocationData() { uuid = "ce1700e2-d9d0-462a-af23-cb55828b062a-2_E-Cliffs", apid = baseid + 304 });//
            pickupTable.Add(new LocationData() { uuid = "235b691b-0f18-4f35-bd8a-7536e12fabfe-2_E-Cliffs", apid = baseid + 305 });//
            pickupTable.Add(new LocationData() { uuid = "ab2ba852-edf4-467f-a1f5-1825341b7596-2_E-Cliffs", apid = baseid + 306 });//
            pickupTable.Add(new LocationData() { uuid = "269526ef-6bb0-42cf-a93d-e2cac970e424-2_E-Cliffs", apid = baseid + 307 });//
            pickupTable.Add(new LocationData() { uuid = "32841813-148d-45f7-8c4e-4464f157200d-2_E-Cliffs", apid = baseid + 308 });//
            pickupTable.Add(new LocationData() { uuid = "aff46c65-7a4a-4b1c-a0ca-840af3898f26-2_C-Village", apid = baseid + 309 });//
            pickupTable.Add(new LocationData() { uuid = "3f1f877c-e5ac-40d1-93b7-2245282b0669-2_C-Village", apid = baseid + 310 });//
            pickupTable.Add(new LocationData() { uuid = "2a160afc-7439-4e22-9f2b-62fa152f6626-2_C-Village", apid = baseid + 311 });//
            pickupTable.Add(new LocationData() { uuid = "08e5033e-a401-4e90-a1a5-7e9e76ed72a5-2_C-Village", apid = baseid + 312 });//
            pickupTable.Add(new LocationData() { uuid = "725998d5-172d-4408-9f70-0f9264a6a6fd-2_C-Village", apid = baseid + 313 });//
            pickupTable.Add(new LocationData() { uuid = "3bd305d9-8d6b-43c4-99c7-1f2be904de06-2_E-Cliffs", apid = baseid + 314 });//
            pickupTable.Add(new LocationData() { uuid = "2601baa3-999e-443b-ae33-ea619346413b-2_E-Cliffs", apid = baseid + 315 });//
            pickupTable.Add(new LocationData() { uuid = "8b9b3b77-b95f-46a2-adfd-ca962e8f0cef-2_E-Cliffs", apid = baseid + 316 });//
            pickupTable.Add(new LocationData() { uuid = "", apid = baseid + 317 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Parries", apid = baseid + 318 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Riposte", apid = baseid + 319 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_NakedParry", apid = baseid + 320 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Aggravation", apid = baseid + 321 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_SelfRepair", apid = baseid + 322 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Kintsugi", apid = baseid + 323 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Skewer", apid = baseid + 324 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Plunge", apid = baseid + 325 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_ScrapHammer", apid = baseid + 326 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Dispatch", apid = baseid + 327 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Fishing", apid = baseid + 328 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_WaveBreaker", apid = baseid + 329 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_AirDodge", apid = baseid + 330 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_Housewarming", apid = baseid + 331 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_ChumInTheWater", apid = baseid + 332 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_ElusivePrey", apid = baseid + 333 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_EbbAndFlow", apid = baseid + 334 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_UmamiTrainingA", apid = baseid + 335 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_UmamiTrainingB", apid = baseid + 336 });//
            pickupTable.Add(new LocationData() { skillName = "Skill_UmamiTrainingC", apid = baseid + 337 });//
            pickupTable.Add(new LocationData() { uuid = "17ac32ac-f0fc-4613-9e02-5ffdf4eecb63-2_B-LowSwamp", apid = baseid + 338 });//
            pickupTable.Add(new LocationData() { uuid = "7aa4931c-e44f-4531-9b5e-eb1a01418fdd-2_B-LowSwamp", apid = baseid + 339 });//
            pickupTable.Add(new LocationData() { uuid = "2f65dc9d-5ea6-4480-b8fb-c9a9bcd779d6-2_B-LowSwamp", apid = baseid + 340 });//
            pickupTable.Add(new LocationData() { uuid = "39365f04-a3c8-44e8-a398-bff99a84d2a7-2_B-LowSwamp", apid = baseid + 341 });//
            pickupTable.Add(new LocationData() { uuid = "d0f6546a-c86e-4b9d-aa13-b45ce26abbba-2_B-LowSwamp", apid = baseid + 342 });//
            pickupTable.Add(new LocationData() { uuid = "60c9e9de-af79-4068-a7bf-7005ac760f4b-2_B-LowSwamp", apid = baseid + 343 });//
            pickupTable.Add(new LocationData() { uuid = "45cca266-a175-4020-9dd7-e0b844421aff-2_B-LowSwamp", apid = baseid + 344 });//
            pickupTable.Add(new LocationData() { uuid = "fbc91e55-5d3e-4c76-8566-458a9f3d1b20-2_B-LowSwamp", apid = baseid + 345 });//
            pickupTable.Add(new LocationData() { uuid = "76b9b9ce-875c-4136-a6da-dabdfc70d40e-2_B-LowSwamp", apid = baseid + 346 });//
            pickupTable.Add(new LocationData() { uuid = "ee9133b1-c508-41d7-8a7d-0b19241e4ccf-2_B-LowSwamp", apid = baseid + 347 });//
            pickupTable.Add(new LocationData() { uuid = "b1f8794b-d43d-41b9-827a-75bb4c7a57bf-NickGym", apid = baseid + 348 });//
            pickupTable.Add(new LocationData() { uuid = "c3ff376c-5f53-4401-a52a-1515dba2a2a3-2_B-LowSwamp", apid = baseid + 349 });//
            pickupTable.Add(new LocationData() { uuid = "d656b2e6-15ec-4e24-bebb-94c1ed7f6c30-2_B-LowSwamp", apid = baseid + 350 });//
            pickupTable.Add(new LocationData() { uuid = "6aa68025-641d-4fd7-a43b-7c0f3ccf4da4-2_B-LowSwamp", apid = baseid + 351 });//
            pickupTable.Add(new LocationData() { uuid = "06b1bae1-7bf1-4b78-97f7-82ac6d6de47f-2_A-HighSwamp", apid = baseid + 352 });//
            pickupTable.Add(new LocationData() { uuid = "282e7092-dc21-45f0-805d-78ea29dfb0bd-2_A-HighSwamp", apid = baseid + 353 });//
            pickupTable.Add(new LocationData() { uuid = "53cea11a-cfb6-4894-8e18-375ea895caed-2_A-HighSwamp", apid = baseid + 354 });//
            pickupTable.Add(new LocationData() { uuid = "2fd87610-e95f-4b28-9521-d3b59fb718f0-2_A-HighSwamp", apid = baseid + 356 });//
            pickupTable.Add(new LocationData() { uuid = "32f43e6e-a382-4938-b068-f1eab35a0269-2_A-HighSwamp", apid = baseid + 357 });//
            pickupTable.Add(new LocationData() { uuid = "125b1086-fa58-45bf-a42f-e715781feffa-2_A-HighSwamp", apid = baseid + 358 });//
            pickupTable.Add(new LocationData() { uuid = "36d0180d-dfa2-49be-b5f0-9b65c5f05d7a-2_A-HighSwamp", apid = baseid + 359 });//
            pickupTable.Add(new LocationData() { uuid = "19c7932e-9133-4dbc-bc25-9f71aa29c271-2_A-HighSwamp", apid = baseid + 360 });//
            pickupTable.Add(new LocationData() { uuid = "2546c006-fb9c-412e-99da-e1533b6c388e-2_A-HighSwamp", apid = baseid + 361 });//
            pickupTable.Add(new LocationData() { uuid = "cf30643f-5b43-4e94-af5d-c879e6531d14-2_A-HighSwamp", apid = baseid + 362 });//
            pickupTable.Add(new LocationData() { uuid = "e5b02de1-7cb7-4a4d-98a9-369c44302cac-2_A-HighSwamp", apid = baseid + 363 });//
            pickupTable.Add(new LocationData() { uuid = "ecbaf918-3632-496e-9598-a9e5da91f495-2_A-HighSwamp", apid = baseid + 364 });//
            pickupTable.Add(new LocationData() { uuid = "19644b3e-d9b8-4cae-be53-7c2cae5a5ac8-2_A-HighSwamp", apid = baseid + 365 });//
            pickupTable.Add(new LocationData() { uuid = "00718126-9001-4bba-980c-e1d20298a13e-2_A-HighSwamp", apid = baseid + 366 });//
            pickupTable.Add(new LocationData() { uuid = "f8d826c0-7319-4f95-ab90-bf28a7218dfb-2_A-HighSwamp", apid = baseid + 367 });//
            pickupTable.Add(new LocationData() { uuid = "855a1efa-0f38-4e6b-a68d-24a991dcac42-2_A-HighSwamp", apid = baseid + 368 });//
            pickupTable.Add(new LocationData() { uuid = "cd5c1ed2-6f66-4d40-a23e-a4099f7890b1-2_A-HighSwamp", apid = baseid + 369 });//
            pickupTable.Add(new LocationData() { uuid = "0337dc3e-fcf7-4f30-8759-bef7afeb04ca-2_A-HighSwamp", apid = baseid + 370 });//
            pickupTable.Add(new LocationData() { uuid = "0ad3895d-4612-4856-a6ed-7a7b48f2c36f-2_A-HighSwamp", apid = baseid + 371 });//
            pickupTable.Add(new LocationData() { uuid = "5ccf4e25-ff98-4a9a-9e31-2eea57589809-2_A-HighSwamp", apid = baseid + 372 });//
            pickupTable.Add(new LocationData() { uuid = "b361630f-a4b5-46fd-8bb3-faa583d0aa3b-2_A-HighSwamp", apid = baseid + 373 });//
            pickupTable.Add(new LocationData() { uuid = "cc22f484-88bf-4a0a-bf32-d5aa0d17ed32-2_A-HighSwamp", apid = baseid + 374 });//
            pickupTable.Add(new LocationData() { uuid = "a569d9f6-d930-4a9b-8041-8eb8621532ac-2_A-HighSwamp", apid = baseid + 375 });//
            pickupTable.Add(new LocationData() { uuid = "39e6038f-740f-4e90-acf5-2f1125ae318b-2_A-HighSwamp", apid = baseid + 376 });//
            pickupTable.Add(new LocationData() { uuid = "235d8af5-0add-4509-b33c-dd3e3dbd27ff-2_A-HighSwamp", apid = baseid + 377 });//
            pickupTable.Add(new LocationData() { uuid = "90422b9d-ec1b-4be4-a606-54f196dc1f1e-2_A-HighSwamp", apid = baseid + 378 });//
            pickupTable.Add(new LocationData() { uuid = "2f19d3b2-03ae-4fbe-8821-2dd01f007d11-2_A-HighSwamp", apid = baseid + 379 });//
            pickupTable.Add(new LocationData() { uuid = "0696b1bf-ecc4-41c3-84fe-df54a0fcbccf-2_B-LowSwamp", apid = baseid + 380 });//
            pickupTable.Add(new LocationData() { uuid = "3190747b-d224-4e01-874f-3dbbac197adc-2_A-HighSwamp", apid = baseid + 381 });//
            pickupTable.Add(new LocationData() { uuid = "cce99207-a255-4e12-ba19-51b5048ac64b-2_A-HighSwamp", apid = baseid + 382 });//
            pickupTable.Add(new LocationData() { uuid = "ad3bbd24-6760-4afc-b988-40943e6f331f-2_A-HighSwamp", apid = baseid + 383 });//
            pickupTable.Add(new LocationData() { uuid = "1704b510-b12f-4156-b688-e82cb02bd933-2_A-HighSwamp", apid = baseid + 384 });//
            pickupTable.Add(new LocationData() { uuid = "442a694b-ba12-4f08-ad49-f2a5697d3c19-2_A-HighSwamp", apid = baseid + 385 });//
            pickupTable.Add(new LocationData() { uuid = "6adbf701-160e-4112-a275-223bffeffb01-2_A-HighSwamp", apid = baseid + 386 });//
            pickupTable.Add(new LocationData() { uuid = "8480a51d-bd34-43bb-a348-a3bf69040248-2_A-HighSwamp", apid = baseid + 387 });//
            pickupTable.Add(new LocationData() { uuid = "3e57df65-8b77-4402-b23e-b1eb7012e88c-2_A-HighSwamp", apid = baseid + 388 });//
            pickupTable.Add(new LocationData() { uuid = "5c219ab5-99f1-4b63-88d0-684a6c5d68aa-2_A-HighSwamp", apid = baseid + 389 });//
            pickupTable.Add(new LocationData() { uuid = "aeb3f185-eb51-4e68-a0cc-6dafc4b48a11-2_A-HighSwamp", apid = baseid + 390 });//
            pickupTable.Add(new LocationData() { uuid = "54f7c579-6ea7-402d-b1eb-9ee67d2bbe9a-2_A-HighSwamp", apid = baseid + 391 });//
            pickupTable.Add(new LocationData() { uuid = "812367fb-0264-4113-9da5-104ba61d368b-2_A-HighSwamp", apid = baseid + 392 });//
            pickupTable.Add(new LocationData() { uuid = "35d5823c-2da6-48c4-b8f1-074221917090-2_A-HighSwamp", apid = baseid + 393 });//
            pickupTable.Add(new LocationData() { uuid = "2112465c-60b4-423a-8bb7-301a64aec108-2_A-HighSwamp", apid = baseid + 394 });//
            pickupTable.Add(new LocationData() { uuid = "daf9fa57-89d6-481f-8a6d-c32c9e8ecb5a-2_A-HighSwamp", apid = baseid + 395 });//
            pickupTable.Add(new LocationData() { uuid = "beafc7da-1e14-43b2-ad4a-522519b036b7-2_A-HighSwamp", apid = baseid + 396 });//
            pickupTable.Add(new LocationData() { uuid = "977f86dc-aeb1-471f-bb86-4db41bcbfc2c-2_A-HighSwamp", apid = baseid + 397 });//
            pickupTable.Add(new LocationData() { uuid = "1abd8a20-f2cc-488b-8e0a-00b12c305667-2_A-HighSwamp", apid = baseid + 398 });//
            pickupTable.Add(new LocationData() { uuid = "a76a0b9b-6307-4b71-be91-da385c17a977-2_A-HighSwamp", apid = baseid + 399 });//
            pickupTable.Add(new LocationData() { uuid = "771c0772-2715-42c1-90b1-e033997895d1-2_A-HighSwamp", apid = baseid + 400 });//
            pickupTable.Add(new LocationData() { uuid = "3ef191f5-0296-4760-b9ef-8d2fa99ac914-2_A-HighSwamp", apid = baseid + 401 });//
            pickupTable.Add(new LocationData() { uuid = "a0549711-5560-4b9c-99d5-65e13911413a-2_A-HighSwamp", apid = baseid + 402 });//
            pickupTable.Add(new LocationData() { uuid = "b6c01d3a-31e3-457d-b53f-ecf0ff3b7e9f-2_B-LowSwamp", apid = baseid + 403 });//
            pickupTable.Add(new LocationData() { uuid = "9d34e68d-94a3-417c-a919-ddaf13b1c257-2_B-LowSwamp", apid = baseid + 404 });//
            pickupTable.Add(new LocationData() { uuid = "7e76017e-13fa-4a96-a1f8-6d881b20b372-2_B-LowSwamp", apid = baseid + 405 });//
            pickupTable.Add(new LocationData() { uuid = "92744ee7-94a9-4abf-a3e2-93d91b82d02f-2_A-HighSwamp", apid = baseid + 406 });//
            pickupTable.Add(new LocationData() { uuid = "2cb7f595-94fa-4b7b-a7c6-602808292c48-2_A-HighSwamp", apid = baseid + 407 });//
            pickupTable.Add(new LocationData() { uuid = "b81ed4dc-3b3e-420d-99e9-d676c88fc120-2_B-LowSwamp", apid = baseid + 408 });//
            pickupTable.Add(new LocationData() { uuid = "ececfd87-e180-425f-9aaa-3a569bbfb5ea-2_B-LowSwamp", apid = baseid + 409 });//
            pickupTable.Add(new LocationData() { uuid = "89d10f86-fcf2-483a-b145-f1576682520c-2_A-HighSwamp", apid = baseid + 410 });//
            pickupTable.Add(new LocationData() { uuid = "5a5395f1-e087-4a30-b3ea-b055fd328d40-2_B-LowSwamp", apid = baseid + 411 });//
            pickupTable.Add(new LocationData() { uuid = "e863acfd-d4ee-4d2b-a868-6fe2420f1eda-2_B-LowSwamp", apid = baseid + 412 });//
            pickupTable.Add(new LocationData() { uuid = "cdf2769f-deb3-43dc-841f-a4c1c0081833-2_A-HighSwamp", apid = baseid + 413 });//
            pickupTable.Add(new LocationData() { uuid = "491e2dc3-ab0b-4da4-b36d-a3c2e60ea2a3-2_B-LowSwamp", apid = baseid + 414 });//
            pickupTable.Add(new LocationData() { uuid = "98e712ca-a037-4b20-81b6-e18d8221581a-2_B-LowSwamp", apid = baseid + 415 });//
            pickupTable.Add(new LocationData() { uuid = "aecd0ac5-352e-4ae3-ae6a-3b810e8588d6-2_B-LowSwamp", apid = baseid + 416 });//
            pickupTable.Add(new LocationData() { uuid = "2cd9c105-6c9e-4225-92d0-68a3d9488a4c-2_B-LowSwamp", apid = baseid + 417 });//
            pickupTable.Add(new LocationData() { uuid = "d4dcf093-3098-419f-83c6-72a4d0626cc3-2_B-LowSwamp", apid = baseid + 418 });//
            pickupTable.Add(new LocationData() { uuid = "4cce8eec-cc51-43c3-b629-768b62a16314-2_A-HighSwamp", apid = baseid + 419 });//
            pickupTable.Add(new LocationData() { uuid = "698a076c-2661-4731-90c4-211d8b1414c6-2_A-HighSwamp", apid = baseid + 420 });//
            pickupTable.Add(new LocationData() { uuid = "f29c22d3-4459-4d21-b76d-dbdd514fe09a-2_A-HighSwamp", apid = baseid + 421 });//
            pickupTable.Add(new LocationData() { uuid = "df4a99c6-a331-4b68-8ab3-33840fa40519-2_A-HighSwamp", apid = baseid + 422 });//
            pickupTable.Add(new LocationData() { uuid = "2fd08de6-99fb-45f8-95e3-d32832180314-2_B-LowSwamp", apid = baseid + 423 });//
            pickupTable.Add(new LocationData() { uuid = "b5875ab1-4e89-4ba5-a9ea-6eca950d51f0-2_A-HighSwamp", apid = baseid + 424 });//
            pickupTable.Add(new LocationData() { uuid = "8f06cd49-3b2b-4883-b7a5-a421b38870ba-2_A-HighSwamp", apid = baseid + 425 });//
            pickupTable.Add(new LocationData() { uuid = "5c795be7-e38a-4986-bcfb-b72ea7e98787-2_A-HighSwamp", apid = baseid + 426 });//
            pickupTable.Add(new LocationData() { uuid = "5736e238-67d5-4215-823d-a0c3b3d60b2a-2_A-HighSwamp", apid = baseid + 427 });//
            pickupTable.Add(new LocationData() { uuid = "69c1d4c5-c467-4127-a12a-6277376c47f0-2_A-HighSwamp", apid = baseid + 428 });//
            pickupTable.Add(new LocationData() { uuid = "a91e708c-89a8-4ef0-bf21-870cd56e652e-2_A-HighSwamp", apid = baseid + 429 });//
            pickupTable.Add(new LocationData() { uuid = "f37cb96e-df05-4aed-b2b5-7d8d5af32483-NickGym", apid = baseid + 430 });//
            pickupTable.Add(new LocationData() { uuid = "b2821ac0-6087-46a8-9809-61e20afb3a6e-2_C-Facilities", apid = baseid + 431 });//
            pickupTable.Add(new LocationData() { uuid = "14992c5b-3377-4664-9309-3f44a31b1ae5-2_C-Facilities", apid = baseid + 432 });//
            pickupTable.Add(new LocationData() { uuid = "6c265bf5-d319-4a6b-8ddf-ad803b9bdde6-2_C-Facilities", apid = baseid + 433 });//
            pickupTable.Add(new LocationData() { uuid = "f738e9de-5eb9-41c1-8358-48a6d51a9fff-2_C-Facilities", apid = baseid + 434 });//
            pickupTable.Add(new LocationData() { uuid = "62ab5a16-8e1f-411b-91d9-c71b45cd449e-2_C-Facilities", apid = baseid + 435 });//
            pickupTable.Add(new LocationData() { uuid = "12994a0d-c89b-4780-aaa9-48bf996c9348-2_C-Facilities", apid = baseid + 436 });//
            pickupTable.Add(new LocationData() { uuid = "34e41971-5cef-4947-b6ff-6b1e4da0f2d6-2_C-Facilities", apid = baseid + 437 });//
            pickupTable.Add(new LocationData() { uuid = "95c7f334-5083-4ee8-bbcb-65033ca154f5-2_C-Facilities", apid = baseid + 438 });//
            pickupTable.Add(new LocationData() { uuid = "5eefb844-f267-416a-ba6e-c1b8ec945cfa-2_C-Facilities", apid = baseid + 439 });//
            pickupTable.Add(new LocationData() { uuid = "f1ce3edd-314d-4cf7-ae79-fdb5cf0a7ac0-2_C-Facilities", apid = baseid + 440 });//
            pickupTable.Add(new LocationData() { uuid = "1a376ec2-e1bf-44a3-bc18-0ec8d4252b0d-2_C-Facilities", apid = baseid + 441 });//
            pickupTable.Add(new LocationData() { uuid = "631f983b-ebc5-4fa3-aedc-38fd4d1efffb-2_C-Facilities", apid = baseid + 442 });//
            pickupTable.Add(new LocationData() { uuid = "b0c6c00b-fcbb-4884-83c7-cf655211e599-2_C-Facilities", apid = baseid + 443 });//
            pickupTable.Add(new LocationData() { uuid = "555557dd-239a-40da-a89f-82d0cc6adc65-2_C-Facilities", apid = baseid + 444 });//
            pickupTable.Add(new LocationData() { uuid = "497aa1a9-1ffa-4ea1-ad17-32fa6654db2e-2_C-Facilities", apid = baseid + 445 });//
            pickupTable.Add(new LocationData() { uuid = "6837a37b-eb98-42da-bd99-b589a7bc15a2-2_C-Facilities", apid = baseid + 446 });//
            pickupTable.Add(new LocationData() { uuid = "c2d88bae-4763-4c16-b79e-802f00a519a7-2_C-Facilities", apid = baseid + 447 });//
            pickupTable.Add(new LocationData() { uuid = "e449f43b-dc05-4e13-8f8c-b93adf0e97fa-2_C-Facilities", apid = baseid + 448 });//
            pickupTable.Add(new LocationData() { uuid = "bb733ac9-8027-47b6-bea5-f2f66e202da3-2_C-Facilities", apid = baseid + 449 });//
            pickupTable.Add(new LocationData() { uuid = "d6b71350-5bc1-4b39-a53c-9ff5b93faf2b-2_C-Facilities", apid = baseid + 450 });//
            pickupTable.Add(new LocationData() { uuid = "f2466d53-7e70-4d19-9c12-cb9da07a72fe-2_C-Facilities", apid = baseid + 451 });//
            pickupTable.Add(new LocationData() { uuid = "7126823c-0add-478d-aa4a-fc62c19959b2-2_C-Facilities", apid = baseid + 452 });//
            pickupTable.Add(new LocationData() { uuid = "de7097a4-7028-454b-98e6-c61a03557736-2_C-Facilities", apid = baseid + 453 });//
            pickupTable.Add(new LocationData() { uuid = "a8fdab67-387d-4d7b-ad71-85cd2e79e307-2_C-Facilities", apid = baseid + 454 });//
            pickupTable.Add(new LocationData() { uuid = "17a5b692-c56b-4f43-8ca2-b2909ef9c446-2_C-Facilities", apid = baseid + 455 });//
            pickupTable.Add(new LocationData() { uuid = "08bbdb7b-63dd-4e3d-9499-fc77548c3bd9-2_C-Facilities", apid = baseid + 456 });//
            pickupTable.Add(new LocationData() { uuid = "3192f8df-dcfa-4383-b5d5-35c92e4eadf4-2_C-Facilities", apid = baseid + 457 });//
            pickupTable.Add(new LocationData() { uuid = "0e380f83-dc56-468f-aa76-30f049c33fd8-2_C-Facilities", apid = baseid + 458 });//
            pickupTable.Add(new LocationData() { uuid = "301e305f-fc4c-4bd5-a839-ca1ea6435c13-2_C-Facilities", apid = baseid + 459 });//
            pickupTable.Add(new LocationData() { uuid = "0710d989-547d-43e3-b8e0-70c634c08441-2_C-Facilities", apid = baseid + 460 });//
            pickupTable.Add(new LocationData() { uuid = "aeaefce5-14fa-4363-b66d-43593a6edc70-2_C-Facilities", apid = baseid + 461 });//
            pickupTable.Add(new LocationData() { uuid = "dd2d2ccc-5586-40f6-b151-cd2c1e6bbd0a-2_C-Facilities", apid = baseid + 462 });//
            pickupTable.Add(new LocationData() { uuid = "bdffecf0-4d87-4734-b99a-24200c469d72-2_C-Facilities", apid = baseid + 463 });//
            pickupTable.Add(new LocationData() { uuid = "b515d67d-b4fa-4282-a872-9c351d087c85-2_C-Facilities", apid = baseid + 464 });//
            pickupTable.Add(new LocationData() { uuid = "0732b3f4-78b7-4584-8459-fab762bd98c5-2_C-Facilities", apid = baseid + 465 });//
            pickupTable.Add(new LocationData() { uuid = "18840259-0160-447d-991f-4390462801ec-2_C-Facilities", apid = baseid + 466 });//
            pickupTable.Add(new LocationData() { uuid = "b2e0c8a0-def7-47af-add1-22b37a414b8d-2_C-Facilities", apid = baseid + 467 });//
            pickupTable.Add(new LocationData() { uuid = "ca68edae-bc53-4270-b2e5-0b3978300fe0-2_C-Facilities", apid = baseid + 468 });//
            pickupTable.Add(new LocationData() { uuid = "f5ed972d-9d53-45b3-93b0-a389464d8941-2_C-Facilities", apid = baseid + 469 });//
            pickupTable.Add(new LocationData() { uuid = "83173b8a-6a13-44f0-b3be-f81c8830eb06-2_C-Facilities", apid = baseid + 470 });//
            pickupTable.Add(new LocationData() { uuid = "60db4097-9756-47cc-994c-506ce4069cab-2_C-Facilities", apid = baseid + 471 });//
            pickupTable.Add(new LocationData() { uuid = "cdd95cfc-844c-4034-9151-51eea2f6592d-2_C-Facilities", apid = baseid + 472 });//
            pickupTable.Add(new LocationData() { uuid = "d9bd9604-874b-4eb1-a738-42297eaf16f3-2_C-Facilities", apid = baseid + 473 });//
            pickupTable.Add(new LocationData() { uuid = "53d359e0-d707-4585-8d8f-75bf20cdf165-2_C-Facilities", apid = baseid + 474 });//
            pickupTable.Add(new LocationData() { uuid = "affcf5d1-3127-4836-9a78-cea5ae6b9766-2_C-Facilities", apid = baseid + 475 });//
            pickupTable.Add(new LocationData() { uuid = "a4d7a5e4-2374-4a0c-8f5d-421b07ae6e2d-2_C-Facilities", apid = baseid + 476 });//
            pickupTable.Add(new LocationData() { uuid = "df0d5a5c-5391-425d-b250-5c26553f2bfb-2_C-Facilities", apid = baseid + 477 });//
            pickupTable.Add(new LocationData() { uuid = "7f30ac6a-2e29-4ccb-9fd2-eb2491741c6c-2_C-Facilities", apid = baseid + 478 });//
            pickupTable.Add(new LocationData() { uuid = "a16b7348-4d15-4bfb-9a0c-ee2f8994deea-2_C-Facilities", apid = baseid + 479 });//
            pickupTable.Add(new LocationData() { uuid = "09d1270f-cab8-45dd-af56-0c7220ad9e98-2_C-Facilities", apid = baseid + 480 });//
            pickupTable.Add(new LocationData() { uuid = "300de773-3757-4ee9-9e43-03bb1ca51410-2_C-Facilities", apid = baseid + 481 });//
            pickupTable.Add(new LocationData() { uuid = "d8ad97ae-8fa3-4a76-98ef-6cf635627776-2_C-Facilities", apid = baseid + 482 });//
            pickupTable.Add(new LocationData() { uuid = "1af5fbc8-b8d7-47e7-a66d-480feb0c2594-2_C-Facilities", apid = baseid + 483 });//
            pickupTable.Add(new LocationData() { uuid = "98b0e1e4-2548-4b28-948d-b3a05147286a-2_A-PinBargeRunup", apid = baseid + 484 });//
            pickupTable.Add(new LocationData() { uuid = "8edddc0a-8caf-48a1-975e-4167e728a4c3-1_A-PinBargeRunup", apid = baseid + 485 });//
            pickupTable.Add(new LocationData() { uuid = "fef13a6e-bf0d-4814-a8f5-965fb9c2cd31-2_A-PinBargeRunup", apid = baseid + 486 });//
            pickupTable.Add(new LocationData() { uuid = "29dab0bb-a195-47d3-b553-ebc5a4f03154-2_A-PinBargeRunup", apid = baseid + 487 });//
            pickupTable.Add(new LocationData() { uuid = "cc630ad8-cb67-4a50-9e84-2c04c0cc3e16-1_A-PinBargeRunup", apid = baseid + 488 });//
            pickupTable.Add(new LocationData() { uuid = "898633d3-36c1-4f61-88b5-8cd4ca33ec2b-2_B-PinBargeArena", apid = baseid + 489 });//
            pickupTable.Add(new LocationData() { uuid = "c762c7f8-33b6-4701-9f4c-7ad7938a5d11-2_B-DarkCanyon", apid = baseid + 490 });//
            pickupTable.Add(new LocationData() { uuid = "87b99366-1eb6-4cd3-af8e-a454cc3c0172-2_B-DarkCanyon", apid = baseid + 491 });//
            pickupTable.Add(new LocationData() { uuid = "98535001-699c-4e64-a07d-74e610f04528-2_B-DarkCanyon", apid = baseid + 492 });//
            pickupTable.Add(new LocationData() { uuid = "54a8f869-ff96-45b2-93de-fb1eeeec313f-2_B-DarkCanyon", apid = baseid + 493 });//
            pickupTable.Add(new LocationData() { uuid = "81a413d0-b588-49c7-9210-86d193c7eb6c-2_B-DarkCanyon", apid = baseid + 494 });//
            pickupTable.Add(new LocationData() { uuid = "5b51f2af-33e2-4ba2-a4f9-579e56decfbb-2_B-DarkCanyon", apid = baseid + 495 });//
            pickupTable.Add(new LocationData() { uuid = "bf3b5664-5805-403b-af6f-98f4557c532b-2_B-DarkCanyon", apid = baseid + 496 });//
            pickupTable.Add(new LocationData() { uuid = "bc5df0c3-4f73-41a8-b348-eff29a1bcc15-2_B-DarkCanyon", apid = baseid + 497 });//
            pickupTable.Add(new LocationData() { uuid = "bcfe64d6-10e9-4075-aa14-a694a798c76b-2_B-DarkCanyon", apid = baseid + 498 });//
            pickupTable.Add(new LocationData() { uuid = "163d1846-c7fb-4af8-b5b6-9100a5572f57-2_B-DarkCanyon", apid = baseid + 499 });//
            pickupTable.Add(new LocationData() { uuid = "f1ae9c92-60d7-40f1-8018-5041e0943cd7-2_B-DarkCanyon", apid = baseid + 500 });//
            pickupTable.Add(new LocationData() { uuid = "619e7f2e-dacb-459a-b942-3b3f9d3981cf-2_B-DarkCanyon", apid = baseid + 501 });//
            pickupTable.Add(new LocationData() { uuid = "434f4643-5f2e-4e5a-810d-280840846d29-2_B-DarkCanyon", apid = baseid + 502 });//
            pickupTable.Add(new LocationData() { uuid = "9de638f7-ee53-4b14-8b12-8880c9a11631-2_B-DarkCanyon", apid = baseid + 503 });//
            pickupTable.Add(new LocationData() { uuid = "8ed2ebbb-d383-43ab-8530-f3c1cd714763-2_B-DarkCanyon", apid = baseid + 504 });//
            pickupTable.Add(new LocationData() { uuid = "baba3ba3-395a-4547-b060-ddaf0e9fe966-2_B-DarkCanyon", apid = baseid + 505 });//
            pickupTable.Add(new LocationData() { uuid = "28d4bed3-bba1-43f5-a620-83c4dd4e14f1-2_B-DarkCanyon", apid = baseid + 506 });//
            pickupTable.Add(new LocationData() { uuid = "1b7cb531-3a0a-4957-ab59-da5c9f1da705-2_B-DarkCanyon", apid = baseid + 507 });//
            pickupTable.Add(new LocationData() { uuid = "aaa567ea-297b-47ad-8402-8c0d57d10974-2_B-DarkCanyon", apid = baseid + 508 });//
            pickupTable.Add(new LocationData() { uuid = "fa136387-6988-46e4-9915-fc0a6f11ec6a-2_B-DarkCanyon", apid = baseid + 509 });//
            pickupTable.Add(new LocationData() { uuid = "ecc7eed3-27e2-4168-8f6e-6733375b7c99-2_B-DarkCanyon", apid = baseid + 510 });//
            pickupTable.Add(new LocationData() { uuid = "b30c36b2-9a59-4dca-9d9b-f851f91070b8-2_B-DarkCanyon", apid = baseid + 511 });//
            pickupTable.Add(new LocationData() { uuid = "75e64603-4664-467d-b2d4-579448396fd4-2_B-DarkCanyon", apid = baseid + 512 });//
            pickupTable.Add(new LocationData() { uuid = "d8080e8e-5cbc-4baf-acad-7e71091ccf3e-2_B-DarkCanyon", apid = baseid + 513 });//
            pickupTable.Add(new LocationData() { uuid = "8b5392a6-4839-4960-858f-d5bddbb23381-2_B-DarkCanyon", apid = baseid + 514 });//
            pickupTable.Add(new LocationData() { uuid = "b2d86f0b-884e-4793-b9bd-39923f22205c-2_B-DarkCanyon", apid = baseid + 515 });//
            pickupTable.Add(new LocationData() { uuid = "501f2b1a-2dbe-4082-b537-ccf1f06c9d80-2_B-DarkCanyon", apid = baseid + 516 });//
            pickupTable.Add(new LocationData() { uuid = "e9fe66b6-b7db-492a-9b64-66f19bfda0c1-2_B-DarkCanyon", apid = baseid + 517 });//
            pickupTable.Add(new LocationData() { uuid = "e1d2aa11-415c-46c1-8a81-0ab60bb39847-2_C-HermitCave", apid = baseid + 518 });//
            pickupTable.Add(new LocationData() { uuid = "41d32998-d9d9-4b64-bedd-23889b575ec4-2_C-HermitCave", apid = baseid + 519 });//
            pickupTable.Add(new LocationData() { uuid = "f6f1900f-540b-4068-8c1a-7ca8e3f820d9-2_D-SilentFlats", apid = baseid + 520 });//
            pickupTable.Add(new LocationData() { uuid = "ae92760b-940b-4925-a369-4112351781c7-2_D-SilentFlats", apid = baseid + 521 });//
            pickupTable.Add(new LocationData() { uuid = "f045426f-2bb7-43d7-8ae9-45d5bd73b3a2-2_D-SilentFlats", apid = baseid + 522 });//
            pickupTable.Add(new LocationData() { uuid = "ce72bf4f-67a6-4ce9-b094-08abfd7fb973-2_D-SilentFlats", apid = baseid + 523 });//
            pickupTable.Add(new LocationData() { uuid = "3c128651-7160-4ff8-9aaa-20f4fe9fa6ef-2_D-SilentFlats", apid = baseid + 524 });//
            pickupTable.Add(new LocationData() { uuid = "a348d1db-7fb6-460a-b327-a09b3effacd9-2_D-SilentFlats", apid = baseid + 525 });//
            pickupTable.Add(new LocationData() { uuid = "a36e3443-c030-4021-8022-df892e62328b-2_D-SilentFlats", apid = baseid + 526 });//
            pickupTable.Add(new LocationData() { uuid = "b3cc9d26-0b24-438b-ba96-084ba135230a-2_D-SilentFlats", apid = baseid + 527 });//
            pickupTable.Add(new LocationData() { uuid = "42a14e36-7015-455e-a2b9-95fc02893e1f-2_D-SilentFlats", apid = baseid + 528 });//
            pickupTable.Add(new LocationData() { uuid = "09879ffa-ec2f-4723-b157-6efbef8f364b-2_D-SilentFlats", apid = baseid + 529 });//
            pickupTable.Add(new LocationData() { uuid = "689ac44f-d0f2-4c33-9aad-686823303bff-2_D-SilentFlats", apid = baseid + 530 });//
            pickupTable.Add(new LocationData() { uuid = "82554ff8-c8f1-42b3-8018-f84f5ec94c55-2_D-SilentFlats", apid = baseid + 531 });//
            pickupTable.Add(new LocationData() { uuid = "163cdd5b-be02-42e9-852c-0b657db299d8-2_D-SilentFlats", apid = baseid + 532 });//
            pickupTable.Add(new LocationData() { uuid = "fda1ad5d-5abb-4351-b682-8e91036ccddd-2_D-SilentFlats", apid = baseid + 533 });//
            pickupTable.Add(new LocationData() { uuid = "214419e5-f322-4971-adaa-6f68e62ea0a9-2_D-SilentFlats", apid = baseid + 534 });//
            pickupTable.Add(new LocationData() { uuid = "2cb43e11-10dd-49af-8c42-f43ae2742bce-2_C-HermitCave", apid = baseid + 535 });//
            pickupTable.Add(new LocationData() { uuid = "dac6390a-11b4-411e-9c6e-a406ef5ae503-2_D-SilentFlats", apid = baseid + 536 });//
            pickupTable.Add(new LocationData() { uuid = "0fd36d2d-dbac-4284-b471-918f407d09aa-2_D-SilentFlats", apid = baseid + 537 });//
            pickupTable.Add(new LocationData() { uuid = "bb24e13b-1c43-4def-8d89-a997fb53772e-2_A-BleachedCopse", apid = baseid + 538 });//
            pickupTable.Add(new LocationData() { uuid = "47f18391-b63c-402e-8787-3cebb4c72224-2_A-BleachedCopse", apid = baseid + 539 });//
            pickupTable.Add(new LocationData() { uuid = "9248df82-ada8-4cd0-a839-3dcfa0443400-2_A-BleachedCopse", apid = baseid + 540 });//
            pickupTable.Add(new LocationData() { uuid = "f1d07afa-7684-4c41-ba1b-e3563cee1302-2_A-BleachedCopse", apid = baseid + 541 });//
            pickupTable.Add(new LocationData() { uuid = "14d445d2-f997-458d-a285-123bf009c223-2_A-BleachedCopse", apid = baseid + 542 });//
            pickupTable.Add(new LocationData() { uuid = "db6e270b-224f-4bd9-9190-f27c522df224-2_A-BleachedCopse", apid = baseid + 543 });//
            pickupTable.Add(new LocationData() { uuid = "5f0809f4-5a0f-4b4d-be76-1386bc665a16-2_A-BleachedCopse", apid = baseid + 544 });//
            pickupTable.Add(new LocationData() { uuid = "19a53c5e-497f-4e5e-a947-6a065747c92a-2_A-BleachedCopse", apid = baseid + 545 });//
            pickupTable.Add(new LocationData() { uuid = "9fea96c3-97f8-4670-a5c5-fd2bf05aed14-2_A-BleachedCopse", apid = baseid + 546 });//
            pickupTable.Add(new LocationData() { uuid = "8af465e9-b1df-49fe-a7fc-cfe9eede0e93-2_A-BleachedCopse", apid = baseid + 547 });//
            pickupTable.Add(new LocationData() { uuid = "9d251937-c0f2-44b4-8247-a80ca0335d8d-2_A-BleachedCopse", apid = baseid + 548 });//
            pickupTable.Add(new LocationData() { uuid = "0cde8fb4-1c06-4c31-ba70-dfd1906404a0-2_A-BleachedCopse", apid = baseid + 549 });//
            pickupTable.Add(new LocationData() { uuid = "c9b315b7-28de-4efb-9d0c-608e9629a9a7-2_A-BleachedCopse", apid = baseid + 550 });//
            pickupTable.Add(new LocationData() { uuid = "d5fed7e0-cd33-4a81-9688-1e7ca948139f-2_A-BleachedCopse", apid = baseid + 551 });//
            pickupTable.Add(new LocationData() { uuid = "2e322c37-6011-41eb-ac2b-7fb67d0d85b1-2_A-BleachedCopse", apid = baseid + 552 });//
            pickupTable.Add(new LocationData() { uuid = "584108d6-147a-4c89-9b42-53eb66c7d779-2_A-BleachedCopse", apid = baseid + 553 });//
            pickupTable.Add(new LocationData() { uuid = "765f4cdd-d7a5-4954-be69-99ed7c68913b-2_A-BleachedCopse", apid = baseid + 554 });//
            pickupTable.Add(new LocationData() { uuid = "a612eb7f-28ff-47a7-9576-8e15f3404802-2_A-BleachedCopse", apid = baseid + 555 });//
            pickupTable.Add(new LocationData() { uuid = "8a4445f5-aaa4-4f73-9aec-adffed944b03-2_A-BleachedCopse", apid = baseid + 556 });//
            pickupTable.Add(new LocationData() { uuid = "6a75e525-e399-482b-b21f-42ae8ae63a17-2_A-BleachedCopse", apid = baseid + 557 });//
            pickupTable.Add(new LocationData() { uuid = "216137c4-ed27-4bc7-89fd-0818f0b5ce89-2_B-GrandCourtyard", apid = baseid + 558 });//
            pickupTable.Add(new LocationData() { uuid = "e9e60f3d-0c0f-4481-ad22-827a32d9987e-2_B-GrandCourtyard", apid = baseid + 559 });//
            pickupTable.Add(new LocationData() { uuid = "674cd75e-e914-4d6d-9bc3-9ead6219f833-2_B-GrandCourtyard", apid = baseid + 560 });//
            pickupTable.Add(new LocationData() { uuid = "6df4f101-e0f2-4147-94e8-54235e8b803f-2_B-GrandCourtyard", apid = baseid + 561 });//
            pickupTable.Add(new LocationData() { uuid = "aa69aafa-0b32-451d-9e5c-b8b0c8be14ff-2_B-GrandCourtyard", apid = baseid + 562 });//
            pickupTable.Add(new LocationData() { uuid = "9cd5413b-91c2-421a-92de-8a703922f888-2_B-GrandCourtyard", apid = baseid + 563 });//
            pickupTable.Add(new LocationData() { uuid = "f94fbed4-a90c-46e8-a6a3-343f81728eba-2_B-GrandCourtyard", apid = baseid + 564 });//
            pickupTable.Add(new LocationData() { uuid = "20dffb0a-e6b1-4c00-8f25-d66aefedcc22-2_B-GrandCourtyard", apid = baseid + 565 });//
            pickupTable.Add(new LocationData() { uuid = "229063ed-5a23-4126-8b6e-ef7ade57e7b8-2_B-GrandCourtyard", apid = baseid + 566 });//
            pickupTable.Add(new LocationData() { uuid = "7596158d-e76e-4bdb-8e09-7739c73eeb3c-2_B-GrandCourtyard", apid = baseid + 567 });//
            pickupTable.Add(new LocationData() { uuid = "76bb55d0-f301-42d2-8933-28613fa84d2f-2_B-GrandCourtyard", apid = baseid + 568 });//
            pickupTable.Add(new LocationData() { uuid = "9a975590-ea4b-4459-bb26-0df4a8cf6cc0-2_B-GrandCourtyard", apid = baseid + 569 });//
            pickupTable.Add(new LocationData() { uuid = "f8ef34b2-f3b7-44d6-84b6-63749adc075f-2_B-GrandCourtyard", apid = baseid + 570 });//
            pickupTable.Add(new LocationData() { uuid = "b27f8f08-2a6d-4f78-8ef5-ca01d2b968ef-2_B-GrandCourtyard", apid = baseid + 571 });//
            pickupTable.Add(new LocationData() { uuid = "081787e6-513e-4428-847d-618a5a4323dc-2_B-GrandCourtyard", apid = baseid + 572 });//
            pickupTable.Add(new LocationData() { uuid = "b2789002-2749-482b-b64e-4cbba29c6a08-2_B-GrandCourtyard", apid = baseid + 573 });//
            pickupTable.Add(new LocationData() { uuid = "6b6e67f0-0d83-4a15-b8b7-30c7627bd0aa-2_B-GrandCourtyard", apid = baseid + 574 });//
            pickupTable.Add(new LocationData() { uuid = "daa4bd21-8cec-4f2a-b259-35635c01c2f4-2_B-GrandCourtyard", apid = baseid + 575 });//
            pickupTable.Add(new LocationData() { uuid = "1705f88a-fcd1-4229-928f-61f0e4aca852-2_B-GrandCourtyard", apid = baseid + 576 });//
            pickupTable.Add(new LocationData() { uuid = "c1520c0d-eed7-4798-a341-f60dc1b99bf3-2_B-GrandCourtyard", apid = baseid + 577 });//
            pickupTable.Add(new LocationData() { uuid = "66972460-b506-45ca-86bb-c22ecf939ad2-2_B-GrandCourtyard", apid = baseid + 578 });//
            pickupTable.Add(new LocationData() { uuid = "5988e9dd-dc40-4f00-af63-66da9db0e7b4-2_B-GrandCourtyard", apid = baseid + 579 });//
            pickupTable.Add(new LocationData() { uuid = "a2b25b1c-e993-4087-b8ae-fb6a35c6dd0e-2_B-GrandCourtyard", apid = baseid + 580 });//
            pickupTable.Add(new LocationData() { uuid = "e8ce25b8-c39a-4892-8573-e390fffc68d9-2_B-GrandCourtyard", apid = baseid + 581 });//
            pickupTable.Add(new LocationData() { uuid = "3e336fbb-9830-4772-a1b3-55dc039380dd-2_B-GrandCourtyard", apid = baseid + 582 });//
            pickupTable.Add(new LocationData() { uuid = "48b919ed-4365-4459-b36b-faff6e0e3b77-2_B-GrandCourtyard", apid = baseid + 583 });//
            pickupTable.Add(new LocationData() { uuid = "082d38e1-09cc-496a-9909-ffe96cc80cb8-2_B-GrandCourtyard", apid = baseid + 584 });//
            pickupTable.Add(new LocationData() { uuid = "9d3c9660-ef88-475b-a2bf-b9af0b80ea43-2_B-GrandCourtyard", apid = baseid + 585 });//
            pickupTable.Add(new LocationData() { uuid = "9e62672b-4805-4b2b-8f23-914860ea4d5b-2_B-GrandCourtyard", apid = baseid + 586 });//
            pickupTable.Add(new LocationData() { uuid = "3ab90ce5-fc9e-435e-b122-39684cc92962-2_B-GrandCourtyard", apid = baseid + 587 });//
            pickupTable.Add(new LocationData() { uuid = "b7757de2-ce70-474e-a332-cef609bdf42f-2_B-GrandCourtyard", apid = baseid + 588 });//
            pickupTable.Add(new LocationData() { uuid = "e815e31b-ec45-4f8b-8a67-fd469cb69dff-2_B-GrandCourtyard", apid = baseid + 589 });//
            pickupTable.Add(new LocationData() { uuid = "9ef9ab02-0e47-455b-9b29-4e1cadcd2770-2_B-GrandCourtyard", apid = baseid + 590 });//
            pickupTable.Add(new LocationData() { uuid = "824a5720-5565-4804-9838-a674ebd3b1f1-2_B-GrandCourtyard", apid = baseid + 591 });//
            pickupTable.Add(new LocationData() { uuid = "4babc398-17b9-4197-958e-c6bebc12a6c6-2_B-GrandCourtyard", apid = baseid + 592 });//
            pickupTable.Add(new LocationData() { uuid = "c0807176-a8e5-4d89-bded-872bfb4660aa-2_B-GrandCourtyard", apid = baseid + 593 });//
            pickupTable.Add(new LocationData() { uuid = "467682ed-a7cd-4943-a524-07d75ef9fa0e-2_B-GrandCourtyard", apid = baseid + 594 });//
            pickupTable.Add(new LocationData() { uuid = "56826edb-20cf-4f5a-bd85-4c4e71ca3fee-2_B-GrandCourtyard", apid = baseid + 595 });//
            pickupTable.Add(new LocationData() { uuid = "1cd3a773-d93a-43be-8b14-cb94ab1ae794-2_B-GrandCourtyard", apid = baseid + 596 });//
            pickupTable.Add(new LocationData() { uuid = "6ab4d5f2-16d8-4212-b86b-4592e30ebb5e-2_B-GrandCourtyard", apid = baseid + 597 });//
            pickupTable.Add(new LocationData() { uuid = "0aec05f7-d626-4371-9faf-d24e89dc997e-2_B-GrandCourtyard", apid = baseid + 598 });//
            pickupTable.Add(new LocationData() { uuid = "80bee4b6-92f0-4180-a7c1-090fe510d32d-2_B-GrandCourtyard", apid = baseid + 599 });//
            pickupTable.Add(new LocationData() { uuid = "a9ac0a98-889e-4138-a62b-0d59f63af118-2_B-GrandCourtyard", apid = baseid + 600 });//
            pickupTable.Add(new LocationData() { uuid = "df538a5c-9108-4210-8b9d-89e777116ec4-2_B-GrandCourtyard", apid = baseid + 601 });//
            pickupTable.Add(new LocationData() { uuid = "b373a831-6880-4823-9ba4-c69a72c6e01f-2_B-GrandCourtyard", apid = baseid + 602 });//
            pickupTable.Add(new LocationData() { uuid = "d2452d82-3625-483d-a93e-401ce4745bbb-2_B-GrandCourtyard", apid = baseid + 603 });//
            pickupTable.Add(new LocationData() { uuid = "21387127-4e04-4017-bb58-3aafb9537876-2_B-GrandCourtyard", apid = baseid + 604 });//


        }

        public static long FindPickupAPID(Item item)
        {
            Debug.Log("Finding APID");
            //Get UUID
            FieldInfo field = AccessTools.Field(typeof(Item), "save");
            SaveStateKillableEntity save = new SaveStateKillableEntity();
            save = (SaveStateKillableEntity)field.GetValue(item);

            string UUID = save.UUID;
            long baseid = 483021700;

            //If there isnt a match return -1
            if (!pickupTable.Exists((x => x.uuid.Contains(UUID))))
            {
                Debug.Log("UUID not found");
                return baseid - 1; 
            }
            //Otherwise return apid
            else
            {
                Debug.Log("UUID found");
                return pickupTable.Find(x => x.uuid.Contains(UUID)).apid;
            }
        }

        public static long FindSkillAPID(String skillname)
        {
            Debug.Log("Finding Skill APID");
            //Get UUID
            /*FieldInfo field = AccessTools.Field(typeof(Item), "save");
            SaveStateKillableEntity save = new SaveStateKillableEntity();
            save = (SaveStateKillableEntity)field.GetValue(item);*/

            //string UUID = save.UUID;
            long baseid = 483021700;

            //If there isnt a match return -1
            if (!pickupTable.Exists((x => x.skillName.Contains(skillname))))
            {
                Debug.Log("UUID not found");
                return baseid - 1;
            }
            //Otherwise return apid
            else
            {
                Debug.Log("UUID found");
                return pickupTable.Find(x => x.skillName.Contains(skillname)).apid;
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
                case "Boss_DiseasedLichenthrope": return baseid + 49; //lycanthrope
                case "Boss_BruiserGrove": return baseid + 50; //carbonara_connessuer
                case "Boss_HeikeaIntimidationCrab": return baseid + 51; //heikea
                case "Boss_GrovekeeperTopoda": return baseid + 52; //topoda
                case "Boss_TheConsortium": return baseid + 53; //consortium
                case "Boss_ScuttleportBruiser": return baseid + 54; //sludge_steamroller
                case "Boss_TwinPistolShrimp": return baseid + 55; //ceviche_sisters
                case "Boss_VoltaiTheAccumulator": return baseid + 56; //voltai
                case "Boss_Roland": return baseid + 57; //roland
                case "BOSS_MOONHERMIT": return baseid + 58; //petroch
                case "BOSS_INKERTON": return baseid + 59; //inkerton
                case "BOSS_MOLTEDKING": return baseid + 60; //camtscha
                case "BOSS_PRAYADUBIA2": return baseid + 61; //praya_dubia
                case "BOSS_FIRTH_2": return baseid + 62; //firth
                default: return baseid -1;
            }
        }
    }
}
