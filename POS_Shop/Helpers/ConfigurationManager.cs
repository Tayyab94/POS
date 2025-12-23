using Newtonsoft.Json;
using POS_Shop.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace POS_Shop.Helpers
{

    public class ConfigurationManager
    {
        // Store config in bin\Debug folder
        private static readonly string ConfigFilePath = Path.Combine(
            Application.StartupPath,
            "POSConfig.json");

        private static AppConfiguration _configuration;
        private static readonly object _lock = new object();
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static AppConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_lock)
                    {
                        if (_configuration == null)
                        {
                            LoadConfiguration();
                        }
                    }
                }
                return _configuration;
            }
        }

        public static void LoadConfiguration()
        {
            try
            {
                Console.WriteLine($"Looking for config at: {ConfigFilePath}");
                Console.WriteLine($"Startup path: {Application.StartupPath}");
                Console.WriteLine($"File exists: {File.Exists(ConfigFilePath)}");

                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    _configuration = JsonConvert.DeserializeObject<AppConfiguration>(json, _jsonSettings);
                    Console.WriteLine("Configuration loaded successfully.");

                    // Log loaded settings for debugging - FIXED PROPERTY NAME
                    if (_configuration != null)
                    {
                        Console.WriteLine($"UpdateQty enabled: {_configuration.Features?.EnableUpdateQty}");
                        Console.WriteLine($"Shop name: {_configuration.InvoiceSettings?.ShopName}"); // Changed from Invoice to InvoiceSettings
                    }
                }
                else
                {
                    Console.WriteLine("Config file not found. Creating default configuration.");
                    // Create default configuration
                    _configuration = new AppConfiguration();
                    SaveConfiguration();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading configuration: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Error loading configuration: {ex.Message}",
                    "Configuration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Create default configuration on error
                _configuration = new AppConfiguration();
            }
        }

        public static void SaveConfiguration()
        {
            lock (_lock)
            {
                try
                {
                    Console.WriteLine($"Saving config to: {ConfigFilePath}");

                    var json = JsonConvert.SerializeObject(_configuration, _jsonSettings);
                    File.WriteAllText(ConfigFilePath, json);

                    Console.WriteLine("Configuration saved successfully.");

                    // Verify file was created
                    if (File.Exists(ConfigFilePath))
                    {
                        Console.WriteLine($"Config file size: {new FileInfo(ConfigFilePath).Length} bytes");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving configuration: {ex.Message}\n{ex.StackTrace}");
                    MessageBox.Show($"Error saving configuration: {ex.Message}",
                        "Configuration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        // Method to get the config file path for debugging
        public static string GetConfigFilePath()
        {
            return ConfigFilePath;
        }

        // Method to reset to default configuration
        public static void ResetToDefault()
        {
            _configuration = new AppConfiguration();
            SaveConfiguration();
        }
    }
}
