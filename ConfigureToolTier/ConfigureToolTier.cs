using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using Jotunn;
using Jotunn.Managers;

namespace ConfigureToolTier {
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    internal class ConfigureToolTierPlugin : BaseUnityPlugin {
        public const string PluginGuid = "org.bepinex.plugins.configure.tool.tier";
        public const string PluginName = "ConfigureToolTier";
        public const string PluginVersion = "0.1.0";

        private static Dictionary<string, Dictionary<string, int>> _config;

        public const string TreeBaseString = "TreeBase";
        public const string TreeLogString = "TreeLog";
        public const string MineRockString = "MineRock";
        public const string MineRock5String = "MineRock5";

        private static readonly List<int> AxeRange = new List<int> {0, 1, 2, 3, 4};
        private static readonly List<int> PickAxeRange = new List<int> {0, 1, 2, 3};

        private void Awake() {
            _config = ToolTierReader.ReadFromFile(ToolTierReader.DefaultInputFile);
            ItemManager.OnItemsRegistered += ModifyWorldObjects;
            CommandManager.Instance.AddConsoleCommand(new ToolTierWriterController());
        }

        private static void ModifyWorldObjects() {
            ModifyTreeBases();
            ModifyTreeLogs();
            ModifyMineRocks();
            ModifyMineRocks5();
            ItemManager.OnItemsRegistered -= ModifyWorldObjects;
        }

        private static bool AxeTierRangeCheck(string prefabName, int axeToolTier) {
            bool matching = AxeRange.Contains(axeToolTier);
            if (!matching) {
                Jotunn.Logger.LogWarning(
                    $"configured tool tier for '{prefabName}' is out of range for axes " +
                    $"(valid are {AxeRange}): {axeToolTier}");
            }

            return matching;
        }

        private static bool PickAxeTierRangeCheck(string prefabName, int pickAxeToolTier) {
            bool matching = PickAxeRange.Contains(pickAxeToolTier);
            if (!matching) {
                Jotunn.Logger.LogWarning(
                    $"configured tool tier for '{prefabName}' is out of range for axes " +
                    $"(valid are {PickAxeRange}): {pickAxeToolTier}");
            }

            return matching;
        }

        private static void ModifyTreeBases() {
            if (!_config.ContainsKey(TreeBaseString)) return;
            foreach (var keyValuePair in _config[TreeBaseString]
                .Where(keyValuePair => AxeTierRangeCheck(keyValuePair.Key, keyValuePair.Value))) {
                try {
                    var treeBase = PrefabManager.Cache.GetPrefab<TreeBase>(keyValuePair.Key);
                    treeBase.m_minToolTier = keyValuePair.Value;
                }
                catch (Exception e) {
                    Jotunn.Logger.LogWarning($"prefab name '{keyValuePair.Key}' caused {e.Message}");
                }
            }
        }

        private static void ModifyTreeLogs() {
            if (!_config.ContainsKey(TreeLogString)) return;
            foreach (var keyValuePair in _config[TreeLogString]
                .Where(keyValuePair => AxeTierRangeCheck(keyValuePair.Key, keyValuePair.Value))) {
                try {
                    var treeLog = PrefabManager.Cache.GetPrefab<TreeLog>(keyValuePair.Key);
                    treeLog.m_minToolTier = keyValuePair.Value;
                }
                catch (Exception e) {
                    Jotunn.Logger.LogWarning($"prefab name '{keyValuePair.Key}' caused {e.Message}");
                }
            }
        }

        private static void ModifyMineRocks() {
            if (!_config.ContainsKey(MineRockString)) return;
            foreach (var keyValuePair in _config[MineRockString]
                .Where(keyValuePair => PickAxeTierRangeCheck(keyValuePair.Key, keyValuePair.Value))) {
                try {
                    var mineRock = PrefabManager.Cache.GetPrefab<MineRock>(keyValuePair.Key);
                    mineRock.m_minToolTier = keyValuePair.Value;
                }
                catch (Exception e) {
                    Jotunn.Logger.LogWarning($"prefab name '{keyValuePair.Key}' caused {e.Message}");
                }
            }
        }

        private static void ModifyMineRocks5() {
            if (!_config.ContainsKey(MineRock5String)) return;
            foreach (var keyValuePair in _config[MineRock5String]
                .Where(keyValuePair => PickAxeTierRangeCheck(keyValuePair.Key, keyValuePair.Value))) {
                try {
                    var mineRock5 = PrefabManager.Cache.GetPrefab<MineRock5>(keyValuePair.Key);
                    mineRock5.m_minToolTier = keyValuePair.Value;
                }
                catch (Exception e) {
                    Jotunn.Logger.LogWarning($"prefab name '{keyValuePair.Key}' caused {e.Message}");
                }
            }
        }
    }
}