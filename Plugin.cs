﻿using BepInEx;
using Archipelago.MultiClient.Net;
using HarmonyLib;
using System;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using System.Collections.Concurrent;
using Archipelago.MultiClient.Net.Packets;

namespace ACTAP
{

    [BepInPlugin("ACTPlugins.Automagic.Archipelago", "AP Randomizer", "0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static Player _player;
        public static LoadingScreen _loadingScreen;
        float alphaAmount = 0f;
        bool showFadingLabel = false;
        UnityEngine.Color originalColor;
        string fadingLabelContent = "";
        string apAdress = "archipelago.gg";
        string apPort = "";
        string apPassword = "";
        string apSlot = "";
        string xCoord = "";
        string yCoord = "";
        string zCoord = "";
        public static bool removePickups = false;
        public static bool debugMode = false;
        public static float microplasticMult = 1.0f;

 

        private Rect windowRect = new Rect(0, 0, 200, 150);
        private UnityEngine.Color backgroundColor = UnityEngine.Color.grey;
        private static bool showMenu = true;
        //public static Archipelago instance { get; set; }
        public static ArchipelagoConnection connection;

        private void Awake()
        {
            //Generate Location Data
            LocationDataTable.GenerateTable();

            //Debug on Scene Load
            SceneManager.sceneLoaded += DebugLogger;



            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            var harmony = new Harmony("com.example.patch");
            harmony.PatchAll();
        }

        private void Start()
        {
            connection = new ArchipelagoConnection();
        }

        public static ArchipelagoConnection GetConnection()
        {
            return connection;
        }

        public void CheckLocation(long locID)
        {
            connection.ActivateCheck(locID);
        }

        public void Update()
        {
            if (!connection.connected)
            {
                return;
            }
            if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "Pretitle" || _player == null || _loadingScreen.IsLoading() == true)
            {
                return;
            }
            if (connection.checkItemsReceived != null)
            {
                connection.checkItemsReceived.MoveNext();
            }


            if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "Loading" && _player != null /*&& SpeedrunData.gameComplete == 0*/)
            {
                //Debug.Log("Try Item");
                if (connection.incomingItemHandler != null)
                {
                    connection.incomingItemHandler.MoveNext();
                }

                if (connection.outgoingItemHandler != null)
                {
                    connection.outgoingItemHandler.MoveNext();
                }

            }

            /*if (SpeedrunData.gameComplete != 0 && !sentCompletion)
            {
                sentCompletion = true;
                SendCompletion();
            }*/


        }

        MethodInfo method = AccessTools.Method(typeof(Item), "ObtainItem");
        public void DebugLogger(Scene s, LoadSceneMode m)
        {
            Debug.Log(s.name);

            //Load AP 
            var texFile = File.ReadAllBytes("BepInEx/plugins/Archipelago/assets/AP.png");
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(texFile);
            Sprite apSprite = Sprite.Create(tex, new Rect(0, 0, 2034, 2112), new Vector2(0, 0));
            ItemSwapData.setAPSprite(apSprite);
        }

        [HarmonyPatch(typeof(LoadingScreen), "Awake")]
        class LoadPatch
        { 
            [HarmonyPrefix]
            public static void loadScreenPatch(LoadingScreen __instance)
            {
                _loadingScreen = __instance;
            }
        }


        //DEBUG CONTROLS
        [HarmonyPatch(typeof(Player), "Update")]
        class PlayerPatch
        {
            [HarmonyPrefix]
            public static void updatePatch()
            {
                _player = Player.singlePlayer;
                if (Input.GetKeyDown(KeyCode.F3))
                {
                    Debug.Log("F3 Pressed");
                    //CrabFile.current.SetInt("LocationChecked-483021702",0);
                }

                if (Input.GetKeyDown(KeyCode.F4))
                {
                    Debug.Log("F4 Pressed");
                    //CrabFile.current.progressData[ProgressData.ShallowsProgress.PearlPickedUp].unlocked = true;
                    //GameManager.events.CheckProgress();
                }

                if (Input.GetKeyDown(KeyCode.F5))
                {
                    Debug.Log("F5 Pressed");
                }
                if (Input.GetKeyDown(KeyCode.Insert))
                { 
                    showMenu = !showMenu;
                    Debug.Log("Toggle Menu");
                }
            }
        }

        public class ArchipelagoConnection
        {
            public ArchipelagoSession session;
            public IEnumerator<bool> incomingItemHandler;
            public IEnumerator<bool> outgoingItemHandler;
            public IEnumerator<bool> checkItemsReceived;

