using System.Collections.Generic;
using System.IO;
using BepInEx;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ConfigureToolTier {
    public static class ToolTierReader {
        private static readonly string DefaultFileName = $"{ConfigureToolTierPlugin.PluginGuid}.yaml";

        public static readonly string DefaultInputFile =
            $"{Paths.ConfigPath}{Path.DirectorySeparatorChar}{DefaultFileName}";

        public static Dictionary<string, Dictionary<string, int>> ReadFromFile(string file) {
            if (!File.Exists(file)) {
                Jotunn.Logger.LogWarning($"configuration file missing : '{file}', will not apply any changes");
                return new Dictionary<string, Dictionary<string, int>>();
            }

            var yamlContent = File.ReadAllText(file);
            return new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance) // see height_in_inches in sample yml 
                .Build().Deserialize<Dictionary<string, Dictionary<string, int>>>(yamlContent);
        }
    }
}