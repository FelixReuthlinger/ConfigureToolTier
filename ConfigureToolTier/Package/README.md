# Configure Tool Tier

Configure the world objects like trees and rocks to use a different required minimum tool tier for being able to harvest
them.

## Features

Modify the world objects prefabs for:

* TreeBase (normal spawned tree)
* TreeLog (log after cutting the tree)
* MineRock (mine rocks)
* MineRock5 (mine rocks)

### Automated generator for configuration

This mod does also provide the ability to automatically generate a configuration file with all the prefabs of the
mentioned categories from in-game. To use this feature call the console command:
```configure_tool_tier_write_defaults```

This will create a file (beware: different name then the file that is read at game startup, you will need to rename it
to make use of it!) ```org.bepinex.plugins.configure.tool.tier.defaults.yaml``` inside the BepInEx config folder.

Note: the writer will filter any prefabs containing the following in their name:

* complete string of "(Clone)"
* Regex "\([0-9]+\)"

Those prefabs from vanilla game containing these strings cannot be configured, since they are clones or objects that
cannot be found when running config settings.

## Configuration

For modifying you need to provide a configuration file named ```org.bepinex.plugins.configure.tool.tier.yaml```
at the BepInEx config folder with the following structure (example, not full list, run
[auto generator](#automated-generator-for-configuration) for full list):

```
TreeBase:
  Beech1: 0
TreeLog:
  beech_log: 0
MineRock:
  MineRock_Stone: 0
MineRock5:
  silvervein_frac: 2
```

The 1st level names are predefined and have to be used like shown in the structure:

* TreeBase
* TreeLog
* MineRock
* MineRock5

At the 2nd level you can add each prefab of those groups that is available in the game and specify the desired minimum
tool like:

```my_custom_prefab_name: 1```

### Complete vanilla game example config

(you can also [automatedly generate](#automated-generator-for-configuration) there yourselves)

```
TreeBase:
  PineTree: 0
  Beech1: 0
  Birch2: 2
  Oak1: 2
  Birch2_aut: 2
  Birch1_aut: 2
  Birch1: 2
  Pinetree_01: 0
  FirTree: 0
  SwampTree1: 2
TreeLog:
  Oak_log: 2
  PineTree_log: 0
  SwampTree1_log: 0
  Birch_log: 2
  FirTree_log: 0
  PineTree_logOLD: 0
  beech_log: 0
  Oak_log_half: 2
  FirTree_log_half: 0
  PineTree_log_half: 0
  beech_log_half: 0
  Birch_log_half: 2
  PineTree_log_halfOLD: 0
MineRock:
  Leviathan: 0
  MineRock_Stone: 0
  MineRock_Meteorite: 2
  MineRock_Iron: 1
  Rock_destructible_test: 0
  MineRock_Copper: 0
  mudpile_old: 0
  stoneblock_fracture: 0
MineRock5:
  Rock_3_frac: 0
  widestone_frac: 0
  highstone_frac: 0
  rock4_coast_frac: 0
  rock3_mountain_frac: 0
  rock4_heath_frac: 0
  silvervein_frac: 2
  rock4_forest_frac: 0
  rock4_copper_frac: 0
  rock3_silver_frac: 2
  tarlump1_frac: 0
  HeathRockPillar_frac: 0
  ice_rock1_frac: 0
  rock1_mountain_frac: 0
  RockFinger_frac: 0
  RockFingerBroken_frac: 0
  RockThumb_frac: 0
  rock2_heath_frac: 0
  rock2_mountain_frac: 0
  mudpile_frac: 0
  mudpile2_frac: 0
```

### Valheim tool tiers

1. Trees
    * 0 -> Stone Axe
    * 1 -> Flint Axe
    * 2 -> Bronze Axe
    * 3 -> Iron Axe
    * 4 -> Black Metal Axe
2. Rocks
    * 0 -> Antler Pickaxe
    * 1 -> Bronze Pickaxe
    * 2 -> Iron Pickaxe

## Changelog

* 0.1.1 -> readme fix (pickaxe tool tier levels)
* 0.1.0 -> initial version

## Contact

* https://github.com/FelixReuthlinger/ConfigureToolTier
* Discord: Flux#0062 (you can find me around some of the Valheim modding discords, too)