            public bool sentCompletion = false;
            public bool sentRelease = false;
            public bool sentCollect = false;

            public Dictionary<string, object> slotData;
            DeathLinkService deathLinkService;
            public int ItemIndex = 0;
            private ConcurrentQueue<(ItemInfo NetworkItem, int index)> incomingItems;
            private ConcurrentQueue<ItemInfo> outgoingItems;
            public bool connected
            {
                get { return session != null ? session.Socket.Connected : false; }
            }

            

            public void TryConnect(string adress, int port, string pass, string player)
            {
                Debug.Log("TryConnect");
                if (connected)
                {
                    Debug.Log("Returning");
                    return;
                }

                TryDisconnect();

                LoginResult result;

                if (session == null)
                {
                    try
                    {
                        session = ArchipelagoSessionFactory.CreateSession(adress, port);
                        Debug.Log("Session at " + session.ToString());
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Failed to create archipelago session!");
                    }
                }

                incomingItemHandler = IncomingItemHandler();
                outgoingItemHandler = OutgoingItemHandler();
                checkItemsReceived = CheckItemsReceived();
                incomingItems = new ConcurrentQueue<(ItemInfo NetworkItem, int index)>();
                outgoingItems = new ConcurrentQueue<ItemInfo>();


                try
                {
                    result = session.TryConnectAndLogin("Another Crabs Treasure", player, ItemsHandlingFlags.AllItems, requestSlotData: true, password: pass);
                }
                catch (Exception e)
                {
                    result = new LoginFailure(e.GetBaseException().Message);
                }
                if (result is LoginSuccessful LoginSuccess)
                {

                    slotData = LoginSuccess.SlotData;

                    Debug.Log("Successfully connected to Archipelago Multiworld server!");

                    deathLinkService = session.CreateDeathLinkService();

                    deathLinkService.OnDeathLinkReceived += (deathLinkObject) => {
                        if (SceneManager.GetActiveScene().name != "TitleScreen")
                        {
                            Debug.Log("Death link received");
                            //PlayerCharacterPatches.DeathLinkMessage = deathLinkObject.Cause == null ? $"\"{deathLinkObject.Source} died and took you with them.\"" : $"\"{deathLinkObject.Cause}\"";
                            //PlayerCharacterPatches.DiedToDeathLink = true;
                        }
                    };

                    
                    /*if (TunicRandomizer.Settings.DeathLinkEnabled)
                    {
                        deathLinkService.EnableDeathLink();
                    }*/

                    //SetupDataStorage();

                }
                else
                {
                    LoginFailure loginFailure = (LoginFailure)result;
                    Debug.Log("Error connecting to Archipelago:");
                    //Notifications.Show($"\"Failed to connect to Archipelago!\"", $"\"Check your settings and/or log output.\"");
                    foreach (string Error in loginFailure.Errors)
                    {
                        Debug.Log(Error);
                    }
                    foreach (ConnectionRefusedError Error in loginFailure.ErrorCodes)
                    {
                        Debug.Log(Error.ToString());
                    }
                    TryDisconnect();
                }
            }


            public void TryDisconnect()
            {
                try
                {
                    if (session != null)
                    {
                        session.Socket.DisconnectAsync();
                        session = null;
                    }

                    //incomingItemHandler = null;
                    //outgoingItemHandler = null;
                    //checkItemsReceived = null;
                    incomingItems = new ConcurrentQueue<(ItemInfo NetworkItem, int ItemIndex)>();
                    outgoingItems = new ConcurrentQueue<ItemInfo>();
                    deathLinkService = null;
                    slotData = null;
                    ItemIndex = 0;
                    //Locations.CheckedLocations.Clear();
                    //ItemLookup.ItemList.Clear();

                    Debug.Log("Disconnected from Archipelago");
                }
                catch (Exception e)
                {
                    Debug.Log("Encountered an error disconnecting from Archipelago!");
                }
            }

            public void ActivateCheck(long locationID)
            {
                Debug.Log("Checked Location " + locationID);
                session.Locations.CompleteLocationChecks(locationID);
                

                //string gameObjectID = session.Locations.locatio
                CrabFile.current.SetInt("LocationChecked-" + locationID, 1);

                session.Locations.ScoutLocationsAsync(locationID)
                    .ContinueWith(locationInfoPacket =>
                    {
                        foreach (ItemInfo itemInfo in locationInfoPacket.Result.Values)
                        {
                            outgoingItems.Enqueue(itemInfo);
                        }
                    });
            }

