# Another Crab's Treasure Archipelago Rando (ACT-AP)

## About
 The **Another Crab's Treasure Archipelago Randomizer** is a randomizer mod for the Aggrocrab game Another Crab's Treasure, for use with the [Archipelago multi-game randomizer](https://archipelago.gg/). In this mod, you play as Kril, a crab whose goal is to get their shell back after it was stolen by the evil crabitalist Prawnathan (or alternatively to execute a hit on one of three bosses &ndash; more on alternative goals in [Options](#options)).

> **Warning:** Mod releases from version 0.4.5 onwards are only compatible with versions of Another Crab's Treasure that have the Year of the Crab update applied. If you wish to play Another Crab's Treasure without the Year of the Crab update, please use version 0.4.4. Conversely, if you wish to use version 0.4.4 of the mod or earlier, you will need to downpatch your game to not include Year of the Crab: on Steam this can be done by following the guide [here](https://steamcommunity.com/sharedfiles/filedetails/?id=3042152647), using `download_depot 1887840 1887841 339460321503769681`.

Should you experience any issues with setting up or using this mod, please feel free to ask in the [Another Crab's Treasure Thread](https://discord.com/channels/731205301247803413/1239467743116525688) on the official Archipelago Discord server, or by [creating an issue](https://github.com/Automagic00/ACT-AP-Client-Plugin/issues) on this Github Repository. We hope you have fun experiencing The Sands Between in a brand new way!

 ## Apworld Instructions 
 Please follow these Apworld Instructions **and** the Client Mod Instructions below to set up the ACT-AP mod &ndash; Step 5 of the Apworld Instructions can be skipped if you are taking part in a multiworld which you are not personally hosting.
 
<details><summary><ins>Generating a YAML/Hosting a Game</ins></summary><br>
  
1) Download the latest `another_crab.apworld` from [releases](https://github.com/Automagic00/ACT-AP-Client-Plugin/releases).
2) Install the Archipelago Launcher following the instructions [here](https://archipelago.gg/tutorial/Archipelago/setup_en) if you have not done so already.
3) Place the `another_crab.apworld` file in the `custom_worlds` folder of your Archipelago install or click on Install APWorld in the Archipelago Launcher and select your `another_crab.apworld` file.
4) To generate a .yaml, click on Generate Template Options in the Archipelago Launcher and select the `Another Crabs Treasure.yaml`. This can then be edited and saved wherever you wish.
5) To generate a game, continue following the instructions on the [Archipelago website](https://archipelago.gg/tutorial/Archipelago/setup_en), using the yaml created in step 4.
</details>

## Client Mod Instructions

In order to install the ACT-AP client mod, it is necessary to first install BepInEx and then the ACT-AP client mod itself. There are two ways to install BepInEx: manually or via Thunderstore. We recommend installing manually as it is easier to debug installation issues and the Thunderstore version of BepInEx is not up to date. Some extra steps are required on Linux to get BepInEx to function with the Proton/Wine compatibility layer (see below).

<details><summary><ins>Manual Installation</ins></summary><br>

1) Download the latest stable release of BepInEx from [GitHub](https://github.com/BepInEx/BepInEx/releases) (5.4.23.3 at time of writing). Make sure you only download the zip folder for your operating system.
2) Install BepInEx to the Another Crab's Treasure game folder following steps 1-3 of the instructions [here](https://docs.bepinex.dev/articles/user_guide/installation/index.html).

> Note: Your file directory should look like ```Another Crab's Treasure > BepInEx > cache``` once the installation is complete. If you have an additional folder between `Another Crab's Treasure` and `BepInEx`, move the contents of the additional folder into `Another Crab's Treasure`, then delete the additional folder.

> **Linux Only:** It is necessary to configure the Proton/Wine prefix for BepInEx to run correctly. Do so by following the instructions [here](https://docs.bepinex.dev/articles/advanced/proton_wine.html) before launching Another Crab's Treasure.
   
3) In `BepInEx/config/BepInEx.cfg` set `Enabled = true` under the `[Logging.Console]` heading -- this enables error logs and is off by default.
4) Download `ACTAP.dll` from [releases](https://github.com/Automagic00/ACT-AP-Client-Plugin/releases) and place it in `BepInEx/Plugins`
5) Launch Another Crab's Treasure (via the .exe or your desired launcher).

