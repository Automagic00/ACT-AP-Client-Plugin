# Another Crabs Treasure Archipelago Randomizer (ACT-AP)

## About
 The **Another Crab's Treasure Archipelago Randomizer** is a randomizer mod for the Aggrocrab game Another Crab's Treasure, for use with the [Archipelago multi-game randomizer](https://archipelago.gg/). In this mod, you play as Kril, a crab whose goal is to get their shell back after it was stolen by the evil crabitalist Prawnathan (or alternatively to execute a hit on one of three bosses &ndash; more on alternative goals in [Options](#options)).

Please follow both the Apworld Instructions and Client Mod Instructions below to set up the ACT-AP mod &ndash; Step 5 of the Apworld Instructions can be skipped if you are taking part in a multiworld which you are not personally hosting.

**Warning:** Mod releases from version 0.4.5 onwards are only compatible with versions of Another Crab's Treasure that have the Year of the Crab update applied. If you wish to play Another Crab's Treasure without the Year of the Crab update, please use version 0.4.4. Conversely, if you wish to use version 0.4.4 of the mod or earlier, you will need to downpatch your game to not include Year of the Crab: on Steam this can be done by following the guide [here](https://steamcommunity.com/sharedfiles/filedetails/?id=3042152647), using `download_depot 1887840 1887841 339460321503769681`.

Should you experience any issues with setting up or using this mod, please feel free to ask in the [Another Crab's Treasure Thread](https://discord.com/channels/731205301247803413/1239467743116525688) on the official Archipelago Discord server, or by [creating an issue](https://github.com/Automagic00/ACT-AP-Client-Plugin/issues) on this Github Repository. We hope you have fun experiencing The Sands Between in a brand new way!

 ## Apworld Instructions 
<details><summary>Generating a YAML/Hosting a Game</summary><br>
  
1) Download the latest `another_crab.apworld` from [releases](https://github.com/Automagic00/ACT-AP-Client-Plugin/releases).
2) Install the Archipelago Launcher following the instructions [here](https://archipelago.gg/tutorial/Archipelago/setup_en) if you have not done so already.
3) Place the `another_crab.apworld` file in the `custom_worlds` folder of your Archipelago install or click on Install APWorld in the Archipelago Launcher and select your `another_crab.apworld` file.

To generate a YAML:

4) Click on Generate Template Options in the Archipelago Launcher and select the `Another Crabs Treasure.yaml`. This can then be edited and saved wherever you wish.

To generate a game:

5) To generate a game, continue following the instructions on the [Archipelago website](https://archipelago.gg/tutorial/Archipelago/setup_en), using the yaml created in step 4.
</details>

## Client Mod Instructions (Installing/Running the Mod)

In order to install the ACT-AP client mod, it is necessary to first install BepInEx and then the ACT-AP client mod itself. There are two ways to install BepInEx: manually or via Thunderstore. We recommend installing manually as it is easier to debug installation issues and the Thunderstore version of BepInEx is not up to date. Some extra steps are required on Linux to get BepInEx to function with the Proton/Wine compatibility layer.

<details><summary>Manual Installation</summary><br>

1) Download the latest stable release of BepInEx from [GitHub](https://github.com/BepInEx/BepInEx/releases) (5.4.23.3 at time of writing). Make sure you only download the zip folder for your operating system.
2) Install BepInEx to the Another Crab's Treasure game folder following steps 1-3 of the instructions [here](https://docs.bepinex.dev/articles/user_guide/installation/index.html).

**Linux Only:** It is necessary to configure the Proton/Wine prefix for BepInEx to run correctly. Do so by following the instructions [here](https://docs.bepinex.dev/articles/advanced/proton_wine.html) before launching Another Crab's Treasure.

Note: Your file directory should look like ```Another Crab's Treasure > BepInEx > cache``` once the installation is complete. If you have an additional folder between `Another Crab's Treasure` and `BepInEx`, move the contents of the additional folder into `Another Crab's Treasure`, then delete the additional folder.
   
3) In `BepInEx/config/BepInEx.cfg` set `Enabled = true` under the `[Logging.Console]` heading -- this enables error logs and is off by default.
4) Download `ACTAP.dll` from [releases](https://github.com/Automagic00/ACT-AP-Client-Plugin/releases) and place it in `BepInEx/Plugins`
5) Launch Another Crab's Treasure (via the .exe or your desired launcher).

Note: If installed correctly, there should be a form on the top left corner of the main menu.
 
6) Enter the server address, port, server password, and player slot name issued at game generation in the listed fields.
7) After successful connection, start a new save file and enjoy!

</details>

<details><summary>Via Thunderstore</summary>
  
1) Install BepinEx following the instructions on [Thunderstore](https://thunderstore.io/c/another-crabs-treasure/p/BepInEx/BepInExPack/) (you will require either Thunderstore Mod Manager or r2modman to do so).

**Linux Only:** It is necessary to configure the Proton/Wine prefix for BepInEx to run correctly. Do so by following the instructions [here](https://docs.bepinex.dev/articles/advanced/proton_wine.html) before launching Another Crab's Treasure.

2) Download `ACTAP.dll` from [releases](https://github.com/Automagic00/ACT-AP-Client-Plugin/releases) and place it in `BepInEx/Plugins`
3) Launch Another Crabs Treasure via Thunderstore or r2modman.

Note: If installed correctly, there should be a form on the top left corner of the main menu.
 
4) Enter the server address, port, server password, and player slot name issued at game generation in the listed fields.
5) After successful connection, start a new save file and enjoy!

</details>


## Options

#### Goal

#### Fork Location

#### Allow Forkless

#### Logic Rules

#### Shelleport Location

#### Fishing Line Location

#### Random Shells

#### Remove Costumes

#### Miscroplastics Multiplier

#### Trap Amount

#### Death Link




## Randomization





#### Randomized Items
- Adaptations
- Skills
- Currency Pickups (i.e. Breadclaws)
- Stowaways
- Upgrade Pickups (i.e. Bloodstar Limbs)
- Map Pieces
- Fork (Optional)
- Grapple (Optional)
- Shells (Randomized between spawn locations)

#### Randomized Locations
- Defeating Bosses
- All Pickups (Including fishing spots and quest items)
- Moon Snail Skill Purchases
- Map Pieces

#### Other Features
- Deathlink
- Microplastic Multiplier (Player Setting)
- Traps
- Combat Logic Settings