            public void SyncLocations()
            {
                int serverLocCount = session.Locations.AllLocationsChecked.Count;
                int clientLocCount = CrabFile.current.GetInt("archipelago items sent to other players");
                
                if (serverLocCount != clientLocCount)
                {
                    Debug.Log("Locations Unsynced, resyncing...");
                    string[] clientLocs = CrabFile.current.GetString("Locations Obtained").Split(',');
                    Debug.Log("Server: " + serverLocCount + "\nClient Count: " + clientLocCount + "\nClient Raw: " + clientLocs.Length);

                    foreach (string location in clientLocs)
                    {
                        ActivateCheck(long.Parse(location));
                    }
                }
            }

            public void ScoutLocation(long id)
            {
                if (session != null)
                {
                    session.Locations.ScoutLocationsAsync(id);
                }
            }

            public string GetLocationName(long id)
            {
                string locationName = session.Locations.GetLocationNameFromId(id);
                return locationName;
            }

            public long GetLocationID(string name)
            {
                long id = session.Locations.GetLocationIdFromName("Another Crabs Treasure", name);
                return id;
            }

            public string GetItemName(long id)
            {
                string name = session.Items.GetItemName(id) ?? $"Item: {id}";
                return name;
            }

            private IEnumerator<bool> CheckItemsReceived()
            {
                while (connected)
                {
                    if (session.Items.AllItemsReceived.Count > ItemIndex)
                    {
                        //NetworkItem Item = session.Items.AllItemsReceived[ItemIndex];
                        ItemInfo Item = session.Items.AllItemsReceived[ItemIndex];
                        string ItemReceivedName = Item.ItemName;
                        Debug.Log("Placing item " + ItemReceivedName + " with index " + ItemIndex + " in queue.");
                        incomingItems.Enqueue((Item, ItemIndex));
                        ItemIndex++;
                        yield return true;
                    }
                    else
                    {
                        yield return true;
                        continue;
                    }
                }
            }
            private IEnumerator<bool> OutgoingItemHandler()
            {
                while (connected)
                {
                    if (!outgoingItems.TryDequeue(out var networkItem))
                    {
                        yield return true;
                        continue;
                    }

                    var itemName = networkItem.ItemName;
                    var location = networkItem.LocationName;
                    var locID = networkItem.LocationId;
                    var receiver = session.Players.GetPlayerName(networkItem.Player);

                    Debug.Log("Sent " + itemName + " at " + location + " for " + receiver);

                    if (networkItem.Player != session.ConnectionInfo.Slot)
                    {
                        CrabFile.current.SetInt("archipelago items sent to other players", CrabFile.current.GetInt("archipelago items sent to other players") + 1);
                        CrabFile.current.SetString("Locations Obtained", CrabFile.current.GetString("Locations Obtained") + locID + ",");
                        //Notifications.Show($"yoo sehnt  {(TextBuilderPatches.ItemNameToAbbreviation.ContainsKey(itemName) && Archipelago.instance.IsTunicPlayer(networkItem.Player) ? TextBuilderPatches.ItemNameToAbbreviation[itemName] : "[archipelago]")}  \"{itemName.Replace("_", " ")}\" too \"{receiver}!\"", $"hOp #A lIk it!");
                    }


                    yield return true;
                }
            }

            private IEnumerator<bool> IncomingItemHandler()
            {
                while (connected)
                {

                    if (!incomingItems.TryPeek(out var pendingItem))
                    {
                        yield return true;
                        continue;
                    }

                    var networkItem = pendingItem.NetworkItem;
                    var itemName = networkItem.ItemName;

                    var itemDisplayName = itemName + " (" + networkItem.ItemName + ") at index " + pendingItem.index;

                    if (CrabFile.current.GetInt($"randomizer processed item index {pendingItem.index}") == 1)
                    {
                        incomingItems.TryDequeue(out _);
                        //TunicRandomizer.Tracker.SetCollectedItem(itemName, false);
                        Debug.Log("Skipping item " + itemName + " at index " + pendingItem.index + " as it has already been processed.");
                        yield return true;
                        continue;
                    }

                    CrabFile.current.SetInt($"randomizer processed item index {pendingItem.index}", 1);
                    ItemSwapData.GetItem(networkItem.ItemId);
                    incomingItems.TryDequeue(out _);

                    yield return true;
                }
            }