> Note: If installed correctly, there should be a form on the top left corner of the main menu.
 
6) Enter the server address, port, server password, and player slot name issued at game generation in the listed fields.
7) After successful connection, start a new save file and enjoy!

</details>

<details><summary><ins>Via Thunderstore</ins></summary><br>
  
1) Install BepinEx following the instructions on [Thunderstore](https://thunderstore.io/c/another-crabs-treasure/p/BepInEx/BepInExPack/) (you will require either Thunderstore Mod Manager or r2modman to do so).

> **Linux Only:** It is necessary to configure the Proton/Wine prefix for BepInEx to run correctly. Do so by following the instructions [here](https://docs.bepinex.dev/articles/advanced/proton_wine.html) before launching Another Crab's Treasure.

2) Download `ACTAP.dll` from [releases](https://github.com/Automagic00/ACT-AP-Client-Plugin/releases) and place it in `BepInEx/Plugins`
3) Launch Another Crabs Treasure via Thunderstore or r2modman.

> Note: If installed correctly, there should be a form on the top left corner of the main menu.
 
4) Enter the server address, port, server password, and player slot name issued at game generation in the listed fields.
5) After successful connection, start a new save file and enjoy!

</details>


## Key Events For Progression

The following event: trigger pairs detail events which unlock access to additional items and what to do to trigger those events. This is in addition to using mantis punch, eelectrocute, the fishing line, and spearfishing to collect individual items.

 - Access Fallen Slacktide: Obtain Pristine Pearl.
 - Access Post Pagarus items: Return Pagarus Map Piece to Conch.
 - Open Entrances to Secluded Ridge Pt.1 and Trashbin Plateau: Have Mantis Punch.
 - Open Entrances to Southern Town Ridge and Secluded Ridge Pt.2: Have Eelectrocute.
 - Unblock Scuttleport Door: Have Fuse Plug Shell.
 - Activate shell dispensers in Scuttleport: Return all three Map Pieces to Conch.
 - Access Pinbarge: Return all three Map Pieces to Conch and obtain Eelectrocute.


## Options

Below is a summary of the options available to you when creating an Another Crab's Treasure Yaml. Click on the triangle next to the option name to view a brief description of the option and the values it may take.

<details><summary><ins>Goal</ins></summary>
 
&nbsp;&nbsp; Choose your goal for the multiworld.
 
- Home *[Default]*: Defeat the final boss and equip the Home shell.
- Roland: Defeat Roland on the Pinbarge.
- Voltai: Defeat Voltai in Scuttleport.
- Magista: Defeat Magista in Fallen Slacktide.
</details>

<details><summary><ins>Fork Location</ins></summary>
 
&nbsp;&nbsp; Choose where the Fork (weapon) location is set. Does nothing if the **[Allow Forkless]** option is enabled.

- Vanilla *[Default]*: Fork is in its vanilla location.
- Shuffled Early Local: Fork is in a random early location in Another Crab's Treasure.
- Shuffled Early Global: Fork is in a random early location in any multiworld.
</details>

<details><summary><ins>Allow Forkless</ins></summary>
 
&nbsp;&nbsp; If enabled, allows for the Fork to not be required early on, meaning you may have to play some of the game without it. <br>
&nbsp;&nbsp; Ignores the **[Fork Location]** option and shuffles Fork into wider item pool.

- Disabled *[Default]*: Fork is required to fight early bosses, meaning it will be placed before them in your run.
- Forkless Easy: Allows for the possibility of more straightforward forkless strategies being necessary for some portion of the game (e.g. adaption and shell spell focused builds).
- Forkless Hard: Allows for the possibility of more advanced forkless strategies to be necessary for some portion of the game as well as the above (e.g. rolling attack and summon focused builds).
</details>

<details><summary><ins>Logic Rules</ins></summary>

&nbsp;&nbsp; Set the preferred logic rules for your game.

- Vanilla: Standard logic, no skips/glitches are required to complete the chosen goal.
- Glitchless: Allows for parkour-based skips to be required to complete the game.
  <details><summary><ins>List of Skips</ins></summary>  
   
  - Naked Slacktide (Nephro Skip)
  - Shallows East Fort Early
  - Shallows Parasol Grappleless
  - Shallows Tower Grappleless
  - Spiral Stairs Fallen Slacktide
  - Urchin Tower Grappleless
  - Magista Skip With Grapple
  - Early Moon Snail Cave
  - Pagarus Quick Kill
  - Sunlight Outfit Mantis-less
  - Ceviche Sisters Skip
  - Consortium from Expired Grove Early
  - Consortium from Flotsam Vale
    
  </details>
