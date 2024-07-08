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
                case "4a22c058-bbb2-467a-b204-9c1a82dc0d9c-2_A-OOGroveRadius": return baseid + 87; //breadclaw_sandsbetween_centralvista
                case "6c5631a3-d941-4990-818f-117140982384-2_A-OOGroveRadius": return baseid + 88; //barbedhook_sandsbetween_centralvista
                case "3197c269-c43d-4bff-b388-a6c3d3e4b5f3-2_A-OOGroveRadius": return baseid + 89; //phytoplankton_sandsbetween_centralvista
                case "73aec841-ee9a-411b-8919-53eeeb33ec59-2_A-OOGroveRadius": return baseid + 90; //anemone_sandsbetween_centralvista
                case "6801f8fb-6dd9-4129-a182-791a7e66d1c2-2_A-OOGroveRadius": return baseid + 91; //bloodstar_sandsbetween_playground
                case "bd3a89be-89f5-4029-ac76-c1966c70299d-2_A-OOGroveRadius": return baseid + 92; //anemone_sandsbetween_playground
                case "8778ffd7-a95d-4021-bb90-4a116de9894a-2_A-OOGroveRadius": return baseid + 93; //rustynail_sandsbetween_fence
                case "cc1a7d82-3803-474e-8f66-c149055c4cb9-2_A-OOGroveRadius": return baseid + 94; //hairclaw_sandsbetween_southernreef
                case "20b38e81-1198-4905-809a-e42ac4e4759d-2_A-OOGroveRadius": return baseid + 95; //barbedhook_sandsbetween_southernreef
                case "52821431-a6c7-458a-b54f-d42196919993-2_A-OOGroveRadius": return baseid + 96; //googlyeye_sandsbetween_pizza
                case "550401bf-30db-4deb-8267-ac73d522236d-2_A-OOGroveRadius": return baseid + 97; //tacklepouch_sandsbetween_cooler
                case "74dac1c4-2458-4cca-a280-11e643e610ce-2_A-OOGroveRadius": return baseid + 98; //barbedhook3_sandsbetween_cooler
                case "21b40d22-140c-493c-98e0-218a5ff29c02-2_A-OOGroveRadius": return baseid + 99; //barbedhook_sandsbetween_fence
                case "8d4c2499-b344-4e0f-b4e4-dc2a3e7ccf74-2_A-OOGroveRadius": return baseid + 100; //siphonophore_sandsbetween_cooler
                case "1f5bc2e0-a613-4afd-bdf6-5ed4805edf29-2_A-OOGroveRadius": return baseid + 101; //limpet_sandsbetween_anchor
                case "0bae76f4-e90b-435b-9089-356ef62ba0fa-2_A-OOGroveRadius": return baseid + 102; //chipclaw_sandsbetween_grove
                case "57559d1c-80ce-4396-afdf-7d7e6af71ff6-2_A-OOGroveRadius": return baseid + 103; //anemone_sandsbetween_chains
                case "6f1f751a-b27c-4eb0-b95c-a89d66dd242a-2_A-OOGroveRadius": return baseid + 104; //fredrick_sandsbetween_fredrick
                case "03ba104a-efe3-4294-b378-b2aeff7aa9e3-2_A-OOGroveRadius": return baseid + 105; //anothercrab_sandsbetween_chains
                case "d78747bd-2da0-4421-add8-3c5842fa0f61-2_A-OOGroveRadius": return baseid + 106; //breadclaw_sandsbetween_chains
                case "e128b8e9-0c20-426f-8f47-783043f829dc-2_A-OOGroveRadius": return baseid + 107; //clothesclaw_sandsbetween_bobbit
                case "8d9edda5-d554-4e75-8842-8239075a334a-2_A-OOGroveRadius": return baseid + 108; //hairclaw_sandsbetween_bobbit
                case "3bb06349-c151-4333-8305-ba0eb7512bc3-2_A-OOGroveRadius": return baseid + 109; //bobbit_trap_pickup
                case "a08ac116-549b-4691-8e88-79daf965dff8-2_A-OOGroveRadius": return baseid + 110; //mussel_sandsbetween_anchor
                case "7e93197a-47f1-4bdd-b7f0-5d0ecffb39f4-2_A-OOGroveRadius": return baseid + 111; //clothesclaw_sandsbetween_northwest
                case "cd2f3731-b9f4-4f88-abd2-44d142eca493-2_A-OOGroveRadius": return baseid + 112; //clothesclaw_sandsbetween_grove
                case "234f3d49-7fc5-4039-949f-8d95f38d7c79-2_A-OOGroveRadius": return baseid + 113; //barnacle_sandsbetween_northeast
                case "2dc13e33-724d-41a7-983b-5f3bf010ce89-2_A-OOGroveRadius": return baseid + 114; //salp_sandsbetween_northeast
                case "c192eaf1-dd16-459b-a489-376899aabb9b-2_A-OOGroveRadius": return baseid + 115; //barbedhook_sandsbetween_spire
                case "fc6e8558-9515-40a7-9857-9584a196a1ec-2_A-OOGroveRadius": return baseid + 116; //rustynailplus_sandsbetween_centralvista
                case "d19ac393-ea1d-4237-9d31-b620c3a2617d-2_A-OOGroveRadius": return baseid + 117; //seastarplus_sandsbetween_flotsam
                case "bceed85e-5560-4a77-a498-749af9f76780-2_A-OOGroveRadius": return baseid + 118; //seaslug_sandsbetween_crabrave
                case "63b93466-f406-492e-ac12-f243fe6a8165-2_A-OOGroveRadius": return baseid + 119; //whelk_sandsbetween_crabrave
                case "d985ffba-0ece-414d-8161-fa65241dd2b1-2_B-ShallowsBigSand": return baseid + 120; //chipclaw_caveofrespite_forkroomfishing
                case "d1af097b-c6c9-44a4-9ced-4a09a973c0d5-2_B-ShallowsBigSand": return baseid + 121; //mussel_caveofrespite_crabfightfishing
                case "0ac3fa03-5aab-4f12-86e4-8e9dfde8c04f-2_B-ShallowsBigSand": return baseid + 122; //clothesclaw_caveofrespite_entrancefishing
                case "9ef4b1f3-5a80-408c-962a-b3fed8c68770-2_B-ShallowsBigSand": return baseid + 123; //sanddollar_caveofrespite_pathfishing
                case "1e33dfd0-6445-47f9-b1bf-3e8be7897a79-2_B-ShallowsBigSand": return baseid + 124; //breadclaw_shallows_sunkentower
                case "41a6780e-55a2-4a0a-96a9-bb279a3b1ec3-2_B-ShallowsBigSand": return baseid + 125; //mussel_shallows_southwestcastlefish
                case "0f64d031-4163-4979-9497-116cac06e234-2_B-ShallowsBigSand": return baseid + 126; //breadclaw_shallows_slacktidesouthfish
                case "daaa6ab6-8aac-4713-942f-f322e918dd55-2_B-ShallowsBigSand": return baseid + 127; //anemone_shallows_southwestcastlehiddenfishing
                case "728b3da1-5b4b-4a7f-885c-b5ed9abfadbc-2_B-ShallowsBigSand": return baseid + 128; //razorblade_shallows_slacktidebottlefishing
                case "b29c140a-08c2-44c6-81c3-5f91e5621e47-2_B-ShallowsBigSand": return baseid + 129; //anemone_shallows_umbrellafishing
                case "0ad90b46-ca8b-46ed-9cc6-f72ffcdb9d33-2_B-ShallowsBigSand": return baseid + 130; //breadclaw_slacktide_training
                case "59e2ec5a-b650-43fa-879d-3af1569fb640-2_B-ShallowsBigSand": return baseid + 131; //breadclaw_slacktide_roofhiddenfish
                case "4ebb3072-53e8-4cff-9672-5f82f926035e-2_A-OOGroveRadius": return baseid + 132; //barbedhook_sandsbetween_southcentral
                case "956ad9a9-5b43-4f18-a7d9-f67f21c50137-2_A-OOGroveRadius": return baseid + 133; //bloodstar_sandsbetween_southwestcentral
                case "90171fe5-d483-4905-a440-a9ab5a7bc074-2_A-OOGroveRadius": return baseid + 134; //barbedhook_sandsbetween_bobbitbase
                case "f553b94c-14ea-4852-8ae7-564b8d30b24d-2_A-OOGroveRadius": return baseid + 135; //breadclaw_sandsbetween_propanecliff
                case "f8b96a25-70ba-492b-bb87-7b51c81d6102-2_A-OOGroveRadius": return baseid + 136; //barbedhook_sandsbetween_northofridge
                case "7e4ef7d0-214a-4358-a0c3-866a869d71e0-2_A-OOGroveRadius": return baseid + 137; //hairclaw_sandsbetween_northchains
                case "9faef117-fd92-47cb-b4d8-cc2a07404a69-2_A-OOGroveRadius": return baseid + 138; //clothesclaw_sandsbetween_litterpitfall
                case "305be10e-d5a7-42e6-88f1-2cebdd524bbb-2_A-OOGroveRadius": return baseid + 139; //hairclaw_sandsbetween_southgrovepit
                case "cd2d0535-6c06-4530-bf60-afbffc2d5086-2_A-OOGroveRadius": return baseid + 140; //clown_costume_pickup
                case "a8e01dfb-5dd9-4da0-92b1-39a7b18e8d83-2_A-OOGroveRadius": return baseid + 141; //sinkerplus_sandsbetween_northridgemantis
                case "56e45097-4ffa-41b0-ac56-cdd6c93b5e88-2_A-OOGroveRadius": return baseid + 142; //limpetplus_sandsbetween_grovemantis
                case "8c7fdc4d-423b-4456-8869-56387a4d7665-2_A-OOGroveRadius": return baseid + 143; //oldworldwhorl_sandsbetween_mantispitfall
                case "859c746b-c52f-4887-aa96-bb19bd7e25dd-2_A-OOGroveRadius": return baseid + 144; //lumpsuckerplus_sandsbetween_anchor
                case "83daf909-77d3-4302-858b-6e92fe54fe74-2_A-OOGroveRadius": return baseid + 145; //stainlessrelic_sandsbetween_eelectrocute
                case "699b7323-fe71-4bfe-864f-cdd0c9186bbd-2_A-OOGroveRadius": return baseid + 146; //tacklepouch_sandsbetween_eelnorthchain
                case "0e2d558b-26a7-4213-be83-77355fe06128-2_A-OOGroveRadius": return baseid + 147; //paperclaw_sandsbetween_southeelshortcut
                case "ae4a7ac6-5eb0-4590-a3c7-5d5f236a978d-2_A-OOGroveRadius": return baseid + 148; //sinkerplus_sandsbetween_southeel
                case "7111735c-772e-4c1f-b255-183e62760ae2-2_A-OOGroveRadius": return baseid + 149; //musselplus_sandsbetween_southeel
                case "5c8133eb-ddb3-4b24-9849-f4d3f03491e0-2_A-OOGroveRadius": return baseid + 150; //hairclaw_sandsbetween_southeel
                case "a7c60b04-8f1b-4833-a6f4-d472e79748c5-2_A-OOGroveRadius": return baseid + 151; //kelpsprout_sandsbetween_southeelside
                case "771e0942-ebf4-4938-a7e7-c03ce67f9c16-2_A-OOGroveRadius": return baseid + 152; //barbedhook_sandsbetween_southeel
                case "2ccd3299-d207-4d4d-b5a5-d777c4e8aa29-2_A-OOGroveRadius": return baseid + 153; //oyster_sandsbetween_southeelashtray
                case "a53aefd5-fa6f-4675-a1b5-2f9e43f3fed0-2_A-OOGroveRadius": return baseid + 154; //barbedhook_sandsbetween_southeelshellsplitter
                case "98e4d994-92c4-4b3b-82b7-f460a49d604b-2_A-OOGroveRadius": return baseid + 155; //paperclaw_sandsbetween_trashanchor
                case "cab91803-4006-456d-95c7-6ec17d90c26a-2_A-OOGroveRadius": return baseid + 156; //hairclaw_sandsbetween_anchorfish
                case "b0bf9c58-ff96-404c-bd1b-361fb1711b64-2_A-OOGroveRadius": return baseid + 157; //chipclaw_sandsbetween_northeastchainfish
                case "17b29697-5b33-4568-a311-2dbfac60317a-2_A-OOGroveRadius": return baseid + 158; //clothesclaw_sandsbetween_propanefish
                case "da5f126f-e470-432c-b5b0-8eca9918ed77-2_A-OOGroveRadius": return baseid + 159; //mussel_sandsbetween_bobbitfish
                case "628bf319-c5f3-448b-8ec0-71a173246a09-2_A-OOGroveRadius": return baseid + 160; //clothesclaw_sandsbetween_anchorcentralfish
                case "15cf7503-de68-4475-b39c-fe687f85c448-2_A-OOGroveRadius": return baseid + 161; //barnacle_sandsbetween_bobbitfish
                case "98c3bf98-c4bd-4c67-8678-0464e7095970-2_A-OOGroveRadius": return baseid + 162; //breadclaw_sandsbetween_nailfish
                case "69ac316d-8d42-48a4-8d55-4ec4d6429c7c-2_A-OOGroveRadius": return baseid + 163; //breadclaw_sandsbetween_palletfish
                case "38edc1a4-dea8-4000-8683-6cda7f6ba3bc-2_A-OOGroveRadius": return baseid + 164; //hairclaw_sandsbetween_southeelfish
                case "01999bed-4813-4fe3-ab9b-5ed19cce90f1-2_A-OOGroveRadius": return baseid + 165; //chipclaw_sandsbetween_anchoreelhiddenfish
                case "d61a21a2-c27d-45d7-8e11-75fcd11e8b78-2_A-OOGroveRadius": return baseid + 166; //whelkplusplus_sandsbetween_southeelpeak
                case "7faef89f-c78a-4002-9454-c20bf5098229-2_A-OOGroveRadius": return baseid + 167; //salpplus_sandsbetween_groveeel
                case "3123f8ec-3548-41f5-a777-c10268e06b8b-2_A-OOGroveRadius": return baseid + 168; //usedbandage_sandsbetween_groveeel
                case "29e54e45-4248-4a3d-96bf-ddf5cf6046c7-2_A-OOGroveRadius": return baseid + 169; //bloodstar_postpag_anchorswarm
                case "e872d437-5bdb-4e70-820a-030bc21d66d5-2_A-OOGroveRadius": return baseid + 170; //stapleclaw_postpag_litter
                case "4e9d02aa-75f5-4b87-b029-6ee1e7d62f7a-2_A-OOGroveRadius": return baseid + 171; //hairclaw_postpag_chains
                case "2fa3fdd0-b3da-4c69-b579-a5d316075635-2_A-OOGroveRadius": return baseid + 172; //clothesclaw_postpag_chains
                case "a5c14863-82c3-47d0-b0e1-a67c93da96b8-2_A-OOGroveRadius": return baseid + 173; //barbedhook_postpag_nechain
                case "d2cd8afa-4bf2-490e-8542-cb92331800d6-2_A-OOGroveRadius": return baseid + 174; //barbedhook_postpag_echain
                case "b03e7050-778b-4029-ab54-f82207d724fa-2_A-OOGroveRadius": return baseid + 175; //barbedhook_postpag_echainpreserver
                case "2738d986-20cb-4a09-b6c5-7076a3918283-2_A-OOGroveRadius": return baseid + 176; //barbedhook_postpag_necentral
                case "a8513df4-5533-4a1e-8fa0-b0423757f162-2_A-OOGroveRadius": return baseid + 177; //barbedhook_postpag_nwridge
                case "f14d696d-88a4-471e-a512-b5770c3cfbad-2_A-OOGroveRadius": return baseid + 178; //barbedhook_postpag_neeelpath
                case "55fafe55-5d22-49a8-8b2b-aea149468a61-2_A-OOGroveRadius": return baseid + 179; //hairclaw_postpag_nridge
                case "c2d06d3f-65f4-46cf-b069-0b4098b425d0-2_A-OOGroveRadius": return baseid + 180; //hairclaw_postpag_ecentral
                case "9ecea6b6-f3db-48f8-a753-6644bbbf1aaf-2_A-OOGroveRadius": return baseid + 181; //barbedhook_postpag_ridgeentrance
                case "45af0c47-a8e0-498f-8345-9c9ddabbd6b3-2_A-OOGroveRadius": return baseid + 182; //chipclaw_postpag_nwpropane
                case "ecea05b3-9908-4b63-b422-d999b78c87e1-2_A-OOGroveRadius": return baseid + 183; //barbedhook_postpag_wplayground
                case "7563f2eb-e052-4107-b787-de2f67a40f2f-2_A-OOGroveRadius": return baseid + 184; //barbedhook_postpag_swcentral
                case "16dfae13-103f-45eb-9661-3e6463a2b805-2_A-OOGroveRadius": return baseid + 185; //paperclaw_postpag_schain
                case "c56be461-0e5c-406e-8af0-83021f2f2fb6-2_A-OOGroveRadius": return baseid + 186; //stainlessrelic_ridge_overlook
                case "97a24ab6-20a3-4f21-b0af-55bf243f33f0-2_A-OOGroveRadius": return baseid + 187; //barbedhook_ridge_near
                case "d56339c0-fc26-4f53-a289-2c00b6f7ab38-2_A-OOGroveRadius": return baseid + 188; //kelpsprout_ridge_eelend
                case "e2803d8a-feea-45a7-addf-3330748d9e4b-2_A-OOGroveRadius": return baseid + 189; //clothesclaw_ridge_neridge
                case "8e381b9e-ab7a-44ad-81c3-19157c97e0c6-2_A-OOGroveRadius": return baseid + 190; //oldworldwhorl_ridge_ncliff
                case "fcb1f0bb-2ebf-449c-b5fd-ebb7d515c7af-2_A-OOGroveRadius": return baseid + 191; //limpet_ridge_ncliffkelp
                case "d9ee8d70-3493-4e7b-91b3-b42278ff0ad4-2_A-OOGroveRadius": return baseid + 192; //barbedhook_ridge_overlookpath
                case "f6db9d48-5df1-4a7f-9f17-b760901954c9-2_A-OOGroveRadius": return baseid + 193; //paperclaw_ridge_nearoverlook
                case "42686eee-4b9f-4d44-a52d-569d2c814f13-2_A-OOGroveRadius": return baseid + 194; //sanddollar_ridge_broom
                case "3f8d4c85-202f-42dc-9105-434d601d1d75-2_A-OOGroveRadius": return baseid + 195; //clothesclaw_ridge_broom
                case "a59b9d02-9711-48e3-8296-e2164c79f1f2-2_A-OOGroveRadius": return baseid + 196; //musselplus_ridge_togo
                case "d1883b57-b339-4c31-826a-e76a3de0ee4b-2_A-OOGroveRadius": return baseid + 197; //breadclaw_ridge_south
                case "35aa72f2-fa50-4efc-b600-b597382ed877-2_A-OOGroveRadius": return baseid + 198; //sunlight_costume_pickup
                case "ca4c297f-e9fd-4a78-b89a-811d2394406b-2_A-OOGroveRadius": return baseid + 199; //cockle_trashbin_peak
                case "36611427-18cc-45f3-ad50-a51cc02ccb1d-2_A-OOGroveRadius": return baseid + 200; //sinker_trashbin_peak
                case "0d86fe0e-ae41-4fb2-beb2-9bb5e558429a-2_A-OOGroveRadius": return baseid + 201; //smallbattery_trashbin_mantis
                case "7935fe1e-318c-4f90-901b-fa001de6e4dc-2_A-OOGroveRadius": return baseid + 202; //googlyeye_trashbin_pineapple
                case "e2a1e3e1-8ee4-4539-ab81-98c4d6d99630-2_A-OOGroveRadius": return baseid + 203; //siphonophoreplus_ridge_pitfall
                case "1b6f2070-3ff2-4f48-b061-0a41990013e1-2_A-OOGroveRadius": return baseid + 204; //anemoneplus_ridge_eel
                case "80e66e11-8e9a-4896-9db1-f8cd4a25fefc-2_A-OOGroveRadius": return baseid + 205; //oldworldwhorl_ridge_ncliff
                case "4f737f9e-a952-490c-b54d-1f74735af9cc-2_A-OOGroveRadius": return baseid + 206; //clothesclaw_ridge_overlookfish
                case "82043889-30eb-45f4-8f87-45dd2f5d69ed-2_A-OOGroveRadius": return baseid + 207; //chipclaw_ridge_southfish
                case "d32ade52-c34e-4cb7-957c-df605339ee4c-2_A-OOGroveRadius": return baseid + 208; //cockle_ridge_eelfish
                case "190034bb-c13e-42ec-bac4-037cc828fde3-2_A-OOGroveRadius": return baseid + 209; //stapleclaw_trashbin_eelfish
                case "2bccca10-1f27-4762-9a9b-8e628586e0a4-2_A-OOGroveRadius": return baseid + 210; //barbedhook_trashbin_eelgrapple
                case "fc2cf7ed-6ff6-40bb-987a-936da0afa93b-2_A-OOGroveRadius": return baseid + 211; //bobber_ridge_broomspire
                case "dcc6b99e-1d47-481f-86ca-e14a5743f460-2_A-OOGroveRadius": return baseid + 212; //sharkegg_ridge_broomspire
                case "74aa4107-6a5a-4868-ae54-942e4421be3f-2_A-GroveForestLow": return baseid + 213; //barnacle_lowergrove_colander
                case "8fd70034-2415-4934-975b-2303d159f567-2_A-GroveForestLow": return baseid + 214; //barbedhook_lowergrove_sniper
                case "0860891c-3db6-4e51-8c0b-58b09ac8ece0-2_A-GroveForestLow": return baseid + 215; //barbedhook_lowergrove_riverledge
                case "2465aea2-9800-49e5-9e7e-d751ca2b3612-2_A-GroveForestLow": return baseid + 216; //barbedhook_lowergrove_riversand
                case "09b60c18-2172-41bb-9588-a3e764559b15-2_B-GroveForestHigh": return baseid + 217; //oldworldwhorl_lowergrove_southclam
                case "17cb1eed-8d88-4464-b33b-215c837035f6-2_A-GroveForestLow": return baseid + 218; //seastar_lowergrove_eastrock
                case "4933d4b0-a56a-4472-b9a6-4469fd335fa4-2_A-GroveForestLow": return baseid + 219; //breadclaw_lowergrove_takeout
                case "c6a56f3d-c134-41cd-83b7-4a755fa32284-2_A-GroveForestLow": return baseid + 220; //pufferquill_lowergrove_sponges
                case "35455018-8cf6-48db-ba76-4ffbca84b0a8-2_A-GroveForestLow": return baseid + 221; //barbedhook_lowergrove_lichenthrope
                case "5a106f4d-8139-44da-9cdf-8d0bd70c6bf3-2_A-GroveForestLow": return baseid + 222; //barbedhook_lowergrove_lichenthropeeast
                case "10a99891-923e-4bab-8057-b6d5781cb35d-2_A-GroveForestLow": return baseid + 223; //barbedhook_lowergrove_sodacans
                case "da04ed8f-2d99-48f9-8412-67748d983d2b-2_A-GroveForestLow": return baseid + 224; //breadclaw_lowergrove_tiretop
                case "902c2deb-1b51-4226-9fd0-1c76bea668c3-2_A-GroveForestLow": return baseid + 225; //chipclaw_lowergrove_plastic
                case "e38ef2da-daaf-4312-bf7f-2e0a97d3d67b-2_A-GroveForestLow": return baseid + 226; //clothesclaw_lowergrove_choccymilk
                case "def84515-cfc0-4124-a1e1-81f6953b0c06-2_A-GroveForestLow": return baseid + 227; //breadclaw_lowergrove_insidetire
                case "50f9f7d1-0e70-4333-b8b3-fbc46f04f3ea-2_A-GroveForestLow": return baseid + 228; //barbedhook_lowergrove_acrossriver
                case "f1d3eafe-c252-4768-abcd-7f459e734d35-2_A-GroveForestLow": return baseid + 229; //bloodstar_lowergrove_waterfall
                case "e7382e22-3232-4380-a734-bef8eb9a2a76-2_A-GroveForestLow": return baseid + 230; //barbedhook_lowergrove_afternets
                case "be378f83-9ec2-4a3b-85b2-9a7499e6d693-2_A-GroveForestLow": return baseid + 231; //breadclaw_lowergrove_smallhall
                case "faa3c9ce-87c7-4f6d-b07e-be334c3bc447-2_A-GroveForestLow": return baseid + 232; //chipclaw_lowergrove_apples
                case "25737275-a222-4a6e-af6b-0c66c197cab7-2_A-GroveForestLow": return baseid + 233; //chipclaw_lowergrove_sodacans
                case "fd557b69-ddba-4b14-bd80-f0cb0d786203-2_A-GroveForestLow": return baseid + 234; //hairclaw_lowergrove_moonshine
                case "dc13eb69-ac98-4de9-9fe0-7f16a03d4ebb-2_A-GroveForestLow": return baseid + 235; //barbedhook_lowergrove_cartledge
                case "093c4972-ff5e-4ba6-b9a8-136411e1017f-2_A-GroveForestLow": return baseid + 236; //limpet_lowergrove_sodacan
                case "a5b6f3f6-6e1c-4934-9006-26da6cee47f4-2_A-GroveForestLow": return baseid + 237; //barbedhook_lowergrove_cartledgebottle
                case "1b74ef4f-528c-484b-9a5f-abbb3434f133-2_A-GroveForestLow": return baseid + 238; //chipclaw_lowergrove_circlerock
                case "f99a120b-43c2-4e65-91fd-71cbfbf3e8f7-2_A-GroveForestLow": return baseid + 239; //clothesclaw_lowergrove_alcove
                case "8f4a1f33-e567-4c84-b34b-739353b3ba08-2_A-GroveForestLow": return baseid + 240; //hairclaw_lowergrove_sniper
                case "776881ba-f61b-4400-8cf5-f11fb11b6f94-2_B-GroveForestHigh": return baseid + 241; //barbedhook_lowergrove_paperplate
                case "12c73a88-beef-4bfb-a2d1-c159c91d8304-2_B-GroveForestHigh": return baseid + 242; //barbedhook_lowergrove_oildrum
                case "c859129f-cc29-4624-a161-fdeffa00d0ca-2_B-GroveForestHigh": return baseid + 243; //breadclaw_lowergrove_amongus
                case "b85cb09c-f1d7-4396-85cb-fde4dc845acd-2_B-GroveForestHigh": return baseid + 244; //breadclaw_lowergrove_oildrum
                case "070b84a6-7a8b-4d88-8216-f55882856713-2_A-GroveForestLow": return baseid + 245; //chipclaw_lowergrove_sniper
                case "e78f03e8-2827-45d7-8cfa-cbaa0f17cd3d-2_A-GroveForestLow": return baseid + 246; //hairclaw_lowergrove_milkurchins
                case "3789e56d-69b5-4d6c-adb1-6454b101840d-2_A-GroveForestLow": return baseid + 247; //lumpsucker_lowergrove_canopy
                case "101eff6d-e09a-471d-86fd-cdc587012c32-2_A-GroveForestLow": return baseid + 248; //barbedhook_lowergrove_canopy
                case "bc18b92b-a6c6-420c-9095-e15c34c2f921-2_B-GroveForestHigh": return baseid + 249; //breadclaw_lowergrove_oilgrapple
                case "3a1e7a2c-5111-4360-b168-cc5e84088b6a-2_B-GroveForestHigh": return baseid + 250; //barbedhook_lowergrove_drumtop
                case "8e34f6a5-5844-4767-ab15-4fbaf6c147dc-2_B-GroveForestHigh": return baseid + 251; //sharktooth_lowergrove_pizza


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
                default: return baseid -1;
            }
        }
    }
}
