using ACTAP.Utils;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using BepInEx;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ACTAP
{

    [BepInPlugin("ACTPlugins.Automagic.Archipelago", "AP Randomizer", "0.4.3")]
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
        float windowWidth = 200;

        public static bool removePickups = false;
        public static bool debugMode = false;
        public static float microplasticMult = 1.0f;

        public static bool settingsSaved = false;

        public static GameObject itemHolder;
        private Rect windowRect = new Rect(0, 0, 200, 150);
        private UnityEngine.Color backgroundColor = UnityEngine.Color.grey;
        private static bool showMenu = true;
        public static Dictionary<string,Dictionary<string,int>> shellData =new Dictionary<string, Dictionary<string, int>>();
        public static ArchipelagoConnection connection;

        //Map Utils
        public static bool fromPretitle = true;
        //public static bool DEBUG = true;
        //public static ManualLogSource logSource;
        public static List<Enemy> crystalEnemies;
        public static List<Item> items;
        public static List<GameObject> mapMarkers = new();

        public static bool RenderWorldMarkers = false;
        public static bool RenderWorldMarkersTemp = false;
        public static bool RenderMapMarkers = false;


        private void Awake()
        {
            //Generate Data Tables
            LocationDataTable.GenerateTable();
            ShellData.GenerateTable();

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
            //Map Stuff
            crystalEnemies = FindObjectsOfType<Enemy>(true).ToList();
            items = FindObjectsOfType<Item>(true).ToList();

            //Handle AP Items
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
        }

        MethodInfo method = AccessTools.Method(typeof(Item), "ObtainItem");
        public void DebugLogger(Scene s, LoadSceneMode m)
        {
            Debug.Log(s.name);

            //Load AP 
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Texture2D texFile = LoadTextureFromDLL("AP.png"); //File.ReadAllBytes("BepInEx/plugins/Archipelago/assets/AP.png");

            Debug.Log("Resources in DLL");
            string[] resNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string resName in resNames)
            { 
                Debug.Log(resName); 
            }
            //Texture2D tex = texFile;
            //tex.LoadImage(texFile);
            Sprite apSprite = Sprite.Create(texFile, new Rect(0, 0, 2034, 2112), new Vector2(0, 0));
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
                if (debugMode)
                {
                    if (Input.GetKeyDown(KeyCode.F1))
                    {
                        Debug.Log("F1 Pressed");
                        DebugSettings.settings.debugBuildSettings.debugMode = true;
                    }

                    if (Input.GetKeyDown(KeyCode.F2))
                    {
                        Debug.Log(Player.singlePlayer.healthyEggCount);
                        Player.singlePlayer.ResetDrink();
                    }


                    if (Input.GetKeyDown(KeyCode.F3))
                    {
                        Debug.Log("F3 Pressed");
                        ItemSwapData.GetItem(ItemSwapData.SkillEnum.Dispatch);
                        //CrabFile.current.SetInt("LocationChecked-483021702",0);
                    }

                    if (Input.GetKeyDown(KeyCode.F4))
                    {
                        Debug.Log("F4 Pressed");
                        Item_Scripts.Traps.ClutzTrap.ActivateTrap();
                        //GameManager.events.CheckProgress();
                    }

                    if (Input.GetKeyDown(KeyCode.F5))
                    {
                        Debug.Log("F5 Pressed");
                        var newShell = GameObject.Instantiate<Shell>(AssetListCollection.GetShellPrefab("Shell_SoloCup"));
                        newShell.transform.position = Player.singlePlayer.transform.position;
                    }

                    if (Input.GetKeyDown(KeyCode.F6))
                    {
                        Debug.Log("F6 Pressed");
                        Item_Scripts.Traps.StatusEffectTraps.Electrocute();
                        //Item_Scripts.Traps.StatusEffectTraps.Hypnotized();
                    }
                    if (Input.GetKeyDown(KeyCode.F8))
                    {
                        Debug.Log("F8 Pressed");
                        ItemSwapData.GetItem(ItemSwapData.ItemEnum.FishingLine);
                        CrabFile.current.unlocks[SkillWorldUnlocks.String].unlocked = true;
                    }
                    if (Input.GetKeyDown(KeyCode.F9))
                    {
                        Debug.Log("F9 Pressed");
                        //ItemSwapData.GetItem(ItemSwapData.ItemEnum.FishingLine);
                        CrabFile.current.unlocks[SkillWorldUnlocks.String].unlocked = false;
                    }
                    if (Input.GetKeyDown(KeyCode.F10))
                    {
                        Debug.Log("F10 Pressed");
                        GUIManager.instance.Load(GUIManager.instance.blackFadeLoaderIllustrated, LevelDirector.SwapLevelRoutine(Level.TheUnfathom, 2, false), false, null, 0f);
                        _player.depressed = false;
                        //_player.Die();
                    }
                    if (Input.GetKeyDown(KeyCode.F11))
                    {
                        string path = "shelldata/shellData.json";

                        FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                        Debug.Log("Adding shells");

                        using (StreamWriter writeText = new StreamWriter(fileStream))
                        {
                            writeText.WriteLine(JsonConvert.SerializeObject(shellData));
                            writeText.Close();
                            Debug.Log("Write Success");
                        }
                    }
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
            public DeathLinkService deathLinkService;
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
                    catch
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

                    deathLinkService.OnDeathLinkReceived += (deathLinkObject) =>
                    {
                        if (SceneManager.GetActiveScene().name != "TitleScreen" && _player != null && !_player.dead && !DeathLinkPatch.isDeathLink /*So rapid fires dont crash game*/)
                        {
                            //Debug.Log("Death link received");
                            DeathLinkPatch.deathMsg = deathLinkObject.Cause == null ? $"{deathLinkObject.Source} died. Point and laugh." : $"{deathLinkObject.Cause}";
                            DeathLinkPatch.isDeathLink = true;
                            //Player.singlePlayer.Die();
                            //DeathLinkPatch.RecieveDeathLink(deathLinkObject.Cause == null ? $"\"{deathLinkObject.Source} died and took you with them.\"" : $"\"{deathLinkObject.Cause}\"");
                            //DeathLinkPatch.RecieveDeathLink(deathLinkObject.Cause == null ? $"{deathLinkObject.Source} died." : $"{deathLinkObject.Cause}");
                            //PlayerCharacterPatches.DeathLinkMessage = deathLinkObject.Cause == null ? $"\"{deathLinkObject.Source} died and took you with them.\"" : $"\"{deathLinkObject.Cause}\"";
                            //PlayerCharacterPatches.DiedToDeathLink = true;
                        }
                    };

                    
                    if ((bool)Plugin.connection.slotData["death_link"])
                    {
                        deathLinkService.EnableDeathLink();
                    }
                    else
                    {
                        deathLinkService.DisableDeathLink();
                    }

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
                catch
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

            public void SendDeathLink(string deathCause)
            {
                if (connected)
                {
                    //Debug.Log("Sending Death Link: " + deathCause);
                    deathLinkService.SendDeathLink(new DeathLink(session.Players.ActivePlayer.Name,deathCause));
                }
            }

        }

        public void OnGUI()
        {
            if (RenderWorldMarkers)
            {
                RenderWorld();
            }
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

            if (showMenu && (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "Pretitle"))
            {
                GUI.backgroundColor = backgroundColor;

                if(windowWidth < 200)
                {
                    windowWidth = 200;
                }

                windowRect = new Rect(0, 0, windowWidth, 150);
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
                windowRect = new Rect(0, 0, 200, 160);
                windowRect = GUI.Window(0, windowRect, APClientMenu, "Archipelago");
            }
        }

        //In game UI for AP
        void APClientMenu(int windowID)
        {
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label("Press [Insert] to toggle menu.");
            GUILayout.Label("\nPlayer Coords: ");
            GUILayout.Label(_player.transform.position.ToString());
            if (CrabFile.current.GetBool("WasSentPearl") == true)
            {
                CrabFile.current.progressData[ProgressData.ShallowsProgress.PearlPickedUp].unlocked = GUILayout.Toggle(CrabFile.current.progressData[ProgressData.ShallowsProgress.PearlPickedUp].unlocked, "Fallen Slacktide");
            }

            RenderMapMarkers = GUILayout.Toggle(RenderMapMarkers, "Show items on map");
            RenderWorldMarkers = GUILayout.Toggle(RenderWorldMarkers, "Show items in world");
            

            if (CrabFile.current.inventoryData.HasItem("Shallows_0_ForkOverlook"))
            {

                if (GUILayout.Button("Teleport to Start"))
                {
                    TeleportPanel tele = new TeleportPanel();
                    MSSCollectable mss = tele.GetMss(TeleportPanel.ZoneSelection.TheShallows, 0);
                    MethodInfo method = AccessTools.Method(typeof(TeleportPanel), "WarpToShellRoutine");

                    StartCoroutine((IEnumerator)method.Invoke(tele, new object[] { mss }));
                    //GameManager.instance.StartCoroutine(method.Invoke(tele,));
                    //tele.WarpToShellRoutine(mss)
                }
            }

            if (CrabFile.current.progressData.scuttleportBools[CrabFile.current.progressData.GetProgressIndex(ProgressData.ScuttleportProgress.RolandDefeated)].unlocked == true)
            {
                if (GUILayout.Button("Teleport to Unfathom"))
                {
                    GUIManager.instance.Load(GUIManager.instance.blackFadeLoaderIllustrated, LevelDirector.SwapLevelRoutine(Level.TheUnfathom, 2, false), false, null, 0f);
                    _player.depressed = false;
                }
            }



            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUI.DragWindow();
        }

        //Teleport to Start button
        IEnumerator TeleToStartRoutine()
        {
            TeleportPanel tele = new TeleportPanel();
            GUIManager.instance.CloseAllWindows();
            GameManager.events.TriggerShelleport();
            PlayerLocationData playerLocationData = ScriptableObject.CreateInstance<PlayerLocationData>();
            playerLocationData.SetSpawnerMSS(Level.TheShallows, 0);
            //GUIManager.instance.Load(GUIManager.instance.blackFadeLoaderIllustrated, tele.LoadWarpLocationRoutine(targetLocation), false, null, 0f);
            AreaMap.RefreshDataMap(playerLocationData);
            yield break;
        }
        
        //AP Connection info on Main Menu
        void APConnectMenu(int windowID)
        {
            if (debugMode == false && SceneManager.GetActiveScene().name == "Title")
            {
                
                
                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                GUILayout.BeginVertical(GUILayout.Width(80), GUILayout.ExpandWidth(true));
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
                GUILayout.BeginVertical(GUILayout.Width(80), GUILayout.ExpandWidth(true));
                apAdress = GUILayout.TextField(apAdress, GUILayout.ExpandWidth(true));
                apPort = GUILayout.TextField(apPort, GUILayout.ExpandWidth(true));
                apPassword = GUILayout.TextField(apPassword, GUILayout.ExpandWidth(true));
                apSlot = GUILayout.TextField(apSlot, GUILayout.ExpandWidth(true));

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
                
            }
            if (debugMode && SceneManager.GetActiveScene().name == "Title")
            {
                removePickups = GUILayout.Toggle(removePickups, "Remove Known Pickups");
            }
            
        }

        //In game ui for debug mode
        void DebugMenu(int windowID)
        {
            if (debugMode == true && _player != null)
            {
                GUILayout.BeginHorizontal(GUILayout.Height(200));
                GUILayout.BeginVertical(GUILayout.Width(160));
                GUILayout.Label("Press [Insert] to toggle menu.");
                GUILayout.Label(_player.transform.position.ToString());

                if (CrabFile.current.progressData.scuttleportBools[CrabFile.current.progressData.GetProgressIndex(ProgressData.ScuttleportProgress.RolandDefeated)].unlocked == true)
                {
                    if (GUILayout.Button("Teleport to Unfathom"))
                    {
                        GUIManager.instance.Load(GUIManager.instance.blackFadeLoaderIllustrated, LevelDirector.SwapLevelRoutine(Level.TheUnfathom, 2, false), false, null, 0f);
                        _player.depressed = false;
                    }
                }

                RenderMapMarkers = GUILayout.Toggle(RenderMapMarkers, "Show items on map");
                RenderWorldMarkers = GUILayout.Toggle(RenderWorldMarkers, "Show items in world");

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

        public static Texture2D LoadTextureFromDLL(string filename)
        {
            Texture2D newTex = new Texture2D(1, 1);
            Assembly _assembly = Assembly.GetExecutingAssembly();

            //if we get here, this is being called as a DLL, extract texture
            Stream _imageStream = null;
            try
            {
                _imageStream = _assembly.GetManifestResourceStream("ACTAP.Resources." + filename);// this is the namespace this function lives in.
            }
            catch
            {
                Debug.LogWarning("Unable to find " + filename + " resource in DLL " + _assembly.FullName);
                return newTex;
            }
            if (_imageStream == null)//sanity check- should be "caught" above
            {
                Debug.LogWarning("Unable to find " + filename + " resource in DLL " + _assembly.FullName);
                return newTex;
            }
            byte[] imageData = new byte[_imageStream.Length];
            _imageStream.Read(imageData, 0, (int)_imageStream.Length);

            

            if (!newTex.LoadImage(imageData))
                Debug.LogWarning("Unable to Load " + filename + " resource from DLL" + _assembly.FullName);
            return newTex;
        }

        public static GameObject LoadFromAssetBundle(string filename)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            AssetBundle asset = AssetBundle.LoadFromFile(Path.Combine(new string[]{ _assembly.FullName,"ACTAP.Resources." + filename}));
            //var assetBundleCreateRequest = AssetBundle.LoadFromFile(_assembly.Location + "Resources.apassets.itemmatsshader");

            //if we get here, this is being called as a DLL, extract texture
            var prefab = asset.LoadAsset<GameObject>(filename);
            return prefab;
        }

        public static void RenderWorld()
        {
            if (RenderWorldMarkersTemp)
            {
                if (crystalEnemies != null)
                {
                    foreach (Enemy enemy in crystalEnemies)
                    {
                        SaveStateKillableEntity state = Traverse.Create(enemy).Field("saveState").GetValue() as SaveStateKillableEntity;
                        if (state == null) { continue; }
                        if (enemy.transform == null || enemy.isBoss || state.killedPreviously)
                        {
                            continue;
                        }
                        if (enemy.umamiDrops > 0)
                        {
                            Vector3 center = enemy.GetCenter();
                            Vector3 screenPoint = Camera.main.WorldToScreenPoint(center);
                            Texture2D crystal = ModHelper.GetSprite("crystal").texture;
                            float iconSize = 64;

                            if (screenPoint.z > 0)
                            {
                                GUI.DrawTexture(new Rect(new Vector2(screenPoint.x - iconSize / 2, Screen.height - screenPoint.y - iconSize / 2), new Vector2(iconSize, iconSize)), crystal, ScaleMode.ScaleToFit);
                            }
                        }
                    }
                }
                if (items != null)
                {
                    foreach (Item item in items)
                    {
                        string itemName = item.DisplayName.Replace("Item_", "").Replace("_Name", "").ToLower();
                        string resourceName;
                        if (ItemNameToResource.ItemToResource.ContainsKey(itemName))
                        {
                            resourceName = ItemNameToResource.ItemToResource[itemName];
                        }
                        else if (itemName.Contains("stowaway"))
                        {
                            resourceName = "stowaways";
                        }
                        else if (itemName.Contains("claw"))
                        {
                            resourceName = "junk";
                        }
                        else if (itemName.Contains("costume"))
                        {
                            resourceName = "costume";
                        }
                        else
                        {
                            resourceName = "junk";
                        }
                        SaveStateKillableEntity state = Traverse.Create(item).Field("save").GetValue() as SaveStateKillableEntity;
                        if (state == null)
                        {
                            continue;
                        }
                        if (item == null || state.killedPreviously)
                        {
                            continue;
                        }
                        Vector3 center = item.GetCenter();
                        Vector3 screenPoint = Camera.main.WorldToScreenPoint(center);
                        Texture2D crystal = ModHelper.GetSprite(resourceName).texture;
                        float iconSize = 64;

                        if (screenPoint.z > 0)
                        {
                            GUI.DrawTexture(new Rect(new Vector2(screenPoint.x - iconSize / 2, Screen.height - screenPoint.y - iconSize / 2), new Vector2(iconSize, iconSize)), crystal, ScaleMode.ScaleToFit);
                        }
                    }
                }
            }
        }

        public static bool EnemiesAggro()
        {
            bool aggro = false;
            foreach (Enemy enemy in crystalEnemies)
            {
                if (enemy.aggro)
                {
                    aggro = true;
                }
            }
            return aggro;
        }
    }
}