- Restricted: In addition to the list under Glitchless, allows for glitches such as CALs and Shell Clipping to be required to beat the game.
  <details><summary><ins>List of Glitches</ins></summary>
   
  - Shallows Shellsplitter Tower Grappleless
  - Magista Skip Grappleless
  - Moon Snail Cave Pink Crab Skip
  - Sunlight Outfit Pt2 Eel-less
  - Bridge Stainless Steel Relic Early
  - Unfathom Tackle Pouch Grappleless
    
  </details>
- Unrestricted: In addition to the lists under both Glitchless and Restricted, allows for some harder/more game-breaking glitches to be required to beat the game.
  <details><summary><ins>List of Glitches</ins></summary>
   
  - TBC
    
  </details>
</details>

<details><summary><ins>Shelleport Location</ins></summary>

&nbsp;&nbsp; Choose where the Shelleport (fast travel) skill location is set. 

- Shuffled: Shelleport is placed into the item pool.
- Shuffled Early Local *[Default]*: Forces Shelleport to be placed somewhere early in your own game.
- Shuffled Early Global: Forces Shelleport to be placed somewhere early in any game in the multiworld.
- Starting Items: Places Shelleport in your starting inventory.
- Vanilla Location: Forces Shelleport to be placed at its intended location.
</details>

<details><summary><ins>Fishing Line Location</ins></summary>

&nbsp;&nbsp; Choose where the Fishing Line (grapple) location is set.

- Shuffled *[Default]*: Fishing Line is placed into the item pool.
- Vanilla Location: Forces Fishing Line to be placed at its intended location.
</details>

<details><summary><ins>Random Shells</ins></summary>

&nbsp;&nbsp; Choose whether or not shells will be randomized. Only randomizes shells between shell locations, not the general item pool.

- True *[Default]*: Shells will be randomized.
- False: Shells will be found in their intended locations.
</details>

<details><summary><ins>Random Fuse</ins></summary>

&nbsp;&nbsp; Choose if the location of the Plug Fuse shell is randomized when using the shell randomizer. Does nothing if **[Random Shells]** is set to False.

- True *[Default]*: The Plug Fuse will be randomized along with all other shells.
- False: The Plug Fuse will be found in its intended location (the shell dispensers in Scuttleport).
</details>

<details><summary><ins>Remove Costumes</ins></summary>

&nbsp;&nbsp; Choose whether or not to include Costumes in the item pool. Set to true to remove them.

- True: Costumes will not be included in the item pool and will not be randomized.
- False *[Default]*: Costumes will be included in the item pool and randomized.
</details>

<details><summary><ins>Microplastics Multiplier</ins></summary>

&nbsp;&nbsp; Multiply the amount of microplastics received from all sources by this value. Takes integer values from 1 to 100, with 1 being the default amount.

</details>

<details><summary><ins>Trap Amount</ins></summary>

&nbsp;&nbsp; Set the percentage of filler items which are replaced by trap items. Takes integer values from 1 to 100, with 0 being the default amount (no traps).

</details>

<details><summary><ins>Death Link</ins></summary>

&nbsp;&nbsp; Choose whether or not to enable Death Link.

- True: Death Link will be enabled. When Kril dies, all other player characters in the multiworld will die, and vice-versa.
- False *[Default]*: Death Link will be disabled.
</details>


## Randomization

The following classes of items are randomized between apworlds by default, in addition to those set by the options above:
- Adaptations
- Skills
- Currency Pickups (e.g. Breadclaws)
- Stowaways
- Upgrade Pickups (e.g. Bloodstar Limbs, Stainless Steel Relics, Heartkelp Sprouts, Tackle Pouches)
- Map Pieces

Items may be randomized to the following locations:
- Defeating Bosses
- All Pickup Locations (Those which have a fixed location in the world - including fishing spots and quest items)
- Moon Snail Skill Purchases
- Map Pieces

At this time, umami crystals, guaranteed drops from strong enemies, and occasional drops from weak enemies are not randomized.