            public void SendCompletion()
            {
                StatusUpdatePacket statusUpdatePacket = new StatusUpdatePacket();
                statusUpdatePacket.Status = ArchipelagoClientState.ClientGoal;
                session.Socket.SendPacket(statusUpdatePacket);
                //UpdateDataStorage("Reached an Ending", true);
            }

            public void Release()
            {
                if (connected && sentCompletion && !sentRelease)
                {
                    session.Socket.SendPacket(new SayPacket() { Text = "!release" });
                    sentRelease = true;
                    Debug.Log("Released remaining checks.");
                }
            }

            public void Collect()
            {
                if (connected && sentCompletion && !sentCollect)
                {
                    session.Socket.SendPacket(new SayPacket() { Text = "!collect" });
                    sentCollect = true;
                    Debug.Log("Collected remaining items.");
                }
            }

        }

        public void OnGUI()
        {
            if (showFadingLabel && alphaAmount < 1f)
            {
                alphaAmount += 0.3f * Time.deltaTime;
                GUI.color = new UnityEngine.Color(originalColor.r, originalColor.g, originalColor.b, alphaAmount);
                GUI.Label(new Rect(Screen.width / 2, 40, 200f, 50f), fadingLabelContent);
            }
            else if (alphaAmount >= 1f)
            {
                alphaAmount = 0f;
                GUI.color = originalColor;
                showFadingLabel = false;
            }

            if (showMenu && SceneManager.GetActiveScene().name == "Title")
            {
                GUI.backgroundColor = backgroundColor;
                windowRect = new Rect(0, 0, 200, 150);
                windowRect = GUI.Window(0, windowRect, APConnectMenu, "Archipelago");
            }
            else if (showMenu && debugMode && _player != null)
            {
                GUI.backgroundColor = backgroundColor;
                windowRect = new Rect(0, 0, 200, 250);
                windowRect = GUI.Window(0, windowRect, DebugMenu, "Debug");
            }
            else if (showMenu && !debugMode && connection.session != null && _player != null)
            {
                GUI.backgroundColor = backgroundColor;
                windowRect = new Rect(0, 0, 200, 100);
                windowRect = GUI.Window(0, windowRect, APClientMenu, "Archipelago");
            }
        }

        void APClientMenu(int windowID)
        {
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(80));
            GUILayout.Label("Press [Insert] to toggle menu.");

