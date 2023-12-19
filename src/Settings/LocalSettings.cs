﻿using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealisticDensity.Settings
{
    public class LocalSettings
    {
        private LocalSettingsItem m_Settings;
        public LocalSettingsItem Settings => m_Settings;

        public void Init() => Load();
        public void Reload() => Load();

        /// <summary>
        /// Save settings to a local JSON file
        /// </summary>
        /// <param name="settings"></param>
        public async Task Save()
        {
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filename = "UserSettings.json";
            string fullFilePath = Path.Combine(assemblyDirectory, filename);

            try
            {
                string updatedSettingsJson = JsonConvert.SerializeObject(m_Settings);
                using StreamWriter writer = new(fullFilePath, false, Encoding.UTF8);
                await writer.WriteAsync(updatedSettingsJson);
            }
            catch (System.Exception e)
            {
                Mod.Instance.Log.Critical($"Error saving settings: {e.Message}");
            }
        }

        /// <summary>
        /// Load settings from a local JSON file
        /// </summary>
        private void Load()
        {
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filename = "UserSettings.json";
            string fullFilePath = Path.Combine(assemblyDirectory, filename);

            if (!File.Exists(fullFilePath))
            {
                Mod.Instance.Log.Info("No user settings found. Use default settings.");
                fullFilePath = Path.Combine(assemblyDirectory, "DefaultSettings.json");
                if (!File.Exists(fullFilePath))
                {
                    Mod.Instance.Log.Critical($"Error loading settings: {fullFilePath} does not exist.");
                    return;
                }
            }
            else
            {
                Mod.Instance.Log.Critical("User settings successfully loaded.");
            }

            try
            {
                // Access settings
                string settingsJson = File.ReadAllText(fullFilePath);
                LocalSettingsItem localSettingsItem = JsonConvert.DeserializeObject<LocalSettingsItem>(settingsJson);

                if (localSettingsItem != null)
                {
                    m_Settings = localSettingsItem;
                }
            }
            catch (System.Exception e)
            {
                Mod.Instance.Log.Critical($"Error loading settings: {e.Message}");
            }
        }
    }
}
