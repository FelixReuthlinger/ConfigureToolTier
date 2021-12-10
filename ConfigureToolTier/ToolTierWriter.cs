using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BepInEx;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ConfigureToolTier {
    public static class ToolTierWriter {
        private static readonly string DefaultFileName = $"{ConfigureToolTierPlugin.PluginGuid}.defaults.yaml";

        public static readonly string DefaultOutputFile =
            $"{Paths.ConfigPath}{Path.DirectorySeparatorChar}{DefaultFileName}";

        private const string CloneString = "(Clone)";
        private const string InvalidObjectRegex = @"\([0-9]+\)";

        public static void WriteYamlFromGameDefaults(string file) {
            Logger.LogInfo($"Writing YAML default contents to file '{file}'");
            var yamlContent = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance).Build()
                .Serialize(GETAllNamesAndTiersGrouped());
            File.WriteAllText(file, yamlContent);
        }

        private static Dictionary<string, Dictionary<string, int>> GETAllNamesAndTiersGrouped() {
            return new Dictionary<string, Dictionary<string, int>> {
                [ConfigureToolTierPlugin.TreeBaseString] = GETAllTreeNames(),
                [ConfigureToolTierPlugin.TreeLogString] = GETAllTreeLogNames(),
                [ConfigureToolTierPlugin.MineRockString] = GETAllRockNames(),
                [ConfigureToolTierPlugin.MineRock5String] = GETAllRock5Names()
            };
        }

        private static Dictionary<string, int> GETAllTreeNames() {
            return PrefabManager.Cache.GetPrefabs(typeof(TreeBase))
                .Where(pair => !pair.Key.Contains(CloneString))
                .Where(pair => !Regex.IsMatch(pair.Key, InvalidObjectRegex))
                .ToDictionary(pair => pair.Key, pair => ((TreeBase) pair.Value).m_minToolTier);
        }

        private static Dictionary<string, int> GETAllTreeLogNames() {
            return PrefabManager.Cache.GetPrefabs(typeof(TreeLog))
                .Where(pair => !pair.Key.Contains(CloneString))
                .Where(pair => !Regex.IsMatch(pair.Key, InvalidObjectRegex))
                .ToDictionary(pair => pair.Key, pair => ((TreeLog) pair.Value).m_minToolTier);
        }

        private static Dictionary<string, int> GETAllRockNames() {
            return PrefabManager.Cache.GetPrefabs(typeof(MineRock))
                .Where(pair => !pair.Key.Contains(CloneString))
                .Where(pair => !Regex.IsMatch(pair.Key, InvalidObjectRegex))
                .ToDictionary(pair => pair.Key, pair => ((MineRock) pair.Value).m_minToolTier);
        }

        private static Dictionary<string, int> GETAllRock5Names() {
            return PrefabManager.Cache.GetPrefabs(typeof(MineRock5))
                .Where(pair => !pair.Key.Contains(CloneString))
                .Where(pair => !Regex.IsMatch(pair.Key, InvalidObjectRegex))
                .ToDictionary(pair => pair.Key, pair => ((MineRock5) pair.Value).m_minToolTier);
        }
    }

    public class ToolTierWriterController : ConsoleCommand {
        public override void Run(string[] args) {
            ToolTierWriter.WriteYamlFromGameDefaults(ToolTierWriter.DefaultOutputFile);
        }

        public override string Name => "configure_tool_tier_write_defaults";

        public override string Help =>
            "Write all tree and rock based minimum required tool tier information to a YAML " +
            "file inside the BepInEx config folder.";
    }
}