            if (GUILayout.Button("Teleport to Start"))
            {
                TeleportPanel tele = new TeleportPanel();
                MSSCollectable mss = tele.GetMss(TeleportPanel.ZoneSelection.TheShallows, 0);
                MethodInfo method = AccessTools.Method(typeof(TeleportPanel), "WarpToShellRoutine");

                StartCoroutine((IEnumerator)method.Invoke(tele, new object[] { mss } ));
                //GameManager.instance.StartCoroutine(method.Invoke(tele,));
                //tele.WarpToShellRoutine(mss)
            }



            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUI.DragWindow();
        }

        IEnumerator TeleToStartRoutine()
        {
            TeleportPanel tele = new TeleportPanel();
            GUIManager.instance.CloseAllWindows();
            GameManager.events.TriggerShelleport();
            /*if (!string.IsNullOrEmpty(TeleportPanel.queuedChatter))
            {
                DialogueManager.instance.StartChatter(TeleportPanel.queuedChatter, null);
                TeleportPanel.queuedChatter = null;
                yield return null;
                while (DialogueManager.instance.playingScene)
                {
                    yield return null;
                }
            }*/
            PlayerLocationData playerLocationData = ScriptableObject.CreateInstance<PlayerLocationData>();
            playerLocationData.SetSpawnerMSS(Level.TheShallows, 0);
            //GUIManager.instance.Load(GUIManager.instance.blackFadeLoaderIllustrated, tele.LoadWarpLocationRoutine(targetLocation), false, null, 0f);
            AreaMap.RefreshDataMap(playerLocationData);
            yield break;
        }
        void APConnectMenu(int windowID)
        {
            if (debugMode == false && SceneManager.GetActiveScene().name == "Title")
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical(GUILayout.Width(80));
                GUILayout.Label("Address");
                GUILayout.Label("Port");
                GUILayout.Label("Password");
                GUILayout.Label("Slot");

                if (!connection.connected)
                {
                    if (GUILayout.Button("Debug Mode"))
                    {
                        Debug.Log("Debug");
                        debugMode = true;
                    }
                }

                GUILayout.EndVertical();
                GUILayout.BeginVertical(GUILayout.Width(80));
                apAdress = GUILayout.TextField(apAdress);
                apPort = GUILayout.TextField(apPort);
                apPassword = GUILayout.TextField(apPassword);
                apSlot = GUILayout.TextField(apSlot);

                if (!connection.connected)
                {
                    if (GUILayout.Button("Connect"))
                    {
                        Debug.Log("Button");
                        connection.TryConnect(apAdress, Int32.Parse(apPort), apPassword, apSlot);
                    }
                }


                
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUI.DragWindow();
            }
            if (debugMode && SceneManager.GetActiveScene().name == "Title")
            {
                removePickups = GUILayout.Toggle(removePickups, "Remove Known Pickups");
            }
            
        }
        void DebugMenu(int windowID)
        {
            if (debugMode == true && _player != null)
            {
                GUILayout.BeginHorizontal(GUILayout.Height(200));
                GUILayout.BeginVertical(GUILayout.Width(160));
                GUILayout.Label("Press [Insert] to toggle menu.");
                GUILayout.Label(_player.transform.position.ToString());

                
                if (GUILayout.Button("Give Useful Items"))
                {
                    CrabFile.current.unlocks[SkillWorldUnlocks.String].unlocked = true;
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotAnyMap].unlocked = true;
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotExpiredGroveMap].unlocked = true;
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotFlotsamValeMap].unlocked = true;
                    CrabFile.current.progressData[ProgressData.NewCarciniaProgress.GotPagurusMap].unlocked = true;

                    ItemSwapData.GetItem(ItemSwapData.ItemEnum.FishingLine);
                    ItemSwapData.GetItem(ItemSwapData.ItemEnum.LurePouch);
                    ItemSwapData.GetItem(ItemSwapData.ItemEnum.BarbedHood_Bundle10);
                    ItemSwapData.GetItem(ItemSwapData.ItemEnum.BarbedHood_Bundle10);

                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.BobbitTrap);
                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.BubbleBullet);
                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.Eelectrocute);
                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.MantisPunch);
                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.RoyalWave);
                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.SnailSanctum);
                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.SpectralTentacle);
                    ItemSwapData.GetItem(ItemSwapData.AdaptationEnum.UrchinToss);

                    ItemSwapData.GetItem(ItemSwapData.StowawayEnum.RazorBlade);

                    ItemSwapData.GetItem(ItemSwapData.ItemEnum.MapPiece1);
                    
                    ItemSwapData.GetItem(ItemSwapData.ItemEnum.MapPiece2);
                    ItemSwapData.GetItem(ItemSwapData.ItemEnum.MapPiece3);

                    SkillTreeData skillTree = new SkillTreeData();
                    skillTree.SetSkill(SkillTreeUnlocks.Aggravation, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.AirDodge, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.BasicUmamiTraining, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.ChumInTheWater, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Dispatch, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.EbbAndFlow, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.ElusivePrey, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Fishing, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Housewarming, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Kintsugi, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.NakedParries, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Parries, true, false);
                    //skillTree.SetSkill(SkillTreeUnlocks.Plunge, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Riposte, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.ScrapHammer, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.SelfRepair, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Sheleport, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.ShellAbility, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Skedaddle, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.Skewer, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.UmamiTrainingA, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.UmamiTrainingB, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.UmamiTrainingC, true, false);
                    skillTree.SetSkill(SkillTreeUnlocks.WaveBreaker, true, false);

                }

                removePickups = GUILayout.Toggle(removePickups, "Remove Known Pickups");

                xCoord = GUILayout.TextField(xCoord);
                yCoord = GUILayout.TextField(yCoord);
                zCoord = GUILayout.TextField(zCoord);
                if (GUILayout.Button("Teleport Player"))
                {
                    _player.transform.position = new Vector3(float.Parse(xCoord), float.Parse(yCoord), float.Parse(zCoord));
                }



                    /*if (GUILayout.Button("Remove Known Pickups"))
                    {
                        foreach (var item in GameObject.FindObjectsOfType<Item>())
                        {
                            if (LocationSwapData.ItemPickupUUIDToAPID(item) != -1)
                            {
                                Destroy(item.gameObject);
                            }
                        }
                    }*/


                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUI.DragWindow();
            }
        }

        
    }
}
