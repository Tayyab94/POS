using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Config
{
    /// <summary>
    /// config.json ko read/write karta hai.
    ///
    /// File path:  {AppBaseDirectory}\config.json
    ///   e.g.  C:\Program Files\POS_Shop\config.json
    ///
    /// NOTE: Newtonsoft.Json use kar raha hai kyunki EF 6 projects
    ///       already is package ko reference karte hain.
    /// </summary>
    public static class ConfigManager
    {
        // ── Config file path ──────────────────────────────────────
        private static readonly string ConfigFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "dbconfig.json"          // config.json se alag naam — clash avoid
        );

        // ── Public API ────────────────────────────────────────────

        /// <summary>
        /// Check karta hai ke app properly configured hai ya nahi.
        /// Program.cs mein startup pe yahi call hoti hai.
        /// </summary>
        public static bool IsConfigured()
        {
            if (!File.Exists(ConfigFilePath))
                return false;

            try
            {
                var cfg = Load();
                return cfg != null && cfg.IsValid();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Saved config load karta hai. Null return karta hai agar file nahi.
        /// </summary>
        public static AppConfig Load()
        {
            if (!File.Exists(ConfigFilePath))
                return null;

            try
            {
                string json = File.ReadAllText(ConfigFilePath);
                return JsonConvert.DeserializeObject<AppConfig>(json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Error occur while reading the dbConfig.json file: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Config ko JSON file mein save karta hai.
        /// </summary>
        public static void Save(AppConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            try
            {
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(ConfigFilePath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Error occur while saving the dbConfig.json file: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Config reset karta hai (dobara setup ke liye)
        /// </summary>
        public static void Reset()
        {
            if (File.Exists(ConfigFilePath))
                File.Delete(ConfigFilePath);
        }

        /// <summary>
        /// Saved config file ka path return karta hai (debugging ke liye useful)
        /// </summary>
        public static string GetConfigFilePath() => ConfigFilePath;
    }
}
